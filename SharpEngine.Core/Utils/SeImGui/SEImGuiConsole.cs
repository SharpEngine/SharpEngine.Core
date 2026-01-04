using ImGuiNET;
using SharpEngine.Core.Utils.SeImGui.ConsoleCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SharpEngine.Core.Utils.SeImGui;

/// <summary>
/// Class which represents Console for SharpEngine
/// </summary>
public class SeImGuiConsole
{
    /// <summary>
    /// Texts of Console
    /// </summary>
    [UsedImplicitly]
    public List<string> ConsoleTexts { get; } = [];

    /// <summary>
    /// History of Console
    /// </summary>
    [UsedImplicitly]
    public List<string> History { get; } = [];

    /// <summary>
    /// Gets or sets the current position within the history sequence.
    /// </summary>
    /// <remarks>A value of -1 typically indicates that there is no current history entry selected.
    /// The valid range and meaning of this property may depend on the context in which it is used.</remarks>
    [UsedImplicitly]
    public int HistoryIndex { get; set; } = -1;

    /// <summary>
    /// Commands of Console
    /// </summary>
    public List<ISeImGuiConsoleCommand> Commands { get; } = [ new SayCommand(), new HelpCommand(), new DebugVarCommand() ];

    /// <summary>
    /// Input of Console
    /// </summary>
    private string _consoleInput = "";

    /// <summary>
    /// Add Text to Console
    /// </summary>
    /// <param name="text"></param>
    public void AddText(string text) => ConsoleTexts.Add(text);

    /// <summary>
    /// Display ImGui Console for SharpEngine
    /// </summary>
    public void Display(Window window)
    {
        ImGui.Begin("SharpEngine Console");

        // Log Window
        var reserve = ImGui.GetStyle().ItemSpacing.Y + ImGui.GetFrameHeightWithSpacing();
        if (ImGui.BeginChild("Console", new System.Numerics.Vector2(0, -reserve), ImGuiChildFlags.None, ImGuiWindowFlags.None))
        {
            ImGui.PushTextWrapPos();

            foreach (var command in ConsoleTexts)
                ImGui.TextUnformatted(command);

            ImGui.PopTextWrapPos();

            ImGui.SetScrollHereY(1.0f);

            ImGui.EndChild();
        }

        ImGui.Separator();

        unsafe
        {
            if (ImGui.InputText("Input", ref _consoleInput, 100, ImGuiInputTextFlags.EnterReturnsTrue | ImGuiInputTextFlags.CallbackHistory, (evt) =>
            {
                if (!ImGui.IsItemActive() || History.Count <= 0) return 0;
                
                ImGuiInputTextCallbackDataPtr evtPtr = evt;
                if (ImGui.IsKeyPressed(ImGuiKey.UpArrow))
                {
                    if (HistoryIndex < History.Count - 1)
                        HistoryIndex++;
                    evtPtr.DeleteChars(0, evtPtr.BufTextLen);
                    evtPtr.InsertChars(0, History[HistoryIndex]);
                }
                else if (ImGui.IsKeyPressed(ImGuiKey.DownArrow))
                {
                    if (HistoryIndex >= 0)
                        HistoryIndex--;
                    if (HistoryIndex == -1)
                        evtPtr.DeleteChars(0, evtPtr.BufTextLen);
                    else
                    {
                        evtPtr.DeleteChars(0, evtPtr.BufTextLen);
                        evtPtr.InsertChars(0, History[HistoryIndex]);
                    }
                }
                return 0;
            }))
            {

                if (_consoleInput.Length > 0)
                    ProcessCommand(window);

                _consoleInput = "";
                ImGui.SetKeyboardFocusHere(-1);
            }
        }

        ImGui.End();
    }

    private void ProcessCommand(Window window)
    {
        AddText("> " + _consoleInput);

        History.Insert(0, _consoleInput);
        HistoryIndex = -1;

        var command = _consoleInput.Split(" ");
        var commandName = command[0];
        var commandArgs = Array.Empty<string>();
        if (command.Length > 1)
            commandArgs = command[1..];
        
        if(Commands.FirstOrDefault(commandObject => commandObject.Command == commandName) is { } cmd)
            cmd.Process(commandArgs, this, window);
        else
            AddText("Unknown command.");
    }
}
