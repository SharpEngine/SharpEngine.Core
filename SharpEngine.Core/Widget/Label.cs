using Raylib_cs;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Widget.Utils;
using Color = SharpEngine.Core.Utils.Color;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which display Label
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
public class Label(
    Vec2 position,
    string text = "",
    string font = "",
    Color? color = null,
    LabelStyles style = LabelStyles.None,
    int rotation = 0,
    bool centerAllLines = false,
    int? fontSize = null,
    int zLayer = 0
) : Widget(position, zLayer)
{
    /// <summary>
    /// Text of Label
    /// </summary>
    public string Text { get; set; } = text;

    /// <summary>
    /// Font of Label
    /// </summary>
    public string Font { get; set; } = font;

    /// <summary>
    /// Color of Label
    /// </summary>
    public Color Color { get; set; } = color ?? Color.Black;

    /// <summary>
    /// If Lines is Centered
    /// </summary>
    public bool CenterAllLines { get; set; } = centerAllLines;

    /// <summary>
    /// Rotation of Label
    /// </summary>
    public int Rotation { get; set; } = rotation;

    /// <summary>
    /// Font Size of Label (or Null)
    /// </summary>
    public int? FontSize { get; set; } = fontSize;

    /// <summary>
    /// Style of Label
    /// </summary>
    public LabelStyles Style { get; set; } = style;

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        var finalFont = Scene?.Window?.FontManager.GetFont(Font);

        if (!Displayed || Scene == null || Text.Length <= 0 || Font.Length <= 0 || finalFont == null)
            return;

        var finalFontSize = FontSize ?? finalFont.Value.BaseSize;

        var textSize = Raylib.MeasureTextEx(finalFont.Value, Text, finalFontSize, 2);

        var lines = Text.Split("\n");
        for (var i = 0; i < lines.Length; i++)
        {
            var lineSize = Raylib.MeasureTextEx(finalFont.Value, lines[i], finalFontSize, 2);
            var finalPosition = new Vec2(
                CenterAllLines ? RealPosition.X - lineSize.X / 2 : RealPosition.X - textSize.X / 2,
                RealPosition.Y - textSize.Y / 2 + i * lineSize.Y
            );
            SERender.DrawText(
                finalFont.Value,
                lines[i],
                finalPosition,
                Vec2.Zero,
                Rotation,
                finalFontSize,
                2,
                Color,
                InstructionSource.UI,
                ZLayer
            );

            if (Style.HasFlag(LabelStyles.Strike))
                SERender.DrawRectangle(
                    (int)finalPosition.X,
                    (int)(finalPosition.Y + lineSize.Y / 2),
                    (int)lineSize.X,
                    2,
                    Color.Black,
                    InstructionSource.UI,
                    ZLayer + 0.00001f
                );
            if (Style.HasFlag(LabelStyles.Underline))
                SERender.DrawRectangle(
                    (int)finalPosition.X,
                    (int)(finalPosition.Y + lineSize.Y),
                    (int)lineSize.X,
                    2,
                    Color.Black,
                    InstructionSource.UI,
                    ZLayer + 0.00001f
                );
        }
    }
}
