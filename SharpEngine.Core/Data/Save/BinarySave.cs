using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace SharpEngine.Core.Data.Save;

/// <summary>
/// Class which represents Save with binary data
/// </summary>
public class BinarySave: ISave
{
    private readonly Dictionary<string, object> _data = new();

    /// <inheritdoc />
    public void Read(string file)
    {
        _data.Clear();
        using var bR = new BinaryReader(System.IO.File.OpenRead(file));

        try
        {
            while (bR.ReadString() is { } key)
            {
                var type = bR.ReadString();
                object value = type switch
                {
                    "b" => bR.ReadBoolean(),
                    "i" => bR.ReadInt32(),
                    "d" => bR.ReadDouble(),
                    _ => bR.ReadString()
                };
                _data.Add(key, value);
            }
        }
        catch(EndOfStreamException) {}
    }

    /// <inheritdoc />
    public void Write(string file)
    {
        using var bW = new BinaryWriter(System.IO.File.Open(file, FileMode.Create));
        foreach (var data in _data)
        {
            bW.Write(data.Key);
            switch (data.Value)
            {
                case bool v:
                    bW.Write("b");
                    bW.Write(v);
                    break;
                case int v:
                    bW.Write("i");
                    bW.Write(v);
                    break;
                case double v:
                    bW.Write("d");
                    bW.Write(v);
                    break;
                default:
                    bW.Write("s");
                    bW.Write(data.Value.ToString() ?? string.Empty);
                    break;
            }
        }
    }

    /// <inheritdoc />
    public object GetObject(string key, object defaultValue) => _data.GetValueOrDefault(key, defaultValue);

    /// <inheritdoc />
    public T GetObjectAs<T>(string key, T defaultValue) =>
        _data.TryGetValue(key, out var value)
            ? (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture)
            : defaultValue;

    /// <inheritdoc />
    public void SetObject(string key, object value) => _data[key] = value;
}