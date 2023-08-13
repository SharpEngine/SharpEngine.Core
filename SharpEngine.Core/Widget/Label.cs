using System.Numerics;
using Raylib_cs;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils.Widget;
using Color = SharpEngine.Core.Utils.Color;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which display Label
/// </summary>
public class Label: Widget
{
    /// <summary>
    /// Text of Label
    /// </summary>
    public string Text { get; set; }
    
    /// <summary>
    /// Font of Label
    /// </summary>
    public string Font { get; set; }
    
    /// <summary>
    /// Color of Label
    /// </summary>
    public Utils.Color Color { get; set; }
    
    /// <summary>
    /// If Lines is Centered
    /// </summary>
    public bool CenterAllLines { get; set; }
    
    /// <summary>
    /// Rotation of Label
    /// </summary>
    public int Rotation { get; set; }

    /// <summary>
    /// Font Size of Label (or Null)
    /// </summary>
    public int? FontSize { get; set; }

    /// <summary>
    /// Style of Label
    /// </summary>
    public LabelStyle Style { get; set; }

    /// <summary>
    /// Create Label
    /// </summary>
    /// <param name="position">Label Position</param>
    /// <param name="text">Label Text</param>
    /// <param name="font">Label Font</param>
    /// <param name="color">Label Color</param>
    /// <param name="style">Label Style</param>
    /// <param name="rotation">Label Rotation</param>
    /// <param name="centerAllLines">If Label Lines is Centered</param>
    /// <param name="fontSize">Label Font Size (or null)</param>
    /// <param name="zLayer">Z Layer</param>
    public Label(Vec2 position, string text = "", string font = "", Utils.Color? color = null,
        LabelStyle style = LabelStyle.None, int rotation = 0, bool centerAllLines = false,
        int? fontSize = null, int zLayer = 0) : base(position, zLayer)
    {
        Text = text;
        Font = font;
        Color = color ?? Utils.Color.Black;
        Rotation = rotation;
        CenterAllLines = centerAllLines;
        FontSize = fontSize;
        Style = style;
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();
        
        var font = Scene?.Window?.FontManager.GetFont(Font);
        
        if(!Displayed || Scene == null || Text.Length <= 0 || Font.Length <= 0 || font == null) return;

        var realPosition = RealPosition;
        var fontSize = FontSize ?? font.Value.baseSize;
        
        var textSize = Raylib.MeasureTextEx(font.Value, Text, fontSize, 2);

        var lines = Text.Split("\n");
        for (var i = 0; i < lines.Length; i++)
        {
            var lineSize = Raylib.MeasureTextEx(font.Value, lines[i], fontSize, 2);
            var finalPosition = new Vec2(
                CenterAllLines ? realPosition.X - lineSize.X / 2 : realPosition.X - textSize.X / 2,
                realPosition.Y - textSize.Y / 2 + i * lineSize.Y);
            SERender.DrawText(font.Value, lines[i], finalPosition, Vec2.Zero, Rotation, fontSize, 2, Color,
                InstructionSource.UI, ZLayer);

            if (Style.HasFlag(LabelStyle.Strike))
                SERender.DrawRectangle((int)finalPosition.X, (int)(finalPosition.Y + lineSize.Y / 2), (int)lineSize.X,
                    2, Utils.Color.Black, InstructionSource.UI, ZLayer + 0.00001f);
            if (Style.HasFlag(LabelStyle.Underline))
                SERender.DrawRectangle((int)finalPosition.X, (int)(finalPosition.Y + lineSize.Y), (int)lineSize.X, 2,
                    Utils.Color.Black, InstructionSource.UI, ZLayer + 0.00001f);
        }
    }
}