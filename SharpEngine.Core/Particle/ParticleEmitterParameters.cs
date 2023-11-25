using SharpEngine.Core.Math;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Particle
{
    /// <summary>
    /// Struct which represents parameters to create Particle Emitter
    /// </summary>
    public struct ParticleEmitterParameters
    {
        /// <summary>
        /// Create Particle Emitter Parameters
        /// </summary>
        public ParticleEmitterParameters()
        {
        }

        /// <summary>
        /// BeginColors of Particle Emitter
        /// </summary>
        public Color[] BeginColors { get; set; } = [];

        /// <summary>
        /// EndColors of Particle Emitter
        /// </summary>
        public Color[]? EndColors { get; set; } = null;

        /// <summary>
        /// Offset of Particle Emitter
        /// </summary>
        public Vec2 Offset { get; set; } = Vec2.Zero;

        /// <summary>
        /// MinVelocity of Particle Emitter
        /// </summary>
        public float MinVelocity { get; set; } = 20;

        /// <summary>
        /// MaxVelocity of Particle Emitter
        /// </summary>
        public float MaxVelocity { get; set; } = 20;

        /// <summary>
        /// MinAcceleration of Particle Emitter
        /// </summary>
        public float MinAcceleration { get; set; } = 0;

        /// <summary>
        /// MaxAcceleration of Particle Emitter
        /// </summary>
        public float MaxAcceleration { get; set; } = 0;

        /// <summary>
        /// MinRotationSpeed of Particle Emitter
        /// </summary>
        public float MinRotationSpeed { get; set; } = 0;

        /// <summary>
        /// MaxRotationSpeed of Particle Emitter
        /// </summary>
        public float MaxRotationSpeed { get; set; } = 0;

        /// <summary>
        /// MinRotation of Particle Emitter
        /// </summary>
        public float MinRotation { get; set; } = 0;

        /// <summary>
        /// MaxRotation of Particle Emitter
        /// </summary>
        public float MaxRotation { get; set; } = 0;

        /// <summary>
        /// MinLifetime of Particle Emitter
        /// </summary>
        public float MinLifetime { get; set; } = 2;

        /// <summary>
        /// MaxLifetime of Particle Emitter
        /// </summary>
        public float MaxLifetime { get; set; } = 2;

        /// <summary>
        /// MinDirection of Particle Emitter
        /// </summary>
        public float MinDirection { get; set; } = 0;

        /// <summary>
        /// MaxDirection of Particle Emitter
        /// </summary>
        public float MaxDirection { get; set; } = 0;

        /// <summary>
        /// MinTimerBeforeSpawn of Particle Emitter
        /// </summary>
        public float MinTimerBeforeSpawn { get; set; } = 0.3f;

        /// <summary>
        /// MaxTimerBeforeSpawn of Particle Emitter
        /// </summary>
        public float MaxTimerBeforeSpawn { get; set; } = 0.3f;

        /// <summary>
        /// MinNbParticlesPerSpawn of Particle Emitter
        /// </summary>
        public int MinNbParticlesPerSpawn { get; set; } = 4;

        /// <summary>
        /// MaxNbParticlesPerSpawn of Particle Emitter
        /// </summary>
        public int MaxNbParticlesPerSpawn { get; set; } = 4;

        /// <summary>
        /// MinSize of Particle Emitter
        /// </summary>
        public float MinSize { get; set; } = 5;

        /// <summary>
        /// MaxSize of Particle Emitter
        /// </summary>
        public float MaxSize { get; set; } = 5;

        /// <summary>
        /// SizeFunction of Particle Emitter
        /// </summary>
        public ParticleParametersFunction SizeFunction { get; set; } = ParticleParametersFunction.Normal;

        /// <summary>
        /// SizeFunctionValue of Particle Emitter
        /// </summary>
        public float SizeFunctionValue { get; set; } = 0;

        /// <summary>
        /// SpawnSize of Particle Emitter
        /// </summary>
        public Vec2 SpawnSize { get; set; } = Vec2.Zero;

        /// <summary>
        /// MaxParticles of Particle Emitter
        /// </summary>
        public int MaxParticles { get; set; } = -1;

        /// <summary>
        /// Active of Particle Emitter
        /// </summary>
        public bool Active { get; set; } = false;

        /// <summary>
        /// ZLayer of Particle Emitter
        /// </summary>
        public int ZLayer { get; set; } = 0;
    }
}
