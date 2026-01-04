namespace SharpEngine.Core.Utils.SeImGui.ConsoleCommand;

internal class SayCommand : ISeImGuiConsoleCommand
{
    public string Command => "say";

    public void Process(string[] args, SeImGuiConsole console, Window window)
    {
        console.AddText(string.Join(" ", args));
    }
}
