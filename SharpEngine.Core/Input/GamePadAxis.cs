using JetBrains.Annotations;

namespace SharpEngine.Core.Input;

/// <summary>
/// Enum which represents Axis of Gamepad
/// </summary>
public enum GamePadAxis
{
    /// <summary>
    /// Left Stick X Axis
    /// </summary>
    LeftX,

    /// <summary>
    /// Left Stick Y Axis
    /// </summary>
    LeftY,

    /// <summary>
    /// Right Stick X Axis
    /// </summary>
    [UsedImplicitly]
    RightX,

    /// <summary>
    /// Right Stick Y Axis
    /// </summary>
    [UsedImplicitly]
    RightY,

    /// <summary>
    /// Left Trigger Axis
    /// </summary>
    [UsedImplicitly]
    LeftTrigger,

    /// <summary>
    /// Right Trigger Axis
    /// </summary>
    [UsedImplicitly]
    RightTrigger,
}
