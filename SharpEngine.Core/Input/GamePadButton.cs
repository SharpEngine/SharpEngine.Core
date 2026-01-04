using JetBrains.Annotations;

namespace SharpEngine.Core.Input;

/// <summary>
/// Enum which represents Button of Gamepad
/// </summary>
public enum GamePadButton
{
    /// <summary>
    /// Unknown Button
    /// </summary>
    [UsedImplicitly]
    Unknown,

    /// <summary>
    /// Directional Cross Up Button
    /// </summary>
    [UsedImplicitly]
    DPadUp,

    /// <summary>
    /// Directional Cross Right Button
    /// </summary>
    [UsedImplicitly]
    DPadRight,

    /// <summary>
    /// Directional Cross Down Button
    /// </summary>
    [UsedImplicitly]
    DPadDown,

    /// <summary>
    /// Directional Cross Left Button
    /// </summary>
    [UsedImplicitly]
    DPadLeft,

    /// <summary>
    /// Y Button (Xbox)
    /// </summary>
    [UsedImplicitly]
    Y,

    /// <summary>
    /// X Button (Xbox)
    /// </summary>
    [UsedImplicitly]
    X,

    /// <summary>
    /// A Button (Xbox)
    /// </summary>
    [UsedImplicitly]
    A,

    /// <summary>
    /// B Button (Xbox)
    /// </summary>
    [UsedImplicitly]
    B,

    /// <summary>
    /// First Left Trigger
    /// </summary>
    [UsedImplicitly]
    LeftTrigger1,

    /// <summary>
    /// Second Left Trigger
    /// </summary>
    [UsedImplicitly]
    LeftTrigger2,

    /// <summary>
    /// First Right Trigger
    /// </summary>
    [UsedImplicitly]
    RightTrigger1,

    /// <summary>
    /// Second Right Trigger
    /// </summary>
    [UsedImplicitly]
    RightTrigger2,

    /// <summary>
    /// Back Button
    /// </summary>
    [UsedImplicitly]
    Back,

    /// <summary>
    /// Big Central Button
    /// </summary>
    [UsedImplicitly]
    BigButton,

    /// <summary>
    /// Start Button
    /// </summary>
    [UsedImplicitly]
    Start,

    /// <summary>
    /// Left Stick Button
    /// </summary>
    [UsedImplicitly]
    LeftStick,

    /// <summary>
    /// Right Stick Button
    /// </summary>
    [UsedImplicitly]
    RightStick,
}
