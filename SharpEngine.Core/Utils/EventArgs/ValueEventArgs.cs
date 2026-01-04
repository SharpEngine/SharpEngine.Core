using JetBrains.Annotations;

namespace SharpEngine.Core.Utils.EventArgs;

/// <summary>
/// Event Args which have old and new value
/// </summary>
/// <typeparam name="T">Type of Value</typeparam>
public class ValueEventArgs<T> : System.EventArgs
{
    /// <summary>
    /// Old Value
    /// </summary>
    [UsedImplicitly]
    public required T OldValue { get; init; }

    /// <summary>
    /// New Value
    /// </summary>
    [UsedImplicitly]
    public required T NewValue { get; init; }
}
