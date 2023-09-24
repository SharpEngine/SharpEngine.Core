using System;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using Color = SharpEngine.Core.Utils.Color;

namespace SharpEngine.Core.Particle;

/// <summary>
/// CLass which represents Particle
/// </summary>
public class Particle
{
    /// <summary>
    /// Position of Particle
    /// </summary>
    public Vec2 Position { get; set; }

    /// <summary>
    /// Velocity of Particle
    /// </summary>
    public Vec2 Velocity { get; set; }

    /// <summary>
    /// Acceleration of Particle
    /// </summary>
    public Vec2 Acceleration { get; set; }

    /// <summary>
    /// Lifetime of Particle
    /// </summary>
    public float Lifetime { get; set; }

    /// <summary>
    /// Time Since Start of Particle
    /// </summary>
    public float TimeSinceStart { get; set; }

    /// <summary>
    /// Size of Particle
    /// </summary>
    public float Size { get; set; }

    /// <summary>
    /// Max Size of Particle
    /// </summary>
    public float MaxSize { get; set; }

    /// <summary>
    /// Rotation of Particle
    /// </summary>
    public float Rotation { get; set; }

    /// <summary>
    /// Rotation Speed of Particle
    /// </summary>
    public float RotationSpeed { get; set; }

    /// <summary>
    /// Begin Color of Particle
    /// </summary>
    public Color BeginColor { get; set; }

    /// <summary>
    /// Current Color of Particle
    /// </summary>
    public Color CurrentColor { get; set; }

    /// <summary>
    /// End Color of Particle
    /// </summary>
    public Color EndColor { get; set; }

    /// <summary>
    /// Size Function of Particle
    /// </summary>
    public ParticleParametersFunction SizeFunction { get; set; }

    /// <summary>
    /// Size Function Value of Particle
    /// </summary>
    public float SizeFunctionValue { get; set; }

    /// <summary>
    /// ZLayer of Particle
    /// </summary>
    public int ZLayer { get; set; }

    /// <summary>
    /// Create Particle
    /// </summary>
    /// <param name="position">Particle Position</param>
    /// <param name="velocity">Particle Velocity</param>
    /// <param name="acceleration">Particle Acceleration</param>
    /// <param name="lifetime">Particle Lifetime</param>
    /// <param name="size">Particle Size</param>
    /// <param name="rotation">Particle Rotation</param>
    /// <param name="rotationSpeed">Particle Rotation Speed</param>
    /// <param name="beginColor">Particle Begin Color</param>
    /// <param name="endColor">Particle End Color</param>
    /// <param name="sizeFunction">Particle Size Function</param>
    /// <param name="sizeFunctionValue">Particle Size Function Value</param>
    /// <param name="zLayer">ZLayer</param>
    public Particle(
        Vec2 position,
        Vec2 velocity,
        Vec2 acceleration,
        float lifetime,
        float size,
        float rotation,
        float rotationSpeed,
        Color beginColor,
        Color endColor,
        ParticleParametersFunction sizeFunction = ParticleParametersFunction.Normal,
        float sizeFunctionValue = 0,
        int zLayer = 0
    )
    {
        Position = position;
        Velocity = velocity;
        Acceleration = acceleration;
        Lifetime = lifetime;
        TimeSinceStart = 0;
        MaxSize = size;
        if (sizeFunction == ParticleParametersFunction.Increase)
            Size = 0;
        else
            Size = size;
        Rotation = rotation;
        RotationSpeed = rotationSpeed;
        BeginColor = beginColor;
        CurrentColor = beginColor;
        EndColor = endColor;
        SizeFunction = sizeFunction;
        SizeFunctionValue = sizeFunctionValue;
        ZLayer = zLayer;
    }

    /// <summary>
    /// Update Particle
    /// </summary>
    /// <param name="delta">Frame Time</param>
    /// <exception cref="ArgumentOutOfRangeException">throws if SizeFunctions is out of range</exception>
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
                throw new ArgumentOutOfRangeException(nameof(SizeFunction), SizeFunction, null);
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
