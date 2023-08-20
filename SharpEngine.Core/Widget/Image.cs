using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which display Image
/// </summary>
public class Image: Widget
{
    /// <summary>
    /// Name of Texture which be displayed
    /// </summary>
    public string Texture { get; set; }

    /// <summary>
    /// Scale of Image
    /// </summary>
    public Vec2 Scale { get; set; }
    
    /// <summary>
    /// Rotation of Image
    /// </summary>
    public int Rotation { get; set; }

    /// <summary>
    /// Create Image
    /// </summary>
    /// <param name="position">Image Position</param>
    /// <param name="texture">Image Texture ("")</param>
    /// <param name="scale">Image Scale (Vec2(1))</param>
    /// <param name="rotation">Image Rotation (0)</param>
    /// <param name="zLayer">Z Layer</param>
    public Image(Vec2 position, string texture = "", Vec2? scale = null, int rotation = 0, int zLayer = 0) : base(
        position, zLayer)
    {
        Texture = texture;
        Scale = scale ?? Vec2.One;
        Rotation = rotation;
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();
        
        var window = Scene?.Window;
        
        if(!Displayed || Texture.Length <= 0 || window == null) return;

        var texture = window.TextureManager.GetTexture(Texture);
        var position = RealPosition;
        SERender.DrawTexture(texture, new Rect(0, 0, texture.width, texture.height),
            new Rect(position.X, position.Y, texture.width * Scale.X, texture.height * Scale.Y),
            new Vec2(texture.width / 2f * Scale.X, texture.height / 2f * Scale.Y), Rotation, Color.White,
            InstructionSource.UI, ZLayer);
    }
}