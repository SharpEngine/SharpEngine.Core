using JetBrains.Annotations;
using Raylib_cs;

namespace SharpEngine.Core.Utils
{
    /// <summary>
    /// Base Class which represents Shader
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class SEShader
    {
        /// <summary>
        /// Internal Raylib Shader
        /// </summary>
        [UsedImplicitly]
        protected Shader InternalShader;

        /// <summary>
        /// Create Shader from Vertex and Fragment Shader
        /// </summary>
        /// <param name="vertexShader">Vertex Shader</param>
        /// <param name="fragmentShader">Fragment Shader</param>
        public SEShader(string? vertexShader, string? fragmentShader)
        {
            InternalShader = Raylib.LoadShaderFromMemory(vertexShader, fragmentShader);
        }

        /// <summary>
        /// Create Shader from Raylib Shader
        /// </summary>
        /// <param name="internalShader">Raylib Shader</param>
        public SEShader(Shader internalShader)
        {
            InternalShader = internalShader;
        }

        /// <summary>
        /// Get Internal Raylib Shader
        /// </summary>
        /// <returns>Raylib Shader</returns>
        public Shader GetInternalShader() => InternalShader;
    }
}
