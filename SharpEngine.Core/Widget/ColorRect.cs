using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which display colored Rect
/// </summary>
/// <param name="position">Color Rect Position</param>
/// <param name="size">Color Rect Size</param>
/// <param name="color">Color Rect Color</param>
/// <param name="rotation">Color Rect Rotation</param>
/// <param name="zLayer">Z Layer</param>
public class ColorRect(
    Vec2 position,
    Vec2? size = null,
    Color? color = null,
    int rotation = 0,
    int zLayer = 0
) : Widget(position, zLayer)
{
    /// <summary>
    /// Color of Rect
    /// </summary>
    public Color Color { get; set; } = color ?? Color.Black;

    /// <summary>
    /// Size of Rect
    /// </summary>
    public Vec2 Size { get; set; } = size ?? Vec2.One;

    /// <summary>
    /// Rotation of Rect
    /// </summary>
    public int Rotation { get; set; } = rotation;

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        if (!Displayed || Size == Vec2.Zero)
            return;

        SERender.DrawRectangle(
            new Rect(RealPosition.X, RealPosition.Y, Size.X, Size.Y),
            Size / 2,
            Rotation,
            Color,
            InstructionSource.UI,
            ZLayer
        );
    }
}
