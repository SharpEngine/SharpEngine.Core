using Raylib_cs;
using SharpEngine.Core.Math;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws rectangle lines extended.
/// </summary>
internal record class DrawRectangleLinesEx : Instruction
{
    internal override void Execute()
    {
        base.Execute();
        Raylib.DrawRectangleLinesEx(
            (Rect)Parameters[0],
            (int)Parameters[1],
            (Utils.Color)Parameters[2]
        );
    }
}
