using JetBrains.Annotations;
using SharpEngine.Core.Math;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component that creates automatic movements
/// </summary>
/// <param name="direction">Direction</param>
/// <param name="rotation">Rotation</param>
public class AutoComponent(Vec2? direction = null, int rotation = 0) : Component
{
    /// <summary>
    /// Automatic Direction
    /// </summary>
    [UsedImplicitly]
    public Vec2 Direction { get; set; } = direction ?? Vec2.Zero;

    /// <summary>
    /// Automatic Rotation
    /// </summary>
    [UsedImplicitly]
    public int Rotation { get; set; } = rotation;

    /// <summary>
    /// Represents the transform component associated with the current object, if available.
    /// </summary>
    [UsedImplicitly]
    protected TransformComponent? Transform;

    /// <summary>
    /// Represents the underlying physics collision component used for basic collision detection.
    /// </summary>
    [UsedImplicitly]
    protected CollisionComponent? BasicPhysics;

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        Transform = Entity?.GetComponentAs<TransformComponent>();
        BasicPhysics = Entity?.GetComponentAs<CollisionComponent>();
    }

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);

        if (Transform == null)
            return;

        if (Direction.Length() != 0)
        {

            var newPos = new Vec2(
                Transform.LocalPosition.X + Direction.X * delta,
                Transform.LocalPosition.Y + Direction.Y * delta
            );

            var newPosX = new Vec2(newPos.X, Transform.LocalPosition.Y);
            var newPosY = new Vec2(Transform.LocalPosition.X, newPos.Y);


            if (BasicPhysics == null || BasicPhysics.CanGo(newPos + (Entity?.Parent?.GetComponentAs<TransformComponent>()?.Position ?? Vec2.Zero)))
                Transform.LocalPosition = newPos;
            else if (Direction.X != 0 && BasicPhysics.CanGo(newPosX + (Entity?.Parent?.GetComponentAs<TransformComponent>()?.Position ?? Vec2.Zero)))
                Transform.LocalPosition = newPosX;
            else if (Direction.Y != 0 && BasicPhysics.CanGo(newPosY + (Entity?.Parent?.GetComponentAs<TransformComponent>()?.Position ?? Vec2.Zero)))
                Transform.LocalPosition = newPosY;
        }

        if (Rotation != 0)
            Transform.LocalRotation += Rotation * delta;
    }
}
