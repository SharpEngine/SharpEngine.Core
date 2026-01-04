using JetBrains.Annotations;
using SharpEngine.Core.Math;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Particle;

/// <summary>
/// Struct which represents parameters to create Particle
/// </summary>
public struct ParticleParameters
{
    /// <summary>
    /// Position of Particle
    /// </summary>
    [UsedImplicitly]
    public Vec2 Position { get; set; }

    /// <summary>
    /// Velocity of Particle
    /// </summary>
    [UsedImplicitly]
    public Vec2 Velocity { get; set; }

    /// <summary>
    /// Acceleration of Particle
    /// </summary>
    [UsedImplicitly]
    public Vec2 Acceleration { get; set; }

    /// <summary>
    /// Lifetime of Particle
    /// </summary>
    [UsedImplicitly]
    public float Lifetime { get; set; }

    /// <summary>
    /// Size of Particle
    /// </summary>
    [UsedImplicitly]
    public float Size { get; set; }

    /// <summary>
    /// Rotation of Particle
    /// </summary>
    [UsedImplicitly]
    public float Rotation { get; set; }

    /// <summary>
    /// Rotation Speed of Particle
    /// </summary>
    [UsedImplicitly]
    public float RotationSpeed { get; set; }

    /// <summary>
    /// Begin Color of Particle
    /// </summary>
    [UsedImplicitly]
    public Color BeginColor { get; set; }

    /// <summary>
    /// End Color of Particle
    /// </summary>
    [UsedImplicitly]
    public Color EndColor { get; set; }

    /// <summary>
    /// Size Function of Particle
    /// </summary>
    [UsedImplicitly]
    public ParticleParametersFunction SizeFunction { get; set; }

    /// <summary>
    /// Size Function Value of Particle
    /// </summary>
    [UsedImplicitly]
    public float SizeFunctionValue { get; set; }

    /// <summary>
    /// ZLayer of Particle
    /// </summary>
    [UsedImplicitly]
    public int ZLayer { get; set; }
}
