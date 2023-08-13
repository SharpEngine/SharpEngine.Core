using System;

namespace SharpEngine.Core.Math;

/// <summary>
/// Struct which represents Rectangle with integers
/// </summary>
public struct RectI
{
    /// <summary>
    /// X Position
    /// </summary>
    public int X { get; set; }
    
    /// <summary>
    /// Y Position
    /// </summary>
    public int Y { get; set; }
    
    /// <summary>
    /// Width Size
    /// </summary>
    public int Width { get; set; }
    
    /// <summary>
    /// Height Size
    /// </summary>
    public int Height { get; set; }


    /// <summary>
    /// Rectangle
    /// </summary>
    /// <param name="position">Position</param>
    /// <param name="size">Size</param>
    public RectI(Vec2I position, Vec2I size): this(position.X, position.Y, size.X, size.Y) { }
    
    /// <summary>
    /// Rectangle
    /// </summary>
    /// <param name="x">X Position</param>
    /// <param name="y">Y Position</param>
    /// <param name="size">Size</param>
    public RectI(int x, int y, Vec2I size): this(x, y, size.X, size.Y) { }
    
    /// <summary>
    /// Rectangle
    /// </summary>
    /// <param name="position">Position</param>
    /// <param name="width">Width Size</param>
    /// <param name="height">Height Size</param>
    public RectI(Vec2I position, int width, int height): this(position.X, position.Y, width, height) { }

    
    /// <summary>
    /// Rectangle
    /// </summary>
    /// <param name="x">X Position</param>
    /// <param name="y">Y Position</param>
    /// <param name="width">Width Size</param>
    /// <param name="height">Height Size</param>
    public RectI(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is RectI rect)
            return this == rect;
        return obj != null && obj.Equals(this);
    }
    
    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

    /// <inheritdoc />
    public override string ToString() => $"RectI(X={X}, Y={Y}, Width={Width}, Height={Height})";
    
    /// <summary>
    /// Operator inequality
    /// </summary>
    /// <param name="r1">First Rect</param>
    /// <param name="r2">Second Rect</param>
    /// <returns>If first is not equals to second</returns>
    public static bool operator !=(RectI r1, RectI r2) => !(r1 == r2);
    
    
    /// <summary>
    /// Operator equality
    /// </summary>
    /// <param name="r1">First Rect</param>
    /// <param name="r2">Second Rect</param>
    /// <returns>If first is equals to second</returns>
    public static bool operator ==(RectI r1, RectI r2) =>
        r1.X == r2.X && r1.Y == r2.Y && r1.Width == r2.Width && r1.Height == r2.Height;

}