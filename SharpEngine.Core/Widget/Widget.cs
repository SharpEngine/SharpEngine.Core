using System.Collections.Generic;
using System.Linq;
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
    public int ZLayer { get; set; } = 0;
    
    /// <summary>
    /// If Widget is Display
    /// </summary>
    public bool Displayed { get; set; } = true;
    
    /// <summary>
    /// If Widget is Active
    /// </summary>
    public bool Active { get; set; } = true;
    
    /// <summary>
    /// Parent of Widget (can be null)
    /// </summary>
    public Widget? Parent { get; set; }
    
    /// <summary>
    /// How Widget must be updated when paused
    /// </summary>
    public PauseState PauseState { get; set; } = PauseState.Normal;

    
    /// <summary>
    /// Get Real Position (Position + Parent RealPostion if widget has Parent)
    /// </summary>
    public Vec2 RealPosition => Parent != null ? Position + Parent.RealPosition : Position;
    
    /// <summary>
    /// Get All Direct Children of Widget
    /// </summary>
    public List<Widget> Children { get; } = new();

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
    /// Widget
    /// </summary>
    /// <param name="position">Position Widget</param>
    /// <param name="zLayer">ZLayer</param>
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
    public List<T> GetChildrenAs<T>() where T : Widget =>
        Children.FindAll(w => w.GetType() == typeof(T)).Cast<T>().ToList();

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
    /// <returns>Scene casted as T</returns>
    public T? GetSceneAs<T>() where T : Scene => (T?)Scene;

    /// <summary>
    /// Add Child and return it
    /// </summary>
    /// <param name="widget">Widget which be added</param>
    /// <typeparam name="T">Type of Widget</typeparam>
    /// <returns>Child</returns>
    public T AddChild<T>(T widget) where T : Widget
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
    /// <param name="widget">Child will be removed</param>
    public void RemoveChild(Widget widget)
    {
        widget.Scene = null;
        Children.Remove(widget);
    }

    /// <summary>
    /// Remove All Children
    /// </summary>
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
    public virtual void Unload()
    {
        foreach (var child in Children)
            child.Unload();
    }

    /// <summary>
    /// Update Widget
    /// </summary>
    /// <param name="delta">Time since last frame</param>
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
        if(!Displayed) return;
        
        foreach (var child in Children)
            child.Draw();
    }
}