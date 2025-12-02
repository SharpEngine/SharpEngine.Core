using System;
using Raylib_cs;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component which display sprite
/// </summary>
/// <param name="texture">Name Texture Displayed</param>
/// <param name="displayed">If Texture is Displayed (true)</param>
/// <param name="offset">Offset (Vec2(0))</param>
/// <param name="flipX">If Sprite is Flip Horizontally</param>
/// <param name="flipY">If Sprite is Flip Vertically</param>
/// <param name="zLayerOffset">Offset of zLayer</param>
/// <param name="shader">Sprite Shader ("")</param>
public class SpriteComponent(
    string texture,
    bool displayed = true,
    Vec2? offset = null,
    bool flipX = false,
    bool flipY = false,
    int zLayerOffset = 0,
    string shader = ""
) : Component
{
    /// <summary>
    /// Name of Texture which be displayed
    /// </summary>
    public string Texture { get; set; } = texture;

    /// <summary>
    /// Define if Sprite is displayed
    /// </summary>
    public bool Displayed { get; set; } = displayed;

    /// <summary>
    /// Offset of Sprite
    /// </summary>
    public Vec2 Offset { get; set; } = offset ?? Vec2.Zero;

    /// <summary>
    /// If Sprite is Flip Horizontally
    /// </summary>
    public bool FlipX { get; set; } = flipX;

    /// <summary>
    /// If Sprite is Flip Vertically
    /// </summary>
    public bool FlipY { get; set; } = flipY;

    /// <summary>
    /// Offset of ZLayer of Sprite
    /// </summary>
    public int ZLayerOffset { get; set; } = zLayerOffset;

    /// <summary>
    /// Shader of Sprite
    /// </summary>
    public string Shader { get; set; } = shader;

    /// <summary>
    /// Tint Color of Sprite
    /// </summary>
    public Utils.Color TintColor { get; set; } = Utils.Color.White;

    /// <summary>
    /// Represents the associated transform component, which provides position, rotation, and scale information for the
    /// object.
    /// </summary>
    protected TransformComponent? _transformComponent;

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

        if (_transformComponent == null || !Displayed || Texture.Length <= 0 || window == null)
            return;

        var finalTexture = window.TextureManager.GetTexture(Texture);
        var position = _transformComponent.GetTransformedPosition(Offset);
        if (Shader == "")
            DrawTexture(finalTexture, position);
        else
        {
            SERender.ShaderMode(
                window.ShaderManager.GetShader(Shader).GetInternalShader(),
                InstructionSource.Entity,
                _transformComponent.ZLayer + ZLayerOffset,
                () => DrawTexture(finalTexture, position)
            );
        }
    }

    private void DrawTexture(Texture2D finalTexture, Vec2 position)
    {
        SERender.DrawTexture(
            finalTexture,
            new Rect(
                0,
                0,
                FlipX ? -finalTexture.Width : finalTexture.Width,
                FlipY ? -finalTexture.Height : finalTexture.Height
            ),
            new Rect(
                position.X,
                position.Y,
                finalTexture.Width * _transformComponent!.Scale.X,
                finalTexture.Height * _transformComponent.Scale.Y
            ),
            new Vec2(
                finalTexture.Width / 2f * _transformComponent.Scale.X,
                finalTexture.Height / 2f * _transformComponent.Scale.Y
            ),
            _transformComponent.Rotation,
            TintColor,
            InstructionSource.Entity,
            _transformComponent.ZLayer + ZLayerOffset
        );
    }
}
