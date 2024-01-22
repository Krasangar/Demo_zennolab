using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Demo.Extensions;

public static class SyntaxProviderExtensions
{

    public static string GetNamespace(this AttributeArgumentSyntax syntax)
    {
        return GetNamespace((BaseTypeDeclarationSyntax)syntax.Parent);
    }

    public static bool TryGetTypedConstant<TType>(this AttributeData attributeData, string argName, out TType typedConstant)
    {
        typedConstant = default!;

        var attributeConstructor = attributeData.AttributeConstructor;
        
        if (attributeConstructor == null)
            return false;
        
        var argDict = attributeConstructor.Parameters
            .GroupBy(symbol => symbol.MetadataName)
            .ToDictionary(symbols => symbols.Key, symbols => symbols.First().Ordinal);

        if (!argDict.TryGetValue(argName, out var val)) return false;

        if (attributeData.ConstructorArguments[val].Value is not TType t) return false;
        typedConstant = t;
        return true;
    }

    public static IEnumerable<string> GetParameterPublicProperties(this ParameterSyntax parameterSyntax, SemanticModel semanticModel) =>
        (semanticModel.GetTypeInfo(parameterSyntax.Type!).Type?.GetMembers().OfType<IPropertySymbol>() ??
         ArraySegment<IPropertySymbol>.Empty)
        .Where(propertySymbol =>
            propertySymbol.IsStatic == false &&
            propertySymbol.DeclaredAccessibility == Accessibility.Public)
        .Select(symbol => symbol.Name);

    public static string GetNamespace(this BaseTypeDeclarationSyntax syntax)
    {
        var nameSpace = string.Empty;

        var potentialNamespaceParent = syntax.Parent;

        while (potentialNamespaceParent != null &&
               potentialNamespaceParent is not NamespaceDeclarationSyntax
               && potentialNamespaceParent is not FileScopedNamespaceDeclarationSyntax)
        {
            potentialNamespaceParent = potentialNamespaceParent.Parent;
        }

        if (potentialNamespaceParent is BaseNamespaceDeclarationSyntax namespaceParent)
        {
            nameSpace = namespaceParent.Name.ToString();

            while (true)
            {
                if (namespaceParent.Parent is not NamespaceDeclarationSyntax parent)
                    break;

                nameSpace = $"{namespaceParent.Name}.{nameSpace}";
                namespaceParent = parent;
            }
        }

        return nameSpace;
    }

    public static bool AttributeExist(this ImmutableArray<AttributeData> attributes, string expectedAttribute)
    {
        return attributes.FirstOrDefault(data => data.AttributeClass!.ToDisplayString() == "Generators.GrpcMappingAttribute") != null;
    }
}