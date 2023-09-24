using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SharpEngine.Core.Data.DataTable;

/// <summary>
/// Json Data Table
/// </summary>
public class JsonDataTable<T> : IDataTable
{
    private List<dynamic?> Objects { get; }

    /// <summary>
    /// Create Data Table from Json
    /// </summary>
    /// <param name="jsonFile">Json File</param>
    public JsonDataTable(string jsonFile)
    {
        Objects =
            JsonSerializer
                .Deserialize<List<T>>(File.ReadAllText(jsonFile))
                ?.Select(x => (dynamic?)x)
                .ToList() ?? new List<dynamic?>();
    }

    /// <inheritdoc />
    public dynamic? Get(Predicate<dynamic?> predicate)
    {
        return Objects.Find(predicate);
    }
}
