using System;
using System.Numerics;
using JetBrains.Annotations;

namespace SharpEngine.Core.Math;

/// <summary>
/// Class which represents Vector 2D
/// </summary>
public class Vec2
{
    /// <summary>
    /// Vector 2D with 0 on X and Y
    /// </summary>
    public static Vec2 Zero => new(0);

    /// <summary>
    /// Vector 2D with 1 on X and Y
    /// </summary>
    public static Vec2 One => new(1);

    /// <summary>
    /// X Position
    /// </summary>
    [UsedImplicitly]
    public float X { get; set; }

    /// <summary>
    /// Y Position
    /// </summary>
    [UsedImplicitly]
    public float Y { get; set; }

    /// <summary>
    /// Vector 2D
    /// </summary>
    /// <param name="xy">Value which be X and Y Positions</param>
    public Vec2(float xy)
    {
        X = xy;
        Y = xy;
    }

    /// <summary>
    /// Vector 2D
    /// </summary>
    /// <param name="x">X Position</param>
    /// <param name="y">Y Position</param>
    public Vec2(float x, float y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// If Vector 2D is Zero
    /// </summary>
    /// <returns>If is Zero</returns>
    public bool IsZero() => X == 0 && Y == 0;

    /// <summary>
    /// Length of Vector 2D
    /// </summary>
    /// <returns>Length</returns>
    public float Length() => MathF.Sqrt(X * X + Y * Y);

    /// <summary>
    /// Squared Length of Vector 2D
    /// </summary>
    /// <returns>Squared Length</returns>
    [UsedImplicitly]
    public float LengthSquared() => X * X + Y * Y;

    /// <summary>
    /// Normalized Vector 2D
    /// </summary>
    /// <returns>Normalized Vector</returns>
    public Vec2 Normalized()
    {
        var length = MathF.Sqrt(X * X + Y * Y);
        return length == 0 ? Zero : new Vec2(X / length, Y / length);
    }

    /// <summary>
    /// Get Distance to go to Vector 2D
    /// </summary>
    /// <param name="vec2">Target</param>
    /// <returns>Distance</returns>
    [UsedImplicitly]
    public float DistanceTo(Vec2 vec2)
    {
        var x = vec2.X - X;
        var y = vec2.Y - Y;
        return MathF.Sqrt(x * x + y * y);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is Vec2 vec)
            return this == vec;
        return obj != null && obj.Equals(this);
    }

    /// <inheritdoc />
    // ReSharper disable NonReadonlyMemberInGetHashCode
    public override int GetHashCode() => HashCode.Combine(X, Y);

    /// <inheritdoc />
    public override string ToString() => $"Vec2(X={X}, Y={Y})";

    /// <summary>
    /// Operator inequality
    /// </summary>
    /// <param name="vec1">First Vector 2D</param>
    /// <param name="vec2">Second Vector 2D</param>
    /// <returns>If first is not equals to second</returns>
    public static bool operator !=(Vec2 vec1, Vec2 vec2) => !(vec1 == vec2);

    /// <summary>
    /// Operator equality
    /// </summary>
    /// <param name="vec1">First Vector 2D</param>
    /// <param name="vec2">Second Vector 2D</param>
    /// <returns>If first is equals to second</returns>
    public static bool operator ==(Vec2 vec1, Vec2 vec2) =>
        System.Math.Abs(vec1.X - vec2.X) < 0.001f && System.Math.Abs(vec1.Y - vec2.Y) < 0.001f;

    /// <summary>
    /// Operator Negation
    /// </summary>
    /// <param name="vec">Vector 2D</param>
    /// <returns>Negative Vector 2D</returns>
    public static Vec2 operator -(Vec2 vec) => new(-vec.X, -vec.Y);

    /// <summary>
    /// Operator Subtraction
    /// </summary>
    /// <param name="vec">First Vector 2D</param>
    /// <param name="vec2">Second Vector 2D</param>
    /// <returns>Vector 2D with first values subtracted by second values</returns>
    public static Vec2 operator -(Vec2 vec, Vec2 vec2) => new(vec.X - vec2.X, vec.Y - vec2.Y);

    /// <summary>
    /// Operator Subtraction
    /// </summary>
    /// <param name="vec">Vector 2D</param>
    /// <param name="factor"></param>
    /// <returns>Vector 2D with values subtracted by factor</returns>
    public static Vec2 operator -(Vec2 vec, float factor) => new(vec.X - factor, vec.Y - factor);

    /// <summary>
    /// Operator Addition
    /// </summary>
    /// <param name="vec">First Vector 2D</param>
    /// <param name="vec2">Second Vector 2D</param>
    /// <returns>Vector 2D with first values added with second values</returns>
    public static Vec2 operator +(Vec2 vec, Vec2 vec2) => new(vec.X + vec2.X, vec.Y + vec2.Y);

    /// <summary>
    /// Operator Addition
    /// </summary>
    /// <param name="vec">Vector 2D</param>
    /// <param name="factor"></param>
    /// <returns>Vector 2D with values added with factor</returns>
    public static Vec2 operator +(Vec2 vec, float factor) => new(vec.X + factor, vec.Y + factor);

    /// <summary>
    /// Operator Multiplication
    /// </summary>
    /// <param name="vec">First Vector 2D</param>
    /// <param name="vec2">Second Vector 2D</param>
    /// <returns>Vector 2D with first values multiplied by second values</returns>
    public static Vec2 operator *(Vec2 vec, Vec2 vec2) => new(vec.X * vec2.X, vec.Y * vec2.Y);

    /// <summary>
    /// Operator Multiplication
    /// </summary>
    /// <param name="vec">Vector 2D</param>
    /// <param name="factor"></param>
    /// <returns>Vector 2D with values multiplied by factor</returns>
    public static Vec2 operator *(Vec2 vec, float factor) => new(vec.X * factor, vec.Y * factor);

    /// <summary>
    /// Operator Division
    /// </summary>
    /// <param name="vec">First Vector 2D</param>
    /// <param name="vec2">Second Vector 2D</param>
    /// <returns>Vector 2D with first values divided by second values</returns>
    public static Vec2 operator /(Vec2 vec, Vec2 vec2) => new(vec.X / vec2.X, vec.Y / vec2.Y);

    /// <summary>
    /// Operator Division
    /// </summary>
    /// <param name="vec">Vector 2D</param>
    /// <param name="factor"></param>
    /// <returns>Vector 2D with values divided by factor</returns>
    public static Vec2 operator /(Vec2 vec, float factor) => new(vec.X / factor, vec.Y / factor);

    /// <summary>
    /// Convert Vector2 to Vec2
    /// </summary>
    /// <param name="vec">Vector2</param>
    /// <returns>Vec2</returns>
    public static implicit operator Vec2(Vector2 vec) => new(vec.X, vec.Y);

    /// <summary>
    /// Convert Vec2 to Vector2
    /// </summary>
    /// <param name="vec">Vec2</param>
    /// <returns>Vector2</returns>
    public static implicit operator Vector2(Vec2 vec) => new(vec.X, vec.Y);
}
