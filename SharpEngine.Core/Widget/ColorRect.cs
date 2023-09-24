using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which display colored Rect
/// </summary>
public class ColorRect : Widget
{
    /// <summary>
    /// Color of Rect
    /// </summary>
    public Color Color { get; set; }

    /// <summary>
    /// Size of Rect
    /// </summary>
    public Vec2 Size { get; set; }

    /// <summary>
    /// Rotation of Rect
    /// </summary>
    public int Rotation { get; set; }

    /// <summary>
    /// Create ColorRect
    /// </summary>
    /// <param name="position">Color Rect Position</param>
    /// <param name="size">Color Rect Size</param>
    /// <param name="color">Color Rect Color</param>
    /// <param name="rotation">Color Rect Rotation</param>
    /// <param name="zLayer">Z Layer</param>
    public ColorRect(
        Vec2 position,
        Vec2? size = null,
        Color? color = null,
        int rotation = 0,
        int zLayer = 0
    ) : base(position, zLayer)
    {
        Color = color ?? Color.Black;
        Size = size ?? Vec2.One;
        Rotation = rotation;
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        if (!Displayed || Size == Vec2.Zero)
            return;

        var position = RealPosition;
        SERender.DrawRectangle(
            new Rect(position.X, position.Y, Size.X, Size.Y),
            Size / 2,
            Rotation,
            Color,
            InstructionSource.UI,
            ZLayer
        );
    }
}
