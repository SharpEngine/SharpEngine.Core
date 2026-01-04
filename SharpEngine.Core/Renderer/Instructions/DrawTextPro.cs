using Raylib_cs;
using SharpEngine.Core.Math;
using System;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws text with pro-parameters.
/// </summary>
internal record DrawTextPro : Instruction
{
    internal override void Execute()
    {
        base.Execute();
        Raylib.DrawTextPro(
            (Font)Parameters[0],
            Convert.ToString(Parameters[1]),
            (Vec2)Parameters[2],
            (Vec2)Parameters[3],
            Convert.ToSingle(Parameters[4]),
            Convert.ToInt32(Parameters[5]),
            Convert.ToInt32(Parameters[6]),
            (Utils.Color)Parameters[7]
        );
    }
}
