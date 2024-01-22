using System.Collections.Generic;

namespace Demo.Renders.ApiController;

public class ApiControllerRenderModel
{
    public ApiControllerRenderModel(string @namespace)
    {
        Namespace = @namespace;
    }

    public string Identifier { get; set; }
    public string Modifiers { get; set; }
    
    public string Namespace { get; set; }

    public IList<ApiControllerMethodDefinition> Methods { get; set; } = new List<ApiControllerMethodDefinition>();
    public HashSet<string> Services { get; set; } = new();
    public HashSet<string> Refences { get; set; } = new();

}

public class ApiControllerMethodDefinition
{
    public string Identifier { get; set; }

    public string Modifiers { get; set; }
    public bool Async { get; set; }
    public string Response { get; set; }
    
    public string Implementation { get; set; }
    public string Name { get; set; }
    
    public IEnumerable<InvocationArgumentMap> Arguments = new List<InvocationArgumentMap>();
}

public class InvocationArgumentMap
{
    public string Source { get; set; }
    public string Target { get; set; }
}