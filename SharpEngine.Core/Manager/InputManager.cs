using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Raylib_cs;
using SharpEngine.Core.Input;
using SharpEngine.Core.Math;
using MouseButton = SharpEngine.Core.Input.MouseButton;

namespace SharpEngine.Core.Manager;

/// <summary>
/// Class which manage input interactions
/// </summary>
public static class InputManager
{
    internal static List<int> InternalPressedChars { get; } = [];
    private static List<int> InternalPressedKeys { get; } = [];
    internal static Window InternalWindow { get; set; } = null!;

    /// <summary>
    /// List of Pressed Chars in Frame
    /// </summary>
    public static List<char> GetPressedChars() => InternalPressedChars.Select(x => (char)x).ToList();

    /// <summary>
    /// List of Pressed Keys in Frame
    /// </summary>
    [UsedImplicitly]
    public static List<Key> GetPressedKeys() => InternalPressedKeys.Cast<Key>().ToList();

    /// <summary>
    /// Check if key is down
    /// </summary>
    /// <param name="key">Key</param>
    /// <returns>If Key is down</returns>
    public static bool IsKeyDown(Key key) => Raylib.IsKeyDown(key.ToRayLib());

    /// <summary>
    /// Check if key is up
    /// </summary>
    /// <param name="key">Key</param>
    /// <returns>If Key is up</returns>
    [UsedImplicitly]
    public static bool IsKeyUp(Key key) => Raylib.IsKeyUp(key.ToRayLib());

    /// <summary>
    /// Check if key is pressed
    /// </summary>
    /// <param name="key">Key</param>
    /// <returns>If Key is pressed</returns>
    public static bool IsKeyPressed(Key key) => Raylib.IsKeyPressed(key.ToRayLib());

    /// <summary>
    /// Check if key is released
    /// </summary>
    /// <param name="key">Key</param>
    /// <returns>If Key is released</returns>
    [UsedImplicitly]
    public static bool IsKeyReleased(Key key) => Raylib.IsKeyReleased(key.ToRayLib());

    /// <summary>
    /// Check if Mouse is in Rect
    /// </summary>
    /// <param name="rect">Rectangle</param>
    /// <returns>If Mouse is in Rect</returns>
    public static bool IsMouseInRectangle(Rect rect) => rect.Contains(GetMousePosition());

    /// <summary>
    /// Check if the mouse button is down
    /// </summary>
    /// <param name="button">Mouse button</param>
    /// <returns>If the mouse button is down</returns>
    public static bool IsMouseButtonDown(MouseButton button) =>
        Raylib.IsMouseButtonDown(button.ToRayLib());

    /// <summary>
    /// Check if the mouse button is up
    /// </summary>
    /// <param name="button">Mouse button</param>
    /// <returns>If the mouse button is up</returns>
    [UsedImplicitly]
    public static bool IsMouseButtonUp(MouseButton button) =>
        Raylib.IsMouseButtonUp(button.ToRayLib());

    /// <summary>
    /// Check if the mouse button is pressed
    /// </summary>
    /// <param name="button">Mouse button</param>
    /// <returns>If the mouse button is pressed</returns>
    public static bool IsMouseButtonPressed(MouseButton button) =>
        Raylib.IsMouseButtonPressed(button.ToRayLib());

    /// <summary>
    /// Check if the mouse button is released
    /// </summary>
    /// <param name="button">Mouse button</param>
    /// <returns>If the mouse button is released</returns>
    [UsedImplicitly]
    public static bool IsMouseButtonReleased(MouseButton button) =>
        Raylib.IsMouseButtonReleased(button.ToRayLib());

    /// <summary>
    /// Get Mouse position
    /// </summary>
    /// <returns>Position</returns>
    public static Vec2 GetMousePosition()
    {
        var realPosition = Raylib.GetMousePosition();
        return new Vec2(
            (realPosition.X - (InternalWindow.ScreenSize.X - (InternalWindow.RenderSize.X * InternalWindow.RenderScale)) * 0.5f) / InternalWindow.RenderScale,
            (realPosition.Y - (InternalWindow.ScreenSize.Y - (InternalWindow.RenderSize.Y * InternalWindow.RenderScale)) * 0.5f) / InternalWindow.RenderScale
        );
    }

    /// <summary>
    /// Get Real Mouse position
    /// </summary>
    /// <returns>Position</returns>
    [UsedImplicitly]
    public static Vec2 GetRealMousePosition() => Raylib.GetMousePosition();

    /// <summary>
    /// Set Mouse Position
    /// </summary>
    /// <param name="position">Position</param>
    public static void SetMousePosition(Vec2 position)
    {
        var x = position.X * InternalWindow.RenderScale + (InternalWindow.ScreenSize.X - (InternalWindow.RenderSize.X * InternalWindow.RenderScale)) * 0.5f;
        var y = position.Y * InternalWindow.RenderScale + (InternalWindow.ScreenSize.Y - (InternalWindow.RenderSize.Y * InternalWindow.RenderScale)) * 0.5f;
        Raylib.SetMousePosition((int)x, (int)y);
    }

    /// <summary>
    /// Set Real Mouse Position
    /// </summary>
    /// <param name="position">Position</param>
    [UsedImplicitly]
    public static void SetRealMousePosition(Vec2 position) =>
        Raylib.SetMousePosition((int)position.X, (int)position.Y);

    /// <summary>
    /// Get Mouse Wheel Movement Value
    /// </summary>
    /// <returns>Value</returns>
    public static float GetMouseWheelMove() => Raylib.GetMouseWheelMove();

    /// <summary>
    /// Check if the gamepad is connected
    /// </summary>
    /// <param name="index">Index of Gamepad</param>
    /// <returns>If the gamepad is connected</returns>
    [UsedImplicitly]
    public static bool IsGamePadConnected(int index) => Raylib.IsGamepadAvailable(index);

    /// <summary>
    /// Check if the gamepad button is down
    /// </summary>
    /// <param name="index">Index of Gamepad</param>
    /// <param name="button">Button of Gamepad</param>
    /// <returns>If the gamepad button is down</returns>
    [UsedImplicitly]
    public static bool IsGamePadButtonDown(int index, GamePadButton button) =>
        Raylib.IsGamepadButtonDown(index, button.ToRayLib());

    /// <summary>
    /// Check if the gamepad button is up
    /// </summary>
    /// <param name="index">Index of Gamepad</param>
    /// <param name="button">Button of Gamepad</param>
    /// <returns>If the gamepad button is up</returns>
    [UsedImplicitly]
    public static bool IsGamePadButtonUp(int index, GamePadButton button) =>
        Raylib.IsGamepadButtonUp(index, button.ToRayLib());

    /// <summary>
    /// Check if the gamepad button is pressed
    /// </summary>
    /// <param name="index">Index of Gamepad</param>
    /// <param name="button">Button of Gamepad</param>
    /// <returns>If the gamepad button is pressed</returns>
    [UsedImplicitly]
    public static bool IsGamePadButtonPressed(int index, GamePadButton button) =>
        Raylib.IsGamepadButtonPressed(index, button.ToRayLib());

    /// <summary>
    /// Check if the gamepad button is released
    /// </summary>
    /// <param name="index">Index of Gamepad</param>
    /// <param name="button">Button of Gamepad</param>
    /// <returns>If the gamepad button is released</returns>
    [UsedImplicitly]
    public static bool IsGamePadButtonReleased(int index, GamePadButton button) =>
        Raylib.IsGamepadButtonReleased(index, button.ToRayLib());

    /// <summary>
    /// Get Gamepad axis value
    /// </summary>
    /// <param name="index">Index of Gamepad</param>
    /// <param name="axis">Axis of Gamepad</param>
    /// <returns>Value</returns>
    public static float GetGamePadAxis(int index, GamePadAxis axis) =>
        Raylib.GetGamepadAxisMovement(index, axis.ToRayLib());

    /// <summary>
    /// Updates the input.
    /// </summary>
    internal static void UpdateInput()
    {
        InternalPressedChars.Clear();
        InternalPressedKeys.Clear();

        var key = Raylib.GetKeyPressed();
        while (key > 0)
        {
            InternalPressedKeys.Add(key);
            key = Raylib.GetKeyPressed();
        }

        var charGot = Raylib.GetCharPressed();
        while (charGot > 0)
        {
            InternalPressedChars.Add(charGot);
            charGot = Raylib.GetCharPressed();
        }
    }
}
