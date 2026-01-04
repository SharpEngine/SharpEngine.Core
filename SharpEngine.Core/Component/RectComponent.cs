using JetBrains.Annotations;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component which draw rectangle
/// </summary>
/// <param name="color">Rectangle Color</param>
/// <param name="size">Rectangle Size</param>
/// <param name="displayed">If displayed</param>
/// <param name="offset">Rectangle Offset</param>
/// <param name="zLayerOffset">Offset of zLayer</param>
public class RectComponent(
    Utils.Color color,
    Vec2 size,
    bool displayed = true,
    Vec2? offset = null,
    int zLayerOffset = 0
) : Component
{
    /// <summary>
    /// Color of Rectangle
    /// </summary>
    [UsedImplicitly]
    public Utils.Color Color { get; set; } = color;

    /// <summary>
    /// Size of Rectangle
    /// </summary>
    [UsedImplicitly]
    public Vec2 Size { get; set; } = size;

    /// <summary>
    /// If Rectangle is displayed
    /// </summary>
    [UsedImplicitly]
    public bool Displayed { get; set; } = displayed;

    /// <summary>
    /// Offset of Rectangle
    /// </summary>
    [UsedImplicitly]
    public Vec2 Offset { get; set; } = offset ?? Vec2.Zero;

    /// <summary>
    /// Offset of ZLayer of Rectangle
    /// </summary>
    [UsedImplicitly]
    public int ZLayerOffset { get; set; } = zLayerOffset;

    /// <summary>
    /// Represents the associated transform component, which defines the position, rotation, and scale of the object in
    /// the scene.
    /// </summary>
    [UsedImplicitly]
    protected TransformComponent? Transform;

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();
        Transform = Entity?.GetComponentAs<TransformComponent>();
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        if (Transform == null || !Displayed)
            return;

        var finalSize = Size * Transform.Scale;
        var position = Transform.GetTransformedPosition(Offset);

        SERender.DrawRectangle(
            new Rect(position.X, position.Y, finalSize.X, finalSize.Y),
            finalSize / 2,
            Transform.Rotation,
            Color,
            InstructionSource.Entity,
            Transform.ZLayer + ZLayerOffset
        );
    }
}
