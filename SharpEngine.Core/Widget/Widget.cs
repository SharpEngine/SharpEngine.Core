using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using SharpEngine.Core.Math;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Abstract Class which represents Widget
/// </summary>
public abstract class Widget
{
    /// <summary>
    /// Position of Widget
    /// </summary>
    public Vec2 Position { get; set; }

    /// <summary>
    /// Z Layer of Widget
    /// </summary>
    [UsedImplicitly]
    public int ZLayer { get; set; }

    /// <summary>
    /// If Widget is Displayed
    /// </summary>
    [UsedImplicitly]
    public bool Displayed { get; set; } = true;

    /// <summary>
    /// If Widget is Active
    /// </summary>
    [UsedImplicitly]
    public bool Active { get; set; } = true;

    /// <summary>
    /// Parent of Widget (can be null)
    /// </summary>
    [UsedImplicitly]
    public Widget? Parent { get; set; }

    /// <summary>
    /// How Widget must be updated when paused
    /// </summary>
    [UsedImplicitly]
    public PauseState PauseState { get; set; } = PauseState.Normal;

    /// <summary>
    /// Name of Widget
    /// </summary>
    [UsedImplicitly]
    public string Name { get; set; } = "";

    /// <summary>
    /// Get Real Position (Position + Parent RealPosition if widget has Parent)
    /// </summary>
    [UsedImplicitly]
    public Vec2 RealPosition => Parent != null ? Position + Parent.RealPosition : Position;

    /// <summary>
    /// If Widget is really displayed (considering parent's display state)
    /// </summary>
    [UsedImplicitly]
    public bool RealDisplayed => Displayed && (Parent == null || Parent.RealDisplayed);

    /// <summary>
    /// Get All Direct Children of Widget
    /// </summary>
    [UsedImplicitly]
    public List<Widget> Children { get; } = [];

    /// <summary>
    /// Scene of Widget
    /// </summary>
    public Scene? Scene
    {
        get => _scene;
        set
        {
            _scene = value;
            foreach (var child in Children)
                child.Scene = value;
        }
    }

    private Scene? _scene;

    /// <summary>
    /// Initializes a new instance of the <see cref="Widget"/> class.
    /// </summary>
    /// <param name="position">The position of the widget.</param>
    /// <param name="zLayer">The Z layer of the widget.</param>
    protected Widget(Vec2 position, int zLayer = 0)
    {
        Position = position;
        ZLayer = zLayer;
    }

    /// <summary>
    /// Get All Direct Children of one Type
    /// </summary>
    /// <typeparam name="T">Type of Children</typeparam>
    /// <returns>Children of type T</returns>
    [UsedImplicitly]
    public List<T> GetChildrenAs<T>()
        where T : Widget => Children.FindAll(w => w.GetType() == typeof(T)).Cast<T>().ToList();

    /// <summary>
    /// Get All Recursive Children
    /// </summary>
    /// <returns>All Children</returns>
    public List<Widget> GetAllChildren()
    {
        var children = new List<Widget>(Children);
        foreach (var child in Children)
            children.AddRange(child.GetAllChildren());
        return children;
    }

    /// <summary>
    /// Get Scene as T
    /// </summary>
    /// <typeparam name="T">Scene Type</typeparam>
    /// <returns>Scene cast as T</returns>
    [UsedImplicitly]
    public T? GetSceneAs<T>()
        where T : Scene => (T?)Scene;

    /// <summary>
    /// Add Child and return it
    /// </summary>
    /// <param name="widget">Widget, which will be added</param>
    /// <typeparam name="T">Type of Widget</typeparam>
    /// <returns>Child</returns>
    [UsedImplicitly]
    public T AddChild<T>(T widget)
        where T : Widget
    {
        if (_scene != null)
            widget.Scene = _scene;
        widget.Parent = this;
        Children.Add(widget);
        return widget;
    }

    /// <summary>
    /// Remove Child
    /// </summary>
    /// <param name="widget">Child that will be removed</param>
    [UsedImplicitly]
    public void RemoveChild(Widget widget)
    {
        widget.Scene = null;
        Children.Remove(widget);
    }

    /// <summary>
    /// Remove All Children
    /// </summary>
    [UsedImplicitly]
    public void RemoveAllChildren()
    {
        foreach (var child in Children)
            child.Scene = null;
        Children.Clear();
    }

    /// <summary>
    /// Load Widget
    /// </summary>
    public virtual void Load()
    {
        foreach (var child in Children)
            child.Load();
    }

    /// <summary>
    /// Unload Widget
    /// </summary>
    [UsedImplicitly]
    public virtual void Unload()
    {
        foreach (var child in Children)
            child.Unload();
    }

    /// <summary>
    /// Update Widget
    /// </summary>
    /// <param name="delta">Time since last frame</param>
    [UsedImplicitly]
    public virtual void Update(float delta)
    {
        foreach (var child in Children)
            child.Update(delta);
    }

    /// <summary>
    /// Draw Widget
    /// </summary>
    public virtual void Draw()
    {
        if (!Displayed)
            return;

        foreach (var child in Children)
            child.Draw();
    }
}
