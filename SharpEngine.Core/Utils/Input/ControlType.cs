namespace SharpEngine.Core.Utils.Input;

/// <summary>
/// Type of Controls
/// </summary>
public enum ControlType
{
    /// <summary>
    /// Follow Mouse
    /// </summary>
    MouseFollow,
    
    /// <summary>
    /// Only Left and Right Movements
    /// </summary>
    LeftRight,
    
    /// <summary>
    /// Only Up and Down Movements
    /// </summary>
    UpDown,
    
    /// <summary>
    /// Up, Down, Left and Right Movements
    /// </summary>
    FourDirection,
    
    /// <summary>
    /// Classic Platformer Movements
    /// </summary>
    ClassicJump
}