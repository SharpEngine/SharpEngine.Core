using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using SharpEngine.Core.Data.DataTable;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Manager;

/// <summary>
/// Static Class which manage Data Tables
/// </summary>
public static class DataTableManager
{
    private static readonly Dictionary<string, object> DataTables = [];

    /// <summary>
    /// List of known data tables
    /// </summary>
    public static List<string> DataTableNames => [.. DataTables.Keys];

    /// <summary>
    /// Checks if a data table with the specified name exists.
    /// </summary>
    /// <param name="name">Name of the data table</param>
    /// <returns>True if the data table exists, otherwise false</returns>
    [UsedImplicitly]
    public static bool HasDataTable(string name) => DataTables.ContainsKey(name);

    /// <summary>
    /// Removes the specified data table.
    /// </summary>
    /// <param name="name">Name of the data table</param>
    [UsedImplicitly]
    public static void RemoveDataTable(string name)
    {
        if (!DataTables.ContainsKey(name))
        {
            DebugManager.Log(
                LogLevel.Error,
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
    [UsedImplicitly]
    public static void AddDataTable<T>(string name, IDataTable<T> dataTable) where T : class
    {
        if (!DataTables.TryAdd(name, dataTable))
            DebugManager.Log(
                LogLevel.Warning,
                $"SE_DATATABLEMANAGER: DataTable already exist : {name}"
            );
    }

    /// <summary>
    /// Get specified data table.
    /// </summary>
    /// <typeparam name="T">Type of the object</typeparam>
    /// <param name="dataTable">Name of the data table</param>
    /// <returns>The data table</returns>
    /// <exception cref="ArgumentException">Thrown if the data table is not found</exception>
    [UsedImplicitly]
    public static IDataTable<T> Get<T>(string dataTable) where T : class
    {
        if (DataTables.TryGetValue(dataTable, out var dTable))
            return (IDataTable<T>)dTable;
        DebugManager.Log(
            LogLevel.Error,
            $"SE_DATATABLEMANAGER: DataTable not found : {dataTable}"
        );
        throw new ArgumentException($"DataTable not found : {dataTable}");
    }
}
