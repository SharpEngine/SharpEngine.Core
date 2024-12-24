using SharpEngine.Core.Math;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component which create automatic movements
/// </summary>
/// <param name="direction">Direction</param>
/// <param name="rotation">Rotation</param>
public class AutoComponent(Vec2? direction = null, int rotation = 0) : Component
{
    /// <summary>
    /// Automatic Direction
    /// </summary>
    public Vec2 Direction { get; set; } = direction ?? Vec2.Zero;

    /// <summary>
    /// Automatic Rotation
    /// </summary>
    public int Rotation { get; set; } = rotation;

    private TransformComponent? _transform;

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        _transform = Entity?.GetComponentAs<TransformComponent>();
    }

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);

        if (_transform == null)
            return;

        if (Direction.Length() != 0)
        {
            _transform.LocalPosition = new Vec2(
                _transform.LocalPosition.X + Direction.X * delta,
                _transform.LocalPosition.Y + Direction.Y * delta
            );
        }

        if (Rotation != 0)
            _transform.LocalRotation += Rotation;
    }
}
