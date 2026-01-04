using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using JetBrains.Annotations;
using Raylib_cs;
using SharpEngine.Core.Math;
using SharpEngine.Core.Utils;
using Color = SharpEngine.Core.Utils.Color;

namespace SharpEngine.Core.Manager;

/// <summary>
/// Class which manages Shaders
/// </summary>
public class ShaderManager
{
    private readonly Dictionary<string, SEShader> _shaders = [];

    /// <summary>
    /// Gets all Shaders
    /// </summary>
    public List<SEShader> Shaders => [.._shaders.Values];

    /// <summary>
    /// Checks if a Shader exists
    /// </summary>
    /// <param name="name">The name of the Shader</param>
    /// <returns>True if the Shader exists, otherwise false</returns>
    [UsedImplicitly]
    public bool HasShader(string name) => _shaders.ContainsKey(name);

    /// <summary>
    /// Removes a Shader from the manager
    /// </summary>
    /// <param name="name">The name of the Shader to remove</param>
    /// <exception cref="ArgumentException">Thrown if the Shader is not found</exception>
    [UsedImplicitly]
    public void RemoveShader(string name)
    {
        if (_shaders.TryGetValue(name, out var shader))
        {
            Raylib.UnloadShader(shader.GetInternalShader());
            _shaders.Remove(name);
            return;
        }

        DebugManager.Log(LogLevel.Error, $"SE_SHADERMANAGER: Shader not found : {name}");
        throw new ArgumentException($"Shader not found : {name}");
    }

    /// <summary>
    /// Defines a float value for a Shader
    /// </summary>
    /// <param name="name">The name of the Shader</param>
    /// <param name="uniform">The name of the uniform</param>
    /// <param name="value">The float value</param>
    [UsedImplicitly]
    public void SetShaderValue(string name, string uniform, float value) =>
        SetShaderValue(name, uniform, value, ShaderUniformDataType.Float);

    /// <summary>
    /// Defines a vec2 value for a Shader
    /// </summary>
    /// <param name="name">The name of the Shader</param>
    /// <param name="uniform">The name of the uniform</param>
    /// <param name="value">The vec2 value</param>
    [UsedImplicitly]
    public void SetShaderValue(string name, string uniform, Vec2 value) =>
        SetShaderValue(
            name,
            uniform,
            new Vector2(value.X, value.Y),
            ShaderUniformDataType.Vec2
        );

    /// <summary>
    /// Defines a color value for a Shader
    /// </summary>
    /// <param name="name">The name of the Shader</param>
    /// <param name="uniform">The name of the uniform</param>
    /// <param name="value">The color value</param>
    [UsedImplicitly]
    public void SetShaderValue(string name, string uniform, Color value) =>
        SetShaderValue(name, uniform, value.ToVec4(), ShaderUniformDataType.Vec4);

    /// <summary>
    /// Defines an int value for a Shader
    /// </summary>
    /// <param name="name">The name of the Shader</param>
    /// <param name="uniform">The name of the uniform</param>
    /// <param name="value">The int value</param>
    [UsedImplicitly]
    public void SetShaderValue(string name, string uniform, int value) =>
        SetShaderValue(name, uniform, value, ShaderUniformDataType.Float);

    /// <summary>
    /// Defines a value for a Shader
    /// </summary>
    /// <typeparam name="T">The type of the value</typeparam>
    /// <param name="name">The name of the Shader</param>
    /// <param name="uniform">The name of the uniform</param>
    /// <param name="value">The value</param>
    /// <param name="uniformType">The type of the uniform</param>
    /// <exception cref="ArgumentException">Thrown if the Shader is not found</exception>
    [UsedImplicitly]
    public void SetShaderValue<T>(
        string name,
        string uniform,
        T value,
        ShaderUniformDataType uniformType
    )
        where T : unmanaged
    {
        if (_shaders.TryGetValue(name, out var shader))
            Raylib.SetShaderValue(
                shader.GetInternalShader(),
                Raylib.GetShaderLocation(shader.GetInternalShader(), uniform),
                value,
                uniformType
            );
        else
            throw new ArgumentException($"Shader not found : {name}");
    }

    /// <summary>
    /// Adds a Shader to the manager
    /// </summary>
    /// <param name="name">The name of the Shader</param>
    /// <param name="shader">The Shader to add</param>
    [UsedImplicitly]
    public void AddShader(string name, SEShader shader)
    {
        if (!_shaders.TryAdd(name, shader))
            DebugManager.Log(
                LogLevel.Warning,
                $"SE_SHADERMANAGER: Shader already exists : {name}"
            );
    }

    /// <summary>
    /// Adds a Shader to the manager
    /// </summary>
    /// <param name="name">The name of the Shader</param>
    /// <param name="vertexFile">The path to the vertex shader file</param>
    /// <param name="fragmentFile">The path to the fragment shader file</param>
    [UsedImplicitly]
    public void AddShader(string name, string vertexFile, string fragmentFile)
    {
        if (!_shaders.TryAdd(name, new SEShader(File.ReadAllText(vertexFile), File.ReadAllText(fragmentFile))))
            DebugManager.Log(
                LogLevel.Warning,
                $"SE_SHADERMANAGER: Shader already exists : {name}"
            );
    }

    /// <summary>
    /// Adds a Shader to the manager
    /// </summary>
    /// <param name="name">The name of the Shader</param>
    /// <param name="vertexCode">The code of the vertex shader</param>
    /// <param name="fragmentCode">The code of the fragment shader</param>
    [UsedImplicitly]
    public void AddShaderFromCode(string name, string vertexCode, string fragmentCode)
    {
        if (!_shaders.TryAdd(name, new SEShader(vertexCode, fragmentCode)))
            DebugManager.Log(
                LogLevel.Warning,
                $"SE_SHADERMANAGER: Shader already exists : {name}"
            );
    }

    /// <summary>
    /// Gets a Shader from the manager
    /// </summary>
    /// <param name="name">The name of the Shader</param>
    /// <returns>The Shader</returns>
    /// <exception cref="ArgumentException">Thrown if the Shader is not found</exception>
    public SEShader GetShader(string name)
    {
        if (_shaders.TryGetValue(name, out var shader))
            return shader;
        DebugManager.Log(LogLevel.Error, $"SE_SHADERMANAGER: Shader not found : {name}");
        throw new ArgumentException($"Shader not found : {name}");
    }

    internal void Unload()
    {
        foreach (var shader in _shaders.Values)
            Raylib.UnloadShader(shader.GetInternalShader());
        _shaders.Clear();
    }
}
