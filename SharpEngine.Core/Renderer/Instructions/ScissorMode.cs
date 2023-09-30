using System.Linq;
using Raylib_cs;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws in scissor mode.
/// </summary>
internal record class ScissorMode : Instruction
{
    internal override void Execute()
    {
        base.Execute();
        Raylib.BeginScissorMode(
            (int)Parameters[0],
            (int)Parameters[1],
            (int)Parameters[2],
            (int)Parameters[3]
        );
        SERender.DrawInstructions(
            Parameters.GetRange(4, Parameters.Count - 4).Select(x => (Instruction)x).ToList()
        );
        Raylib.EndScissorMode();
    }
}
