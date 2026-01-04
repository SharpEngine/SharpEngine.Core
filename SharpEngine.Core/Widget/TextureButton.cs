using JetBrains.Annotations;
using Raylib_cs;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using Color = SharpEngine.Core.Utils.Color;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which display Texture Button
/// </summary>
/// <param name="position">Texture Button Position</param>
/// <param name="text">Texture Button Text</param>
/// <param name="font">Texture Button Font</param>
/// <param name="texture">Texture Button Texture</param>
/// <param name="size">Texture Button Size</param>
/// <param name="fontColor">Texture Button Font Color</param>
/// <param name="fontSize">Texture Button Font Size</param>
/// <param name="zLayer">Z Layer</param>
[UsedImplicitly]
public class TextureButton(
    Vec2 position,
    string texture = "",
    string text = "",
    string font = "RAYLIB_DEFAULT",
    Vec2? size = null,
    Color? fontColor = null,
    int? fontSize = null,
    int zLayer = 0
) : Button(position, text, font, size, fontColor, null, fontSize, zLayer)
{
    /// <summary>
    /// Texture of Texture Button
    /// </summary>
    [UsedImplicitly]
    public string Texture { get; set; } = texture;

    /// <inheritdoc />
    public override void Update(float delta)
    {
        if (Size == Vec2.Zero)
        {
            var finalTexture = Scene?.Window?.TextureManager.GetTexture(Texture);
            if (finalTexture != null)
                Size = new Vec2(finalTexture.Value.Width, finalTexture.Value.Height);
        }

        base.Update(delta);
    }

    /// <inheritdoc />
    public override void Draw()
    {
        if (!Displayed)
            return;

        foreach (var child in Children)
            child.Draw();

        var finalFont = Scene?.Window?.FontManager.GetFont(Font);
        var finalTexture = Scene?.Window?.TextureManager.GetTexture(Texture);

        if (Size == Vec2.Zero && finalTexture != null)
            Size = new Vec2(finalTexture.Value.Width, finalTexture.Value.Height);

        if (
            Scene == null
            || Font.Length <= 0
            || finalFont == null
            || finalTexture == null
        )
            return;

        if (State == ButtonState.Hover && Active)
            SERender.DrawRectangle(
                (RealPosition.X - (Size.X + 4) / 2),
                (RealPosition.Y - (Size.Y + 4) / 2),
                (Size.X + 4),
                (Size.Y + 4),
                Color.White,
                InstructionSource.Ui,
                ZLayer
            );

        SERender.DrawTexture(
            finalTexture.Value,
            new Rect(0, 0, finalTexture.Value.Width, finalTexture.Value.Height),
            new Rect(RealPosition.X + 2, RealPosition.Y + 2, Size.X - 4, Size.Y - 4),
            Size / 2,
            0,
            Color.White,
            InstructionSource.Ui,
            ZLayer + 0.00002f
        );

        var finalFontSize = FontSize ?? finalFont.Value.BaseSize;
        var textSize = Raylib.MeasureTextEx(finalFont.Value, Text, finalFontSize, 2);
        if(Text.Length > 0)
            SERender.DrawText(
                finalFont.Value,
                Text,
                RealPosition,
                textSize / 2,
                0,
                finalFontSize,
                2,
                FontColor,
                InstructionSource.Ui,
                ZLayer + 0.00003f
            );

        if (State == ButtonState.Down || !Active)
            SERender.DrawRectangle(
                (RealPosition.X - Size.X / 2),
                (RealPosition.Y - Size.Y / 2),
                Size.X,
                Size.Y,
                new Color(0, 0, 0, 128),
                InstructionSource.Ui,
                ZLayer + 0.00004f
            );
    }
}
