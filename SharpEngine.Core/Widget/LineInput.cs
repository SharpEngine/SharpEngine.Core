using System;
using Raylib_cs;
using SharpEngine.Core.Input;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils.EventArgs;
using Color = SharpEngine.Core.Utils.Color;
using MouseButton = SharpEngine.Core.Input.MouseButton;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which represents Line Input
/// </summary>
/// <param name="position">Line Edit Position</param>
/// <param name="text">Line Edit Text ("")</param>
/// <param name="font">Line Edit Font ("")</param>
/// <param name="size">Line Edit Size (Vec2(300, 50))</param>
/// <param name="fontSize">Line Edit Font Size (null)</param>
/// <param name="zLayer">Z Layer</param>
public class LineInput(
    Vec2 position,
    string text = "",
    string font = "",
    Vec2? size = null,
    int? fontSize = null,
    int zLayer = 0
) : Widget(position, zLayer)
{
    /// <summary>
    /// Current Text of Line Input
    /// </summary>
    public string Text { get; set; } = text;

    /// <summary>
    /// Font of Line Input
    /// </summary>
    public string Font { get; set; } = font;

    /// <summary>
    /// Size of Line Input
    /// </summary>
    public Vec2 Size { get; set; } = size ?? new Vec2(300, 50);

    /// <summary>
    /// If Line Input is Focused
    /// </summary>
    public bool Focused { get; private set; } = false;

    /// <summary>
    /// Font Size of Line Input (or null)
    /// </summary>
    public int? FontSize { get; set; } = fontSize;

    /// <summary>
    /// Event trigger when value is changed
    /// </summary>
    public event EventHandler<ValueEventArgs<string>>? ValueChanged;

    /// <inheritdoc />
    public override void Update(float delta)
    {
        if (!Active)
        {
            Focused = false;
            return;
        }

        if (InputManager.IsMouseButtonPressed(MouseButton.Left))
            Focused = InputManager.IsMouseInRectangle(new Rect(RealPosition - Size / 2, Size));

        if (!Focused)
            return;

        #region Text Processing

        if (InputManager.IsKeyPressed(Key.Backspace) && Text.Length >= 1)
        {
            var old = Text;
            Text = Text[..^1];
            ValueChanged?.Invoke(
                this,
                new ValueEventArgs<string> { OldValue = old, NewValue = Text }
            );
        }

        var finalFont = Scene?.Window?.FontManager.GetFont(Font);
        if (finalFont != null)
        {
            foreach (var pressedChar in InputManager.GetPressedChars())
            {
                if (
                    char.IsSymbol(pressedChar)
                    || char.IsWhiteSpace(pressedChar)
                    || char.IsLetterOrDigit(pressedChar)
                    || char.IsPunctuation(pressedChar)
                )
                {
                    Text += pressedChar;
                    ValueChanged?.Invoke(
                        this,
                        new ValueEventArgs<string> { OldValue = Text, NewValue = Text[..^1] }
                    );
                }
            }
        }

        #endregion
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        if (!Displayed || Scene == null)
            return;

        SERender.DrawRectangle(
            new Rect(position.X, position.Y, Size.X, Size.Y),
            Size / 2,
            0,
            Color.Black,
            InstructionSource.UI,
            ZLayer
        );
        SERender.DrawRectangle(
            new Rect(position.X + 2, position.Y + 2, Size.X - 4, Size.Y - 4),
            Size / 2,
            0,
            Color.White,
            InstructionSource.UI,
            ZLayer + 0.00001f
        );

        var finalFont = Scene?.Window?.FontManager.GetFont(Font);

        if (Font.Length <= 0 || finalFont == null)
            return;

        var finalFontSize = FontSize ?? finalFont.Value.BaseSize;
        var textSize = Raylib.MeasureTextEx(finalFont.Value, Text, finalFontSize, 2);
        var offset = textSize.X - (Size.X - 20);

        if (Text.Length > 0)
        {
            var finalPosition = new Vec2(RealPosition.X - Size.X / 2 + 4, RealPosition.Y - textSize.Y / 2);

            SERender.ScissorMode(
                (int)finalPosition.X,
                (int)finalPosition.Y,
                (int)Size.X - 8,
                (int)textSize.Y,
                InstructionSource.UI,
                ZLayer + 0.00002f,
                () =>
                {
                    SERender.DrawText(
                        finalFont.Value,
                        Text,
                        new Vec2(finalPosition.X - (offset > 0 ? offset : 0), finalPosition.Y),
                        finalFontSize,
                        2,
                        Color.Black,
                        InstructionSource.UI,
                        0
                    );
                }
            );
        }

        if (Focused)
            SERender.DrawRectangle(
                (int)(RealPosition.X - Size.X / 2 + 10 + textSize.X - (offset > 0 ? offset : 0)),
                (int)(RealPosition.Y - textSize.Y / 2 + 4),
                5,
                (int)textSize.Y - 8,
                Color.Black,
                InstructionSource.UI,
                ZLayer + 0.00003f
            );
    }
}
