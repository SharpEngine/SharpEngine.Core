using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace SharpEngine.Core.Utils.Tween;

/// <summary>
/// Represents a single step in a tween animation sequence.
/// </summary>
/// <remarks>A TweenStep typically encapsulates the logic for updating a portion of an animation over
/// time. Instances of this class are commonly used within tweening frameworks to manage incremental changes to
/// animated properties.</remarks>
public class TweenStep
{
    /// <summary>
    /// Duration of the step
    /// </summary>
    [UsedImplicitly]
    public float CurrentDuration { get; set; }

    private readonly List<TweenData<float, float>> _floatTweens = [];
    private readonly List<TweenData<int, int>> _intTweens = [];
    private readonly List<TweenData<Color, ColorStep>> _colorTweens = [];

    /// <summary>
    /// Initializes a new instance of the TweenStep class with the specified duration.
    /// </summary>
    /// <param name="duration">The duration, in seconds, for this tween step. Must be greater than or equal to zero.</param>
    public TweenStep(float duration)
    {
        CurrentDuration = duration;
    }

    /// <summary>
    /// Add a float tween to the step
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <param name="property">Property which be modified</param>
    /// <param name="to">Target Value</param>
    /// <param name="duration">Duration of the tween</param>
    /// <param name="from">Source Value</param>
    /// <returns>Tween Step</returns>
    public TweenStep Float(object entity, Expression<Func<object, float>> property, float to, float duration, float? from = null)
    {
        _floatTweens.Add(new TweenData<float, float>(entity, property, from ?? 0, to, duration, from is not null));
        return this;
    }

    /// <summary>
    /// Add an int tween to the step
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <param name="property">Property which be modified</param>
    /// <param name="to">Target Value</param>
    /// <param name="duration">Duration of the tween</param>
    /// <param name="from">Source Value</param>
    /// <returns>Tween Steps</returns>
    [UsedImplicitly]
    public TweenStep Int(object entity, Expression<Func<object, int>> property, int to, float duration, int? from = null)
    {
        _intTweens.Add(new TweenData<int, int>(entity, property, from ?? 0, to, duration, from is not null));
        return this;
    }

    /// <summary>
    /// Animates a color property of the specified entity from a starting value to a target value over the given
    /// duration.
    /// </summary>
    /// <param name="entity">The entity whose color property will be animated.</param>
    /// <param name="property">An expression that selects the color property of the entity to animate.</param>
    /// <param name="to">The target color value to animate to.</param>
    /// <param name="duration">The duration, in seconds, over which the animation occurs. Must be greater than zero.</param>
    /// <param name="from">The initial color value to animate from. If null, the current value of the property is used.</param>
    /// <returns>The current <see cref="TweenStep"/> instance, allowing for method chaining.</returns>
    [UsedImplicitly]
    public TweenStep Color(object entity, Expression<Func<object, Color>> property, Color to, float duration, Color? from = null)
    {
        _colorTweens.Add(new TweenData<Color, ColorStep>(entity, property, from ?? Utils.Color.White, to, duration, from is not null));
        return this;
    }

    /// <summary>
    /// Update the tween step
    /// </summary>
    /// <param name="deltaTime">Delta time</param>
    /// <returns>If a step ended</returns>
    public bool Update(float deltaTime)
    {
        CurrentDuration -= deltaTime;
        foreach (var floatTween in _floatTweens)
            floatTween.Update(deltaTime);
        foreach (var intTween in _intTweens)
            intTween.Update(deltaTime);
        foreach (var colorTween in _colorTweens)
            colorTween.Update(deltaTime);
        return CurrentDuration <= 0f;
    }

    internal void Launch()
    {
        foreach (var floatTween in _floatTweens)
            floatTween.Launch();
        foreach (var intTween in _intTweens)
            intTween.Launch();
        foreach (var colorTween in _colorTweens)
            colorTween.Launch();
    }
}
