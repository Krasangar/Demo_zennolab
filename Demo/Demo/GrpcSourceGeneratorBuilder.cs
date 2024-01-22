using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Demo.Extensions;
using Demo.Renders;
using Demo.Renders.ApiController;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Demo;

internal class GrpcSourceGeneratorBuilder
{
    internal static void Build(SourceProductionContext productionContext, (Compilation Compilation, ImmutableArray<PipeModel> model) data)
    {
        var controllers = data.model.GroupBy(tuple => tuple.method.Parent);

        foreach (var controller in controllers)
        {
            var classDeclaration = (ClassDeclarationSyntax)controller.Key!;
            var templateModel = new ApiControllerRenderModel(classDeclaration.GetNamespace())
            {
                Identifier = classDeclaration.Identifier.ToString(),
                Modifiers = classDeclaration.Modifiers.ToString(),
            };
            foreach (var (_, methodDeclaration, attributeData, requestArgs) in controller)
            {
                if (attributeData.TryGetTypedConstant<ITypeSymbol>("implType", out var implType) &&
                    attributeData.TryGetTypedConstant<string>("method", out var invocationMethod))
                {
                    templateModel.Refences.Add(implType.ContainingNamespace.ToDisplayString());
                    templateModel.Services.Add(implType.MetadataName);

                    var implMethods = implType
                        .GetMembers()
                        .ToDictionary(symbol => symbol.Name, symbol =>
                        {
                            return ((IMethodSymbol)symbol).Parameters.Select(parameterSymbol =>
                            {
                                var s = parameterSymbol.Name.ToString();
                                return s;
                            });
                        });

                    templateModel.Methods.Add(new ApiControllerMethodDefinition
                    {
                        Identifier = methodDeclaration.Identifier.ToString(),
                        Modifiers = methodDeclaration.Modifiers.ToString(),
                        Async = true,
                        Response = methodDeclaration.ReturnType.ToString(),
                        Implementation = implType.MetadataName,
                        Name = invocationMethod,
                        Arguments = MapArgs(implMethods, invocationMethod, requestArgs)
                    });
                }
            }

            IRenderTemplate template = new ApiControllerRenderTemplate(classDeclaration.Identifier.ToString(), templateModel);

            productionContext.AddSource($"{template.HintName}.g.cs", SourceText.From(template.Render(), Encoding.UTF8));
        }
    }
    
    private static IEnumerable<InvocationArgumentMap> MapArgs(IReadOnlyDictionary<string, IEnumerable<string>> implMethods,
        string invocationMethod, IEnumerable<string> requestArgs) =>
        implMethods.TryGetValue(invocationMethod, out var invocationArgs)
            ? requestArgs.Join(
                    invocationArgs,
                    requestArg => requestArg.ToLower(),
                    invocationArg => invocationArg.ToLower(),
                    (requestArg, invocationArg) => (requestArg, invocationArg))
                .Select(tuple => new InvocationArgumentMap
                    { Target = tuple.invocationArg, Source = tuple.requestArg })
            : ArraySegment<InvocationArgumentMap>.Empty;
}