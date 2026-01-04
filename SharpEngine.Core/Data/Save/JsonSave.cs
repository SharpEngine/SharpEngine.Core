using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using JetBrains.Annotations;

namespace SharpEngine.Core.Data.Save;

/// <summary>
/// Class which represents Save with json data
/// </summary>
[UsedImplicitly]
public class JsonSave : ISave
{
    private Dictionary<string, object> _data = [];

    /// <inheritdoc />
    public void Read(string file)
    {
        _data =
            JsonSerializer.Deserialize<Dictionary<string, object>>(File.ReadAllText(file))
            ?? [];
    }

    /// <inheritdoc />
    public void Write(string file) => File.WriteAllText(file, JsonSerializer.Serialize(_data));

    /// <inheritdoc />
    public object GetObject(string key, object defaultValue) =>
        _data.GetValueOrDefault(key, defaultValue);

    /// <inheritdoc />
    public T GetObjectAs<T>(string key, T defaultValue)
    {
        if(_data.TryGetValue(key, out var value))
            return (value is JsonElement element ? element.Deserialize<T>() : (T) value) ?? defaultValue;
        return defaultValue;
    }

    /// <inheritdoc />
    public void SetObject(string key, object value) => _data[key] = value;
}
