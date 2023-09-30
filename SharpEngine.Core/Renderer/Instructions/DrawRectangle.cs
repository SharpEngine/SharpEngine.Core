using Raylib_cs;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws a rectangle.
/// </summary>
internal record class DrawRectangle : Instruction
{
    internal override void Execute()
    {
        base.Execute();
        Raylib.DrawRectangle(
            (int)Parameters[0],
            (int)Parameters[1],
            (int)Parameters[2],
            (int)Parameters[3],
            (Utils.Color)Parameters[4]
        );
    }
}
