using System;
using SharpEngine.Core.Input;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;
using SharpEngine.Core.Utils.EventArgs;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which represents Slider
/// </summary>
/// <param name="position">Position</param>
/// <param name="size">Size (Vec2(150, 60))</param>
/// <param name="color">Color (Color.Green)</param>
/// <param name="value">Value (0)</param>
/// <param name="zLayer">ZLayer (0)</param>
public class Slider(
    Vec2 position,
    Vec2? size = null,
    Color? color = null,
    float value = 0,
    int zLayer = 0
    ) : ProgressBar(position, size, color, value, zLayer)
{
    /// <summary>
    /// Event trigger when value is changed
    /// </summary>
    public event EventHandler<ValueEventArgs<float>>? ValueChanged;

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);

        if (InputManager.IsMouseButtonDown(MouseButton.Left))
        {
            var position = RealPosition - Size / 2;
            if (!InputManager.IsMouseInRectangle(new Rect(position, Size)))
                return;

            var barSize = Size.X;
            var point = InputManager.GetMousePosition().X - position.X;
            var temp = Value;
            Value = (int)System.Math.Round(point * 100 / barSize, MidpointRounding.AwayFromZero);
            if (System.Math.Abs(temp - Value) > 0.001f)
            {
                ValueChanged?.Invoke(
                    this,
                    new ValueEventArgs<float> { OldValue = temp, NewValue = Value }
                );
            }
        }
    }
}
