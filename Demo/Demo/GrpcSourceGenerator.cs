using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using static Demo.GrpcSourceGeneratorBuilder;
using static Demo.GrpcSourceGeneratorPipeline;

namespace Demo;

[Generator]
public class GrpcSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        foreach (var template in Store.AttributesTemplates())
        {
            context.RegisterPostInitializationOutput(ctx
                => ctx.AddSource($"{template.HintName}.g.cs", SourceText.From(template.Render(), Encoding.UTF8))
            );
        }

        var pipeline = context.CompilationProvider
            .Combine(context.SyntaxProvider
                .ForAttributeWithMetadataName("Generators.GrpcInterfaceAttribute",
                    Predicate,
                    Transfrom)
                .Where(tuple => tuple.accepted)
                .Collect());
        
        context.RegisterSourceOutput(pipeline, Build);
    }
}