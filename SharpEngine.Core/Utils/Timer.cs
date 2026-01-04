using System;
using JetBrains.Annotations;

namespace SharpEngine.Core.Utils;

/// <summary>
/// Class which represents Timer
/// </summary>
/// <param name="startTime">Time of Timer</param>
/// <param name="endFunction">Function which be called at the end</param>
/// <param name="loop">True if timer will loop</param>
[UsedImplicitly]
public class Timer(float startTime, Action endFunction, bool loop = false)
{
    /// <summary>
    /// Start Time of Timer
    /// </summary>
    [UsedImplicitly]
    public float StartTime { get; set; } = startTime;

    /// <summary>
    /// Current Time of Timer
    /// </summary>
    [UsedImplicitly]
    public float CurrentTime { get; set; } = startTime;

    /// <summary>
    /// Function which be called when time is ended
    /// </summary>
    [UsedImplicitly]
    public Action EndFunction { get; set; } = endFunction;

    /// <summary>
    /// True if timer will loop
    /// </summary>
    [UsedImplicitly]
    public bool Loop { get; set; } = loop;

    /// <summary>
    /// Update Timer with Time
    /// </summary>
    /// <param name="delta">Passed Time</param>
    /// <returns>True if ended, else false</returns>
    public bool Update(float delta)
    {
        CurrentTime -= delta;
        if (!(CurrentTime <= 0)) return false;
        
        EndFunction();
        if (!Loop) return true;
        
        CurrentTime = StartTime;
        return false;
    }
}
