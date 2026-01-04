using SharpEngine.Core;
using SharpEngine.Core.Utils.SeImGui;
using SharpEngine.Core.Utils.SeImGui.ConsoleCommand;

namespace Testing
{
    internal class TestConsoleCommand : ISeImGuiConsoleCommand
    {
        public string Command => "test";

        public void Process(string[] args, SeImGuiConsole console, Window window)
        {
            DebugVarCommand.CustomVariables.TryAdd("testvar", 0);

            DebugVarCommand.CustomVariables["testvar"] = (int)DebugVarCommand.CustomVariables["testvar"] + 1;
            console.AddText(window.IndexCurrentScene.ToString());
        }
    }
}
