using System.Reflection;
using Demo.Extensions;
using Scriban;

namespace Demo.Renders;

internal interface IRenderTemplate
{
    string HintName { get; }
    string TemplateName { get; }

    string Render() =>
        Template
            .Parse(Assembly.GetExecutingAssembly().GetResourceString(TemplateName))
            .Render();
}

internal interface IRenderTemplate<out TModel> : IRenderTemplate
{
    TModel Data { get; }

    string IRenderTemplate.Render() =>
        Template
            .Parse(Assembly.GetExecutingAssembly().GetResourceString(TemplateName))
            .Render(Data);
}