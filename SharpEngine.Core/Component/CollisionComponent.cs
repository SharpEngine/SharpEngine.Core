using System;
using Raylib_cs;
using SharpEngine.Core.Math;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component which add basic collisions
/// </summary>
public class CollisionComponent : Component
{
    /// <summary>
    /// Size
    /// </summary>
    public Vec2 Size { get; set; }
    
    /// <summary>
    /// Offset
    /// </summary>
    public Vec2 Offset { get; set; }
    
    /// <summary>
    /// If Collision is Solid
    /// </summary>
    public bool Solid { get; set; }
    
    /// <summary>
    /// Collision Callback
    /// </summary>
    public Action<Entity.Entity, Entity.Entity>? CollisionCallback { get; set; }

    private TransformComponent? _transformComponent;

    /// <summary>
    /// Create Collision Component
    /// </summary>
    /// <param name="size">Collision Size</param>
    /// <param name="offset">Collision Offset (Vec2(0))</param>
    /// <param name="solid">If Collision is Solid (true)</param>
    /// <param name="collisionCallback">Action called when collision (null)</param>
    public CollisionComponent(Vec2 size, Vec2? offset = null, bool solid = true,
        Action<Entity.Entity, Entity.Entity>? collisionCallback = null)
    {
        Size = size;
        Offset = offset ?? Vec2.Zero;
        Solid = solid;
        CollisionCallback = collisionCallback;
    }

    /// <summary>
    /// Get Collision Rectangle
    /// </summary>
    /// <param name="position">Position (or current position)</param>
    /// <returns>Collision Rect</returns>
    public Rect GetCollisionRect(Vec2? position = null)
    {
        position ??= _transformComponent?.Position;
        return new Rect(position?.X + Offset.X - Size.X / 2 ?? 0, position?.Y + Offset.Y - Size.Y / 2 ?? 0, Size);
    }

    /// <summary>
    /// Check if entity can go to position
    /// </summary>
    /// <param name="position">Position</param>
    /// <returns>True if can go to position</returns>
    public bool CanGo(Vec2 position)
    {
        foreach (var entity in Entity?.Scene?.Entities!)
        {
            if (entity == Entity) continue;
            
            if (entity.GetComponentAs<CollisionComponent>() is { } entityPhysics && 
                entityPhysics.GetCollisionRect() is var entityRect &&
                GetCollisionRect(position) is var selfRect &&
                Raylib.CheckCollisionRecs(entityRect, selfRect))
            {
                CollisionCallback?.Invoke(Entity, entity);
                entityPhysics.CollisionCallback?.Invoke(Entity, entity);
                return !(Solid && entityPhysics.Solid);
            }
        }

        return true;
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();
        _transformComponent = Entity?.GetComponentAs<TransformComponent>();
    }
}