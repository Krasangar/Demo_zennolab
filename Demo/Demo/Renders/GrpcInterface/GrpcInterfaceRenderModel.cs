namespace Demo.Renders.GrpcInterface;

internal class GrpcInterfaceRenderModel
{
    public GrpcInterfaceRenderModel(string @namespace)
    {
        Namespace = @namespace;
    }

    public string Namespace { get; set; }
}