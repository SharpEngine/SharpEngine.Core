using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharpEngine.Core.Utils.Tween;

/// <summary>
/// Represents an animation or value interpolation that transitions smoothly between states over time.
/// </summary>
/// <remarks>A Tween is typically used to animate properties or values gradually, such as moving
/// an object, fading colors, or scaling elements. The specific behavior and configuration of the tween depend on
/// the implementation and usage context.</remarks>
public class Tween
{
    /// <summary>
    /// Steps of Tween
    /// </summary>
    [UsedImplicitly]
    public List<TweenStep> Steps { get; set; }

    /// <summary>
    /// Function which be called when Tween ends
    /// </summary>
    [UsedImplicitly]
    public Action? EndFunction { get; set; }

    /// <summary>
    /// Gets or sets the zero-based index of the current step in the workflow.
    /// </summary>
    [UsedImplicitly]
    public int CurrentStepIndex { get; set; }

    /// <summary>
    /// Initializes a new instance of the Tween class with the specified steps and an optional end function.
    /// </summary>
    /// <param name="steps">Steps</param>
    /// <param name="endFunction">End Function</param>
    public Tween(List<TweenStep> steps, Action? endFunction = null)
    {
        Steps = steps;
        EndFunction = endFunction;
        if (Steps.Count > 0)
            Steps[0].Launch();
    }

    /// <summary>
    /// Updates the tween based on the elapsed time since the last update.
    /// </summary>
    /// <param name="deltaTime">Delta Time</param>
    /// <returns>If the tween has completed</returns>
    public bool Update(float deltaTime)
    {
        if (Steps.Count == 0)
            return true;
        var currentStep = Steps[CurrentStepIndex];
        if (!currentStep.Update(deltaTime)) return false;
        
        CurrentStepIndex++;
        if (CurrentStepIndex >= Steps.Count)
        {
            EndFunction?.Invoke();
            return true;
        }
        Steps[CurrentStepIndex].Launch();
        return false;
    }
}