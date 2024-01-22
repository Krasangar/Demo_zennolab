using System.Collections.Generic;
using Demo.Renders;
using Demo.Renders.GrpcInterface;
using Demo.Renders.GrpcMapping;

namespace Demo;

internal class Store
{
    internal static IEnumerable<IRenderTemplate> AttributesTemplates()
    {
        yield return new GrpcMappingRenderTemplate(
            new GrpcMappingRenderModel("Generators")
        );

        yield return new GrpcInterfaceRenderTemplate(
            new GrpcInterfaceRenderModel("Generators")
        );
    }
}