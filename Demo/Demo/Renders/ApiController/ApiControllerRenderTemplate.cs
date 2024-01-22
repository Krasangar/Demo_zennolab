namespace Demo.Renders.ApiController;

public class ApiControllerRenderTemplate : IRenderTemplate<ApiControllerRenderModel>
{
    public ApiControllerRenderTemplate(string hintName, ApiControllerRenderModel data)
    {
        HintName = hintName;
        Data = data;
    }

    public string HintName { get; }
    public string TemplateName => "ApiControllerRenderTemplate.txt";
    public ApiControllerRenderModel Data { get; }
}