using System.Collections.Generic;

namespace SharpEngine.Core.Utils;

/// <summary>
/// Struct which represents animation used by SpriteSheetComponent
/// </summary>
/// <param name="name">Animation</param>
/// <param name="indices">Frame Indices</param>
/// <param name="timer">Frame Timer</param>
public readonly struct Animation(string name, List<uint> indices, float timer)
{
    /// <summary>
    /// Name of Animation
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Indices of frames
    /// </summary>
    public List<uint> Indices { get; } = indices;

    /// <summary>
    /// Timer between frames
    /// </summary>
    public float Timer { get; } = timer;
}
