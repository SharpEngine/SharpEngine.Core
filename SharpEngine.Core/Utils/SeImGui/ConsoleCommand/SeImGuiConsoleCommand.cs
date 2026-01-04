namespace SharpEngine.Core.Utils.SeImGui.ConsoleCommand;

/// <summary>
/// Interface for console command
/// </summary>
public interface ISeImGuiConsoleCommand
{
    /// <summary>
    /// Command Name
    /// </summary>
    public string Command { get; }

    /// <summary>
    /// Process command
    /// </summary>
    /// <param name="args">Console Arguments</param>
    /// <param name="console">Console</param>
    /// <param name="window">Game Window</param>
    public void Process(string[] args, SeImGuiConsole console, Window window);
}
