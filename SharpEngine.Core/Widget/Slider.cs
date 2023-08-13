using System;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;
using SharpEngine.Core.Utils.EventArgs;
using SharpEngine.Core.Utils.Input;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which represents Slider
/// </summary>
public class Slider: Widget
{
    private float _value;
    /// <summary>
    /// Value of Slider (0 to 100)
    /// </summary>
    public float Value
    {
        get => _value;
        set => _value = MathHelper.Clamp(value, 0, 100);
    }
    
    /// <summary>
    /// Size of Slider
    /// </summary>
    public Vec2 Size { get; set; }
    
    /// <summary>
    /// Color of Slider
    /// </summary>
    public Color Color { get; set; }
    
    /// <summary>
    /// Event trigger when value is changed
    /// </summary>
    public event EventHandler<ValueEventArgs<float>>? ValueChanged; 

    /// <summary>
    /// Create Slider
    /// </summary>
    /// <param name="position">Position</param>
    /// <param name="size">Size (Vec2(150, 60))</param>
    /// <param name="color">Color (Color.Green)</param>
    /// <param name="value">Value (0)</param>
    /// <param name="zLayer">ZLayer (0)</param>
    public Slider(Vec2 position, Vec2? size = null, Color? color = null, float value = 0, int zLayer = 0) : base(
        position, zLayer)
    {
        Size = size ?? new Vec2(150, 60);
        Color = color ?? Color.Green;
        Value = value;
    }

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);

        if (InputManager.IsMouseButtonDown(MouseButton.Left))
        {
            var position = RealPosition - Size / 2;
            if(!InputManager.IsMouseInRectangle(new Rect(position, Size))) return;

            var barSize = Size.X;
            var point = InputManager.GetMousePosition().X - position.X;
            var temp = Value;
            Value = (int)System.Math.Round(point * 100 / barSize, MidpointRounding.AwayFromZero);
            if (System.Math.Abs(temp - Value) > Internal.FloatTolerance)
            {
                ValueChanged?.Invoke(this, new ValueEventArgs<float>
                {
                    OldValue = temp,
                    NewValue = Value
                });
            }
        }
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();
        
        if(!Displayed || Size == Vec2.Zero) return;

        var position = RealPosition;
        SERender.DrawRectangle(new Rect(position, Size), Size / 2, 0, Color.Black, InstructionSource.UI,
            ZLayer);
        SERender.DrawRectangle(new Rect(position, Size - 4), (Size - 4) / 2, 0, Color.White, InstructionSource.UI,
            ZLayer + 0.00001f);
        SERender.DrawRectangle(new Rect(position, (Size.X - 8) * Value / 100, Size.Y - 8), (Size - 8) / 2, 0, Color,
            InstructionSource.UI, ZLayer + 0.00002f);
    }
}