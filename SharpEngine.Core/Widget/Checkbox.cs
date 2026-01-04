using System;
using JetBrains.Annotations;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils.EventArgs;
using SharpEngine.Core.Utils;
using SharpEngine.Core.Input;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which display Checkbox
/// </summary>
/// <param name="position">Checkbox Position</param>
/// <param name="size">Checkbox Size</param>
/// <param name="isChecked">If Checkbox is Checked</param>
/// <param name="zLayer">Z Layer</param>
[UsedImplicitly]
public class Checkbox(Vec2 position, Vec2? size = null, bool isChecked = false, int zLayer = 0)
    : Widget(position, zLayer)
{
    /// <summary>
    /// Size of Checkbox
    /// </summary>
    [UsedImplicitly]
    public Vec2 Size { get; set; } = size ?? new Vec2(20);

    /// <summary>
    /// If Checkbox is Checked
    /// </summary>
    [UsedImplicitly]
    public bool IsChecked { get; set; } = isChecked;

    /// <summary>
    /// Event trigger when value is changed
    /// </summary>
    [UsedImplicitly]
    public event EventHandler<ValueEventArgs<bool>>? ValueChanged;

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);

        if (!RealDisplayed || !Active)
            return;

        if (
            InputManager.IsMouseButtonPressed(MouseButton.Left)
            && InputManager.IsMouseInRectangle(new Rect(RealPosition - Size / 2, Size))
        )
        {
            IsChecked = !IsChecked;
            ValueChanged?.Invoke(
                this,
                new ValueEventArgs<bool> { OldValue = !IsChecked, NewValue = IsChecked }
            );
        }
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        if (!Displayed || Size == Vec2.Zero)
            return;

        SERender.DrawRectangle(
            new Rect(RealPosition.X, RealPosition.Y, Size.X, Size.Y),
            Size / 2,
            0,
            Color.Black,
            InstructionSource.Ui,
            ZLayer
        );
        SERender.DrawRectangle(
            new Rect(RealPosition.X, RealPosition.Y, Size.X - 4, Size.Y - 4),
            (Size - 4) / 2,
            0,
            Color.White,
            InstructionSource.Ui,
            ZLayer + 0.00001f
        );

        if (IsChecked)
            SERender.DrawRectangle(
                new Rect(RealPosition.X, RealPosition.Y, Size.X - 6, Size.Y - 6),
                (Size - 6) / 2,
                0,
                Color.Black,
                InstructionSource.Ui,
                ZLayer + 0.00002f
            );
    }
}
