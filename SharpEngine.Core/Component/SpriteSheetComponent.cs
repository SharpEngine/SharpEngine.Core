using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;
using Color = SharpEngine.Core.Utils.Color;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component that draws animations with sprite sheet
/// </summary>
/// <param name="texture">Texture Name</param>
/// <param name="spriteSize">Frame Size</param>
/// <param name="animations">Animation List</param>
/// <param name="currentAnim">Current Animation</param>
/// <param name="displayed">If Displayed</param>
/// <param name="offset">Offset</param>
/// <param name="zLayerOffset">Offset of zLayer</param>
[UsedImplicitly]
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
    [UsedImplicitly]
    public string Texture { get; set; } = texture;

    /// <summary>
    /// Size of one frame
    /// </summary>
    [UsedImplicitly]
    public Vec2 SpriteSize { get; set; } = spriteSize;

    /// <summary>
    /// List of Animations
    /// </summary>
    [UsedImplicitly]
    public List<Animation> Animations { get; set; } = animations;

    /// <summary>
    /// If a component is displayed
    /// </summary>
    [UsedImplicitly]
    public bool Displayed { get; set; } = displayed;

    /// <summary>
    /// Offset
    /// </summary>
    [UsedImplicitly]
    public Vec2 Offset { get; set; } = offset ?? Vec2.Zero;

    /// <summary>
    /// If Sprite is Flip Horizontally
    /// </summary>
    [UsedImplicitly]
    public bool FlipX { get; set; }

    /// <summary>
    /// If Sprite is Flip Vertically
    /// </summary>
    [UsedImplicitly]
    public bool FlipY { get; set; }

    /// <summary>
    /// Offset of ZLayer of Sprite Sheet
    /// </summary>
    [UsedImplicitly]
    public int ZLayerOffset { get; set; } = zLayerOffset;

    /// <summary>
    /// Current animation
    /// </summary>
    [UsedImplicitly]
    public string Anim
    {
        get => CurrentAnim;
        set
        {
            CurrentAnim = value;
            CurrentImage = 0;
            InternalTimer = GetAnimation(CurrentAnim)?.Timer ?? 0;
        }
    }

    /// <summary>
    /// Current image index
    /// </summary>
    [UsedImplicitly]
    public int CurrentImage { get; protected set; }


    /// <summary>
    /// Event triggered when animation ends
    /// </summary>
    [UsedImplicitly]
    public EventHandler? AnimationEnded;

    /// <summary>
    /// Current internal animation
    /// </summary>
    [UsedImplicitly]
    protected string CurrentAnim = currentAnim;

    /// <summary>
    /// Current internal timer
    /// </summary>
    [UsedImplicitly]
    protected float InternalTimer;

    /// <summary>
    /// Represents the associated transform component for the current object, if available.
    /// </summary>
    [UsedImplicitly]
    protected TransformComponent? Transform;

    /// <summary>
    /// Return Animation by name
    /// </summary>
    /// <param name="name">Animation Name</param>
    /// <returns>Animation or null</returns>
    [UsedImplicitly]
    public Animation? GetAnimation(string name) => Animations.FirstOrDefault(animation => animation.Name == name);

    /// <summary>
    /// Replay current animation
    /// </summary>
    [UsedImplicitly]
    public void Replay()
    {
        CurrentImage = 0;
        InternalTimer = GetAnimation(CurrentAnim)?.Timer ?? 0;
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();
        Transform = Entity?.GetComponentAs<TransformComponent>();
    }

    /// <inheritdoc />
    public override void Update(float delta)
    {
        var anim = GetAnimation(CurrentAnim);

        if (anim == null)
            return;

        if (InternalTimer <= 0)
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
            InternalTimer = anim.Value.Timer;
        }

        InternalTimer -= delta;
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        var window = Entity?.Scene?.Window;
        var anim = GetAnimation(CurrentAnim);

        if (
            Transform == null
            || !Displayed
            || Texture.Length <= 0
            || SpriteSize == Vec2.Zero
            || anim == null
            || window == null
        )
            return;

        var currentImage = CurrentImage == -1 ? anim.Value.Indices.Count - 1 : CurrentImage;
        var finalTexture = window.TextureManager.GetTexture(Texture);
        var position = Transform.GetTransformedPosition(Offset);
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
                SpriteSize.X * Transform.Scale.X,
                SpriteSize.Y * Transform.Scale.Y
            ),
            new Vec2(
                SpriteSize.X / 2f * Transform.Scale.X,
                SpriteSize.Y / 2f * Transform.Scale.Y
            ),
            Transform.Rotation,
            Color.White,
            InstructionSource.Entity,
            Transform.ZLayer + ZLayerOffset
        );
    }
}
