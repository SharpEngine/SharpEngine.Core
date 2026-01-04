using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Raylib_cs;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Manager;

/// <summary>
/// Class which manage Sound
/// </summary>
public class SoundManager
{
    private readonly Dictionary<string, Sound> _sounds = [];

    /// <summary>
    /// Get All Sounds
    /// </summary>
    public List<Sound> Sounds => [.._sounds.Values];

    /// <summary>
    /// Add sound to the manager
    /// </summary>
    /// <param name="name">Sound Name</param>
    /// <param name="file">Sound File</param>
    [UsedImplicitly]
    public void AddSound(string name, string file)
    {
        if (!_sounds.TryAdd(name, Raylib.LoadSound(file)))
            DebugManager.Log(LogLevel.Warning, $"SE_SOUNDMANAGER: Sound already exist : {name}");
    }

    /// <summary>
    /// Play Sound
    /// </summary>
    /// <param name="name">Sound Name</param>
    /// <exception cref="ArgumentException">Throws if sound not found</exception>
    [UsedImplicitly]
    public void PlaySound(string name)
    {
        if (_sounds.TryGetValue(name, out var sound))
        {
            Raylib.PlaySound(sound);
            return;
        }
        DebugManager.Log(LogLevel.Error, $"SE_SOUNDMANAGER: Sound not found : {name}");
        throw new ArgumentException($"Sound not found : {name}");
    }

    /// <summary>
    /// Stop Sound
    /// </summary>
    /// <param name="name">Sound Name</param>
    /// <exception cref="ArgumentException">Throws if sound not found</exception>
    [UsedImplicitly]
    public void StopSound(string name)
    {
        if (_sounds.TryGetValue(name, out var sound))
        {
            Raylib.StopSound(sound);
            return;
        }
        DebugManager.Log(LogLevel.Error, $"SE_SOUNDMANAGER: Sound not found : {name}");
        throw new ArgumentException($"Sound not found : {name}");
    }

    /// <summary>
    /// Resume Sound
    /// </summary>
    /// <param name="name">Sound Name</param>
    /// <exception cref="ArgumentException">Throws if sound not found</exception>
    [UsedImplicitly]
    public void ResumeSound(string name)
    {
        if (_sounds.TryGetValue(name, out var sound))
        {
            Raylib.ResumeSound(sound);
            return;
        }
        DebugManager.Log(LogLevel.Error, $"SE_SOUNDMANAGER: Sound not found : {name}");
        throw new ArgumentException($"Sound not found : {name}");
    }

    /// <summary>
    /// Pause Sound
    /// </summary>
    /// <param name="name">Sound Name</param>
    /// <exception cref="ArgumentException">Throws if sound not found</exception>
    [UsedImplicitly]
    public void PauseSound(string name)
    {
        if (_sounds.TryGetValue(name, out var sound))
        {
            Raylib.PauseSound(sound);
            return;
        }
        DebugManager.Log(LogLevel.Error, $"SE_SOUNDMANAGER: Sound not found : {name}");
        throw new ArgumentException($"Sound not found : {name}");
    }

    /// <summary>
    /// Check if Sound is playing
    /// </summary>
    /// <param name="name">Sound Name</param>
    /// <returns>If Sound is playing</returns>
    /// <exception cref="ArgumentException">Throws if Sound not found</exception>
    [UsedImplicitly]
    public bool IsSoundPlaying(string name)
    {
        if (_sounds.TryGetValue(name, out var sound))
            return Raylib.IsSoundPlaying(sound);
        DebugManager.Log(LogLevel.Error, $"SE_SOUNDMANAGER: Sound not found : {name}");
        throw new ArgumentException($"Sound not found : {name}");
    }
}
