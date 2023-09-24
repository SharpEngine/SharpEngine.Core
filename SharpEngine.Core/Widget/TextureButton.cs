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
public class TextureButton : Widget
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
    public string Text { get; set; }

    /// <summary>
    /// Texture of Texture Button
    /// </summary>
    public string Texture { get; set; }

    /// <summary>
    /// Font of Button
    /// </summary>
    public string Font { get; set; }

    /// <summary>
    /// Size of Button
    /// </summary>
    public Vec2 Size { get; set; }

    /// <summary>
    /// Color of Button Font
    /// </summary>
    public Color FontColor { get; set; }

    /// <summary>
    /// Font Size of Button (or Null)
    /// </summary>
    public int? FontSize { get; set; }

    /// <summary>
    /// Event which trigger when button is clicked
    /// </summary>
    public event EventHandler? Clicked;

    private ButtonState _state;

    /// <summary>
    /// Create Texture Button
    /// </summary>
    /// <param name="position">Texture Button Position</param>
    /// <param name="text">Texture Button Text</param>
    /// <param name="font">Texture Button Font</param>
    /// <param name="texture">Texture Button Texture</param>
    /// <param name="size">Texture Button Size</param>
    /// <param name="fontColor">Texture Button Font Color</param>
    /// <param name="fontSize">Texture Button Font Size</param>
    /// <param name="zLayer">Z Layer</param>
    public TextureButton(
        Vec2 position,
        string text = "",
        string font = "",
        string texture = "",
        Vec2? size = null,
        Color? fontColor = null,
        int? fontSize = null,
        int zLayer = 0
    )
        : base(position, zLayer)
    {
        Text = text;
        Texture = texture;
        Font = font;
        Size = size ?? Vec2.Zero;
        FontColor = fontColor ?? Color.Black;
        FontSize = fontSize;
        _state = ButtonState.Idle;
    }

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);

        if (Size == Vec2.Zero)
        {
            var texture = Scene?.Window?.TextureManager.GetTexture(Texture);
            if (texture != null)
                Size = new Vec2(texture.Value.width, texture.Value.height);
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

        var font = Scene?.Window?.FontManager.GetFont(Font);
        var texture = Scene?.Window?.TextureManager.GetTexture(Texture);

        if (Size == Vec2.Zero && texture != null)
            Size = new Vec2(texture.Value.width, texture.Value.height);

        if (
            !Displayed
            || Scene == null
            || Text.Length <= 0
            || Font.Length <= 0
            || font == null
            || texture == null
        )
            return;

        var position = RealPosition;

        if (_state == ButtonState.Hover && Active)
            SERender.DrawRectangle(
                (int)(position.X - (Size.X + 4) / 2),
                (int)(position.Y - (Size.Y + 4) / 2),
                (int)(Size.X + 4),
                (int)(Size.Y + 4),
                Color.White,
                InstructionSource.UI,
                ZLayer
            );

        SERender.DrawRectangle(
            (int)(position.X - Size.X / 2),
            (int)(position.Y - Size.Y / 2),
            (int)Size.X,
            (int)Size.Y,
            Color.Black,
            InstructionSource.UI,
            ZLayer + 0.00001f
        );
        SERender.DrawTexture(
            texture.Value,
            new Rect(0, 0, texture.Value.width, texture.Value.height),
            new Rect(position.X + 2, position.Y + 2, Size.X - 4, Size.Y - 4),
            Size / 2,
            0,
            Color.White,
            InstructionSource.UI,
            ZLayer + 0.00002f
        );

        var fontSize = FontSize ?? font.Value.baseSize;
        var textSize = Raylib.MeasureTextEx(font.Value, Text, fontSize, 2);
        SERender.DrawText(
            font.Value,
            Text,
            position,
            textSize / 2,
            0,
            fontSize,
            2,
            FontColor,
            InstructionSource.UI,
            ZLayer + 0.00003f
        );

        if (_state == ButtonState.Down || !Active)
            SERender.DrawRectangle(
                (int)(position.X - Size.X / 2),
                (int)(position.Y - Size.Y / 2),
                (int)Size.X,
                (int)Size.Y,
                new Color(0, 0, 0, 128),
                InstructionSource.UI,
                ZLayer + 0.00004f
            );
    }
}
