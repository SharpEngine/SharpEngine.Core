using System;
using System.Collections.Generic;
using System.Data;
using SharpEngine.Core.Data.DataTable;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Manager;

/// <summary>
/// Static Class which manage Data Tables
/// </summary>
public static class DataTableManager
{
    private static readonly Dictionary<string, IDataTable> DataTables = [];

    /// <summary>
    /// List of known data tables
    /// </summary>
    public static List<string> DataTableNames => new(DataTables.Keys);

    /// <summary>
    /// Checks if the specified data table exists.
    /// </summary>
    /// <param name="name">Name of the data table</param>
    public static void HasDataTable(string name) => DataTables.ContainsKey(name);

    /// <summary>
    /// Removes the specified data table.
    /// </summary>
    /// <param name="name">Name of the data table</param>
    public static void RemoveDataTable(string name)
    {
        if(!DataTables.ContainsKey(name))
        {
            DebugManager.Log(
                LogLevel.LogError,
                $"SE_DATATABLEMANAGER: DataTable not found : {name}"
            );
            throw new ArgumentException($"DataTable not found : {name}");
        }

        DataTables.Remove(name);
    }

    /// <summary>
    /// Add a data table to the manager.
    /// </summary>
    /// <param name="name">Name of the data table</param>
    /// <param name="dataTable">Data table to add</param>
    public static void AddDataTable(string name, IDataTable dataTable)
    {
        if (!DataTables.TryAdd(name, dataTable))
            DebugManager.Log(
                LogLevel.LogWarning,
                $"SE_DATATABLEMANAGER: DataTable already exist : {name}"
            );
    }

    /// <summary>
    /// Get an object from the specified data table.
    /// </summary>
    /// <typeparam name="T">Type of the object</typeparam>
    /// <param name="dataTable">Name of the data table</param>
    /// <param name="predicate">Predicate to filter the object</param>
    /// <returns>The object from the data table</returns>
    /// <exception cref="ArgumentException">Thrown if the data table is not found</exception>
    public static T? Get<T>(string dataTable, Predicate<dynamic?> predicate)
    {
        if (DataTables.TryGetValue(dataTable, out var dTable))
            return dTable.Get(predicate);
        DebugManager.Log(
            LogLevel.LogError,
            $"SE_DATATABLEMANAGER: DataTable not found : {dataTable}"
        );
        throw new ArgumentException($"DataTable not found : {dataTable}");
    }
}
