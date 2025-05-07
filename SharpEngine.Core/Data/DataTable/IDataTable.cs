using System;
using System.Collections.Generic;

namespace SharpEngine.Core.Data.DataTable;

/// <summary>
/// Table of Data
/// </summary>
public interface IDataTable<T> where T : class
{
    /// <summary>
    /// Add Object
    /// </summary>
    /// <param name="obj">Object</param>
    public void Add(T obj);

    /// <summary>
    /// Remove Object
    /// </summary>
    /// <param name="obj">Object</param>
    public void Remove(T obj);

    /// <summary>
    /// Get Object
    /// </summary>
    /// <param name="predicate">Predicate</param>
    /// <returns>Objects</returns>
    public IEnumerable<T> Get(Func<T, bool> predicate);
}
