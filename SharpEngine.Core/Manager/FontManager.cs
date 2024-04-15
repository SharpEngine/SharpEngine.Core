using System;
using System.Collections.Generic;
using System.IO;
using Raylib_cs;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Manager;

/// <summary>
/// Class which manage font
/// </summary>
public class FontManager
{
    private readonly Dictionary<string, Font> _fonts = [];

    /// <summary>
    /// Get All Fonts
    /// </summary>
    public List<Font> Fonts => new(_fonts.Values);

    /// <summary>
    /// Checks if the font with the specified name exists.
    /// </summary>
    /// <param name="name">The name of the font.</param>
    public void HasFont(string name) => _fonts.ContainsKey(name);

    /// <summary>
    /// Removes the font with the specified name from the manager.
    /// </summary>
    /// <param name="name">The name of the font to remove.</param>
    public void RemoveFont(string name)
    {
        if (_fonts.TryGetValue(name, out var font))
        {
            Raylib.UnloadFont(font);
            _fonts.Remove(name);
            return;
        }

        DebugManager.Log(LogLevel.LogError, $"SE_FONTMANAGER: Font not found : {name}");
        throw new ArgumentException($"Font not found : {name}");
    }

    /// <summary>
    /// Adds a font to the manager.
    /// </summary>
    /// <param name="name">The name of the font.</param>
    /// <param name="file">The path to the font file.</param>
    /// <param name="fontSize">The size of the font (default is 25).</param>
    /// <exception cref="FileNotFoundException">Thrown if the font file is not found.</exception>
    public void AddFont(string name, string file, int fontSize = 25)
    {
        if (!File.Exists(file))
        {
            DebugManager.Log(LogLevel.LogFatal, $"SE_FONTMANAGER: Font not found : {name}");
            throw new FileNotFoundException($"Font not found : {file}");
        }

        if (!_fonts.TryAdd(name, Raylib.LoadFontEx(file, fontSize, null, 250)))
            DebugManager.Log(LogLevel.LogWarning, $"SE_FONTMANAGER: Font already exist : {name}");
    }

    /// <summary>
    /// Gets the font with the specified name from the manager.
    /// </summary>
    /// <param name="name">The name of the font.</param>
    /// <returns>The font.</returns>
    /// <exception cref="ArgumentException">Thrown if the font is not found.</exception>
    public Font GetFont(string name)
    {
        if (name == "RAYLIB_DEFAULT")
            return Raylib.GetFontDefault();

        if (_fonts.TryGetValue(name, out var font))
            return font;
        DebugManager.Log(LogLevel.LogError, $"SE_FONTMANAGER: Font not found : {name}");
        throw new ArgumentException($"Font not found : {name}");
    }

    internal void Unload()
    {
        foreach (var font in _fonts.Values)
            Raylib.UnloadFont(font);
        _fonts.Clear();
    }
}
