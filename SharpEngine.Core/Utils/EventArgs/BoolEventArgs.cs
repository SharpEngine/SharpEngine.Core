namespace SharpEngine.Core.Utils.EventArgs;

/// <summary>
/// Event Args which have a boolean result
/// </summary>
public class BoolEventArgs: System.EventArgs
{
    /// <summary>
    /// Result of Event
    /// </summary>
    public bool Result { get; set; } = true;
}