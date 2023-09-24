using SharpEngine.Core.Math;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component which define Transform (Position, Rotation, Scale)
/// </summary>
public class TransformComponent : Component
{
    /// <summary>
    /// Position of Component
    /// </summary>
    public Vec2 Position { get; set; }

    /// <summary>
    /// Scale of Component
    /// </summary>
    public Vec2 Scale { get; set; }

    /// <summary>
    /// Rotation of Component
    /// </summary>
    public float Rotation { get; set; }

    /// <summary>
    /// ZLayer of Component
    /// </summary>
    public int ZLayer { get; set; }

    /// <summary>
    /// Create TransformComponent
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="scale">Scale (Vec2(1))</param>
    /// <param name="rotation">Rotation (0)</param>
    /// <param name="zLayer">ZLayer (0)</param>
    public TransformComponent(
        Vec2? position = null,
        Vec2? scale = null,
        float rotation = 0,
        int zLayer = 0
    )
    {
        Position = position ?? Vec2.Zero;
        Scale = scale ?? Vec2.One;
        Rotation = rotation;
        ZLayer = zLayer;
    }

    /// <summary>
    /// Get transformed Position
    /// </summary>
    /// <param name="offset">Offset (Vec2(0))</param>
    /// <returns>Transformed Position</returns>
    public Vec2 GetTransformedPosition(Vec2? offset = null)
    {
        return new Vec2(Position.X + (offset?.X ?? 0), Position.Y + (offset?.Y ?? 0));
    }
}
