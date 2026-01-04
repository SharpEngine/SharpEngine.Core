using Raylib_cs;
using System;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws a rectangle.
/// </summary>
internal record DrawRectangle : Instruction
{
    internal override void Execute()
    {
        base.Execute();
        Raylib.DrawRectangle(
            Convert.ToInt32(Parameters[0]), // X
            Convert.ToInt32(Parameters[1]), // Y
            Convert.ToInt32(Parameters[2]), // Width
            Convert.ToInt32(Parameters[3]), // Height
            (Utils.Color)Parameters[4]
        );
    }
}
