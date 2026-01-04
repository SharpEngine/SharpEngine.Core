using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Entity;

/// <summary>
/// Class which represents Entity
/// </summary>
[UsedImplicitly]
public class Entity
{
    /// <summary>
    /// How Entity must be updated when paused
    /// </summary>
    [UsedImplicitly]
    public PauseState PauseState { get; set; } = PauseState.Normal;

    /// <summary>
    /// Tag of Entity
    /// </summary>
    [UsedImplicitly]
    public string Tag { get; set; } = "";

    /// <summary>
    /// Name of Entity
    /// </summary>
    [UsedImplicitly]
    public string Name { get; set; } = "";

    /// <summary>
    /// Scene of Entity
    /// </summary>
    public Scene? Scene { get; set; }

    /// <summary>
    /// Get All Components of Entity
    /// </summary>
    [UsedImplicitly]
    public List<Component.Component> Components { get; } = [];

    /// <summary>
    /// Get All Children of Entity
    /// </summary>
    [UsedImplicitly]
    public List<Entity> Children { get; } = [];

    /// <summary>
    /// Parent of Entity
    /// </summary>
    [UsedImplicitly]
    public Entity? Parent { get; set; }

    /// <summary>
    /// Get All Components of one Type
    /// </summary>
    /// <typeparam name="T">Type of Component</typeparam>
    /// <returns>Components of type T</returns>
    public List<T> GetComponentsAs<T>()
        where T : Component.Component => Components.OfType<T>().ToList();

    /// <summary>
    /// Get Component of one Type
    /// </summary>
    /// <typeparam name="T">Type of Component</typeparam>
    /// <returns>Component of type T</returns>
    public T? GetComponentAs<T>()
        where T : Component.Component => Components.OfType<T>().FirstOrDefault();

    /// <summary>
    /// Get Scene as T
    /// </summary>
    /// <typeparam name="T">Scene Type</typeparam>
    /// <returns>Scene cast as T</returns>
    [UsedImplicitly]
    public T? GetSceneAs<T>()
        where T : Scene => (T?)Scene;

    /// <summary>
    /// Add Child Entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    [UsedImplicitly]
    public T AddChild<T>(T entity)
        where T : Entity
    {
        Children.Add(entity);
        entity.Parent = this;
        entity.Scene = Scene;
        return entity;
    }

    /// <summary>
    /// Add a Component and return it
    /// </summary>
    /// <param name="component">Component which be added</param>
    /// <typeparam name="T">Type of Component</typeparam>
    /// <returns>Component</returns>
    [UsedImplicitly]
    public T AddComponent<T>(T component)
        where T : Component.Component
    {
        Components.Add(component);
        component.Entity = this;
        return component;
    }

    /// <summary>
    /// Remove Child
    /// </summary>
    /// <param name="entity">Child</param>
    [UsedImplicitly]
    public void RemoveChild(Entity entity)
    {
        entity.Parent = null;
        entity.Scene = null;
        Children.Remove(entity);
    }

    /// <summary>
    /// Remove Component
    /// </summary>
    /// <param name="component">Component will be removed</param>
    [UsedImplicitly]
    public void RemoveComponent(Component.Component component)
    {
        component.Entity = null;
        Components.Remove(component);
    }

    /// <summary>
    /// Load Entity
    /// </summary>
    public virtual void Load()
    {
        foreach (var component in Components)
            component.Load();

        foreach (var child in Children)
            child.Load();
    }

    /// <summary>
    /// Unload Entity
    /// </summary>
    public virtual void Unload()
    {
        foreach (var component in Components)
            component.Unload();

        foreach (var child in Children)
            child.Unload();
    }

    /// <summary>
    /// Update Entity
    /// </summary>
    /// <param name="delta">Time since last frame</param>
    public virtual void Update(float delta)
    {
        foreach (var component in Components)
            component.Update(delta);

        foreach (var child in Children)
            child.Update(delta);
    }

    /// <summary>
    /// Draw Entity
    /// </summary>
    public virtual void Draw()
    {
        foreach (var component in Components)
            component.Draw();

        foreach (var child in Children)
            child.Draw();
    }
}
