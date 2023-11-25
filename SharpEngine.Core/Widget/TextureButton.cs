using System;
using Raylib_cs;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using Color = SharpEngine.Core.Utils.Color;
using MouseButton = SharpEngine.Core.Input.MouseButton;

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
public class TextureButton(
    Vec2 position,
    string text = "",
    string font = "",
    string texture = "",
    Vec2? size = null,
    Color? fontColor = null,
    int? fontSize = null,
    int zLayer = 0
) : Widget(position, zLayer)
{
    private enum ButtonState
    {
        Idle,
        Down,
        Hover
    }

    /// <summary>
    /// Text of Texture Button
    /// </summary>
    public string Text { get; set; } = text;

    /// <summary>
    /// Texture of Texture Button
    /// </summary>
    public string Texture { get; set; } = texture;

    /// <summary>
    /// Font of Button
    /// </summary>
    public string Font { get; set; } = font;

    /// <summary>
    /// Size of Button
    /// </summary>
    public Vec2 Size { get; set; } = size ?? Vec2.Zero;

    /// <summary>
    /// Color of Button Font
    /// </summary>
    public Color FontColor { get; set; } = fontColor ?? Color.Black;

    /// <summary>
    /// Font Size of Button (or Null)
    /// </summary>
    public int? FontSize { get; set; } = fontSize;

    /// <summary>
    /// Event which trigger when button is clicked
    /// </summary>
    public event EventHandler? Clicked;

    private ButtonState _state = ButtonState.Idle;

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);

        if (Size == Vec2.Zero)
        {
            var finalTexture = Scene?.Window?.TextureManager.GetTexture(Texture);
            if (finalTexture != null)
                Size = new Vec2(finalTexture.Value.Width, finalTexture.Value.Height);
        }

        if (!Active)
            return;

        if (InputManager.IsMouseInRectangle(new Rect(RealPosition - Size / 2, Size)))
        {
            _state = ButtonState.Hover;
            if (InputManager.IsMouseButtonPressed(MouseButton.Left))
                Clicked?.Invoke(this, EventArgs.Empty);
            if (InputManager.IsMouseButtonDown(MouseButton.Left))
                _state = ButtonState.Down;
        }
        else
            _state = ButtonState.Idle;
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        var finalFont = Scene?.Window?.FontManager.GetFont(Font);
        var finalTexture = Scene?.Window?.TextureManager.GetTexture(Texture);

        if (Size == Vec2.Zero && finalTexture != null)
            Size = new Vec2(finalTexture.Value.Width, finalTexture.Value.Height);

        if (
            !Displayed
            || Scene == null
            || Text.Length <= 0
            || Font.Length <= 0
            || finalFont == null
            || finalTexture == null
        )
            return;

        if (_state == ButtonState.Hover && Active)
            SERender.DrawRectangle(
                (int)(RealPosition.X - (Size.X + 4) / 2),
                (int)(RealPosition.Y - (Size.Y + 4) / 2),
                (int)(Size.X + 4),
                (int)(Size.Y + 4),
                Color.White,
                InstructionSource.UI,
                ZLayer
            );

        SERender.DrawRectangle(
            (int)(RealPosition.X - Size.X / 2),
            (int)(RealPosition.Y - Size.Y / 2),
            (int)Size.X,
            (int)Size.Y,
            Color.Black,
            InstructionSource.UI,
            ZLayer + 0.00001f
        );
        SERender.DrawTexture(
            finalTexture.Value,
            new Rect(0, 0, finalTexture.Value.Width, finalTexture.Value.Height),
            new Rect(RealPosition.X + 2, RealPosition.Y + 2, Size.X - 4, Size.Y - 4),
            Size / 2,
            0,
            Color.White,
            InstructionSource.UI,
            ZLayer + 0.00002f
        );

        var finalFontSize = FontSize ?? finalFont.Value.BaseSize;
        var textSize = Raylib.MeasureTextEx(finalFont.Value, Text, finalFontSize, 2);
        SERender.DrawText(
            finalFont.Value,
            Text,
            RealPosition,
            textSize / 2,
            0,
            finalFontSize,
            2,
            FontColor,
            InstructionSource.UI,
            ZLayer + 0.00003f
        );

        if (_state == ButtonState.Down || !Active)
            SERender.DrawRectangle(
                (int)(RealPosition.X - Size.X / 2),
                (int)(RealPosition.Y - Size.Y / 2),
                (int)Size.X,
                (int)Size.Y,
                new Color(0, 0, 0, 128),
                InstructionSource.UI,
                ZLayer + 0.00004f
            );
    }
}
