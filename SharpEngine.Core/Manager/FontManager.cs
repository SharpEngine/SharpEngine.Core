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
    private readonly Dictionary<string, Font> _fonts = new();

    /// <summary>
    /// Get All Fonts
    /// </summary>
    public List<Font> Fonts => new(_fonts.Values);

    /// <summary>
    /// Add font to manager
    /// </summary>
    /// <param name="name">Font Name</param>
    /// <param name="file">Font Path</param>
    /// <param name="fontSize">Font Size (25)</param>
    public void AddFont(string name, string file, int fontSize = 25)
    {
        if (!File.Exists(file))
        {
            DebugManager.Log(LogLevel.LogFatal, $"SE_FONTMANAGER: Font not found : {name}");
            throw new FileNotFoundException($"Font not found : {file}");
        }

        if(!_fonts.TryAdd(name, Raylib.LoadFontEx(file, fontSize, null, 250)))
            DebugManager.Log(LogLevel.LogWarning, $"SE_FONTMANAGER: Font already exist : {name}");
    }

    /// <summary>
    /// Get font from manager
    /// </summary>
    /// <param name="name">Font Name</param>
    /// <returns>Font</returns>
    /// <exception cref="Exception">Throws if font not found</exception>
    public Font GetFont(string name)
    {
        if (name == "RAYLIB_DEFAULT")
            return Raylib.GetFontDefault();
        
        if (_fonts.TryGetValue(name, out var font))
            return font;
        DebugManager.Log(LogLevel.LogError, $"SE_FONTMANAGER: Font not found : {name}");
        throw new Exception($"Font not found : {name}");
    }

    internal void Unload()
    {
        foreach (var font in _fonts.Values)
            Raylib.UnloadFont(font);
        _fonts.Clear();
    }
}