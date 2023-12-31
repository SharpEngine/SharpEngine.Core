﻿using System;
using System.Numerics;
using Raylib_cs;
using SharpEngine.Core.Component;
using SharpEngine.Core.Math;

namespace SharpEngine.Core.Manager;

/// <summary>
/// Class which manager Camera information
/// </summary>
public class CameraManager
{
    /// <summary>
    /// Entity followed on mode Follow and FollowSmooth
    /// <seealso cref="Mode"/>
    /// </summary>
    public Entity.Entity? FollowEntity { get; set; }

    /// <summary>
    /// Camera Mode
    /// </summary>
    public Utils.CameraMode Mode { get; set; }

    /// <summary>
    /// Minimum Speed used when mode is FollowSmooth
    /// </summary>
    public float MinSpeed { get; set; } = 30;

    /// <summary>
    /// Minimum Effect Length used when mode is FollowSmooth
    /// </summary>
    public float MinEffectLength { get; set; } = 10;

    /// <summary>
    /// Fraction Speed used when mode is FollowSmooth
    /// </summary>
    public float FractionSpeed { get; set; } = 0.8f;

    /// <summary>
    /// Rotation of Camera
    /// </summary>
    public float Rotation
    {
        get => Camera2D.Rotation;
        set => Camera2D.Rotation = value;
    }

    /// <summary>
    /// Zoom of Camera
    /// </summary>
    public float Zoom
    {
        get => Camera2D.Zoom;
        set => Camera2D.Zoom = value;
    }

    /// <summary>
    /// Position of Camera
    /// </summary>
    public Vec2 Position
    {
        get => Camera2D.Target;
        set => Camera2D.Target = value;
    }

    internal Camera2D Camera2D;

    /// <summary>
    /// Create Camera Manager
    /// </summary>
    public CameraManager()
    {
        Camera2D = new Camera2D(Vector2.Zero, Vector2.Zero, 0, 1);
        Mode = Utils.CameraMode.Basic;
    }

    internal void SetScreenSize(Vec2 screenSize)
    {
        Camera2D.Offset = screenSize / 2;
        if (Mode == Utils.CameraMode.Basic)
            Camera2D.Target = Camera2D.Offset;
    }

    /// <summary>
    /// Update Camera Manager
    /// </summary>
    /// <param name="delta">Delta frame</param>
    public void Update(float delta)
    {
        switch (Mode)
        {
            case Utils.CameraMode.Follow:
                if (FollowEntity?.GetComponentAs<TransformComponent>() is { } transform)
                    Camera2D.Target = transform.Position;
                break;
            case Utils.CameraMode.FollowSmooth:
                if (FollowEntity?.GetComponentAs<TransformComponent>() is { } transformSmooth)
                {
                    var diff = transformSmooth.Position - (Vec2)Camera2D.Target;
                    var length = diff.Length();
                    if (length > MinEffectLength)
                    {
                        var speed = MathF.Max(FractionSpeed * length, MinSpeed);
                        Camera2D.Target += new Vector2(
                            diff.X * (speed * delta / length),
                            diff.Y * (speed * delta / length)
                        );
                    }
                }
                break;
        }
    }
}
