using Raylib_cs;
using SharpEngine.Core.Math;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws a texture with pro parameters.
/// </summary>
internal record class DrawTexturePro : Instruction
{
    internal override void Execute()
    {
        base.Execute();
        Raylib.DrawTexturePro(
            (Texture2D)Parameters[0],
            (Rect)Parameters[1],
            (Rect)Parameters[2],
            (Vec2)Parameters[3],
            (float)Parameters[4],
            (Utils.Color)Parameters[5]
        );
    }
}
