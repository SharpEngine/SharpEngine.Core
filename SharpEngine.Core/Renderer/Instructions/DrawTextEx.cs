using Raylib_cs;
using SharpEngine.Core.Math;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws text with extended parameters.
/// </summary>
internal record class DrawTextEx : Instruction
{
    internal override void Execute()
    {
        base.Execute();
        Raylib.DrawTextEx(
            (Font)Parameters[0],
            (string)Parameters[1],
            (Vec2)Parameters[2],
            (int)Parameters[3],
            (int)Parameters[4],
            (Utils.Color)Parameters[5]
        );
    }
}
