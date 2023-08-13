using Raylib_cs;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using Color = SharpEngine.Core.Utils.Color;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which display frame
/// </summary>
public class Frame: Widget
{
    /// <summary>
    /// Color of Frame Border
    /// </summary>
    public Utils.Color BorderColor { get; set; }
    
    /// <summary>
    /// Size of Frame
    /// </summary>
    public Vec2 Size { get; set; }
    
    /// <summary>
    /// Size of Frame Border 
    /// </summary>
    public int BorderSize { get; set; }
    
    /// <summary>
    /// Color of Frame Background
    /// </summary>
    public Utils.Color? BackgroundColor { get; set; }

    /// <summary>
    /// Create Frame
    /// </summary>
    /// <param name="position">Frame Position</param>
    /// <param name="size">Frame Size</param>
    /// <param name="borderSize">Frame Border Size (3)</param>
    /// <param name="borderColor">Frame Border Color (Color.Black)</param>
    /// <param name="backgroundColor">Frame Background Color (null)</param>
    /// <param name="zLayer">Z Layer</param>
    public Frame(Vec2 position, Vec2 size, int borderSize = 3, Utils.Color? borderColor = null,
        Utils.Color? backgroundColor = null, int zLayer = 0) : base(position, zLayer)
    {
        BorderColor = borderColor ?? Utils.Color.Black;
        Size = size;
        BorderSize = borderSize;
        BackgroundColor = backgroundColor;
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();
        
        if(!Displayed || Size == Vec2.Zero) return;

        var position = RealPosition;
        if (BackgroundColor != null)
            SERender.DrawRectangle(new Rect(position.X, position.Y, Size.X, Size.Y), Size / 2, 0, BackgroundColor.Value,
                InstructionSource.UI, ZLayer);
        SERender.DrawRectangleLines(new Rect(position.X - Size.X / 2, position.Y - Size.Y / 2, Size.X, Size.Y),
            BorderSize, BorderColor, InstructionSource.UI, ZLayer + 0.00001f);
    }
}