using System;
using System.Collections.Generic;
using JetBrains.Annotations;

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
    [UsedImplicitly]
    public void Add(T obj);

    /// <summary>
    /// Remove Object
    /// </summary>
    /// <param name="obj">Object</param>
    [UsedImplicitly]
    public void Remove(T obj);

    /// <summary>
    /// Get Object
    /// </summary>
    /// <param name="predicate">Predicate</param>
    /// <returns>Objects</returns>
    [UsedImplicitly]
    public IEnumerable<T> Get(Func<T, bool> predicate);
}
