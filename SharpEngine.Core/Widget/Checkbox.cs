using System;
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
public class Checkbox: Widget
{
    /// <summary>
    /// Size of Checkbox
    /// </summary>
    public Vec2 Size { get; set; }
    
    /// <summary>
    /// If Checkbox is Checked
    /// </summary>
    public bool IsChecked { get; set; }
    
    /// <summary>
    /// Event trigger when value is changed
    /// </summary>
    public event EventHandler<ValueEventArgs<bool>>? ValueChanged; 

    /// <summary>
    /// Create Checkbox
    /// </summary>
    /// <param name="position">Checkbox Position</param>
    /// <param name="size">Checkbox Size</param>
    /// <param name="isChecked">If Checkbox is Checked</param>
    /// <param name="zLayer">Z Layer</param>
    public Checkbox(Vec2 position, Vec2? size = null, bool isChecked = false, int zLayer = 0) : base(position, zLayer)
    {
        Size = size ?? new Vec2(20);
        IsChecked = isChecked;
    }

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);
        
        if(!Active) return;

        if (InputManager.IsMouseButtonPressed(MouseButton.Left) &&
            InputManager.IsMouseInRectangle(new Rect(RealPosition - Size / 2, Size)))
        {
            IsChecked = !IsChecked;
            ValueChanged?.Invoke(this, new ValueEventArgs<bool>
            {
                OldValue = !IsChecked,
                NewValue = IsChecked
            });
        }
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();
        
        if(!Displayed || Size == Vec2.Zero) return;
        
        var position = RealPosition;
        SERender.DrawRectangle(new Rect(position.X, position.Y, Size.X, Size.Y), Size / 2, 0, Color.Black,
            InstructionSource.UI, ZLayer);
        SERender.DrawRectangle(new Rect(position.X, position.Y, Size.X - 4, Size.Y - 4), (Size - 4) / 2,
            0, Color.White, InstructionSource.UI, ZLayer + 0.00001f);
        
        if(IsChecked)
            SERender.DrawRectangle(new Rect(position.X, position.Y, Size.X - 6, Size.Y - 6), (Size - 6) / 2,
            0, Color.Black, InstructionSource.UI, ZLayer + 0.00002f);
    }
}