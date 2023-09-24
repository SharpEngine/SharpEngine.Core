using System;
using System.Collections.Generic;
using Raylib_cs;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Manager;

/// <summary>
/// Class which manage Sound
/// </summary>
public class SoundManager
{
    private readonly Dictionary<string, Sound> _sounds = new();

    /// <summary>
    /// Get All Sounds
    /// </summary>
    public List<Sound> Sounds => new(_sounds.Values);

    /// <summary>
    /// Add sound to manager
    /// </summary>
    /// <param name="name">Sound Name</param>
    /// <param name="file">Sound File</param>
    public void AddSound(string name, string file)
    {
        if (!_sounds.TryAdd(name, Raylib.LoadSound(file)))
            DebugManager.Log(LogLevel.LogWarning, $"SE_SOUNDMANAGER: Sound already exist : {name}");
    }

    /// <summary>
    /// Play Sound
    /// </summary>
    /// <param name="name">Sound Name</param>
    /// <exception cref="Exception">Throws if sound not found</exception>
    public void PlaySound(string name)
    {
        if (_sounds.TryGetValue(name, out var sound))
            Raylib.PlaySound(sound);
        DebugManager.Log(LogLevel.LogError, $"SE_SOUNDMANAGER: Sound not found : {name}");
        throw new Exception($"Sound not found : {name}");
    }

    /// <summary>
    /// Stop Sound
    /// </summary>
    /// <param name="name">Sound Name</param>
    /// <exception cref="Exception">Throws if sound not found</exception>
    public void StopSound(string name)
    {
        if (_sounds.TryGetValue(name, out var sound))
            Raylib.StopSound(sound);
        DebugManager.Log(LogLevel.LogError, $"SE_SOUNDMANAGER: Sound not found : {name}");
        throw new Exception($"Sound not found : {name}");
    }

    /// <summary>
    /// Resume Sound
    /// </summary>
    /// <param name="name">Sound Name</param>
    /// <exception cref="Exception">Throws if sound not found</exception>
    public void ResumeSound(string name)
    {
        if (_sounds.TryGetValue(name, out var sound))
            Raylib.ResumeSound(sound);
        DebugManager.Log(LogLevel.LogError, $"SE_SOUNDMANAGER: Sound not found : {name}");
        throw new Exception($"Sound not found : {name}");
    }

    /// <summary>
    /// Pause Sound
    /// </summary>
    /// <param name="name">Sound Name</param>
    /// <exception cref="Exception">Throws if sound not found</exception>
    public void PauseSound(string name)
    {
        if (_sounds.TryGetValue(name, out var sound))
            Raylib.PauseSound(sound);
        DebugManager.Log(LogLevel.LogError, $"SE_SOUNDMANAGER: Sound not found : {name}");
        throw new Exception($"Sound not found : {name}");
    }

    /// <summary>
    /// Check if Sound is playing
    /// </summary>
    /// <param name="name">Sound Name</param>
    /// <returns>If Sound is playing</returns>
    /// <exception cref="Exception">Throws if Sound not found</exception>
    public bool IsSoundPlaying(string name)
    {
        if (_sounds.TryGetValue(name, out var sound))
            return Raylib.IsSoundPlaying(sound);
        DebugManager.Log(LogLevel.LogError, $"SE_SOUNDMANAGER: Sound not found : {name}");
        throw new Exception($"Sound not found : {name}");
    }
}
