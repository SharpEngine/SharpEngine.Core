namespace SharpEngine.Core.Utils;

/// <summary>
/// Working mode of Camera Manager
/// </summary>
public enum CameraMode
{
    /// <summary>
    /// Basic Camera
    /// </summary>
    Basic,

    /// <summary>
    /// Camera which follow entity
    /// </summary>
    Follow,

    /// <summary>
    /// Camera which follow entity with smooth movements
    /// </summary>
    FollowSmooth
}
