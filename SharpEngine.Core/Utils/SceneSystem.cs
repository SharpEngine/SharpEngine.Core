namespace SharpEngine.Core.Utils;

/// <summary>
/// System which can be added to scene (useful for optional packages)
/// </summary>
public interface ISceneSystem
{
    /// <summary>
    /// Function call when system is loaded
    /// </summary>
    public void Load();

    /// <summary>
    /// Function call when system is unloaded
    /// </summary>
    public void Unload();

    /// <summary>
    /// Function call when system is updated
    /// </summary>
    /// <param name="delta">Time since last update</param>
    public void Update(float delta);

    /// <summary>
    /// Function call when system draws
    /// </summary>
    public void Draw();

    /// <summary>
    /// Function call when system is opened
    /// </summary>
    public void OpenScene();

    /// <summary>
    /// Function call when system is closed
    /// </summary>
    public void CloseScene();
}