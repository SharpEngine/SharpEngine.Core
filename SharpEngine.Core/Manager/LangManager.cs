using System.Collections.Generic;
using SharpEngine.Core.Data.Lang;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Manager;

/// <summary>
/// Static Class which manage Langs
/// </summary>
public static class LangManager
{
    /// <summary>
    /// Current Language Used
    /// </summary>
    public static string CurrentLang = "default";

    /// <summary>
    /// List of known languages
    /// </summary>
    public static List<string> Langs => new(Languages.Keys);

    /// <summary>
    /// Add Language to Manager
    /// </summary>
    /// <param name="name">Language Name</param>
    /// <param name="lang">Language Object</param>
    public static void AddLang(string name, ILang lang)
    {
        if (!Languages.TryAdd(name, lang))
            DebugManager.Log(LogLevel.LogWarning, $"SE_LANGMANAGER: Lang already exist : {name}");
    }

    /// <summary>
    /// Get Translation with current language
    /// </summary>
    /// <param name="key">Translation Key</param>
    /// <param name="defaultTranslation">Default Translation</param>
    /// <returns>Translation or Default Translation if key or language not found</returns>
    public static string GetTranslation(string key, string defaultTranslation) =>
        GetTranslation(key, defaultTranslation, CurrentLang);

    /// <summary>
    /// Get Translation with specified language
    /// </summary>
    /// <param name="key">Translation Key</param>
    /// <param name="defaultTranslation">Default Translation</param>
    /// <param name="lang">Language Name</param>
    /// <returns>Translation or Default Translation if key or language not found</returns>
    public static string GetTranslation(string key, string defaultTranslation, string lang)
    {
        if (Languages.TryGetValue(lang, out var translation))
            return translation.GetTranslation(key, defaultTranslation);

        if (lang != "default")
            DebugManager.Log(
                LogLevel.LogWarning,
                $"SE_LANGMANAGER: Lang not found : {lang} (pass default translation)"
            );
        return defaultTranslation;
    }

    private static readonly Dictionary<string, ILang> Languages = new();
}
