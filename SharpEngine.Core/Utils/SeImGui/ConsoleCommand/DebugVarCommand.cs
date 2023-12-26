using ImGuiNET;
using SharpEngine.Core.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEngine.Core.Utils.SeImGui.ConsoleCommand
{
    internal class DebugVarCommand : ISeImGuiConsoleCommand
    {
        public string Command => "debugvar";

        public void Process(string[] args, SeImGuiConsole console, Window window)
        {
            if (args.Length == 0)
                console.AddText("Liste des variables disponible : fps, versions");
            else if (args[0] == "fps")
                console.AddText($"FPS : {DebugManager.FrameRate}");
            else if (args[0] == "versions")
                foreach (var version in DebugManager.Versions)
                    console.AddText($"{version.Key} Version : {version.Value}");
            else
                console.AddText("Liste des variables disponible : fps, versions");
        }
    }
}
