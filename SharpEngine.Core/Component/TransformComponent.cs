using JetBrains.Annotations;
using SharpEngine.Core.Math;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component that defines Transform (Position, Rotation, Scale)
/// </summary>
/// <param name="position">Position (Vec2(0))</param>
/// <param name="scale">Scale (Vec2(1))</param>
/// <param name="rotation">Rotation (0)</param>
/// <param name="zLayer">ZLayer (0)</param>
public class TransformComponent(
    Vec2? position = null,
    Vec2? scale = null,
    float rotation = 0,
    int zLayer = 0
) : Component
{
    /// <summary>
    /// Position of Component
    /// </summary>
    public Vec2 Position => Entity?.Parent?.GetComponentAs<TransformComponent>()?.Position is { } parentPosition ? parentPosition + LocalPosition : LocalPosition;

    /// <summary>
    /// Scale of Component
    /// </summary>
    public Vec2 Scale => Entity?.Parent?.GetComponentAs<TransformComponent>()?.Scale is { } parentScale ? parentScale * LocalScale : LocalScale;

    /// <summary>
    /// Rotation of Component
    /// </summary>
    public float Rotation => Entity?.Parent?.GetComponentAs<TransformComponent>()?.Rotation + LocalRotation ?? LocalRotation;

    /// <summary>
    /// ZLayer of Component
    /// </summary>
    public int ZLayer => Entity?.Parent?.GetComponentAs<TransformComponent>()?.ZLayer + LocalZLayer ?? LocalZLayer;
    /// <summary>
    /// Position of Component
    /// </summary>
    public Vec2 LocalPosition { get; set; } = position ?? Vec2.Zero;

    /// <summary>
    /// Scale of Component
    /// </summary>
    [UsedImplicitly]
    public Vec2 LocalScale { get; set; } = scale ?? Vec2.One;

    /// <summary>
    /// Rotation of Component
    /// </summary>
    public float LocalRotation { get; set; } = rotation;

    /// <summary>
    /// ZLayer of Component
    /// </summary>
    [UsedImplicitly]
    public int LocalZLayer { get; set; } = zLayer;

    /// <summary>
    /// Get transformed Position
    /// </summary>
    /// <param name="offset">Offset (Vec2(0))</param>
    /// <returns>Transformed Position</returns>
    public Vec2 GetTransformedPosition(Vec2? offset = null) => new Vec2(Position.X + (offset?.X ?? 0), Position.Y + (offset?.Y ?? 0));
}
