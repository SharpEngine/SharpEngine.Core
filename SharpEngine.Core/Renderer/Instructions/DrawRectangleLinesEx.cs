using Raylib_cs;
using SharpEngine.Core.Math;
using System;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws rectangle lines extended.
/// </summary>
internal record DrawRectangleLinesEx : Instruction
{
    internal override void Execute()
    {
        base.Execute();
        Raylib.DrawRectangleLinesEx(
            (Rect)Parameters[0],
            Convert.ToInt32(Parameters[1]),
            (Utils.Color)Parameters[2]
        );
    }
}
