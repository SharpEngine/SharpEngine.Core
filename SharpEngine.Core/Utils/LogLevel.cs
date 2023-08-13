namespace SharpEngine.Core.Utils;

/// <summary>
/// Enum which represents Log Levels
/// </summary>
public enum LogLevel
{
    /// <summary>
    /// Display all logs
    /// </summary>
    LogAll,
    
    /// <summary>
    /// Trace logging, intended for internal use only
    /// </summary>
    LogTrace,
    
    /// <summary>
    /// Debug logging, used for internal debugging, it should be disabled on release builds
    /// </summary>
    LogDebug,
    
    /// <summary>
    /// Info logging, used for program execution info
    /// </summary>
    LogInfo,
    
    /// <summary>
    /// Warning logging, used on recoverable failures
    /// </summary>
    LogWarning,
    
    /// <summary>
    /// Error logging, used on unrecoverable failures
    /// </summary>
    LogError,
    
    /// <summary>
    /// Fatal logging, used to abort program: exit(EXIT_FAILURE)
    /// </summary>
    LogFatal,
    
    /// <summary>
    /// Disable logging
    /// </summary>
    LogNone,
}