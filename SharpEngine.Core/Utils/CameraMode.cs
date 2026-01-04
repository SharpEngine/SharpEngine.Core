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
    /// Camera that follows entity
    /// </summary>
    Follow,

    /// <summary>
    /// Camera that follows an entity with smooth movements
    /// </summary>
    FollowSmooth
}
