using System;
using Raylib_cs;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using Color = SharpEngine.Core.Utils.Color;
using MouseButton = SharpEngine.Core.Input.MouseButton;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which display Button
/// </summary>
public class Button : Widget
{
    private enum ButtonState
    {
        Idle,
        Down,
        Hover
    }

    /// <summary>
    /// Text of Button
    /// </summary>
    public string Text { get; set; }

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
    /// Color of Button Background
    /// </summary>
    public Color BackgroundColor { get; set; }

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
    /// Create Button
    /// </summary>
    /// <param name="position">Button Position</param>
    /// <param name="text">Button Text</param>
    /// <param name="font">Button Font</param>
    /// <param name="size">Button Size</param>
    /// <param name="fontColor">Button Font Color</param>
    /// <param name="backgroundColor">Button Background Color</param>
    /// <param name="fontSize">Button Font Size</param>
    /// <param name="zLayer">Z Layer</param>
    public Button(
        Vec2 position,
        string text = "",
        string font = "",
        Vec2? size = null,
        Color? fontColor = null,
        Color? backgroundColor = null,
        int? fontSize = null,
        int zLayer = 0
    )
        : base(position, zLayer)
    {
        Text = text;
        Font = font;
        Size = size ?? new Vec2(200, 40);
        FontColor = fontColor ?? Color.Black;
        BackgroundColor = backgroundColor ?? Color.Gray;
        FontSize = fontSize;
        _state = ButtonState.Idle;
    }

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);

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

        if (!Displayed || Scene == null || Text.Length <= 0 || Font.Length <= 0 || font == null)
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
        SERender.DrawRectangle(
            (int)(position.X - (Size.X - 4) / 2),
            (int)(position.Y - (Size.Y - 4) / 2),
            (int)(Size.X - 4),
            (int)(Size.Y - 4),
            BackgroundColor,
            InstructionSource.UI,
            ZLayer + 0.00002f
        );

        var fontSize = FontSize ?? font.Value.BaseSize;
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
