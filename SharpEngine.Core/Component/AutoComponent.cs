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

    /// <summary>
    /// Represents the transform component associated with the current object, if available.
    /// </summary>
    protected TransformComponent? _transform;

    /// <summary>
    /// Represents the underlying physics collision component used for basic collision detection.
    /// </summary>
    protected CollisionComponent? _basicPhysics;

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        _transform = Entity?.GetComponentAs<TransformComponent>();
        _basicPhysics = Entity?.GetComponentAs<CollisionComponent>();
    }

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);

        if (_transform == null)
            return;

        if (Direction.Length() != 0)
        {

            var newPos = new Vec2(
                _transform.LocalPosition.X + Direction.X * delta,
                _transform.LocalPosition.Y + Direction.Y * delta
            );

            var newPosX = new Vec2(newPos.X, _transform.LocalPosition.Y);
            var newPosY = new Vec2(_transform.LocalPosition.X, newPos.Y);


            if (_basicPhysics == null || _basicPhysics.CanGo(newPos + (Entity?.Parent?.GetComponentAs<TransformComponent>()?.Position ?? Vec2.Zero)))
                _transform.LocalPosition = newPos;
            else if (Direction.X != 0 && _basicPhysics.CanGo(newPosX + (Entity?.Parent?.GetComponentAs<TransformComponent>()?.Position ?? Vec2.Zero)))
                _transform.LocalPosition = newPosX;
            else if (Direction.Y != 0 && _basicPhysics.CanGo(newPosY + (Entity?.Parent?.GetComponentAs<TransformComponent>()?.Position ?? Vec2.Zero)))
                _transform.LocalPosition = newPosY;
        }

        if (Rotation != 0)
            _transform.LocalRotation += Rotation * delta;
    }
}
