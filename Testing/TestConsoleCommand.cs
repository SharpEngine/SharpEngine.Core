using SharpEngine.Core;
using SharpEngine.Core.Utils.SeImGui;
using SharpEngine.Core.Utils.SeImGui.ConsoleCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    internal class TestConsoleCommand : ISeImGuiConsoleCommand
    {
        public string Command => "test";

        public void Process(string[] args, SeImGuiConsole console, Window window)
        {
            console.AddText(window.IndexCurrentScene.ToString());
        }
    }
}
