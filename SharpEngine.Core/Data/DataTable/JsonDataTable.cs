using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using JetBrains.Annotations;

namespace SharpEngine.Core.Data.DataTable;

/// <summary>
/// Json Data Table
/// </summary>
/// <param name="jsonFile">JSON File</param>
[UsedImplicitly]
public class JsonDataTable<T>(string jsonFile) : IDataTable<T> where T : class
{
    private List<T> Objects { get; } = JsonSerializer.Deserialize<List<T>>(File.ReadAllText(jsonFile))?.ToList() ?? [];

    /// <inheritdoc/>
    public void Add(T obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        Objects.Add(obj);
        File.WriteAllText(jsonFile, JsonSerializer.Serialize(Objects));
    }

    /// <inheritdoc/>
    public IEnumerable<T> Get(Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return Objects.Where(predicate);
    }

    /// <inheritdoc/>
    public void Remove(T obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        Objects.Remove(obj);
        File.WriteAllText(jsonFile, JsonSerializer.Serialize(Objects));
    }
}
