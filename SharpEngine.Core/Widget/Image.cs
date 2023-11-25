using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which display Image
/// </summary>
/// <param name="position">Image Position</param>
/// <param name="texture">Image Texture ("")</param>
/// <param name="scale">Image Scale (Vec2(1))</param>
/// <param name="rotation">Image Rotation (0)</param>
/// <param name="zLayer">Z Layer</param>
public class Image(
    Vec2 position,
    string texture = "",
    Vec2? scale = null,
    int rotation = 0,
    int zLayer = 0
) : Widget(position, zLayer)
{
    /// <summary>
    /// Name of Texture which be displayed
    /// </summary>
    public string Texture { get; set; } = texture;

    /// <summary>
    /// Scale of Image
    /// </summary>
    public Vec2 Scale { get; set; } = scale ?? Vec2.One;

    /// <summary>
    /// Rotation of Image
    /// </summary>
    public int Rotation { get; set; } = rotation;

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        var window = Scene?.Window;

        if (!Displayed || Texture.Length <= 0 || window == null)
            return;

        var finalTexture = window.TextureManager.GetTexture(Texture);
        SERender.DrawTexture(
            finalTexture,
            new Rect(0, 0, finalTexture.Width, finalTexture.Height),
            new Rect(RealPosition.X, RealPosition.Y, finalTexture.Width * Scale.X, finalTexture.Height * Scale.Y),
            new Vec2(finalTexture.Width / 2f * Scale.X, finalTexture.Height / 2f * Scale.Y),
            Rotation,
            Color.White,
            InstructionSource.UI,
            ZLayer
        );
    }
}
