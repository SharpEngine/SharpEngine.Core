using SharpEngine.Core.Math;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component which create automatic movements
/// </summary>
public class AutoComponent: Component
{
    /// <summary>
    /// Automatic Direction
    /// </summary>
    public Vec2 Direction { get; set; }
    
    /// <summary>
    /// Automatic Rotation
    /// </summary>
    public int Rotation { get; set; }

    private TransformComponent? _transform;

    /// <summary>
    /// Create Auto Component
    /// </summary>
    /// <param name="direction">Direction</param>
    /// <param name="rotation">Rotation</param>
    public AutoComponent(Vec2? direction = null, int rotation = 0)
    {
        Direction = direction ?? Vec2.Zero;
        Rotation = rotation;
    }

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
        
        if(_transform == null) return;

        if (Direction.Length() != 0)
        {
            _transform.Position = new Vec2(
                _transform.Position.X + Direction.X * delta,
                _transform.Position.Y + Direction.Y * delta
                );
        }

        if (Rotation != 0)
            _transform.Rotation += Rotation;
    }
}