using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEngine.Core.Utils
{
    /// <summary>
    /// Base Class which represents Shader
    /// </summary>
    public class SEShader
    {
        /// <summary>
        /// Internal Raylib Shader
        /// </summary>
        protected Shader internalShader;

        /// <summary>
        /// Create Shader from Vertex and Fragment Shader
        /// </summary>
        /// <param name="vertexShader">Vertex Shader</param>
        /// <param name="fragmentShader">Fragment Shader</param>
        public SEShader(string? vertexShader, string? fragmentShader)
        {
            internalShader = Raylib.LoadShaderFromMemory(vertexShader, fragmentShader);
        }

        /// <summary>
        /// Create Shader from Raylib Shader
        /// </summary>
        /// <param name="internalShader">Raylib Shader</param>
        public SEShader(Shader internalShader)
        {
            this.internalShader = internalShader;
        }

        /// <summary>
        /// Get Internal Raylib Shader
        /// </summary>
        /// <returns>Raylib Shader</returns>
        public Shader GetInternalShader() => internalShader;
    }
}
