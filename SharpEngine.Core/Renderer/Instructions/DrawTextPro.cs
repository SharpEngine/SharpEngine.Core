using Raylib_cs;
using SharpEngine.Core.Math;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws text with pro parameters.
/// </summary>
internal record class DrawTextPro : Instruction
{
    internal override void Execute()
    {
        base.Execute();
        Raylib.DrawTextPro(
            (Font)Parameters[0],
            (string)Parameters[1],
            (Vec2)Parameters[2],
            (Vec2)Parameters[3],
            (float)Parameters[4],
            (int)Parameters[5],
            (int)Parameters[6],
            (Utils.Color)Parameters[7]
        );
    }
}
