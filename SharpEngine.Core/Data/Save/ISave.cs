using JetBrains.Annotations;

namespace SharpEngine.Core.Data.Save;

/// <summary>
/// Interface which represents Save
/// </summary>
public interface ISave
{
    /// <summary>
    /// Read Save
    /// </summary>
    /// <param name="file">Save File</param>
    [UsedImplicitly]
    public void Read(string file);

    /// <summary>
    /// Write Save
    /// </summary>
    /// <param name="file">Save File</param>
    [UsedImplicitly]
    public void Write(string file);

    /// <summary>
    /// Get Object from Save
    /// </summary>
    /// <param name="key">Object Key</param>
    /// <param name="defaultValue">Default Value</param>
    /// <returns>Object or Default value if key not found</returns>
    [UsedImplicitly]
    public object GetObject(string key, object defaultValue);

    /// <summary>
    /// Get Object from Save and cast it
    /// </summary>
    /// <param name="key">Object Key</param>
    /// <param name="defaultValue">Default Value</param>
    /// <typeparam name="T">Type of Object</typeparam>
    /// <returns>Object or Default value if key not found</returns>
    [UsedImplicitly]
    public T GetObjectAs<T>(string key, T defaultValue);

    /// <summary>
    /// Define Object
    /// </summary>
    /// <param name="key">Object Key</param>
    /// <param name="value">Object</param>
    [UsedImplicitly]
    public void SetObject(string key, object value);
}
