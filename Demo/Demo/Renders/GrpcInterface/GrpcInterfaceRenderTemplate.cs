namespace Demo.Renders.GrpcInterface;

internal class GrpcInterfaceRenderTemplate : IRenderTemplate<GrpcInterfaceRenderModel>
{
    public GrpcInterfaceRenderTemplate(GrpcInterfaceRenderModel data)
    {
        Data = data;
    }

    public string HintName => "GrpcInterfaceAttribute";
    public string TemplateName => "GrpcInterfaceAttributeTemplate.txt";
    public GrpcInterfaceRenderModel Data { get; }
}