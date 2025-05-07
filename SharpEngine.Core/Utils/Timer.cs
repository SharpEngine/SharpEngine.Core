using System;

namespace SharpEngine.Core.Utils;

/// <summary>
/// Class which represents Timer
/// </summary>
/// <param name="startTime">Time of Timer</param>
/// <param name="endFunction">Function which be called at the end</param>
/// <param name="loop">True if timer will loop</param>
public class Timer(float startTime, Action endFunction, bool loop = false)
{
    /// <summary>
    /// Start Time of Timer
    /// </summary>
    public float StartTime { get; set; } = startTime;

    /// <summary>
    /// Current Time of Timer
    /// </summary>
    public float CurrentTime { get; set; } = startTime;

    /// <summary>
    /// Function which be called when time is ended
    /// </summary>
    public Action EndFunction { get; set; } = endFunction;

    /// <summary>
    /// True if timer will loop
    /// </summary>
    public bool Loop { get; set; } = loop;

    /// <summary>
    /// Update Timer with Time
    /// </summary>
    /// <param name="delta">Passed Time</param>
    /// <returns>True if ended, else false</returns>
    public bool Update(float delta)
    {
        CurrentTime -= delta;
        if (CurrentTime <= 0)
        {
            EndFunction();
            if (Loop)
                CurrentTime = StartTime;
            else
                return true;
        }

        return false;
    }
}
