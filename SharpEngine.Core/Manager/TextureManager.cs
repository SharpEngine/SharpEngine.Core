using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Raylib_cs;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Manager;

/// <summary>
/// Class which manage textures
/// </summary>
public class TextureManager
{
    private readonly Dictionary<string, Texture2D> _texture2Ds = [];

    /// <summary>
    /// Get All Textures
    /// </summary>
    public List<Texture2D> Textures => [.._texture2Ds.Values];

    /// <summary>
    /// Checks if the texture with the specified name exists in the manager.
    /// </summary>
    /// <param name="name">The name of the texture.</param>
    /// <returns>True if the texture exists, otherwise false.</returns>
    [UsedImplicitly]
    public bool HasTexture(string name) => _texture2Ds.ContainsKey(name);

    ///<summary>
    /// Removes a texture from the manager.
    /// </summary>
    /// <param name="name">The name of the texture.</param>
    /// <exception cref="ArgumentException">Thrown if the texture is not found.</exception>
    [UsedImplicitly]
    public void RemoveTexture(string name)
    {
        if (_texture2Ds.TryGetValue(name, out var texture))
        {
            Raylib.UnloadTexture(texture);
            _texture2Ds.Remove(name);
            return;
        }

        DebugManager.Log(LogLevel.Error, $"SE_TEXTUREMANAGER: Texture not found : {name}");
        throw new ArgumentException($"Texture not found : {name}");
    }

    /// <summary>
    /// Adds a texture to the manager.
    /// </summary>
    /// <param name="name">The name of the texture.</param>
    /// <param name="texture2D">The texture to add.</param>
    [UsedImplicitly]
    public void AddTexture(string name, Texture2D texture2D)
    {
        if (!_texture2Ds.TryAdd(name, texture2D))
            DebugManager.Log(
                LogLevel.Warning,
                $"SE_TEXTUREMANAGER: Texture already exists : {name}"
            );
    }

    /// <summary>
    /// Adds a texture to the manager.
    /// </summary>
    /// <param name="name">The name of the texture.</param>
    /// <param name="file">The file path of the texture.</param>
    [UsedImplicitly]
    public void AddTexture(string name, string file)
    {
        if (!_texture2Ds.TryAdd(name, Raylib.LoadTexture(file)))
            DebugManager.Log(
                LogLevel.Warning,
                $"SE_TEXTUREMANAGER: Texture already exists : {name}"
            );
    }

    /// <summary>
    /// Gets a texture from the manager.
    /// </summary>
    /// <param name="name">The name of the texture.</param>
    /// <returns>The texture.</returns>
    /// <exception cref="ArgumentException">Thrown if the texture is not found.</exception>
    public Texture2D GetTexture(string name)
    {
        if (_texture2Ds.TryGetValue(name, out var texture))
            return texture;
        DebugManager.Log(LogLevel.Error, $"SE_TEXTUREMANAGER: Texture not found : {name}");
        throw new ArgumentException($"Texture not found : {name}");
    }

    internal void Unload()
    {
        foreach (var texture in _texture2Ds.Values)
            Raylib.UnloadTexture(texture);
        _texture2Ds.Clear();
    }
}
