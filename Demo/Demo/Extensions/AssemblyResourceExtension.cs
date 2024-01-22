using System.IO;
using System.Reflection;
using System.Text;

namespace Demo.Extensions;

internal static class AssemblyResourceExtension
{
    internal static string GetResourceString(this Assembly assembly, string name)
    {
        name = $"Demo.Templates.{name}";
        return assembly.GetManifestResourceStream(name).GetString();
    }

    private static string GetString(this Stream? stream)
    {
        if (stream == null) return string.Empty;
        using (stream)
        {
            if (stream.Length <= 0) return string.Empty;
            if (stream.Position != 0) stream.Position = 0;

            var bytes = new byte[stream.Length];

            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);

            return Encoding.UTF8.GetString(bytes);
        }
    }
}