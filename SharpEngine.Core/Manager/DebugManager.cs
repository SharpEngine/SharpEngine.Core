using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// SharpEngine.Core Version
    /// </summary>
    public const string SeVersion = "1.1.1";

    /// <summary>
    /// Create ImGui Window for SharpEngine
    /// </summary>
    public static void CreateSeImGuiWindow(Window window, bool showAssemblyVersion = false)
    {
        ImGui.Begin("SharpEngine.Core Debug");
        ImGui.Text($"SharpEngine.Core Version : {SeVersion}");
        if(showAssemblyVersion)
            foreach (var an in typeof(DebugManager).Assembly.GetReferencedAssemblies())
                if((!an.Name?.StartsWith("Microsoft") ?? false) && (!an.Name?.StartsWith("System") ?? false))
                    ImGui.Text($"{an.Name} Version : {an.Version}");
        ImGui.Separator();
        ImGui.Text($"FPS (from ImGui) : {1000.0/ImGui.GetIO().Framerate:.000}ms/frame ({ImGui.GetIO().Framerate} FPS)");
        ImGui.Text($"FPS (from SE) : {1000.0/FrameRate:.000}ms/frame ({FrameRate} FPS)");
        ImGui.Text($"GC Memory : {GcMemory/1000000.0:.000} mo");
        ImGui.Separator();
        ImGui.Text($"Textures Number : {window.TextureManager.Textures.Count}");
        ImGui.Text($"Fonts Number : {window.FontManager.Fonts.Count}");
        ImGui.Text($"Sounds Number : {window.SoundManager.Sounds.Count}");
        ImGui.Text($"Musics Number : {window.MusicManager.Musics.Count}");
        ImGui.Text($"Langs Number : {LangManager.Langs.Count}");
        ImGui.Text($"Saves Number : {SaveManager.Saves.Count}");
        ImGui.Text($"DataTable Number : {DataTableManager.DataTableNames.Count}");
        ImGui.Text($"Scenes Number : {window.Scenes.Count}");
        ImGui.Text($"Entities Number : {window.Scenes.Select(x => x.Entities.Count).Sum()}");
        ImGui.Text($"Widgets (Without Child) Number : {window.Scenes.Select(x => x.Widgets.Count).Sum()}");
        ImGui.Text(
            $"Widgets (With Child) Number : {window.Scenes.Select(x => x.Widgets.Count).Sum() + window.Scenes.Select(x => x.Widgets).SelectMany(x => x).Select(x => x.GetAllChildren()).SelectMany(x => x).Count()}");
        ImGui.Separator();
        ImGui.Text($"Camera Mode : {window.CameraManager.Mode}");
        ImGui.Text($"Camera Position : {window.CameraManager.Camera2D.target}");
        ImGui.Text($"Camera Rotation : {window.CameraManager.Rotation}");
        ImGui.Separator();
        ImGui.Text($"Number of Render Instructions : {SERender.LastInstructionsNumber}");
        ImGui.Text($"Number of Entity Render Instructions : {SERender.LastEntityInstructionsNumber}");
        ImGui.Text($"Number of UI Render Instructions : {SERender.LastUIInstructionsNumber}");
        ImGui.End();
    }

    /// <summary>
    /// Log Message
    /// </summary>
    /// <param name="level">Level of Log</param>
    /// <param name="message">Message</param>
    public static void Log(LogLevel level, string message) => Raylib.TraceLog(level.ToRayLib(), message);

    /// <summary>
    /// Set Log Level
    /// </summary>
    /// <param name="level">Level of Log</param>
    public static void SetLogLevel(LogLevel level) => Raylib.SetTraceLogLevel(level.ToRayLib());
}