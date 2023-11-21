using System;
using System.Collections.Generic;
using Raylib_cs;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Manager;

/// <summary>
/// Class which manage Music
/// </summary>
public class MusicManager
{
    private readonly Dictionary<string, Music> _musics = [];

    /// <summary>
    /// Get All Musics
    /// </summary>
    public List<Music> Musics => new(_musics.Values);

    /// <summary>
    /// Add music to manager
    /// </summary>
    /// <param name="name">Music Name</param>
    /// <param name="file">Music File</param>
    public void AddMusic(string name, string file)
    {
        if (!_musics.TryAdd(name, Raylib.LoadMusicStream(file)))
            DebugManager.Log(LogLevel.LogWarning, $"SE_MUSICMANAGER: Music already exist : {name}");
    }

    /// <summary>
    /// Play music
    /// </summary>
    /// <param name="name">Music Name</param>
    /// <exception cref="Exception">Throws if music not found</exception>
    public void PlayMusic(string name)
    {
        if (_musics.TryGetValue(name, out var music))
        {
            Raylib.PlayMusicStream(music);
            return;
        }
        DebugManager.Log(LogLevel.LogError, $"SE_MUSICMANAGER: Music not found : {name}");
        throw new Exception($"Music not found : {name}");
    }

    /// <summary>
    /// Stop music
    /// </summary>
    /// <param name="name">Music Name</param>
    /// <exception cref="Exception">Throws if music not found</exception>
    public void StopMusic(string name)
    {
        if (_musics.TryGetValue(name, out var music))
        {
            Raylib.StopMusicStream(music);
            return;
        }
        DebugManager.Log(LogLevel.LogError, $"SE_MUSICMANAGER: Music not found : {name}");
        throw new Exception($"Music not found : {name}");
    }

    /// <summary>
    /// Resume music
    /// </summary>
    /// <param name="name">Music Name</param>
    /// <exception cref="Exception">Throws if music not found</exception>
    public void ResumeMusic(string name)
    {
        if (_musics.TryGetValue(name, out var music))
        {
            Raylib.ResumeMusicStream(music);
            return;
        }
        DebugManager.Log(LogLevel.LogError, $"SE_MUSICMANAGER: Music not found : {name}");
        throw new Exception($"Music not found : {name}");
    }

    /// <summary>
    /// Pause music
    /// </summary>
    /// <param name="name">Music Name</param>
    /// <exception cref="Exception">Throws if music not found</exception>
    public void PauseMusic(string name)
    {
        if (_musics.TryGetValue(name, out var music))
        {
            Raylib.PauseMusicStream(music);
            return;
        }
        DebugManager.Log(LogLevel.LogError, $"SE_MUSICMANAGER: Music not found : {name}");
        throw new Exception($"Music not found : {name}");
    }

    /// <summary>
    /// Seek music to position
    /// </summary>
    /// <param name="name">Music Name</param>
    /// <param name="position">Position in seconds</param>
    /// <exception cref="Exception">Throws if music not found</exception>
    public void SeekMusic(string name, float position)
    {
        if (_musics.TryGetValue(name, out var music))
        {
            Raylib.SeekMusicStream(music, position);
            return;
        }
        DebugManager.Log(LogLevel.LogError, $"SE_MUSICMANAGER: Music not found : {name}");
        throw new Exception($"Music not found : {name}");
    }

    /// <summary>
    /// Get Music Length
    /// </summary>
    /// <param name="name">Music Name</param>
    /// <returns>Length in seconds</returns>
    /// <exception cref="Exception">Throws if music not found</exception>
    public float GetMusicLength(string name)
    {
        if (_musics.TryGetValue(name, out var music))
            return Raylib.GetMusicTimeLength(music);
        DebugManager.Log(LogLevel.LogError, $"SE_MUSICMANAGER: Music not found : {name}");
        throw new Exception($"Music not found : {name}");
    }

    /// <summary>
    /// Get Music Current Time
    /// </summary>
    /// <param name="name">Music Name</param>
    /// <returns>Current Time in seconds</returns>
    /// <exception cref="Exception">Throws if music not found</exception>
    public float GetMusicCurrentTime(string name)
    {
        if (_musics.TryGetValue(name, out var music))
            return Raylib.GetMusicTimePlayed(music);
        DebugManager.Log(LogLevel.LogError, $"SE_MUSICMANAGER: Music not found : {name}");
        throw new Exception($"Music not found : {name}");
    }

    /// <summary>
    /// Check if music is playing
    /// </summary>
    /// <param name="name">Music Name</param>
    /// <returns>If music is playing</returns>
    /// <exception cref="Exception">Throws if music not found</exception>
    public bool IsMusicPlaying(string name)
    {
        if (_musics.TryGetValue(name, out var music))
            return Raylib.IsMusicStreamPlaying(music);
        DebugManager.Log(LogLevel.LogError, $"SE_MUSICMANAGER: Music not found : {name}");
        throw new Exception($"Music not found : {name}");
    }
}
