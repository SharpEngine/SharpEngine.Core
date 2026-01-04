using JetBrains.Annotations;

namespace SharpEngine.Core.Utils;

/// <summary>
/// Enum, which represents how Entity or Widget must be updated when paused
/// </summary>
public enum PauseState
{
    /// <summary>
    /// Will be updated only when not paused
    /// </summary>
    Normal,

    /// <summary>
    /// Will never be updated
    /// </summary>
    [UsedImplicitly]
    Disabled,

    /// <summary>
    /// Will always be updated
    /// </summary>
    Enabled,

    /// <summary>
    /// Will be updated only when paused
    /// </summary>
    WhenPaused
}
