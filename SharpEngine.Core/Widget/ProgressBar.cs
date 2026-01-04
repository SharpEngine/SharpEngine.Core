using JetBrains.Annotations;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which represents ProgressBar
/// </summary>
public class ProgressBar : Widget
{
    /// <summary>
    /// Value of Bar (0 to 100)
    /// </summary>
    [UsedImplicitly]
    public float Value
    {
        get;
        set => field = MathHelper.Clamp(value, 0, 100);
    }

    /// <summary>
    /// Size of Bar
    /// </summary>
    [UsedImplicitly]
    public Vec2 Size { get; set; }

    /// <summary>
    /// Color of Bar
    /// </summary>
    [UsedImplicitly]
    public Color Color { get; set; }

    /// <summary>
    /// If Bar is Horizontal
    /// </summary>
    [UsedImplicitly]
    public bool Horizontal { get; set; }

    /// <summary>
    /// Create a ProgressBar
    /// </summary>
    /// <param name="position">Position</param>
    /// <param name="size">Size (Vec2(150, 60))</param>
    /// <param name="color">Color (Color.Green)</param>
    /// <param name="value">Value (0)</param>
    /// <param name="horizontal">Horizontal (true)</param>
    /// <param name="zLayer">ZLayer (0)</param>
    public ProgressBar(
        Vec2 position,
        Vec2? size = null,
        Color? color = null,
        float value = 0,
        bool horizontal = true,
        int zLayer = 0
    )
        : base(position, zLayer)
    {
        Size = size ?? new Vec2(150, 60);
        Color = color ?? Color.Green;
        Value = value;
        Horizontal = horizontal;
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        if (!Displayed || Size == Vec2.Zero)
            return;

        SERender.DrawRectangle(
            new Rect(RealPosition, Size),
            Size / 2,
            0,
            Color.Black,
            InstructionSource.Ui,
            ZLayer
        );
        SERender.DrawRectangle(
            new Rect(RealPosition, Size - 4),
            (Size - 4) / 2,
            0,
            Color.White,
            InstructionSource.Ui,
            ZLayer + 0.00001f
        );
        SERender.DrawRectangle(
            Horizontal ? new Rect(RealPosition, (Size.X - 8) * Value / 100, Size.Y - 8) : new Rect(RealPosition, Size.X - 8, (Size.Y - 8) * Value / 100),
            (Size - 8) / 2,
            0,
            Color,
            InstructionSource.Ui,
            ZLayer + 0.00002f
        );
    }
}
