using Raylib_cs;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using Color = SharpEngine.Core.Utils.Color;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component which display Text
/// </summary>
public class TextComponent: Component
{
    /// <summary>
    /// Text which be displayed
    /// </summary>
    public string Text { get; set; }
    
    /// <summary>
    /// Name of Font
    /// </summary>
    public string Font { get; set; }
    
    /// <summary>
    /// Color of Text
    /// </summary>
    public Utils.Color Color { get; set; }
    
    /// <summary>
    /// Define if Text is displayed
    /// </summary>
    public bool Displayed { get; set; }
    
    /// <summary>
    /// Offset of Text
    /// </summary>
    public Vec2 Offset { get; set; }
    
    /// <summary>
    /// Font Size (can be null and use basic size of Font)
    /// </summary>
    public int? FontSize;
    
    /// <summary>
    /// Offset of ZLayer of Text
    /// </summary>
    public int ZLayerOffset { get; set; }
    
    private TransformComponent? _transformComponent;

    /// <summary>
    /// Create TextComponent
    /// </summary>
    /// <param name="text">Text</param>
    /// <param name="font">Font Name (RAYLIB_DEFAULT)</param>
    /// <param name="color">Color (Color.Black)</param>
    /// <param name="displayed">If Text is Displayed (true)</param>
    /// <param name="fontSize">Font Size (null)</param>
    /// <param name="offset">Offset (Vec2(0))</param>
    /// <param name="zLayerOffset">Offset of zLayer</param>
    public TextComponent(string text, string font = "RAYLIB_DEFAULT", Utils.Color? color = null, bool displayed = true,
        int? fontSize = null, Vec2? offset = null, int zLayerOffset = 0)
    {
        Text = text;
        Font = font;
        Color = color ?? Utils.Color.Black;
        Displayed = displayed;
        Offset = offset ?? Vec2.Zero;
        FontSize = fontSize;
        ZLayerOffset = zLayerOffset;
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        _transformComponent = Entity?.GetComponentAs<TransformComponent>();
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        var window = Entity?.Scene?.Window;
        
        if(_transformComponent == null || !Displayed || Text.Length <= 0 || Font.Length <= 0 || window == null) return;

        var font = window.FontManager.GetFont(Font);
        var fontSize = FontSize ?? font.baseSize;
        var position = _transformComponent.GetTransformedPosition(Offset);
        var textSize = Raylib.MeasureTextEx(font, Text, fontSize, 2);

        SERender.DrawText(font, Text, position, textSize / 2, _transformComponent.Rotation, fontSize, 2, Color,
            InstructionSource.Entity, _transformComponent.ZLayer + ZLayerOffset);
    }
}