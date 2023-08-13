using System;
using System.Collections.Generic;
using Raylib_cs;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Manager;

/// <summary>
/// Class which manage textures
/// </summary>
public class TextureManager
{
    private readonly Dictionary<string, Texture2D> _texture2Ds = new();

    /// <summary>
    /// Get All Textures
    /// </summary>
    public List<Texture2D> Textures => new(_texture2Ds.Values);
    
    /// <summary>
    /// Add texture to manager
    /// </summary>
    /// <param name="name">Texture Name</param>
    /// <param name="texture2D">Texture</param>
    public void AddTexture(string name, Texture2D texture2D)
    {
        if(!_texture2Ds.TryAdd(name, texture2D))
            DebugManager.Log(LogLevel.LogWarning, $"SE_TEXTUREMANAGER: Texture already exist : {name}");
    }

    /// <summary>
    /// Add texture to manager
    /// </summary>
    /// <param name="name">Texture Name</param>
    /// <param name="file">Texture File</param>
    public void AddTexture(string name, string file)
    {
        if(!_texture2Ds.TryAdd(name, Raylib.LoadTexture(file)))
            DebugManager.Log(LogLevel.LogWarning, $"SE_TEXTUREMANAGER: Texture already exist : {name}");
    }

    /// <summary>
    /// Get Texture from Manager
    /// </summary>
    /// <param name="name">Texture Name</param>
    /// <returns>Texture</returns>
    /// <exception cref="Exception">Throws if texture not found</exception>
    public Texture2D GetTexture(string name)
    {
        if (_texture2Ds.TryGetValue(name, out var texture))
            return texture;
        DebugManager.Log(LogLevel.LogError, $"SE_TEXTUREMANAGER: Texture not found : {name}");
        throw new Exception($"Texture not found : {name}");
    }

    internal void Unload()
    {
        foreach (var texture in _texture2Ds.Values)
            Raylib.UnloadTexture(texture);
        _texture2Ds.Clear();
    }
}