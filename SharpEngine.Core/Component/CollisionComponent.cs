using System;
using System.Diagnostics;
using Raylib_cs;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using Color = SharpEngine.Core.Utils.Color;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component which adds basic collision functionality.
/// </summary>
/// <param name="size">The size of the collision.</param>
/// <param name="offset">The offset of the collision (default: Vec2(0)).</param>
/// <param name="solid">Determines if the collision is solid (default: true).</param>
/// <param name="collisionCallback">The action to be called when a collision occurs (default: null).</param>
/// <param name="drawDebug">Determines if the collision should be drawn for debugging (default: false).</param>
public class CollisionComponent(
    Vec2 size,
    Vec2? offset = null,
    bool solid = true,
    Action<Entity.Entity, Entity.Entity>? collisionCallback = null,
    bool drawDebug = false
) : Component
{
    /// <summary>
    /// Gets or sets the size of the collision.
    /// </summary>
    public Vec2 Size { get; set; } = size;

    /// <summary>
    /// Gets or sets the offset of the collision.
    /// </summary>
    public Vec2 Offset { get; set; } = offset ?? Vec2.Zero;

    /// <summary>
    /// Gets or sets a value indicating whether the collision is solid.
    /// </summary>
    public bool Solid { get; set; } = solid;

    /// <summary>
    /// Gets or sets the collision callback action.
    /// </summary>
    public Action<Entity.Entity, Entity.Entity>? CollisionCallback { get; set; } = collisionCallback;

    /// <summary>
    /// Gets or sets a value indicating whether the collision should be drawn for debugging.
    /// </summary>
    public bool DrawDebug { get; set; } = drawDebug;

    private TransformComponent? _transformComponent;

    /// <summary>
    /// Gets the collision rectangle.
    /// </summary>
    /// <param name="position">The position (or current position).</param>
    /// <returns>The collision rectangle.</returns>
    public Rect GetCollisionRect(Vec2? position = null)
    {
        position ??= _transformComponent?.Position;
        return new Rect(
            position?.X + Offset.X - Size.X / 2 ?? 0,
            position?.Y + Offset.Y - Size.Y / 2 ?? 0,
            Size
        );
    }

    /// <summary>
    /// Checks if the entity can go to the specified position.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <returns>True if the entity can go to the position; otherwise, false.</returns>
    public bool CanGo(Vec2 position)
    {
        var canGo = true;
        foreach (var entity in Entity?.Scene?.Entities!)
        {
            if (entity == Entity)
                continue;

            if (entity.GetComponentAs<CollisionComponent>() is { } entityPhysics)
            {
                var entityRect = entityPhysics.GetCollisionRect();
                var selfRect = GetCollisionRect(position);
                if (Raylib.CheckCollisionRecs(entityRect, selfRect))
                {
                    CollisionCallback?.Invoke(Entity, entity);
                    entityPhysics.CollisionCallback?.Invoke(Entity, entity);

                    if (canGo)
                        canGo = !(Solid && entityPhysics.Solid);
                }
            }
        }

        return canGo;
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();
        _transformComponent = Entity?.GetComponentAs<TransformComponent>();
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();
        if (_transformComponent == null)
            return;

        if (DrawDebug)
            SERender.DrawRectangleLines(
                new Rect(
                    new Vec2(
                        _transformComponent.Position.X - Size.X / 2 + Offset.X,
                        _transformComponent.Position.Y - Size.Y / 2 + Offset.Y
                    ),
                    Size
                ),
                2,
                Color.Red,
                InstructionSource.Entity,
                float.MaxValue
            );
    }
}
