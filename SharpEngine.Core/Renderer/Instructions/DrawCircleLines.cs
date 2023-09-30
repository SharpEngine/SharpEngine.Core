using Raylib_cs;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws circle lines.
/// </summary>
internal record class DrawCircleLines : Instruction
{
    internal override void Execute()
    {
        base.Execute();
        Raylib.DrawCircleLines(
            (int)Parameters[0],
            (int)Parameters[1],
            (float)Parameters[2],
            (Utils.Color)Parameters[3]
        );
    }
}
