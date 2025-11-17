using ImGuiNET;
using SharpEngine.Core.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharpEngine.Core.Utils.SeImGui.ConsoleCommand
{
    /// <summary>
    /// Represents a console command that displays or manages debug variables for the application.
    /// </summary>
    /// <remarks>The DebugVarCommand allows users to query specific debug-related variables, such as frame
    /// rate (fps) and version information, through the console interface. It can also be extended with custom variables
    /// via the CustomVariables dictionary. This command is typically used for diagnostic or development purposes within
    /// the application's debug console.</remarks>
    public class DebugVarCommand : ISeImGuiConsoleCommand
    {
        /// <summary>
        /// A dictionary to hold custom debug variables.
        /// </summary>
        public static Dictionary<string, object> CustomVariables = [];

        /// <summary>
        /// Gets the command name.
        /// </summary>
        public string Command => "debugvar";

        /// <summary>
        /// Processes the debug variable command.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <param name="console">The console instance.</param>
        /// <param name="window">The window instance.</param>
        public void Process(string[] args, SeImGuiConsole console, Window window)
        {
            if (args.Length == 0)
                console.AddText("Liste des variables disponible : fps, versions" + (CustomVariables.Keys.Any() ? ", " + string.Join(", ", CustomVariables.Keys) : ""));
            else if(CustomVariables.ContainsKey(args[0]))
                console.AddText($"{args[0]} : {CustomVariables[args[0]]}");
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
