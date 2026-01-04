using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using SharpEngine.Core.Data.Save;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Manager;

/// <summary>
/// Static Class which manage Saves
/// </summary>
public static class SaveManager
{
    /// <summary>
    /// List of known Saves
    /// </summary>
    public static List<string> Saves => [..InternalSaves.Keys];

    /// <summary>
    /// Add Save to Manager
    /// </summary>
    /// <param name="name">Save Name</param>
    /// <param name="save">Save Object</param>
    [UsedImplicitly]
    public static void AddSave(string name, ISave save)
    {
        if (!InternalSaves.TryAdd(name, save))
            DebugManager.Log(LogLevel.Warning, $"SE_SAVEMANAGER: Save already exist : {name}");
    }

    /// <summary>
    /// Get Save from Manager
    /// </summary>
    /// <param name="name">Save Name</param>
    /// <returns>Save</returns>
    /// <exception cref="ArgumentException">Throws if save not found</exception>
    [UsedImplicitly]
    public static ISave GetSave(string name)
    {
        if (InternalSaves.TryGetValue(name, out var save))
            return save;
        DebugManager.Log(LogLevel.Error, $"SE_SAVEMANAGER: Save not found : {name}");
        throw new ArgumentException($"Save not found : {name}");
    }

    private static readonly Dictionary<string, ISave> InternalSaves = [];
}
