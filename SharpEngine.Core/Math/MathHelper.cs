using JetBrains.Annotations;

namespace SharpEngine.Core.Math;

/// <summary>
/// Class with some Math functions and values
/// </summary>
public static class MathHelper
{
    /// <summary>
    /// E Value
    /// </summary>
    [UsedImplicitly]
    public const float E = 2.71828175F;

    /// <summary>
    /// Log10E Value
    /// </summary>
    [UsedImplicitly]
    public const float Log10E = 0.4342945F;

    /// <summary>
    /// Log2E Value
    /// </summary>
    [UsedImplicitly]
    public const float Log2E = 1.442695F;

    /// <summary>
    /// Pi Value
    /// </summary>
    [UsedImplicitly]
    public const float Pi = 3.14159274F;

    /// <summary>
    /// Pi/2 Value
    /// </summary>
    [UsedImplicitly]
    public const float PiOver2 = 1.57079637F;

    /// <summary>
    /// Pi/4 Value
    /// </summary>
    [UsedImplicitly]
    public const float PiOver4 = 0.7853982F;

    /// <summary>
    /// Tau Value
    /// </summary>
    [UsedImplicitly]
    public const float Tau = 6.28318548F;

    /// <summary>
    /// 2*Pi Value
    /// </summary>
    [UsedImplicitly]
    public const float TwoPi = 6.28318548F;

    /// <summary>
    /// Convert Radians to Degrees
    /// </summary>
    /// <param name="radians">Radians Value</param>
    /// <returns>Degrees Value</returns>
    [UsedImplicitly]
    public static float ToDegrees(float radians) => radians * 180 / Pi;

    /// <summary>
    /// Convert Degrees to Radians
    /// </summary>
    /// <param name="degrees">Degrees Value</param>
    /// <returns>Radians Value</returns>
    [UsedImplicitly]
    public static float ToRadians(float degrees) => degrees * Pi / 180;

    /// <summary>
    /// Block value between two values (included)
    /// </summary>
    /// <param name="value">Current Value</param>
    /// <param name="mini">Min Value (included)</param>
    /// <param name="maxi">Max Value (included)</param>
    /// <returns>Clamped Value</returns>
    public static float Clamp(float value, float mini = float.MinValue, float maxi = float.MaxValue)
    {
        if (value < mini)
            return mini;
        return value > maxi ? maxi : value;
    }

    /// <summary>
    /// Block value between two values (included)
    /// </summary>
    /// <param name="value">Current Value</param>
    /// <param name="mini">Min Value (included)</param>
    /// <param name="maxi">Max Value (included)</param>
    /// <returns>Clamped Value</returns>
    public static int Clamp(int value, int mini = int.MinValue, int maxi = int.MaxValue)
    {
        if (value < mini)
            return mini;
        return value > maxi ? maxi : value;
    }
}
