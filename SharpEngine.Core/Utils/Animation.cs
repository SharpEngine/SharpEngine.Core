using System.Collections.Generic;

namespace SharpEngine.Core.Utils;

/// <summary>
/// Struct which represents animation used by SpriteSheetComponent
/// </summary>
public readonly struct Animation
{
    /// <summary>
    /// Name of Animation
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Indices of frames
    /// </summary>
    public List<uint> Indices { get; }

    /// <summary>
    /// Timer between frames
    /// </summary>
    public float Timer { get; }

    /// <summary>
    /// Create Animation
    /// </summary>
    /// <param name="name">Animation</param>
    /// <param name="indices">Frame Indices</param>
    /// <param name="timer">Frame Timer</param>
    public Animation(string name, List<uint> indices, float timer)
    {
        Name = name;
        Indices = indices;
        Timer = timer;
    }
}
