using SharpEngine.Core.Math;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Particle
{
    /// <summary>
    /// Struct which represents parameters to create Particle
    /// </summary>
    public struct ParticleParameters
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
        /// Size of Particle
        /// </summary>
        public float Size { get; set; }

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
    }
}
