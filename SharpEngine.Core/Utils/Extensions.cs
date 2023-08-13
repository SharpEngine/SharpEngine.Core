using Raylib_cs;
using SharpEngine.Core.Utils.Input;
using MouseButton = SharpEngine.Core.Utils.Input.MouseButton;

namespace SharpEngine.Core.Utils;

internal static class Extensions
{
    #region Input Converter
    
    public static Raylib_cs.MouseButton ToRayLib(this Input.MouseButton button) => (Raylib_cs.MouseButton)button;
    public static KeyboardKey ToRayLib(this Key key) => (KeyboardKey)key;
    public static GamepadButton ToRayLib(this GamePadButton button) => (GamepadButton)button;
    public static GamepadAxis ToRayLib(this GamePadAxis axis) => (GamepadAxis)axis;
    
    public static Input.MouseButton ToSe(this Raylib_cs.MouseButton button) => (Input.MouseButton)button;
    public static Key ToSe(this KeyboardKey key) => (Key)key;
    public static GamePadButton ToSe(this GamepadButton button) => (GamePadButton)button;
    public static GamePadAxis ToSe(this GamepadAxis axis) => (GamePadAxis)axis;

    #endregion

    #region LogLevel Converter
    
    public static TraceLogLevel ToRayLib(this LogLevel logLevel) => (TraceLogLevel)logLevel;
    public static LogLevel ToSe(this TraceLogLevel logLevel) => (LogLevel)logLevel;
    
    #endregion
}