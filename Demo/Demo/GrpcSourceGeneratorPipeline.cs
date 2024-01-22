using System.Linq;
using System.Threading;
using Demo.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Demo;

internal class GrpcSourceGeneratorPipeline
{
    internal static bool Predicate(SyntaxNode node, CancellationToken cancellationToken)
    {
        return node is MethodDeclarationSyntax;
    }
    internal static PipeModel Transfrom(
        GeneratorAttributeSyntaxContext syntaxContext, CancellationToken cancellationToken)
    {
        var declarationSyntax = ((MethodDeclarationSyntax)syntaxContext.TargetNode);

        var semanticModel = syntaxContext.SemanticModel;
                        
        var requestArgs = declarationSyntax
            .ParameterList.Parameters.First()
            .GetParameterPublicProperties(semanticModel);

        var parentAttributes =
            ((INamedTypeSymbol)semanticModel.GetDeclaredSymbol(syntaxContext.TargetNode.Parent!)!)
            .GetAttributes();
                        
        if (parentAttributes.AttributeExist("Generators.GrpcMappingAttribute") == false)
            return (false, null, null, null);

        var attributeData = syntaxContext.Attributes.First(data => data.AttributeClass!.Name == "GrpcInterfaceAttribute");
                        
                        
        return (accepted: true, method: declarationSyntax, attribute: attributeData, args: requestArgs);
    }
}