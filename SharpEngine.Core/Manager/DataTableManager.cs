using System;
using System.Collections.Generic;
using SharpEngine.Core.Data.DataTable;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Manager;

/// <summary>
/// Static Class which manage Data Tables
/// </summary>
public static class DataTableManager
{
    private static readonly Dictionary<string, IDataTable> DataTables = new();
    
    /// <summary>
    /// List of known data tables
    /// </summary>
    public static List<string> DataTableNames => new(DataTables.Keys);

    /// <summary>
    /// Add Data Table
    /// </summary>
    /// <param name="name">Name of DataTable</param>
    /// <param name="dataTable">Data Table</param>
    public static void AddDataTable(string name, IDataTable dataTable)
    {
        if(!DataTables.TryAdd(name, dataTable))
            DebugManager.Log(LogLevel.LogWarning, $"SE_DATATABLEMANAGER: DataTable already exist : {name}");
    }

    /// <summary>
    /// Get Object from DataTable
    /// </summary>
    /// <param name="dataTable">Name of Data Table</param>
    /// <param name="predicate">Predicate</param>
    /// <typeparam name="T">Type of Object</typeparam>
    /// <returns>Object</returns>
    /// <exception cref="Exception">If DataTable not found</exception>
    public static T? Get<T>(string dataTable, Predicate<dynamic> predicate)
    {
        if (DataTables.TryGetValue(dataTable, out var dTable))
            return dTable.Get(predicate);
        DebugManager.Log(LogLevel.LogError, $"SE_DATATABLEMANAGER: DataTable not found : {dataTable}");
        throw new Exception($"DataTable not found : {dataTable}");
    }
}