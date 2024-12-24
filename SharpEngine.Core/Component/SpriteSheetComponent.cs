using System;
using System.Collections.Generic;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;
using Color = SharpEngine.Core.Utils.Color;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component which draw animations with sprite sheet
/// </summary>
/// <param name="texture">Texture Name</param>
/// <param name="spriteSize">Frame Size</param>
/// <param name="animations">Animation List</param>
/// <param name="currentAnim">Current Animation</param>
/// <param name="displayed">If Displayed</param>
/// <param name="offset">Offset</param>
/// <param name="zLayerOffset">Offset of zLayer</param>
public class SpriteSheetComponent(
    string texture,
    Vec2 spriteSize,
    List<Animation> animations,
    string currentAnim = "",
    bool displayed = true,
    Vec2? offset = null,
    int zLayerOffset = 0
) : Component
{
    /// <summary>
    /// Name of Texture
    /// </summary>
    public string Texture { get; set; } = texture;

    /// <summary>
    /// Size of one frame
    /// </summary>
    public Vec2 SpriteSize { get; set; } = spriteSize;

    /// <summary>
    /// List of Animations
    /// </summary>
    public List<Animation> Animations { get; set; } = animations;

    /// <summary>
    /// If component is displayed
    /// </summary>
    public bool Displayed { get; set; } = displayed;

    /// <summary>
    /// Offset
    /// </summary>
    public Vec2 Offset { get; set; } = offset ?? Vec2.Zero;

    /// <summary>
    /// If Sprite is Flip Horizontally
    /// </summary>
    public bool FlipX { get; set; } = false;

    /// <summary>
    /// If Sprite is Flip Vertically
    /// </summary>
    public bool FlipY { get; set; } = false;

    /// <summary>
    /// Offset of ZLayer of Sprite Sheet
    /// </summary>
    public int ZLayerOffset { get; set; } = zLayerOffset;

    /// <summary>
    /// Current animation
    /// </summary>
    public string Anim
    {
        get => currentAnim;
        set
        {
            currentAnim = value;
            CurrentImage = 0;
            internalTimer = GetAnimation(currentAnim)?.Timer ?? 0;
        }
    }

    /// <summary>
    /// Current image index
    /// </summary>
    public int CurrentImage { get; protected set; }


    /// <summary>
    /// Event triggered when animation ends
    /// </summary>
    public EventHandler? AnimationEnded;

    /// <summary>
    /// Current internal animation
    /// </summary>
    protected string currentAnim = currentAnim;

    /// <summary>
    /// Current internal timer
    /// </summary>
    protected float internalTimer;

    private TransformComponent? _transform;

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

    /// <summary>
    /// Replay current animation
    /// </summary>
    public void Replay()
    {
        CurrentImage = 0;
        internalTimer = GetAnimation(currentAnim)?.Timer ?? 0;
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
        var anim = GetAnimation(currentAnim);

        if (anim == null)
            return;

        if (internalTimer <= 0)
        {
            if(CurrentImage == -1)
                return;

            if (CurrentImage >= anim.Value.Indices.Count - 1)
            {
                AnimationEnded?.Invoke(this, EventArgs.Empty);
                if (anim.Value.Loop)
                    CurrentImage = 0;
                else
                    CurrentImage = -1;
            }
            else
                CurrentImage++;
            internalTimer = anim.Value.Timer;
        }

        internalTimer -= delta;
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        var window = Entity?.Scene?.Window;
        var anim = GetAnimation(currentAnim);

        if (
            _transform == null
            || !Displayed
            || Texture.Length <= 0
            || SpriteSize == Vec2.Zero
            || anim == null
            || window == null
        )
            return;

        var currentImage = CurrentImage == -1 ? anim.Value.Indices.Count - 1 : CurrentImage;
        var finalTexture = window.TextureManager.GetTexture(Texture);
        var position = _transform.GetTransformedPosition(Offset);
        SERender.DrawTexture(
            finalTexture,
            new Rect(
                SpriteSize.X * (anim.Value.Indices[currentImage] % (finalTexture.Width / SpriteSize.X)),
                SpriteSize.Y
                    * (int)(
                        anim.Value.Indices[currentImage] / (int)(finalTexture.Width / SpriteSize.X)
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
