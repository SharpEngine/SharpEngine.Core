using System;
using JetBrains.Annotations;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using Color = SharpEngine.Core.Utils.Color;

namespace SharpEngine.Core.Particle;

/// <summary>
/// CLass which represents Particle
/// </summary>
/// <param name="parameters">Particle Parameters</param>
public class Particle(ParticleParameters parameters)
{
    /// <summary>
    /// Position of Particle
    /// </summary>
    [UsedImplicitly]
    public Vec2 Position { get; set; } = parameters.Position;

    /// <summary>
    /// Velocity of Particle
    /// </summary>
    [UsedImplicitly]
    public Vec2 Velocity { get; set; } = parameters.Velocity;

    /// <summary>
    /// Acceleration of Particle
    /// </summary>
    [UsedImplicitly]
    public Vec2 Acceleration { get; set; } = parameters.Acceleration;

    /// <summary>
    /// Lifetime of Particle
    /// </summary>
    [UsedImplicitly]
    public float Lifetime { get; set; } = parameters.Lifetime;

    /// <summary>
    /// Time Since Start of Particle
    /// </summary>
    [UsedImplicitly]
    public float TimeSinceStart { get; set; }

    /// <summary>
    /// Size of Particle
    /// </summary>
    [UsedImplicitly]
    public float Size { get; set; } = parameters.SizeFunction == ParticleParametersFunction.Increase ? 0 : parameters.Size;

    /// <summary>
    /// Max Size of Particle
    /// </summary>
    [UsedImplicitly]
    public float MaxSize { get; set; } = parameters.Size;

    /// <summary>
    /// Rotation of Particle
    /// </summary>
    [UsedImplicitly]
    public float Rotation { get; set; } = parameters.Rotation;

    /// <summary>
    /// Rotation Speed of Particle
    /// </summary>
    [UsedImplicitly]
    public float RotationSpeed { get; set; } = parameters.RotationSpeed;

    /// <summary>
    /// Begin Color of Particle
    /// </summary>
    [UsedImplicitly]
    public Color BeginColor { get; set; } = parameters.BeginColor;

    /// <summary>
    /// Current Color of Particle
    /// </summary>
    [UsedImplicitly]
    public Color CurrentColor { get; set; } = parameters.BeginColor;

    /// <summary>
    /// End Color of Particle
    /// </summary>
    [UsedImplicitly]
    public Color EndColor { get; set; } = parameters.EndColor;

    /// <summary>
    /// Size Function of Particle
    /// </summary>
    [UsedImplicitly]
    public ParticleParametersFunction SizeFunction { get; set; } = parameters.SizeFunction;

    /// <summary>
    /// Size Function Value of Particle
    /// </summary>
    [UsedImplicitly]
    public float SizeFunctionValue { get; set; } = parameters.SizeFunctionValue;

    /// <summary>
    /// ZLayer of Particle
    /// </summary>
    [UsedImplicitly]
    public int ZLayer { get; set; } = parameters.ZLayer;

    /// <summary>
    /// Update Particle
    /// </summary>
    /// <param name="delta">Frame Time</param>
    /// <exception cref="ArgumentException">throws if SizeFunction is unknown</exception>
    public void Update(float delta)
    {
        Velocity = new Vec2(
            Velocity.X + Acceleration.X * delta,
            Velocity.Y + Acceleration.Y * delta
        );
        Position = new Vec2(Position.X + Velocity.X * delta, Position.Y + Velocity.Y * delta);
        Rotation += RotationSpeed * delta;

        switch (SizeFunction)
        {
            case ParticleParametersFunction.Increase when SizeFunctionValue == 0:
                Size = MaxSize * TimeSinceStart / Lifetime;
                break;
            case ParticleParametersFunction.Increase:
                Size += SizeFunctionValue;
                break;
            case ParticleParametersFunction.Decrease when SizeFunctionValue == 0:
                Size = MaxSize * (Lifetime - TimeSinceStart) / Lifetime;
                break;
            case ParticleParametersFunction.Decrease:
                Size -= SizeFunctionValue;
                break;
            case ParticleParametersFunction.Normal:
                break;
            default:
                throw new ArgumentException("Unknown Size Function");
        }

        if (EndColor != BeginColor)
            CurrentColor = BeginColor.TranslateTo(EndColor, TimeSinceStart, Lifetime);
        TimeSinceStart += delta;
    }

    /// <summary>
    /// Draw Particle
    /// </summary>
    public void Draw()
    {
        if (Size == 0)
            return;

        SERender.DrawRectangle(
            new Rect(Position.X, Position.Y, Size, Size),
            new Vec2(Size / 2, Size / 2),
            Rotation,
            CurrentColor,
            InstructionSource.Entity,
            ZLayer
        );
    }
}
