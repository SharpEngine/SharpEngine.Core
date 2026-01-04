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
/// <param name="position">Button Position</param>
/// <param name="text">Button Text</param>
/// <param name="font">Button Font</param>
/// <param name="size">Button Size</param>
/// <param name="fontColor">Button Font Color</param>
/// <param name="backgroundColor">Button Background Color</param>
/// <param name="fontSize">Button Font Size</param>
/// <param name="zLayer">Z Layer</param>
public class Button(
    Vec2 position,
    string text = "",
    string font = "",
    Vec2? size = null,
    Color? fontColor = null,
    Color? backgroundColor = null,
    int? fontSize = null,
    int zLayer = 0
) : Widget(position, zLayer)
{
    /// <summary>
    /// State of Button
    /// </summary>
    protected enum ButtonState
    {
        /// <summary>
        /// Waiting
        /// </summary>
        Idle,

        /// <summary>
        /// Pressed
        /// </summary>
        Down,

        /// <summary>
        /// Hover
        /// </summary>
        Hover
    }

    /// <summary>
    /// Text of Button
    /// </summary>
    public string Text { get; set; } = text;

    /// <summary>
    /// Font of Button
    /// </summary>
    public string Font { get; set; } = font;

    /// <summary>
    /// Size of Button
    /// </summary>
    public Vec2 Size { get; set; } = size ?? new Vec2(200, 40);

    /// <summary>
    /// Color of Button Font
    /// </summary>
    public Color FontColor { get; set; } = fontColor ?? Color.Black;

    /// <summary>
    /// Color of Button Background
    /// </summary>
    public Color BackgroundColor { get; set; } = backgroundColor ?? Color.Gray;

    /// <summary>
    /// Font Size of Button (or Null)
    /// </summary>
    public int? FontSize { get; set; } = fontSize;

    /// <summary>
    /// Event that triggers when a button is clicked
    /// </summary>
    public event EventHandler? Clicked;

    /// <summary>
    /// State of Button
    /// </summary>
    protected ButtonState State = ButtonState.Idle;

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);

        if (!RealDisplayed || !Active)
            return;

        if (InputManager.IsMouseInRectangle(new Rect(RealPosition - Size / 2, Size)))
        {
            State = ButtonState.Hover;
            if (InputManager.IsMouseButtonPressed(MouseButton.Left))
                Clicked?.Invoke(this, EventArgs.Empty);
            if (InputManager.IsMouseButtonDown(MouseButton.Left))
                State = ButtonState.Down;
        }
        else
            State = ButtonState.Idle;
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        var finalFont = Scene?.Window?.FontManager.GetFont(Font);

        if (!Displayed || Scene == null || Text.Length <= 0 || Font.Length <= 0 || finalFont == null)
            return;

        if (State == ButtonState.Hover && Active)
            SERender.DrawRectangle(
                RealPosition.X - (Size.X + 4) / 2,
                RealPosition.Y - (Size.Y + 4) / 2,
                Size.X + 4,
                Size.Y + 4,
                Color.White,
                InstructionSource.Ui,
                ZLayer
            );

        SERender.DrawRectangle(
            RealPosition.X - Size.X / 2,
            RealPosition.Y - Size.Y / 2,
            Size.X,
            Size.Y,
            Color.Black,
            InstructionSource.Ui,
            ZLayer + 0.00001f
        );
        SERender.DrawRectangle(
            RealPosition.X - (Size.X - 4) / 2,
            RealPosition.Y - (Size.Y - 4) / 2,
            Size.X - 4,
            Size.Y - 4,
            BackgroundColor,
            InstructionSource.Ui,
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
            InstructionSource.Ui,
            ZLayer + 0.00003f
        );

        if (State == ButtonState.Down || !Active)
            SERender.DrawRectangle(
                RealPosition.X - Size.X / 2,
                RealPosition.Y - Size.Y / 2,
                Size.X,
                Size.Y,
                new Color(0, 0, 0, 128),
                InstructionSource.Ui,
                ZLayer + 0.00004f
            );
    }
}
