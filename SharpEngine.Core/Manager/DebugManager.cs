using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ImGuiNET;
using Raylib_cs;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Manager;

/// <summary>
/// Static class which manage debug information
/// </summary>
public static class DebugManager
{
    /// <summary>
    /// Number of frame per seconds
    /// </summary>
    public static int FrameRate => Raylib.GetFPS();

    /// <summary>
    /// Number of bytes in GC
    /// </summary>
    public static long GcMemory => GC.GetTotalMemory(false);

    /// <summary>
    /// Packages Versions
    /// </summary>
    public static Dictionary<string, string> Versions { get; } = new()
    {
        { "Raylib-cs", "5.0.0" },
        { "ImGui.NET", "1.89.9.4" },
        { "SharpEngine.Core", "1.8.0" }
    };

    /// <summary>
    /// Log Message
    /// </summary>
    /// <param name="level">Level of Log</param>
    /// <param name="message">Message</param>
    public static void Log(LogLevel level, string message) =>
        Raylib.TraceLog(level.ToRayLib(), message);

    /// <summary>
    /// Set Log Level
    /// </summary>
    /// <param name="level">Level of Log</param>
    public static void SetLogLevel(LogLevel level) => Raylib.SetTraceLogLevel(level.ToRayLib());
}
