using System;
using JetBrains.Annotations;
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
    [UsedImplicitly]
    public Vec2 Size { get; set; } = size;

    /// <summary>
    /// Gets or sets the offset of the collision.
    /// </summary>
    [UsedImplicitly]
    public Vec2 Offset { get; set; } = offset ?? Vec2.Zero;

    /// <summary>
    /// Gets or sets a value indicating whether the collision is solid.
    /// </summary>
    [UsedImplicitly]
    public bool Solid { get; set; } = solid;

    /// <summary>
    /// Gets or sets the collision callback action.
    /// </summary>
    [UsedImplicitly]
    public Action<Entity.Entity, Entity.Entity>? CollisionCallback { get; set; } = collisionCallback;

    /// <summary>
    /// Gets or sets a value indicating whether the collision should be drawn for debugging.
    /// </summary>
    [UsedImplicitly]
    public bool DrawDebug { get; set; } = drawDebug;

    /// <summary>
    /// Represents the transform component associated with the current object, if available.
    /// </summary>
    [UsedImplicitly]
    protected TransformComponent? TransformComponent;

    /// <summary>
    /// Gets the collision rectangle.
    /// </summary>
    /// <param name="position">The position (or current position).</param>
    /// <returns>The collision rectangle.</returns>
    [UsedImplicitly]
    public Rect GetCollisionRect(Vec2? position = null)
    {
        position ??= TransformComponent?.Position;
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

            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (var entityPhysics in entity.GetComponentsAs<CollisionComponent>())
            {
                var entityRect = entityPhysics.GetCollisionRect();
                var selfRect = GetCollisionRect(position);
                if (!Raylib.CheckCollisionRecs(entityRect, selfRect)) continue;
                
                CollisionCallback?.Invoke(Entity, entity);
                entityPhysics.CollisionCallback?.Invoke(Entity, entity);

                if (canGo)
                    canGo = !(Solid && entityPhysics.Solid);
            }
        }

        return canGo;
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();
        TransformComponent = Entity?.GetComponentAs<TransformComponent>();
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();
        if (TransformComponent == null)
            return;

        if (DrawDebug)
            SERender.DrawRectangleLines(
                new Rect(
                    new Vec2(
                        TransformComponent.Position.X - Size.X / 2 + Offset.X,
                        TransformComponent.Position.Y - Size.Y / 2 + Offset.Y
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
