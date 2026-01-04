using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Manager;

/// <summary>
/// Class which manage timer
/// </summary>
public class TimerManager
{
    private readonly Dictionary<string, Timer> _timers = [];

    /// <summary>
    /// Get All Timers
    /// </summary>
    public List<Timer> Timers => [.._timers.Values];

    /// <summary>
    /// Add Timer to manager
    /// </summary>
    /// <param name="name">Timer Name</param>
    /// <param name="timer">Timer</param>
    [UsedImplicitly]
    public void AddTimer(string name, Timer timer)
    {
        if (!_timers.TryAdd(name, timer))
            DebugManager.Log(LogLevel.Warning, $"SE_TIMERMANAGER: Timer already exist : {name}");
    }

    /// <summary>
    /// Get Timer from Manager
    /// </summary>
    /// <param name="name">Timer Name</param>
    /// <returns>Timer</returns>
    [UsedImplicitly]
    public Timer? GetTimer(string name) => _timers.GetValueOrDefault(name);

    internal void Update(float delta)
    {
        for (var i = _timers.Count - 1; i > -1; i--)
        {
            if (_timers.ElementAt(i).Value.Update(delta))
                _timers.Remove(_timers.ElementAt(i).Key);
        }
    }
}
