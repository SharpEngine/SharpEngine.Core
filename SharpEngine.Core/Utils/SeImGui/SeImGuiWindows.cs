using ImGuiNET;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEngine.Core.Utils.SeImGui
{
    /// <summary>
    /// Class which have imgui windows
    /// </summary>
    public static class SeImGuiWindows
    {
        private static SeImGuiConsole Console { get; } = new();

        /// <summary>
        /// Display ImGui Console for SharpEngine
        /// </summary>
        public static void CreateSeImGuiConsole(Window window)
        {
            Console.Display(window);
        }

        /// <summary>
        /// Create ImGui Window for SharpEngine
        /// </summary>
        public static void CreateSeImGuiWindow(Window window)
        {
            ImGui.Begin("SharpEngine Debug");
            foreach (var version in DebugManager.Versions)
                ImGui.Text($"{version.Key} Version : {version.Value}");
            ImGui.Separator();
            ImGui.Text(
                $"FPS (from ImGui) : {1000.0 / ImGui.GetIO().Framerate:.000}ms/frame ({ImGui.GetIO().Framerate} FPS)"
            );
            ImGui.Text($"FPS (from SE) : {1000.0 / DebugManager.FrameRate:.000}ms/frame ({DebugManager.FrameRate} FPS)");
            ImGui.Text($"GC Memory : {DebugManager.GcMemory / 1000000.0:.000} mo");
            ImGui.Separator();
            ImGui.Text($"Screen Size : {window.ScreenSize}");
            ImGui.Text($"Render Size : {window.RenderSize}");
            ImGui.Text($"Render Scale : {window.RenderScale}");
            ImGui.Separator();
            ImGui.Text($"Textures Number : {window.TextureManager.Textures.Count}");
            ImGui.Text($"Shaders Number : {window.ShaderManager.Shaders.Count}");
            ImGui.Text($"Fonts Number : {window.FontManager.Fonts.Count}");
            ImGui.Text($"Sounds Number : {window.SoundManager.Sounds.Count}");
            ImGui.Text($"Musics Number : {window.MusicManager.Musics.Count}");
            ImGui.Text($"Timers Number : {window.TimerManager.Timers.Count}");
            ImGui.Text($"Langs Number : {LangManager.Langs.Count}");
            ImGui.Text($"Saves Number : {SaveManager.Saves.Count}");
            ImGui.Text($"DataTable Number : {DataTableManager.DataTableNames.Count}");
            ImGui.Text($"Scenes Number : {window.Scenes.Count}");
            ImGui.Text($"Entities Number : {window.Scenes.Select(x => x.Entities.Count).Sum()}");
            ImGui.Text(
                $"Widgets (Without Child) Number : {window.Scenes.Select(x => x.Widgets.Count).Sum()}"
            );
            ImGui.Text(
                $"Widgets (With Child) Number : {window.Scenes.Select(x => x.Widgets.Count).Sum() + window.Scenes.Select(x => x.Widgets).SelectMany(x => x).Select(x => x.GetAllChildren()).SelectMany(x => x).Count()}"
            );
            ImGui.Separator();
            ImGui.Text($"Camera Mode : {window.CameraManager.Mode}");
            ImGui.Text($"Camera Position : {window.CameraManager.Camera2D.Target}");
            ImGui.Text($"Camera Rotation : {window.CameraManager.Rotation}");
            ImGui.Separator();
            ImGui.Text($"Number of Render Instructions : {SERender.LastInstructionsNumber}");
            ImGui.Text(
                $"Number of Entity Render Instructions : {SERender.LastEntityInstructionsNumber}"
            );
            ImGui.Text($"Number of UI Render Instructions : {SERender.LastUIInstructionsNumber}");
            ImGui.End();
        }
    }
}
