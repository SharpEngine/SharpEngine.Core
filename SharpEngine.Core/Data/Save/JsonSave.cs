using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SharpEngine.Core.Data.Save;

/// <summary>
/// Class which represents Save with json data
/// </summary>
public class JsonSave: ISave
{
    private Dictionary<string, object> _data = new();

    /// <inheritdoc />
    public void Read(string file)
    {
        _data = JsonSerializer.Deserialize<Dictionary<string, object>>(File.ReadAllText(file)) ??
                new Dictionary<string, object>();
    }

    /// <inheritdoc />
    public void Write(string file) => File.WriteAllText(file, JsonSerializer.Serialize(_data));

    /// <inheritdoc />
    public object GetObject(string key, object defaultValue) => _data.GetValueOrDefault(key, defaultValue);

    /// <inheritdoc />
    public T GetObjectAs<T>(string key, T defaultValue) =>
        _data.TryGetValue(key, out var value) ? (value is JsonElement element ? element.Deserialize<T>()! : (T) value) : defaultValue;

    /// <inheritdoc />
    public void SetObject(string key, object value) => _data[key] = value;
}