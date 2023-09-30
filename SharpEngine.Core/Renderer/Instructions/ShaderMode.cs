using System.Linq;
using Raylib_cs;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws in shader mode.
/// </summary>
internal record class ShaderMode : Instruction
{
    /// <inheritdoc/>
    internal override void Execute()
    {
        base.Execute();
        Raylib.BeginShaderMode((Shader)Parameters[0]);
        SERender.DrawInstructions(
            Parameters.GetRange(1, Parameters.Count - 1).Select(x => (Instruction)x).ToList()
        );
        Raylib.EndShaderMode();
    }
}
