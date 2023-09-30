using Raylib_cs;
using SharpEngine.Core.Math;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws a rectangle with pro parameters.
/// </summary>
internal record class DrawRectanglePro : Instruction
{
    internal override void Execute()
    {
        base.Execute();
        Raylib.DrawRectanglePro(
            (Rect)Parameters[0],
            (Vec2)Parameters[1],
            (float)Parameters[2],
            (Utils.Color)Parameters[3]
        );
    }
}
