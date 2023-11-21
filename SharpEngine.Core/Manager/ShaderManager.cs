using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using SharpEngine.Core.Math;
using SharpEngine.Core.Utils;
using Color = SharpEngine.Core.Utils.Color;

namespace SharpEngine.Core.Manager;

/// <summary>
/// Class which manage Shaders
/// </summary>
public class ShaderManager
{
    private readonly Dictionary<string, Shader> _shaders = [];

    /// <summary>
    /// Get All Shaders
    /// </summary>
    public List<Shader> Shaders => new(_shaders.Values);

    /// <summary>
    /// Define float value for shader
    /// </summary>
    /// <param name="name">Shader Name</param>
    /// <param name="uniform">Uniform Name</param>
    /// <param name="value">Value</param>
    public void SetShaderValue(string name, string uniform, float value) =>
        SetShaderValue(name, uniform, value, ShaderUniformDataType.SHADER_UNIFORM_FLOAT);

    /// <summary>
    /// Define vec2 value for shader
    /// </summary>
    /// <param name="name">Shader Name</param>
    /// <param name="uniform">Uniform Name</param>
    /// <param name="value">Value</param>
    public void SetShaderValue(string name, string uniform, Vec2 value) =>
        SetShaderValue(
            name,
            uniform,
            new Vector2(value.X, value.Y),
            ShaderUniformDataType.SHADER_UNIFORM_VEC2
        );

    /// <summary>
    /// Define color value for shader
    /// </summary>
    /// <param name="name">Shader Name</param>
    /// <param name="uniform">Uniform Name</param>
    /// <param name="value">Value</param>
    public void SetShaderValue(string name, string uniform, Color value) =>
        SetShaderValue(name, uniform, value.ToVec4(), ShaderUniformDataType.SHADER_UNIFORM_VEC4);

    /// <summary>
    /// Define int value for shader
    /// </summary>
    /// <param name="name">Shader Name</param>
    /// <param name="uniform">Uniform Name</param>
    /// <param name="value">Value</param>
    public void SetShaderValue(string name, string uniform, int value) =>
        SetShaderValue(name, uniform, value, ShaderUniformDataType.SHADER_UNIFORM_FLOAT);

    /// <summary>
    /// Define value for shader
    /// </summary>
    /// <param name="name">Shader Name</param>
    /// <param name="uniform">Uniform Name</param>
    /// <param name="value">Value</param>
    /// <param name="uniformType">Uniform Type</param>
    /// <typeparam name="T">Value Type</typeparam>
    /// <exception cref="Exception">Throw if shader not found</exception>
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
                shader,
                Raylib.GetShaderLocation(shader, uniform),
                value,
                uniformType
            );
        else
            throw new Exception($"Shader not found : {name}");
    }

    /// <summary>
    /// Add shader to manager
    /// </summary>
    /// <param name="name">Shader Name</param>
    /// <param name="shader">Shader</param>
    public void AddShader(string name, Shader shader)
    {
        if (!_shaders.TryAdd(name, shader))
            DebugManager.Log(
                LogLevel.LogWarning,
                $"SE_SHADERMANAGER: Shader already exist : {name}"
            );
    }

    /// <summary>
    /// Add shader to manager
    /// </summary>
    /// <param name="name">Texture Name</param>
    /// <param name="vertexFile">Vertex Shader File</param>
    /// <param name="fragmentFile">Fragment Shader File</param>
    public void AddShader(string name, string vertexFile, string fragmentFile)
    {
        if (!_shaders.TryAdd(name, Raylib.LoadShader(vertexFile, fragmentFile)))
            DebugManager.Log(
                LogLevel.LogWarning,
                $"SE_SHADERMANAGER: Shader already exist : {name}"
            );
    }

    /// <summary>
    /// Add shader to manager
    /// </summary>
    /// <param name="name">Texture Name</param>
    /// <param name="vertexCode">Vertex Shader Code</param>
    /// <param name="fragmentCode">Fragment Shader Code</param>
    public void AddShaderFromCode(string name, string vertexCode, string fragmentCode)
    {
        if (!_shaders.TryAdd(name, Raylib.LoadShaderFromMemory(vertexCode, fragmentCode)))
            DebugManager.Log(
                LogLevel.LogWarning,
                $"SE_SHADERMANAGER: Shader already exist : {name}"
            );
    }

    /// <summary>
    /// Get Shader from Manager
    /// </summary>
    /// <param name="name">Shader Name</param>
    /// <returns>Shader</returns>
    /// <exception cref="Exception">Throws if shader not found</exception>
    public Shader GetShader(string name)
    {
        if (_shaders.TryGetValue(name, out var shader))
            return shader;
        DebugManager.Log(LogLevel.LogError, $"SE_SHADERMANAGER: Shader not found : {name}");
        throw new Exception($"Shader not found : {name}");
    }

    internal void Unload()
    {
        foreach (var shader in _shaders.Values)
            Raylib.UnloadShader(shader);
        _shaders.Clear();
    }
}
