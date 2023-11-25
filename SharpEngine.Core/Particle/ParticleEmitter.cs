using System;
using System.Collections.Generic;
using SharpEngine.Core.Math;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Particle;

/// <summary>
/// Class which represents Particle Emitter
/// </summary>
/// <param name="parameters">Particle Emitter Parameters</param>
public class ParticleEmitter(ParticleEmitterParameters parameters)
{
    /// <summary>
    /// new() of Particle Emitter
    /// </summary>
    public List<Particle> Particles { get; } = [];

    /// <summary>
    /// BeginColors of Particle Emitter
    /// </summary>
    public Color[] BeginColors { get; set; } = parameters.BeginColors;

    /// <summary>
    /// EndColors of Particle Emitter
    /// </summary>
    public Color[]? EndColors { get; set; } = parameters.EndColors;

    /// <summary>
    /// Offset of Particle Emitter
    /// </summary>
    public Vec2 Offset { get; set; } = parameters.Offset;

    /// <summary>
    /// MinVelocity of Particle Emitter
    /// </summary>
    public float MinVelocity { get; set; } = parameters.MinVelocity;

    /// <summary>
    /// MaxVelocity of Particle Emitter
    /// </summary>
    public float MaxVelocity { get; set; } = parameters.MaxVelocity;

    /// <summary>
    /// MinAcceleration of Particle Emitter
    /// </summary>
    public float MinAcceleration { get; set; } = parameters.MinAcceleration;

    /// <summary>
    /// MaxAcceleration of Particle Emitter
    /// </summary>
    public float MaxAcceleration { get; set; } = parameters.MaxAcceleration;

    /// <summary>
    /// MinRotationSpeed of Particle Emitter
    /// </summary>
    public float MinRotationSpeed { get; set; } = parameters.MinRotationSpeed;

    /// <summary>
    /// MaxRotationSpeed of Particle Emitter
    /// </summary>
    public float MaxRotationSpeed { get; set; } = parameters.MaxRotationSpeed;

    /// <summary>
    /// MinRotation of Particle Emitter
    /// </summary>
    public float MinRotation { get; set; } = parameters.MinRotation;

    /// <summary>
    /// MaxRotation of Particle Emitter
    /// </summary>
    public float MaxRotation { get; set; } = parameters.MaxRotation;

    /// <summary>
    /// MinLifetime of Particle Emitter
    /// </summary>
    public float MinLifetime { get; set; } = parameters.MinLifetime;

    /// <summary>
    /// MaxLifetime of Particle Emitter
    /// </summary>
    public float MaxLifetime { get; set; } = parameters.MaxLifetime;

    /// <summary>
    /// MinDirection of Particle Emitter
    /// </summary>
    public float MinDirection { get; set; } = parameters.MinDirection;

    /// <summary>
    /// MaxDirection of Particle Emitter
    /// </summary>
    public float MaxDirection { get; set; } = parameters.MaxDirection;

    /// <summary>
    /// MinTimerBeforeSpawn of Particle Emitter
    /// </summary>
    public float MinTimerBeforeSpawn { get; set; } = parameters.MinTimerBeforeSpawn;

    /// <summary>
    /// MaxTimerBeforeSpawn of Particle Emitter
    /// </summary>
    public float MaxTimerBeforeSpawn { get; set; } = parameters.MaxTimerBeforeSpawn;

    /// <summary>
    /// MinNbParticlesPerSpawn of Particle Emitter
    /// </summary>
    public int MinNbParticlesPerSpawn { get; set; } = parameters.MinNbParticlesPerSpawn;

    /// <summary>
    /// MaxNbParticlesPerSpawn of Particle Emitter
    /// </summary>
    public int MaxNbParticlesPerSpawn { get; set; } = parameters.MaxNbParticlesPerSpawn;

    /// <summary>
    /// MinSize of Particle Emitter
    /// </summary>
    public float MinSize { get; set; } = parameters.MinSize;

    /// <summary>
    /// MaxSize of Particle Emitter
    /// </summary>
    public float MaxSize { get; set; } = parameters.MaxSize;

    /// <summary>
    /// SizeFunction of Particle Emitter
    /// </summary>
    public ParticleParametersFunction SizeFunction { get; set; } = parameters.SizeFunction;

    /// <summary>
    /// SizeFunctionValue of Particle Emitter
    /// </summary>
    public float SizeFunctionValue { get; set; } = parameters.SizeFunctionValue;

    /// <summary>
    /// SpawnSize of Particle Emitter
    /// </summary>
    public Vec2 SpawnSize { get; set; } = parameters.SpawnSize;

    /// <summary>
    /// MaxParticles of Particle Emitter
    /// </summary>
    public int MaxParticles { get; set; } = parameters.MaxParticles;

    /// <summary>
    /// Active of Particle Emitter
    /// </summary>
    public bool Active { get; set; } = parameters.Active;

    /// <summary>
    /// ZLayer of Particle Emitter
    /// </summary>
    public int ZLayer { get; set; } = parameters.ZLayer;

    private float _timerBeforeSpawn;

    /// <summary>
    /// Number of Particles
    /// </summary>
    public int ParticlesCount => Particles.Count;

    /// <summary>
    /// Spawn Particle
    /// </summary>
    /// <param name="objectPosition">Particle Position</param>
    public void SpawnParticle(Vec2 objectPosition)
    {
        Vec2 position;
        if (SpawnSize == Vec2.Zero)
            position = new Vec2(Offset.X + objectPosition.X, Offset.Y + objectPosition.Y);
        else
            position = new Vec2(
                Rand.GetRandF(-SpawnSize.X / 2, SpawnSize.X / 2) + Offset.X + objectPosition.X,
                Rand.GetRandF(-SpawnSize.Y / 2, SpawnSize.Y / 2) + Offset.Y + objectPosition.Y
            );
        var angle = Rand.GetRandF(MinDirection, MaxDirection);
        var velocity =
            new Vec2(MathF.Cos(MathHelper.ToRadians(angle)), MathF.Sin(MathHelper.ToRadians(angle)))
            * Rand.GetRandF(MinVelocity, MaxVelocity);
        var acceleration =
            new Vec2(MathF.Cos(MathHelper.ToRadians(angle)), MathF.Sin(MathHelper.ToRadians(angle)))
            * Rand.GetRandF(MinAcceleration, MaxAcceleration);
        var rotation = Rand.GetRandF(MinRotation, MaxRotation);
        var rotationSpeed = Rand.GetRandF(MinRotationSpeed, MaxRotationSpeed);
        var lifetime = Rand.GetRandF(MinLifetime, MaxLifetime);
        var size = Rand.GetRandF(MinSize, MaxSize);
        var beginColor = BeginColors[Rand.GetRand(0, BeginColors.Length - 1)];
        var endColor = beginColor;
        if (EndColors != null)
            endColor = EndColors[Rand.GetRand(0, EndColors.Length - 1)];

        var particle = new Particle(
            new ParticleParameters {
                Position = position,
                Velocity = velocity,
                Acceleration = acceleration,
                Lifetime = lifetime,
                Size = size,
                Rotation = rotation,
                RotationSpeed = rotationSpeed,
                BeginColor = beginColor,
                EndColor = endColor,
                SizeFunction = SizeFunction,
                SizeFunctionValue = SizeFunctionValue,
                ZLayer = ZLayer
            }
        );
        Particles.Add(particle);
    }

    /// <summary>
    /// Update Emitter
    /// </summary>
    /// <param name="delta">Frame Time</param>
    /// <param name="objectPosition">Emitter Position</param>
    public void Update(float delta, Vec2 objectPosition)
    {
        List<Particle> mustBeDeleted = [];
        foreach (var particle in Particles)
        {
            particle.Update(delta);
            if (particle.TimeSinceStart >= particle.Lifetime)
                mustBeDeleted.Add(particle);
        }

        foreach (var particle in mustBeDeleted)
            Particles.Remove(particle);

        mustBeDeleted.Clear();

        if (!Active)
            return;

        if (_timerBeforeSpawn <= 0)
        {
            if (MaxParticles == -1 || MaxParticles > Particles.Count)
            {
                var nbParticles = Rand.GetRand(MinNbParticlesPerSpawn, MaxNbParticlesPerSpawn);
                for (var i = 0; i < nbParticles; i++)
                    SpawnParticle(objectPosition);
            }

            _timerBeforeSpawn = Rand.GetRandF(MinTimerBeforeSpawn, MaxTimerBeforeSpawn);
        }

        _timerBeforeSpawn -= delta;
    }

    /// <summary>
    /// Draw Particles of Emitter
    /// </summary>
    public void Draw()
    {
        foreach (var particle in Particles)
            particle.Draw();
    }
}
