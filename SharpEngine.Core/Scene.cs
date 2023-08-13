using System.Collections.Generic;
using System.Linq;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core;

/// <summary>
/// Class which represents a Scene
/// </summary>
public class Scene
{
    /// <summary>
    /// Define if Scene is paused
    /// </summary>
    public bool Paused = false;

    /// <summary>
    /// Window which have this scene
    /// </summary>
    public Window? Window;

    /// <summary>
    /// All Widgets of Scene
    /// </summary>
    public List<Widget.Widget> Widgets { get; } = new();

    /// <summary>
    /// All Entities of Scene
    /// </summary>
    public List<Entity.Entity> Entities { get; } = new();
    
    private readonly List<Entity.Entity> _removeEntities = new();
    private readonly List<Widget.Widget> _removeWidgets = new();

    /// <summary>
    /// Create Scene
    /// </summary>
    public Scene()
    {}

    /// <summary>
    /// Add Widget to Scene
    /// </summary>
    /// <param name="widget">Widget which be added</param>
    /// <typeparam name="T">Type of Widget</typeparam>
    /// <returns>Widget</returns>
    public T AddWidget<T>(T widget) where T : Widget.Widget
    {
        widget.Scene = this;
        Widgets.Add(widget);
        return widget;
    }

    /// <summary>
    /// Get All Widgets of one Type
    /// </summary>
    /// <typeparam name="T">Type of Widgets</typeparam>
    /// <returns>Widgets</returns>
    public List<T> GetWidgetsAs<T>() where T : Widget.Widget =>
        Widgets.OfType<T>().ToList();

    /// <summary>
    /// Remove Widget
    /// </summary>
    /// <param name="widget">Widget which be removed</param>
    /// <param name="delay">If remove must be delayed</param>
    public void RemoveWidget(Widget.Widget widget, bool delay = false)
    {
        if(delay)
            _removeWidgets.Add(widget);
        else
        {
            widget.Scene = null;
            Widgets.Remove(widget);
        }
    }

    /// <summary>
    /// Remove All Widgets
    /// </summary>
    public void RemoveAllWidgets()
    {
        foreach (var widget in Widgets)
            widget.Scene = null;
        Widgets.Clear();
    }

    /// <summary>
    /// Add Entity To Scene
    /// </summary>
    /// <param name="entity">Entity to be added</param>
    /// <typeparam name="T">Type of Widgets</typeparam>
    /// <returns>Entity</returns>
    public T AddEntity<T>(T entity) where T : Entity.Entity
    {
        entity.Scene = this;
        Entities.Add(entity);
        return entity;
    }

    /// <summary>
    /// Remove Entity From Scene
    /// </summary>
    /// <param name="entity">Entity to be removed</param>
    /// <param name="delay">If remove must be delayed</param>
    public void RemoveEntity(Entity.Entity entity, bool delay = false)
    {
        if(delay)
            _removeEntities.Add(entity);
        else
        {
            entity.Scene = null;
            Entities.Remove(entity);
        }
    }

    /// <summary>
    /// Remove all Entities
    /// </summary>
    public void RemoveAllEntities()
    {
        foreach (var entity in Entities)
            entity.Scene = null;
        Entities.Clear();
    }

    /// <summary>
    /// Load Scene
    /// </summary>
    public virtual void Load()
    {
        foreach (var entity in Entities)
            entity.Load();
        foreach (var widget in Widgets)
            widget.Load();
    }

    /// <summary>
    /// Unload Scene
    /// </summary>
    public virtual void Unload()
    {
        foreach (var entity in Entities)
            entity.Unload();
        foreach (var widget in Widgets)
            widget.Unload();
    }

    /// <summary>
    /// Update Scene
    /// </summary>
    /// <param name="delta">Time since last update</param>
    public virtual void Update(float delta)
    {
        foreach (var removeEntity in _removeEntities)
            RemoveEntity(removeEntity);
        foreach (var removeWidget in _removeWidgets)
            RemoveWidget(removeWidget);

        for (var i = Entities.Count - 1; i > -1; i--)
            if(Entities[i].PauseState is PauseState.Enabled ||
               !Paused && Entities[i].PauseState is PauseState.Normal ||
               Paused && Entities[i].PauseState is PauseState.WhenPaused)
                Entities[i].Update(delta);
        
        for(var i = Widgets.Count - 1; i > -1; i--)
            if(Widgets[i].PauseState is PauseState.Enabled ||
               !Paused && Widgets[i].PauseState is PauseState.Normal ||
               Paused && Widgets[i].PauseState is PauseState.WhenPaused)
                Widgets[i].Update(delta);
    }

    /// <summary>
    /// Draw Scene
    /// </summary>
    public virtual void Draw()
    {
        foreach (var ent in Entities)
            ent.Draw();
        foreach (var widget in Widgets)
            widget.Draw();
    }

    /// <summary>
    /// Function call when Scene is opened
    /// </summary>
    public virtual void OpenScene() { }

    /// <summary>
    /// Function call when Scene is closed
    /// </summary>
    public virtual void CloseScene() { }
}