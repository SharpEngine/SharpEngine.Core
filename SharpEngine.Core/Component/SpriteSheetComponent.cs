using System.Collections.Generic;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;
using Color = SharpEngine.Core.Utils.Color;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component which draw animations with sprite sheet
/// </summary>
public class SpriteSheetComponent : Component
{
    /// <summary>
    /// Name of Texture
    /// </summary>
    public string Texture { get; set; }

    /// <summary>
    /// Size of one frame
    /// </summary>
    public Vec2 SpriteSize { get; set; }

    /// <summary>
    /// List of Animations
    /// </summary>
    public List<Animation> Animations { get; set; }

    /// <summary>
    /// If component is displayed
    /// </summary>
    public bool Displayed { get; set; }

    /// <summary>
    /// Offset
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
    /// Offset of ZLayer of Sprite Sheet
    /// </summary>
    public int ZLayerOffset { get; set; }

    /// <summary>
    /// Current animation
    /// </summary>
    public string Anim
    {
        get => _currentAnim;
        set
        {
            _currentAnim = value;
            _currentImage = 0;
            _internalTimer = GetAnimation(_currentAnim)?.Timer ?? 0;
        }
    }

    private string _currentAnim;
    private int _currentImage;
    private float _internalTimer;

    private TransformComponent? _transform;

    /// <summary>
    /// Create Sprite Sheet Component
    /// </summary>
    /// <param name="texture">Texture Name</param>
    /// <param name="spriteSize">Frame Size</param>
    /// <param name="animations">Animation List</param>
    /// <param name="currentAnim">Current Animation</param>
    /// <param name="displayed">If Displayed</param>
    /// <param name="offset">Offset</param>
    /// <param name="flipX">If Sprite is Flip Horizontally</param>
    /// <param name="flipY">If Sprite is Flip Vertically</param>
    /// <param name="zLayerOffset">Offset of zLayer</param>
    public SpriteSheetComponent(
        string texture,
        Vec2 spriteSize,
        List<Animation> animations,
        string currentAnim = "",
        bool displayed = true,
        Vec2? offset = null,
        bool flipX = false,
        bool flipY = false,
        int zLayerOffset = 0
    )
    {
        Texture = texture;
        SpriteSize = spriteSize;
        Animations = animations;
        _currentAnim = currentAnim;
        Displayed = displayed;
        Offset = offset ?? Vec2.Zero;
        FlipX = flipX;
        FlipY = flipY;
        ZLayerOffset = zLayerOffset;
    }

    /// <summary>
    /// Return Animation by name
    /// </summary>
    /// <param name="name">Animation Name</param>
    /// <returns>Animation or null</returns>
    public Animation? GetAnimation(string name)
    {
        foreach (var animation in Animations)
        {
            if (animation.Name == name)
                return animation;
        }

        return null;
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();
        _transform = Entity?.GetComponentAs<TransformComponent>();
    }

    /// <inheritdoc />
    public override void Update(float delta)
    {
        var anim = GetAnimation(_currentAnim);

        if (anim == null)
            return;

        if (_internalTimer <= 0)
        {
            if (_currentImage >= anim.Value.Indices.Count - 1)
                _currentImage = 0;
            else
                _currentImage++;
            _internalTimer = anim.Value.Timer;
        }

        _internalTimer -= delta;
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        var window = Entity?.Scene?.Window;
        var anim = GetAnimation(_currentAnim);

        if (
            _transform == null
            || !Displayed
            || Texture.Length <= 0
            || SpriteSize == Vec2.Zero
            || anim == null
            || window == null
        )
            return;

        var texture = window.TextureManager.GetTexture(Texture);
        var position = _transform.GetTransformedPosition(Offset);
        SERender.DrawTexture(
            texture,
            new Rect(
                SpriteSize.X * (anim.Value.Indices[_currentImage] % (texture.Width / SpriteSize.X)),
                SpriteSize.Y
                    * (int)(
                        anim.Value.Indices[_currentImage] / (int)(texture.Width / SpriteSize.X)
                    ),
                FlipX ? -SpriteSize.X : SpriteSize.X,
                FlipY ? -SpriteSize.Y : SpriteSize.Y
            ),
            new Rect(
                position.X,
                position.Y,
                SpriteSize.X * _transform.Scale.X,
                SpriteSize.Y * _transform.Scale.Y
            ),
            new Vec2(
                SpriteSize.X / 2f * _transform.Scale.X,
                SpriteSize.Y / 2f * _transform.Scale.Y
            ),
            _transform.Rotation,
            Color.White,
            InstructionSource.Entity,
            _transform.ZLayer + ZLayerOffset
        );
    }
}
