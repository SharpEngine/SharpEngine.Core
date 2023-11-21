using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which display frame
/// </summary>
/// <param name="position">Frame Position</param>
/// <param name="size">Frame Size</param>
/// <param name="borderSize">Frame Border Size (3)</param>
/// <param name="borderColor">Frame Border Color (Color.Black)</param>
/// <param name="backgroundColor">Frame Background Color (null)</param>
/// <param name="zLayer">Z Layer</param>
public class Frame(
    Vec2 position,
    Vec2 size,
    int borderSize = 3,
    Color? borderColor = null,
    Color? backgroundColor = null,
    int zLayer = 0
) : Widget(position, zLayer)
{
    /// <summary>
    /// Color of Frame Border
    /// </summary>
    public Color BorderColor { get; set; } = borderColor ?? Color.Black;

    /// <summary>
    /// Size of Frame
    /// </summary>
    public Vec2 Size { get; set; } = size;

    /// <summary>
    /// Size of Frame Border
    /// </summary>
    public int BorderSize { get; set; } = borderSize;

    /// <summary>
    /// Color of Frame Background
    /// </summary>
    public Color? BackgroundColor { get; set; } = backgroundColor;

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        if (!Displayed || Size == Vec2.Zero)
            return;

        var position = RealPosition;
        if (BackgroundColor != null)
            SERender.DrawRectangle(
                new Rect(position.X, position.Y, Size.X, Size.Y),
                Size / 2,
                0,
                BackgroundColor.Value,
                InstructionSource.UI,
                ZLayer
            );
        SERender.DrawRectangleLines(
            new Rect(position.X - Size.X / 2, position.Y - Size.Y / 2, Size.X, Size.Y),
            BorderSize,
            BorderColor,
            InstructionSource.UI,
            ZLayer + 0.00001f
        );
    }
}
