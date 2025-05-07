using System;
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
            Convert.ToInt32(Parameters[0]),
            Convert.ToInt32(Parameters[1]),
            Convert.ToInt32(Parameters[2]),
            Convert.ToInt32(Parameters[3])
        );
        SERender.DrawInstructions(
            Parameters.GetRange(4, Parameters.Count - 4).Select(x => (Instruction)x).ToList()
        );
        Raylib.EndScissorMode();
    }
}
