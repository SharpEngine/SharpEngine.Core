using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using JetBrains.Annotations;

namespace SharpEngine.Core.Data.Lang;

/// <summary>
/// Class which represents Lang File with Json
/// </summary>
[UsedImplicitly]
public class JsonLang : ILang
{
    private Dictionary<string, string> _translations = [];

    /// <summary>
    /// Create JsonLang
    /// </summary>
    /// <param name="file">Json Path</param>
    public JsonLang(string file)
    {
        Reload(file);
    }

    /// <inheritdoc />
    public string GetTranslation(string key, string defaultTranslation) =>
        _translations.GetValueOrDefault(key, defaultTranslation);

    /// <inheritdoc />
    public void Reload(string file)
    {
        _translations =
            JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(file))
            ?? [];
    }
}
