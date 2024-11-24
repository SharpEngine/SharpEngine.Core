using System;
using Raylib_cs;

namespace SharpEngine.Core.Math;

/// <summary>
/// Struct which represents Rectangle
/// </summary>
/// <param name="x">X Position</param>
/// <param name="y">Y Position</param>
/// <param name="width">Width Size</param>
/// <param name="height">Height Size</param>
public struct Rect(float x, float y, float width, float height)
{
    /// <summary>
    /// X Position
    /// </summary>
    public float X { get; set; } = x;

    /// <summary>
    /// Y Position
    /// </summary>
    public float Y { get; set; } = y;

    /// <summary>
    /// Width Size
    /// </summary>
    public float Width { get; set; } = width;

    /// <summary>
    /// Height Size
    /// </summary>
    public float Height { get; set; } = height;

    /// <summary>
    /// Rectangle
    /// </summary>
    /// <param name="position">Position</param>
    /// <param name="size">Size</param>
    public Rect(Vec2 position, Vec2 size)
        : this(position.X, position.Y, size.X, size.Y) { }

    /// <summary>
    /// Rectangle
    /// </summary>
    /// <param name="x">X Position</param>
    /// <param name="y">Y Position</param>
    /// <param name="size">Size</param>
    public Rect(float x, float y, Vec2 size)
        : this(x, y, size.X, size.Y) { }

    /// <summary>
    /// Rectangle
    /// </summary>
    /// <param name="position">Position</param>
    /// <param name="width">Width Size</param>
    /// <param name="height">Height Size</param>
    public Rect(Vec2 position, float width, float height)
        : this(position.X, position.Y, width, height) { }

    /// <summary>
    /// Return if position is in Rect
    /// </summary>
    /// <param name="position">Position</param>
    /// <returns>if Position is in Rect</returns>
    public readonly bool Contains(Vec2 position) =>
        X <= position.X && position.X <= X + Width && Y <= position.Y && position.Y <= Y + Height;

    /// <summary>
    /// Return if other Rect is in Rect
    /// </summary>
    /// <param name="other">Rect</param>
    /// <returns>If intersect</returns>
    public readonly bool Intersect(Rect other) =>
        X <= other.X + other.Width && X + Width >= other.X && Y <= other.Y + other.Height && Y + Height >= other.Y;

    /// <inheritdoc />
    public override readonly bool Equals(object? obj)
    {
        if (obj is Rect rect)
            return this == rect;
        return obj != null && obj.Equals(this);
    }

    /// <inheritdoc />
    public override readonly int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

    /// <inheritdoc />
    public override readonly string ToString() =>
        $"Rect(X={X}, Y={Y}, Width={Width}, Height={Height})";

    /// <summary>
    /// Operator inequality
    /// </summary>
    /// <param name="r1">First Rect</param>
    /// <param name="r2">Second Rect</param>
    /// <returns>If first is not equals to second</returns>
    public static bool operator !=(Rect r1, Rect r2) => !(r1 == r2);

    /// <summary>
    /// Operator equality
    /// </summary>
    /// <param name="r1">First Rect</param>
    /// <param name="r2">Second Rect</param>
    /// <returns>If first is equals to second</returns>
    public static bool operator ==(Rect r1, Rect r2) =>
        System.Math.Abs(r1.X - r2.X) < 0.001f
        && System.Math.Abs(r1.Y - r2.Y) < 0.001f
        && System.Math.Abs(r1.Width - r2.Width) < 0.001f
        && System.Math.Abs(r1.Height - r2.Height) < 0.001f;

    /// <summary>
    /// Convert Rect to Rectangle
    /// </summary>
    /// <param name="rect">Rect</param>
    /// <returns>Rect</returns>
    public static implicit operator Rectangle(Rect rect) =>
        new(rect.X, rect.Y, rect.Width, rect.Height);
}
