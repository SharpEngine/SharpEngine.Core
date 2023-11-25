using System;
using System.Collections.Generic;
using SharpEngine.Core.Math;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Particle;

/// <summary>
/// Class which represents Particle Emitter
/// </summary>
/// <param name="beginColors">Particle Emitter BeginColors</param>
/// <param name="endColors">Particle Emitter EndColors</param>
/// <param name="spawnSize">Particle Emitter SpawnSize</param>
/// <param name="offset">Particle Emitter Offset</param>
/// <param name="minVelocity">Particle Emitter MinVelocity</param>
/// <param name="maxVelocity">Particle Emitter MaxVelocity</param>
/// <param name="minAcceleration">Particle Emitter MinAcceleration</param>
/// <param name="maxAcceleration">Particle Emitter MaxAcceleration</param>
/// <param name="minRotationSpeed">Particle Emitter MinRotationSpeed</param>
/// <param name="maxRotationSpeed">Particle Emitter MaxRotationSpeed</param>
/// <param name="minRotation">Particle Emitter MinRotation</param>
/// <param name="maxRotation">Particle Emitter MaxRotation</param>
/// <param name="minLifetime">Particle Emitter MinLifetime</param>
/// <param name="maxLifetime">Particle Emitter MaxLifetime</param>
/// <param name="minDirection">Particle Emitter MinDirection</param>
/// <param name="maxDirection">Particle Emitter MaxDirection</param>
/// <param name="minTimerBeforeSpawn">Particle Emitter MinTimerBeforeSpawn</param>
/// <param name="maxTimerBeforeSpawn">Particle Emitter MaxTimerBeforeSpawn</param>
/// <param name="minSize">Particle Emitter MinSize</param>
/// <param name="maxSize">Particle Emitter MaxSize</param>
/// <param name="minNbParticlesPerSpawn">Particle Emitter MinNbParticlesPerSpawn</param>
/// <param name="maxNbParticlesPerSpawn">Particle Emitter MaxNbParticlesPerSpawn</param>
/// <param name="maxParticles">Particle Emitter MaxParticles</param>
/// <param name="active">Particle Emitter Active</param>
/// <param name="sizeFunction">Particle Emitter SizeFunction</param>
/// <param name="sizeFunctionValue">Particle Emitter SizeFunctionValue</param>
/// <param name="zLayer">Particle Emitter ZLayer</param>
public class ParticleEmitter(
    Color[] beginColors,
    Color[]? endColors = null,
    Vec2? spawnSize = null,
    Vec2? offset = null,
    float minVelocity = 20,
    float maxVelocity = 20,
    float minAcceleration = 0,
    float maxAcceleration = 0,
    float minRotationSpeed = 0,
    float maxRotationSpeed = 0,
    float minRotation = 0,
    float maxRotation = 0,
    float minLifetime = 2,
    float maxLifetime = 2,
    float minDirection = 0,
    float maxDirection = 0,
    float minTimerBeforeSpawn = 0.3f,
    float maxTimerBeforeSpawn = 0.3f,
    float minSize = 5,
    float maxSize = 5,
    int minNbParticlesPerSpawn = 4,
    int maxNbParticlesPerSpawn = 4,
    int maxParticles = -1,
    bool active = false,
    ParticleParametersFunction sizeFunction = ParticleParametersFunction.Normal,
    float sizeFunctionValue = 0,
    int zLayer = 0
    )
{
    /// <summary>
    /// new() of Particle Emitter
    /// </summary>
    public List<Particle> Particles { get; } = [];

    /// <summary>
    /// BeginColors of Particle Emitter
    /// </summary>
    public Color[] BeginColors { get; set; } = beginColors;

    /// <summary>
    /// EndColors of Particle Emitter
    /// </summary>
    public Color[]? EndColors { get; set; } = endColors;

    /// <summary>
    /// Offset of Particle Emitter
    /// </summary>
    public Vec2 Offset { get; set; } = offset ?? Vec2.Zero;

    /// <summary>
    /// MinVelocity of Particle Emitter
    /// </summary>
    public float MinVelocity { get; set; } = minVelocity;

    /// <summary>
    /// MaxVelocity of Particle Emitter
    /// </summary>
    public float MaxVelocity { get; set; } = maxVelocity;

    /// <summary>
    /// MinAcceleration of Particle Emitter
    /// </summary>
    public float MinAcceleration { get; set; } = minAcceleration;

    /// <summary>
    /// MaxAcceleration of Particle Emitter
    /// </summary>
    public float MaxAcceleration { get; set; } = maxAcceleration;

    /// <summary>
    /// MinRotationSpeed of Particle Emitter
    /// </summary>
    public float MinRotationSpeed { get; set; } = minRotationSpeed;

    /// <summary>
    /// MaxRotationSpeed of Particle Emitter
    /// </summary>
    public float MaxRotationSpeed { get; set; } = maxRotationSpeed;

    /// <summary>
    /// MinRotation of Particle Emitter
    /// </summary>
    public float MinRotation { get; set; } = minRotation;

    /// <summary>
    /// MaxRotation of Particle Emitter
    /// </summary>
    public float MaxRotation { get; set; } = maxRotation;

    /// <summary>
    /// MinLifetime of Particle Emitter
    /// </summary>
    public float MinLifetime { get; set; } = minLifetime;

    /// <summary>
    /// MaxLifetime of Particle Emitter
    /// </summary>
    public float MaxLifetime { get; set; } = maxLifetime;

    /// <summary>
    /// MinDirection of Particle Emitter
    /// </summary>
    public float MinDirection { get; set; } = minDirection;

    /// <summary>
    /// MaxDirection of Particle Emitter
    /// </summary>
    public float MaxDirection { get; set; } = maxDirection;

    /// <summary>
    /// MinTimerBeforeSpawn of Particle Emitter
    /// </summary>
    public float MinTimerBeforeSpawn { get; set; } = minTimerBeforeSpawn;

    /// <summary>
    /// MaxTimerBeforeSpawn of Particle Emitter
    /// </summary>
    public float MaxTimerBeforeSpawn { get; set; } = maxTimerBeforeSpawn;

    /// <summary>
    /// MinNbParticlesPerSpawn of Particle Emitter
    /// </summary>
    public int MinNbParticlesPerSpawn { get; set; } = minNbParticlesPerSpawn;

    /// <summary>
    /// MaxNbParticlesPerSpawn of Particle Emitter
    /// </summary>
    public int MaxNbParticlesPerSpawn { get; set; } = maxNbParticlesPerSpawn;

    /// <summary>
    /// MinSize of Particle Emitter
    /// </summary>
    public float MinSize { get; set; } = minSize;

    /// <summary>
    /// MaxSize of Particle Emitter
    /// </summary>
    public float MaxSize { get; set; } = maxSize;

    /// <summary>
    /// SizeFunction of Particle Emitter
    /// </summary>
    public ParticleParametersFunction SizeFunction { get; set; } = sizeFunction;

    /// <summary>
    /// SizeFunctionValue of Particle Emitter
    /// </summary>
    public float SizeFunctionValue { get; set; } = sizeFunctionValue;

    /// <summary>
    /// SpawnSize of Particle Emitter
    /// </summary>
    public Vec2 SpawnSize { get; set; } = spawnSize ?? Vec2.Zero;

    /// <summary>
    /// MaxParticles of Particle Emitter
    /// </summary>
    public int MaxParticles { get; set; } = maxParticles;

    /// <summary>
    /// Active of Particle Emitter
    /// </summary>
    public bool Active { get; set; } = active;

    /// <summary>
    /// ZLayer of Particle Emitter
    /// </summary>
    public int ZLayer { get; set; } = zLayer;

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

        var particle = Particle.FromParameters(
            position,
            velocity,
            acceleration,
            lifetime,
            size,
            rotation,
            rotationSpeed,
            beginColor,
            endColor,
            SizeFunction,
            SizeFunctionValue,
            ZLayer
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
