using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SharpEngine.Core.Data.DataTable;

/// <summary>
/// Json Data Table
/// </summary>
/// <param name="jsonFile">Json File</param>
public class JsonDataTable<T>(string jsonFile) : IDataTable
{
    private List<dynamic?> Objects { get; } =
            JsonSerializer
                .Deserialize<List<T>>(File.ReadAllText(jsonFile))
                ?.Select(x => (dynamic?)x)
                .ToList() ?? [];

    /// <inheritdoc />
    public dynamic? Get(Predicate<dynamic?> predicate)
    {
        return Objects.Find(predicate);
    }
}
