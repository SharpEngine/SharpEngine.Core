using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component which draw rectangle
/// </summary>
public class RectComponent: Component
{
    /// <summary>
    /// Color of Rectangle
    /// </summary>
    public Utils.Color Color { get; set; }
    
    /// <summary>
    /// Size of Rectangle
    /// </summary>
    public Vec2 Size { get; set; }
    
    /// <summary>
    /// If Rectangle is displayed
    /// </summary>
    public bool Displayed { get; set; }
    
    /// <summary>
    /// Offset of Rectangle
    /// </summary>
    public Vec2 Offset { get; set; }
    
    /// <summary>
    /// Offset of ZLayer of Rectangle
    /// </summary>
    public int ZLayerOffset { get; set; }

    private TransformComponent? _transform;

    /// <summary>
    /// Create Rect Component
    /// </summary>
    /// <param name="color">Rectangle Color</param>
    /// <param name="size">Rectangle Size</param>
    /// <param name="displayed">If displayed</param>
    /// <param name="offset">Rectangle Offset</param>
    /// <param name="zLayerOffset">Offset of zLayer</param>
    public RectComponent(Utils.Color color, Vec2 size, bool displayed = true, Vec2? offset = null, int zLayerOffset = 0)
    {
        Color = color;
        Size = size;
        Displayed = displayed;
        Offset = offset ?? Vec2.Zero;
        ZLayerOffset = zLayerOffset;
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();
        _transform = Entity?.GetComponentAs<TransformComponent>();
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();
        
        if(_transform == null || !Displayed) return;

        var size = Size * _transform.Scale;
        var position = _transform.GetTransformedPosition(Offset);

        SERender.DrawRectangle(new Rect(position.X, position.Y, size.X, size.Y), size / 2, _transform.Rotation, Color,
            InstructionSource.Entity, _transform.ZLayer + ZLayerOffset);
    }
}