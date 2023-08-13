using System.Collections.Generic;
using System.Linq;
using SharpEngine.Core.Component;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Entity;

/// <summary>
/// Class which represents Entity
/// </summary>
public class Entity
{
    /// <summary>
    /// How Entity must be updated when paused
    /// </summary>
    public PauseState PauseState  { get; set; } = PauseState.Normal;

    /// <summary>
    /// Tag of Entity
    /// </summary>
    public string Tag { get; set; } = "";

    /// <summary>
    /// Scene of Entity
    /// </summary>
    public Scene? Scene { get; set; }

    /// <summary>
    /// Get All Components of Entity
    /// </summary>
    public List<Component.Component> Components { get; } = new();

    /// <summary>
    /// Get All Components of one Type
    /// </summary>
    /// <typeparam name="T">Type of Component</typeparam>
    /// <returns>Components of type T</returns>
    public List<T> GetComponentsAs<T>() where T : Component.Component =>
        Components.OfType<T>().ToList();
    
    /// <summary>
    /// Get Component of one Type
    /// </summary>
    /// <typeparam name="T">Type of Component</typeparam>
    /// <returns>Component of type T</returns>
    public T? GetComponentAs<T>() where T: Component.Component =>
        Components.OfType<T>().FirstOrDefault();

    /// <summary>
    /// Get Scene as T
    /// </summary>
    /// <typeparam name="T">Scene Type</typeparam>
    /// <returns>Scene casted as T</returns>
    public T? GetSceneAs<T>() where T : Scene => (T?)Scene;

    /// <summary>
    /// Add Component and return it
    /// </summary>
    /// <param name="component">Component which be added</param>
    /// <typeparam name="T">Type of Component</typeparam>
    /// <returns>Component</returns>
    public T AddComponent<T>(T component) where T : Component.Component
    {
        Components.Add(component);
        component.Entity = this;
        return component;
    }

    /// <summary>
    /// Remove Component
    /// </summary>
    /// <param name="component">Component will be removed</param>
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
    }

    /// <summary>
    /// Unload Entity
    /// </summary>
    public virtual void Unload()
    {
        foreach (var component in Components)
            component.Unload();
    }

    /// <summary>
    /// Update Entity
    /// </summary>
    /// <param name="delta">Time since last frame</param>
    public virtual void Update(float delta)
    {
        foreach (var component in Components)
            component.Update(delta);
    }

    /// <summary>
    /// Draw Entity
    /// </summary>
    public virtual void Draw()
    {
        foreach (var component in Components)
            component.Draw();
    }
}