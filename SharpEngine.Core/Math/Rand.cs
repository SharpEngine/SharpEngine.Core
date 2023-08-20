using System;

namespace SharpEngine.Core.Math;

/// <summary>
/// Class for random numbers
/// </summary>
public static class Rand
{
    private static readonly Random Random = new();

    /// <summary>
    /// Get random integer between minimum and maximum values
    /// </summary>
    /// <param name="min">Minimum Value (included)</param>
    /// <param name="max">Maximum Value (excluded)</param>
    /// <returns>Random Number</returns>
    public static int GetRand(int min, int max) => Random.Next(min, max);

    /// <summary>
    /// Get random integer between 0 and maximum values
    /// </summary>
    /// <param name="max">Maximum Value (excluded)</param>
    /// <returns>Random Number</returns>
    public static int GetRand(int max) => Random.Next(max);

    /// <summary>
    /// Get random non negative integer
    /// </summary>
    /// <returns>Random Number</returns>
    public static int GetRand() => Random.Next();
    

    /// <summary>
    /// Get random float between minimum and maximum values
    /// </summary>
    /// <param name="min">Minimum Value (included)</param>
    /// <param name="max">Maximum Value (excluded)</param>
    /// <returns>Random Number</returns>
    public static float GetRandF(float min, float max) => Random.NextSingle() * (max - min) + min;
    
    /// <summary>
    /// Get random float between 0 and maximum values
    /// </summary>
    /// <param name="max">Maximum Value (excluded)</param>
    /// <returns>Random Number</returns>
    public static float GetRandF(float max) => Random.NextSingle() * max;

    /// <summary>
    /// Get random float between 0 and 1
    /// </summary>
    /// <returns>Random Number</returns>
    public static float GetRandF() => Random.NextSingle();
}