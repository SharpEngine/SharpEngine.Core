using Raylib_cs;
using SharpEngine.Core.Math;
using System;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws text with extended parameters.
/// </summary>
internal record DrawTextEx : Instruction
{
    internal override void Execute()
    {
        base.Execute();
        Raylib.DrawTextEx(
            (Font)Parameters[0],
            Convert.ToString(Parameters[1]),
            (Vec2)Parameters[2],
            Convert.ToInt32(Parameters[3]),
            Convert.ToInt32(Parameters[4]),
            (Utils.Color)Parameters[5]
        );
    }
}
