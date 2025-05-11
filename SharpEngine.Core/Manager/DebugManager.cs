using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ImGuiNET;
using Raylib_cs;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;
using SharpEngine.Core.Utils.SeImGui;
using SharpEngine.Core.Utils.SeImGui.ConsoleCommand;

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
        { "Raylib-cs", "7.0.1" },
        { "ImGui.NET", "1.91.6.1" },
        { "SharpEngine.Core", "2.2.1" }
    };

    /// <summary>
    /// Default Render ImGui for SharpEngine
    /// </summary>
    /// <param name="window">Window</param>
    public static void SeRenderImGui(Window window)
    {
        if (window._imguiDisplayWindow)
            SeImGuiWindows.CreateSeImGuiWindow(window);
        if (window._imguiDisplayConsole)
            SeImGuiWindows.CreateSeImGuiConsole(window);

        if (InputManager.IsKeyPressed(Input.Key.F7))
            window._imguiDisplayWindow = !window._imguiDisplayWindow;
        if (InputManager.IsKeyPressed(Input.Key.F8))
            window._imguiDisplayConsole = !window._imguiDisplayConsole;
    }

    /// <summary>
    /// Add Console Command
    /// </summary>
    /// <param name="command">Console Command</param>
    public static void AddConsoleCommand(ISeImGuiConsoleCommand command) =>
        SeImGuiWindows.Console.Commands.Add(command);

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
