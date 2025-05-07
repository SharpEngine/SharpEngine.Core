using System;
using Raylib_cs;
using SharpEngine.Core.Input;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils.EventArgs;
using MouseButton = SharpEngine.Core.Input.MouseButton;
using Color = SharpEngine.Core.Utils.Color;
using Microsoft.Win32.SafeHandles;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which represents Multi Line Input
/// </summary>
/// <param name="position">Multi Line Edit Position</param>
/// <param name="text">Multi Line Edit Text ("")</param>
/// <param name="font">Multi Line Edit Font ("")</param>
/// <param name="size">Multi Line Edit Size (Vec2(500, 200))</param>
/// <param name="fontSize">Multi Line Edit Font Size (null)</param>
/// <param name="secret">If Multi Line Edit is Secret (false)</param>
/// <param name="zLayer">Z Layer</param>
public class MultiLineInput(
    Vec2 position,
    string text = "",
    string font = "",
    Vec2? size = null,
    int? fontSize = null,
    bool secret = false,
    int zLayer = 0
) : LineInput(position, text, font, size, fontSize, secret, zLayer)
{
    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);

        #region Text Processing

        if (InputManager.IsKeyPressed(Key.Enter))
        {
            Text += '\n';
            InvokeValueChanged(new ValueEventArgs<string> { OldValue = Text[..^1], NewValue = Text });
        }

        #endregion
    }

    /// <inheritdoc />
    public override void Draw()
    {
        if (!Displayed)
            return;

        foreach (var child in Children)
            child.Draw();

        if (Scene == null)
            return;

        SERender.DrawRectangle(
            new Rect(RealPosition.X, RealPosition.Y, Size.X, Size.Y),
            Size / 2,
            0,
            Color.Black,
            InstructionSource.UI,
            ZLayer
        );
        SERender.DrawRectangle(
            new Rect(RealPosition.X + 2, RealPosition.Y + 2, Size.X - 4, Size.Y - 4),
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

        var textSize = Raylib.MeasureTextEx(finalFont.Value, Text.Split("\n")[^1], finalFontSize, 2);
        var realTextSize = Raylib.MeasureTextEx(finalFont.Value, "A", finalFontSize, 2);

        var finalPosition = new Vec2(RealPosition.X - Size.X / 2 + 4, RealPosition.Y - Size.Y / 2 + 4);

        var lines = Text.Split("\n");
        var offsetX = textSize.X - (Size.X - 20);
        var offsetY = textSize.Y * lines.Length - (Size.Y - 8);

        SERender.ScissorMode(
            (int)finalPosition.X,
            (int)finalPosition.Y,
            (int)Size.X - 8,
            (int)Size.Y - 8,
            InstructionSource.UI,
            ZLayer + 0.00002f,
            () =>
            {
                DrawLines(finalFont!.Value, finalFontSize, finalPosition, lines, offsetX, offsetY);

                if (Focused)
                {
                    SERender.DrawRectangle(
                        finalPosition.X + 6 + textSize.X - (offsetX > 0 ? offsetX : 0),
                        finalPosition.Y + realTextSize.Y * (lines.Length == 0 ? 1 : lines.Length - 1) - (offsetY > 0 ? offsetY : 0),
                        5,
                        realTextSize.Y,
                        Color.Black,
                        InstructionSource.UI,
                        ZLayer + 0.00003f
                    );
                }
            }
        );

    }

    private void DrawLines(Font finalFont, int finalFontSize, Vec2 finalPosition, string[] lines, float offsetX, float offsetY)
    {
        for (var i = 0; i < lines.Length; i++)
        {
            var text = Secret ? new string('*', lines[i].Length) : lines[i];
            var lineSize = Raylib.MeasureTextEx(finalFont, text, finalFontSize, 2);
            var pos = new Vec2(
                finalPosition.X - (offsetX > 0 ? offsetX : 0),
                finalPosition.Y + i * lineSize.Y - (offsetY > 0 ? offsetY : 0)
            );
            SERender.DrawText(
                finalFont,
                text,
                pos,
                finalFontSize,
                2,
                Color.Black,
                InstructionSource.UI,
                0
            );
        }
    }
}
