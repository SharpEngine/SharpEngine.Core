using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
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
    [UsedImplicitly]
    public bool Paused { get; set; }

    /// <summary>
    /// Window that has this scene
    /// </summary>
    public Window? Window { get; set; }

    /// <summary>
    /// All Widgets of Scene
    /// </summary>
    public List<Widget.Widget> Widgets { get; } = [];

    /// <summary>
    /// All Entities of Scene
    /// </summary>
    public List<Entity.Entity> Entities { get; } = [];

    private readonly List<Entity.Entity> _addEntities = [];
    private readonly List<Widget.Widget> _addWidgets = [];
    private readonly List<Entity.Entity> _removeEntities = [];
    private readonly List<Widget.Widget> _removeWidgets = [];
    private readonly List<ISceneSystem> _sceneSystems = [];

    /// <summary>
    /// Create Scene
    /// </summary>
    public Scene() { }

    /// <summary>
    /// Add Scene System
    /// </summary>
    /// <param name="system">Scene System</param>
    [UsedImplicitly]
    public void AddSceneSystem(ISceneSystem system) => _sceneSystems.Add(system);

    /// <summary>
    /// Get Scene System
    /// </summary>
    /// <typeparam name="T">Type of System</typeparam>
    /// <returns>System or null</returns>
    [UsedImplicitly]
    public T? GetSceneSystem<T>()
        where T : ISceneSystem => _sceneSystems.OfType<T>().FirstOrDefault();

    /// <summary>
    /// Add Widget to Scene
    /// </summary>
    /// <param name="widget">Widget which be added</param>
    /// <param name="delay">If adding must be delayed</param>
    /// <typeparam name="T">Type of Widget</typeparam>
    /// <returns>Widget</returns>
    [UsedImplicitly]
    public T AddWidget<T>(T widget, bool delay = false)
        where T : Widget.Widget
    {
        if (delay)
            _addWidgets.Add(widget);
        else
        {
            widget.Scene = this;
            Widgets.Add(widget);
        }

        return widget;
    }

    /// <summary>
    /// Get All Widgets of one Type
    /// </summary>
    /// <typeparam name="T">Type of Widgets</typeparam>
    /// <returns>Widgets</returns>
    [UsedImplicitly]
    public List<T> GetWidgetsAs<T>()
        where T : Widget.Widget => Widgets.OfType<T>().ToList();

    /// <summary>
    /// Remove Widget
    /// </summary>
    /// <param name="widget">Widget which be removed</param>
    /// <param name="delay">If remove must be delayed</param>
    [UsedImplicitly]
    public void RemoveWidget(Widget.Widget widget, bool delay = false)
    {
        if (delay)
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
    [UsedImplicitly]
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
    /// <param name="delay">If adding must be delayed</param>
    /// <typeparam name="T">Type of Widgets</typeparam>
    /// <returns>Entity</returns>
    [UsedImplicitly]
    public T AddEntity<T>(T entity, bool delay = false)
        where T : Entity.Entity
    {
        if (delay)
            _addEntities.Add(entity);
        else
        {
            entity.Scene = this;
            Entities.Add(entity);
        }

        return entity;
    }

    /// <summary>
    /// Remove Entity From Scene
    /// </summary>
    /// <param name="entity">Entity to be removed</param>
    /// <param name="delay">If remove must be delayed</param>
    [UsedImplicitly]
    public void RemoveEntity(Entity.Entity entity, bool delay = false)
    {
        if (delay)
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
    [UsedImplicitly]
    public void RemoveAllEntities()
    {
        foreach (var entity in Entities)
            entity.Scene = null;
        Entities.Clear();
    }

    /// <summary>
    /// Load Scene
    /// </summary>
    [UsedImplicitly]
    public virtual void Load()
    {
        foreach (var system in _sceneSystems)
            system.Load();

        foreach (var entity in Entities)
            entity.Load();
        foreach (var widget in Widgets)
            widget.Load();
    }

    /// <summary>
    /// Unload Scene
    /// </summary>
    [UsedImplicitly]
    public virtual void Unload()
    {
        foreach (var entity in Entities)
            entity.Unload();
        foreach (var widget in Widgets)
            widget.Unload();

        foreach (var system in _sceneSystems)
            system.Unload();
    }

    /// <summary>
    /// Update Scene
    /// </summary>
    /// <param name="delta">Time since last update</param>
    [UsedImplicitly]
    public virtual void Update(float delta)
    {
        foreach (var system in _sceneSystems)
            system.Update(delta);

        ProcessAddingAndRemoving();

        for (var i = Entities.Count - 1; i > -1; i--)
            if (
                Entities[i].PauseState is PauseState.Enabled
                || !Paused && Entities[i].PauseState is PauseState.Normal
                || Paused && Entities[i].PauseState is PauseState.WhenPaused
            )
                Entities[i].Update(delta);

        for (var i = Widgets.Count - 1; i > -1; i--)
            if (
                Widgets[i].PauseState is PauseState.Enabled
                || !Paused && Widgets[i].PauseState is PauseState.Normal
                || Paused && Widgets[i].PauseState is PauseState.WhenPaused
            )
                Widgets[i].Update(delta);
    }

    private void ProcessAddingAndRemoving()
    {
        foreach (var removeEntity in _removeEntities)
            RemoveEntity(removeEntity);
        foreach (var removeWidget in _removeWidgets)
            RemoveWidget(removeWidget);
        foreach (var addEntity in _addEntities)
            AddEntity(addEntity).Load();
        foreach (var addWidget in _addWidgets)
            AddWidget(addWidget).Load();

        _removeEntities.Clear();
        _removeWidgets.Clear();
        _addEntities.Clear();
        _addWidgets.Clear();
    }

    /// <summary>
    /// Draw Scene
    /// </summary>
    [UsedImplicitly]
    public virtual void Draw()
    {
        foreach (var system in _sceneSystems)
            system.Draw();

        foreach (var ent in Entities)
            ent.Draw();
        foreach (var widget in Widgets)
            widget.Draw();
    }

    /// <summary>
    /// Function call when Scene is opened
    /// </summary>
    public virtual void OpenScene()
    {
        foreach (var system in _sceneSystems)
            system.OpenScene();
    }

    /// <summary>
    /// Function call when Scene is closed
    /// </summary>
    [UsedImplicitly]
    public virtual void CloseScene()
    {
        foreach (var system in _sceneSystems)
            system.CloseScene();
    }
}
