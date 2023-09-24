using System;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component which display sprite
/// </summary>
public class SpriteComponent : Component
{
    /// <summary>
    /// Name of Texture which be displayed
    /// </summary>
    public string Texture { get; set; }

    /// <summary>
    /// Define if Sprite is displayed
    /// </summary>
    public bool Displayed { get; set; }

    /// <summary>
    /// Offset of Sprite
    /// </summary>
    public Vec2 Offset { get; set; }

    /// <summary>
    /// If Sprite is Flip Horizontally
    /// </summary>
    public bool FlipX { get; set; }

    /// <summary>
    /// If Sprite is Flip Vertically
    /// </summary>
    public bool FlipY { get; set; }

    /// <summary>
    /// Offset of ZLayer of Sprite
    /// </summary>
    public int ZLayerOffset { get; set; }

    /// <summary>
    /// Shader of Sprite
    /// </summary>
    public string Shader { get; set; }

    private TransformComponent? _transformComponent;

    /// <summary>
    /// Create SpriteComponent
    /// </summary>
    /// <param name="texture">Name Texture Displayed</param>
    /// <param name="displayed">If Texture is Displayed (true)</param>
    /// <param name="offset">Offset (Vec2(0))</param>
    /// <param name="flipX">If Sprite is Flip Horizontally</param>
    /// <param name="flipY">If Sprite is Flip Vertically</param>
    /// <param name="zLayerOffset">Offset of zLayer</param>
    /// <param name="shader">Sprite Shader ("")</param>
    public SpriteComponent(
        string texture,
        bool displayed = true,
        Vec2? offset = null,
        bool flipX = false,
        bool flipY = false,
        int zLayerOffset = 0,
        string shader = ""
    )
    {
        Texture = texture;
        Displayed = displayed;
        Offset = offset ?? Vec2.Zero;
        FlipX = flipX;
        FlipY = flipY;
        ZLayerOffset = zLayerOffset;
        Shader = shader;
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

        if (_transformComponent == null || !Displayed || Texture.Length <= 0 || window == null)
            return;

        var texture = window.TextureManager.GetTexture(Texture);
        var position = _transformComponent.GetTransformedPosition(Offset);
        if (Shader == "")
        {
            SERender.DrawTexture(
                texture,
                new Rect(
                    0,
                    0,
                    FlipX ? -texture.width : texture.width,
                    FlipY ? -texture.height : texture.height
                ),
                new Rect(
                    position.X,
                    position.Y,
                    texture.width * _transformComponent.Scale.X,
                    texture.height * _transformComponent.Scale.Y
                ),
                new Vec2(
                    texture.width / 2f * _transformComponent.Scale.X,
                    texture.height / 2f * _transformComponent.Scale.Y
                ),
                _transformComponent.Rotation,
                Utils.Color.White,
                InstructionSource.Entity,
                _transformComponent.ZLayer + ZLayerOffset
            );
        }
        else
        {
            var shader = window.ShaderManager.GetShader(Shader);
            SERender.ShaderMode(
                shader,
                InstructionSource.Entity,
                _transformComponent.ZLayer + ZLayerOffset,
                () =>
                {
                    SERender.DrawTexture(
                        texture,
                        new Rect(
                            0,
                            0,
                            FlipX ? -texture.width : texture.width,
                            FlipY ? -texture.height : texture.height
                        ),
                        new Rect(
                            position.X,
                            position.Y,
                            texture.width * _transformComponent.Scale.X,
                            texture.height * _transformComponent.Scale.Y
                        ),
                        new Vec2(
                            texture.width / 2f * _transformComponent.Scale.X,
                            texture.height / 2f * _transformComponent.Scale.Y
                        ),
                        _transformComponent.Rotation,
                        Utils.Color.White,
                        InstructionSource.Entity,
                        _transformComponent.ZLayer + ZLayerOffset
                    );
                }
            );
        }
    }
}
