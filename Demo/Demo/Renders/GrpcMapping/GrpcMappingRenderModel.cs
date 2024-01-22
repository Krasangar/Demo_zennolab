namespace Demo.Renders.GrpcMapping;

internal class GrpcMappingRenderModel
{
    public GrpcMappingRenderModel(string @namespace)
    {
        Namespace = @namespace;
    }

    public string Namespace { get; set; }
}