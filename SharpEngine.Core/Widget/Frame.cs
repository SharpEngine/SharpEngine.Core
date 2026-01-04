using JetBrains.Annotations;
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
    [UsedImplicitly]
    public Color BorderColor { get; set; } = borderColor ?? Color.Black;

    /// <summary>
    /// Size of Frame
    /// </summary>
    [UsedImplicitly]
    public Vec2 Size { get; set; } = size;

    /// <summary>
    /// Size of Frame Border
    /// </summary>
    [UsedImplicitly]
    public int BorderSize { get; set; } = borderSize;

    /// <summary>
    /// Color of Frame Background
    /// </summary>
    [UsedImplicitly]
    public Color? BackgroundColor { get; set; } = backgroundColor;

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        if (!Displayed || Size == Vec2.Zero)
            return;

        if (BackgroundColor != null)
            SERender.DrawRectangle(
                new Rect(RealPosition.X, RealPosition.Y, Size.X, Size.Y),
                Size / 2,
                0,
                BackgroundColor.Value,
                InstructionSource.Ui,
                ZLayer
            );
        SERender.DrawRectangleLines(
            new Rect(RealPosition.X - Size.X / 2, RealPosition.Y - Size.Y / 2, Size.X, Size.Y),
            BorderSize,
            BorderColor,
            InstructionSource.Ui,
            ZLayer + 0.00001f
        );
    }
}
