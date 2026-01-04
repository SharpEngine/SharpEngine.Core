using JetBrains.Annotations;
using Raylib_cs;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component which display Text
/// </summary>
/// <param name="text">Text</param>
/// <param name="font">Font Name (RAYLIB_DEFAULT)</param>
/// <param name="color">Color (Color.Black)</param>
/// <param name="displayed">If Text is Displayed (true)</param>
/// <param name="fontSize">Font Size (null)</param>
/// <param name="offset">Offset (Vec2(0))</param>
/// <param name="zLayerOffset">Offset of zLayer</param>
[UsedImplicitly]
public class TextComponent(
    string text,
    string font = "RAYLIB_DEFAULT",
    Utils.Color? color = null,
    bool displayed = true,
    int? fontSize = null,
    Vec2? offset = null,
    int zLayerOffset = 0
) : Component
{
    /// <summary>
    /// Text which be displayed
    /// </summary>
    [UsedImplicitly]
    public string Text { get; set; } = text;

    /// <summary>
    /// Name of Font
    /// </summary>
    [UsedImplicitly]
    public string Font { get; set; } = font;

    /// <summary>
    /// Color of Text
    /// </summary>
    [UsedImplicitly]
    public Utils.Color Color { get; set; } = color ?? Utils.Color.Black;

    /// <summary>
    /// Define if Text is displayed
    /// </summary>
    [UsedImplicitly]
    public bool Displayed { get; set; } = displayed;

    /// <summary>
    /// Offset of Text
    /// </summary>
    [UsedImplicitly]
    public Vec2 Offset { get; set; } = offset ?? Vec2.Zero;

    /// <summary>
    /// Font Size (can be null and use the basic size of Font)
    /// </summary>
    [UsedImplicitly]
    public int? FontSize { get; set; } = fontSize;

    /// <summary>
    /// Offset of ZLayer of Text
    /// </summary>
    [UsedImplicitly]
    public int ZLayerOffset { get; set; } = zLayerOffset;

    /// <summary>
    /// Represents the transform component associated with the current object, if available.
    /// </summary>
    [UsedImplicitly]
    protected TransformComponent? TransformComponent;

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        TransformComponent = Entity?.GetComponentAs<TransformComponent>();
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        var window = Entity?.Scene?.Window;

        if (
            TransformComponent == null
            || !Displayed
            || Text.Length <= 0
            || Font.Length <= 0
            || window == null
        )
            return;

        var finalFont = window.FontManager.GetFont(Font);
        var finalFontSize = FontSize ?? finalFont.BaseSize;
        var position = TransformComponent.GetTransformedPosition(Offset);
        var textSize = Raylib.MeasureTextEx(finalFont, Text, finalFontSize, 2);

        SERender.DrawText(
            finalFont,
            Text,
            position,
            textSize / 2,
            TransformComponent.Rotation,
            finalFontSize,
            2,
            Color,
            InstructionSource.Entity,
            TransformComponent.ZLayer + ZLayerOffset
        );
    }
}
