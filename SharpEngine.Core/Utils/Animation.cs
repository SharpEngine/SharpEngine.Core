using System.Collections.Generic;

namespace SharpEngine.Core.Utils;

/// <summary>
/// Struct which represents animation used by SpriteSheetComponent
/// </summary>
/// <param name="name">Animation</param>
/// <param name="indices">Frame Indices</param>
/// <param name="timer">Frame Timer</param>
/// <param name="loop">If Animation Loop</param>
public struct Animation(string name, List<uint> indices, float timer, bool loop = true)
{
    /// <summary>
    /// Name of Animation
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Indices of frames
    /// </summary>
    public List<uint> Indices { get; set; } = indices;

    /// <summary>
    /// Timer between frames
    /// </summary>
    public float Timer { get; set; } = timer;

    /// <summary>
    /// Loop Animation
    /// </summary>
    public bool Loop { get; set; } = loop;
}
