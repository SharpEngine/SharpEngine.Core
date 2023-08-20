using System;

namespace SharpEngine.Core.Data.DataTable;

/// <summary>
/// Table of Data
/// </summary>
public interface IDataTable
{
    /// <summary>
    /// Get Object
    /// </summary>
    /// <param name="predicate">Predicate</param>
    /// <returns>Object</returns>
    public dynamic? Get(Predicate<dynamic?> predicate);
}