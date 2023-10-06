using System;

namespace SharpEngine.Core.Utils;

/// <summary>
/// Class which represents Timer
/// </summary>
public class Timer
{
    /// <summary>
    /// Start Time of Timer
    /// </summary>
    public float StartTime { get; set; }

    /// <summary>
    /// Current Time of Timer
    /// </summary>
    public float CurrentTime { get; set; }

    /// <summary>
    /// Function which be called when time is ended
    /// </summary>
    public Action EndFunction { get; set; }

    /// <summary>
    /// Create Timer
    /// </summary>
    /// <param name="startTime">Time of Timer</param>
    /// <param name="endFunction">Function which be called at the end</param>
    public Timer(float startTime, Action endFunction)
    {
        StartTime = startTime;
        CurrentTime = startTime;
        EndFunction = endFunction;
    }

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
            return true;
        }

        return false;
    }
}
