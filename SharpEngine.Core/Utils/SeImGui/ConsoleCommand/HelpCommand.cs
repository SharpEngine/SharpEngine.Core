using System.Linq;

namespace SharpEngine.Core.Utils.SeImGui.ConsoleCommand;

internal class HelpCommand : ISeImGuiConsoleCommand
{
    public string Command => "help";

    public void Process(string[] args, SeImGuiConsole console, Window window)
    {
        console.AddText("Liste des commandes disponibles : " + string.Join(", ", console.Commands.Select(x => x.Command)));
    }
}
