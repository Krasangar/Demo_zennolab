namespace Demo.Renders.GrpcMapping;

internal class GrpcMappingRenderTemplate(GrpcMappingRenderModel data) : IRenderTemplate<GrpcMappingRenderModel>
{
    public string HintName => "GrpcMappingAttribute";
    public string TemplateName => "GrpcMappingAttributeTemplate.txt";
    public GrpcMappingRenderModel Data { get; } = data;
}