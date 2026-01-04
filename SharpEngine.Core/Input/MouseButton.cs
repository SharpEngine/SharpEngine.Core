using JetBrains.Annotations;

namespace SharpEngine.Core.Input;

/// <summary>
/// Enum, which represents Mouse Buttons
/// </summary>
public enum MouseButton
{
    /// <summary>
    /// Left Button
    /// </summary>
    Left = 0,

    /// <summary>
    /// Right Button
    /// </summary>
    [UsedImplicitly]
    Right = 1,

    /// <summary>
    /// Middle Button
    /// </summary>
    [UsedImplicitly]
    Middle = 2,

    /// <summary>
    /// Side Button
    /// </summary>
    [UsedImplicitly]
    Side = 3,

    /// <summary>
    /// Extra Button
    /// </summary>
    [UsedImplicitly]
    Extra = 4,

    /// <summary>
    /// Forward Button
    /// </summary>
    [UsedImplicitly]
    Forward = 5,

    /// <summary>
    /// Back Button
    /// </summary>
    [UsedImplicitly]
    Back = 6
}
