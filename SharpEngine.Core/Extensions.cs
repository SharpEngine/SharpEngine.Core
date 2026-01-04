using JetBrains.Annotations;
using Raylib_cs;
using SharpEngine.Core.Input;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core;

internal static class Extensions
{
    public static Raylib_cs.MouseButton ToRayLib(this Input.MouseButton button) =>
        (Raylib_cs.MouseButton)button;

    public static KeyboardKey ToRayLib(this Key key) => (KeyboardKey)key;

    public static GamepadButton ToRayLib(this GamePadButton button) => (GamepadButton)button;

    public static GamepadAxis ToRayLib(this GamePadAxis axis) => (GamepadAxis)axis;

    public static TraceLogLevel ToRayLib(this LogLevel logLevel) => (TraceLogLevel)logLevel;

    [UsedImplicitly]
    public static Input.MouseButton ToSe(this Raylib_cs.MouseButton button) =>
        (Input.MouseButton)button;

    [UsedImplicitly]
    public static Key ToSe(this KeyboardKey key) => (Key)key;

    [UsedImplicitly]
    public static GamePadButton ToSe(this GamepadButton button) => (GamePadButton)button;

    [UsedImplicitly]
    public static GamePadAxis ToSe(this GamepadAxis axis) => (GamePadAxis)axis;

    [UsedImplicitly]
    public static LogLevel ToSe(this TraceLogLevel logLevel) => (LogLevel)logLevel;
}
