namespace SharpEngine.Core.Component;

/// <summary>
/// Abstract Class for Components
/// </summary>
public abstract class Component
{
    /// <summary>
    /// Entity to which the component is attached 
    /// </summary>
    public Entity.Entity? Entity { get; set; }

    /// <summary>
    /// Load Component
    /// </summary>
    public virtual void Load() {}
    
    /// <summary>
    /// Unload Component
    /// </summary>
    public virtual void Unload() {}
    
    /// <summary>
    /// Update Component
    /// </summary>
    /// <param name="delta">Time since last frame</param>
    public virtual void Update(float delta) {}
    
    /// <summary>
    /// Draw Component
    /// </summary>
    public virtual void Draw() {}
}