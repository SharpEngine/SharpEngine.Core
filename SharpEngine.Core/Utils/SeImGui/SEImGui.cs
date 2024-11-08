using System;
using System.Collections.Generic;
using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using SharpEngine.Core.Input;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;

namespace SharpEngine.Core.Utils.SeImGui;

/// <summary>
/// Class which control ImGui
/// </summary>
public class SeImGui : IDisposable
{
    private readonly nint _context;
    private Texture2D _fontTexture;
    private readonly Vector2 _scaleFactor = Vector2.One;
    private readonly Dictionary<KeyboardKey, ImGuiKey> _keyMap = [];

    internal SeImGui()
    {
        _context = ImGui.CreateContext();
        ImGui.SetCurrentContext(_context);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc />
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            ImGui.DestroyContext(_context);
            Raylib.UnloadTexture(_fontTexture);
        }
    }

    /// <summary>
    /// Creates a texture and loads the font data from ImGui.
    /// </summary>
    public void Load(int width, int height)
    {
        Resize(width, height);
        LoadFontTexture();
        SetupInput();
        ImGui.NewFrame();
    }

    private unsafe void LoadFontTexture()
    {
        var io = ImGui.GetIO();

        // Load as RGBA 32-bit (75% of the memory is wasted, but default font is so small) because it is more likely to be compatible with user's existing shaders.
        // If your ImTextureId represent a higher-level concept than just a GL texture id, consider calling GetTexDataAsAlpha8() instead to save on GPU memory.
        io.Fonts.GetTexDataAsRGBA32(out byte* pixels, out var width, out var height);

        // Upload texture to graphics system
        var data = new nint(pixels);
        var image = new Image
        {
            Data = (void*)data,
            Width = width,
            Height = height,
            Mipmaps = 1,
            Format = PixelFormat.UncompressedR8G8B8A8,
        };
        _fontTexture = Raylib.LoadTextureFromImage(image);

        // Store texture id in imgui font
        io.Fonts.SetTexID(new nint(_fontTexture.Id));

        // Clears font data on the CPU side
        io.Fonts.ClearTexData();
    }

    private static void SetupInput()
    {
        var io = ImGui.GetIO();

        // Setup config flags
        io.ConfigFlags |= ImGuiConfigFlags.NavEnableKeyboard;
        io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;

        // Setup back-end capabilities flags
        io.BackendFlags |= ImGuiBackendFlags.HasMouseCursors;
        io.BackendFlags |= ImGuiBackendFlags.HasSetMousePos;

        _keyMap.Add(KeyboardKey.Apostrophe, ImGuiKey.Apostrophe);
        _keyMap.Add(KeyboardKey.Comma, ImGuiKey.Comma); _keyMap.Add(KeyboardKey.Minus, ImGuiKey.Minus);
        _keyMap.Add(KeyboardKey.Period, ImGuiKey.Period);
        _keyMap.Add(KeyboardKey.Slash, ImGuiKey.Slash);
        _keyMap.Add(KeyboardKey.Zero, ImGuiKey._0);
        _keyMap.Add(KeyboardKey.One, ImGuiKey._1);
        _keyMap.Add(KeyboardKey.Two, ImGuiKey._2);
        _keyMap.Add(KeyboardKey.Three, ImGuiKey._3);
        _keyMap.Add(KeyboardKey.Four, ImGuiKey._4);
        _keyMap.Add(KeyboardKey.Five, ImGuiKey._5);
        _keyMap.Add(KeyboardKey.Six, ImGuiKey._6);
        _keyMap.Add(KeyboardKey.Seven, ImGuiKey._7);
        _keyMap.Add(KeyboardKey.Eight, ImGuiKey._8);
        _keyMap.Add(KeyboardKey.Nine, ImGuiKey._9);
        _keyMap.Add(KeyboardKey.Semicolon, ImGuiKey.Semicolon);
        _keyMap.Add(KeyboardKey.Equal, ImGuiKey.Equal);
        _keyMap.Add(KeyboardKey.A, ImGuiKey.A);
        _keyMap.Add(KeyboardKey.B, ImGuiKey.B);
        _keyMap.Add(KeyboardKey.C, ImGuiKey.C);
        _keyMap.Add(KeyboardKey.D, ImGuiKey.D);
        _keyMap.Add(KeyboardKey.E, ImGuiKey.E);
        _keyMap.Add(KeyboardKey.F, ImGuiKey.F);
        _keyMap.Add(KeyboardKey.G, ImGuiKey.G);
        _keyMap.Add(KeyboardKey.H, ImGuiKey.H);
        _keyMap.Add(KeyboardKey.I, ImGuiKey.I);
        _keyMap.Add(KeyboardKey.J, ImGuiKey.J);
        _keyMap.Add(KeyboardKey.K, ImGuiKey.K);
        _keyMap.Add(KeyboardKey.L, ImGuiKey.L);
        _keyMap.Add(KeyboardKey.M, ImGuiKey.M);
        _keyMap.Add(KeyboardKey.N, ImGuiKey.N);
        _keyMap.Add(KeyboardKey.O, ImGuiKey.O);
        _keyMap.Add(KeyboardKey.P, ImGuiKey.P);
        _keyMap.Add(KeyboardKey.Q, ImGuiKey.Q);
        _keyMap.Add(KeyboardKey.R, ImGuiKey.R);
        _keyMap.Add(KeyboardKey.S, ImGuiKey.S);
        _keyMap.Add(KeyboardKey.T, ImGuiKey.T);
        _keyMap.Add(KeyboardKey.U, ImGuiKey.U);
        _keyMap.Add(KeyboardKey.V, ImGuiKey.V);
        _keyMap.Add(KeyboardKey.W, ImGuiKey.W);
        _keyMap.Add(KeyboardKey.X, ImGuiKey.X);
        _keyMap.Add(KeyboardKey.Y, ImGuiKey.Y);
        _keyMap.Add(KeyboardKey.Z, ImGuiKey.Z);
        _keyMap.Add(KeyboardKey.Space, ImGuiKey.Space);
        _keyMap.Add(KeyboardKey.Escape, ImGuiKey.Escape);
        _keyMap.Add(KeyboardKey.Enter, ImGuiKey.Enter);
        _keyMap.Add(KeyboardKey.Tab, ImGuiKey.Tab);
        _keyMap.Add(KeyboardKey.Backspace, ImGuiKey.Backspace);
        _keyMap.Add(KeyboardKey.Insert, ImGuiKey.Insert);
        _keyMap.Add(KeyboardKey.Delete, ImGuiKey.Delete);
        _keyMap.Add(KeyboardKey.Right, ImGuiKey.RightArrow);
        _keyMap.Add(KeyboardKey.Left, ImGuiKey.LeftArrow);
        _keyMap.Add(KeyboardKey.Down, ImGuiKey.DownArrow);
        _keyMap.Add(KeyboardKey.Up, ImGuiKey.UpArrow);
        _keyMap.Add(KeyboardKey.PageUp, ImGuiKey.PageUp);
        _keyMap.Add(KeyboardKey.PageDown, ImGuiKey.PageDown);
        _keyMap.Add(KeyboardKey.Home, ImGuiKey.Home);
        _keyMap.Add(KeyboardKey.End, ImGuiKey.End);
        _keyMap.Add(KeyboardKey.CapsLock, ImGuiKey.CapsLock);
        _keyMap.Add(KeyboardKey.ScrollLock, ImGuiKey.ScrollLock);
        _keyMap.Add(KeyboardKey.NumLock, ImGuiKey.NumLock);
        _keyMap.Add(KeyboardKey.PrintScreen, ImGuiKey.PrintScreen);
        _keyMap.Add(KeyboardKey.Pause, ImGuiKey.Pause);
        _keyMap.Add(KeyboardKey.F1, ImGuiKey.F1);
        _keyMap.Add(KeyboardKey.F2, ImGuiKey.F2);
        _keyMap.Add(KeyboardKey.F3, ImGuiKey.F3);
        _keyMap.Add(KeyboardKey.F4, ImGuiKey.F4);
        _keyMap.Add(KeyboardKey.F5, ImGuiKey.F5);
        _keyMap.Add(KeyboardKey.F6, ImGuiKey.F6);
        _keyMap.Add(KeyboardKey.F7, ImGuiKey.F7);
        _keyMap.Add(KeyboardKey.F8, ImGuiKey.F8);
        _keyMap.Add(KeyboardKey.F9, ImGuiKey.F9);
        _keyMap.Add(KeyboardKey.F10, ImGuiKey.F10);
        _keyMap.Add(KeyboardKey.F11, ImGuiKey.F11);
        _keyMap.Add(KeyboardKey.F12, ImGuiKey.F12);
        _keyMap.Add(KeyboardKey.LeftShift, ImGuiKey.LeftShift);
        _keyMap.Add(KeyboardKey.LeftControl, ImGuiKey.LeftCtrl);
        _keyMap.Add(KeyboardKey.LeftAlt, ImGuiKey.LeftAlt);
        _keyMap.Add(KeyboardKey.LeftSuper, ImGuiKey.LeftSuper);
        _keyMap.Add(KeyboardKey.RightShift, ImGuiKey.RightShift);
        _keyMap.Add(KeyboardKey.RightControl, ImGuiKey.RightCtrl);
        _keyMap.Add(KeyboardKey.RightAlt, ImGuiKey.RightAlt);
        _keyMap.Add(KeyboardKey.RightSuper, ImGuiKey.RightSuper);
        _keyMap.Add(KeyboardKey.KeyboardMenu, ImGuiKey.Menu);
        _keyMap.Add(KeyboardKey.LeftBracket, ImGuiKey.LeftBracket);
        _keyMap.Add(KeyboardKey.Backslash, ImGuiKey.Backslash);
        _keyMap.Add(KeyboardKey.RightBracket, ImGuiKey.RightBracket);
        _keyMap.Add(KeyboardKey.Grave, ImGuiKey.GraveAccent);
        _keyMap.Add(KeyboardKey.Kp0, ImGuiKey.Keypad0);
        _keyMap.Add(KeyboardKey.Kp1, ImGuiKey.Keypad1);
        _keyMap.Add(KeyboardKey.Kp2, ImGuiKey.Keypad2);
        _keyMap.Add(KeyboardKey.Kp3, ImGuiKey.Keypad3);
        _keyMap.Add(KeyboardKey.Kp4, ImGuiKey.Keypad4);
        _keyMap.Add(KeyboardKey.Kp5, ImGuiKey.Keypad5);
        _keyMap.Add(KeyboardKey.Kp6, ImGuiKey.Keypad6);
        _keyMap.Add(KeyboardKey.Kp7, ImGuiKey.Keypad7);
        _keyMap.Add(KeyboardKey.Kp8, ImGuiKey.Keypad8);
        _keyMap.Add(KeyboardKey.Kp9, ImGuiKey.Keypad9);
        _keyMap.Add(KeyboardKey.KpDecimal, ImGuiKey.KeypadDecimal);
        _keyMap.Add(KeyboardKey.KpDivide, ImGuiKey.KeypadDivide);
        _keyMap.Add(KeyboardKey.KpMultiply, ImGuiKey.KeypadMultiply);
        _keyMap.Add(KeyboardKey.KpSubtract, ImGuiKey.KeypadSubtract);
        _keyMap.Add(KeyboardKey.KpAdd, ImGuiKey.KeypadAdd);
        _keyMap.Add(KeyboardKey.KpEnter, ImGuiKey.KeypadEnter);
        _keyMap.Add(KeyboardKey.KpEqual, ImGuiKey.KeypadEqual);
    }

    /// <summary>
    /// Update imgui internals(input, frameData)
    /// </summary>
    /// <param name="dt"></param>
    public static void Update(float dt)
    {
        var io = ImGui.GetIO();

        io.DisplayFramebufferScale = Vector2.One;
        io.DeltaTime = dt;

        UpdateKeyboard();
        UpdateMouse();

        ImGui.NewFrame();
    }

    /// <summary>
    /// Resize imgui display
    /// </summary>
    public void Resize(int width, int height)
    {
        var io = ImGui.GetIO();
        io.DisplaySize = new Vector2(width, height) / _scaleFactor;
        DebugManager.Log(LogLevel.LogInfo, $"IMGUI: Display size {io.DisplaySize}");
    }

    private static void UpdateKeyboard()
    {
        var io = ImGui.GetIO();

        // Key states
        foreach(var keys in _keyMap)
        {
            if(Raylib.IsKeyPressed(keys.Key))
                io.AddKeyEvent(keys.Value, true);
            if (Raylib.IsKeyReleased(keys.Key))
                io.AddKeyEvent(keys.Value, false);
        }

        // Key input
        foreach (var charGot in InputManager.InternalPressedChars)
        {
            if (charGot != 0)
                io.AddInputCharacter((uint)charGot);
        }
    }

    private static void UpdateMouse()
    {
        var io = ImGui.GetIO();

        // Store button states
        for (var i = 0; i < io.MouseDown.Count; i++)
        {
            io.MouseDown[i] = Raylib.IsMouseButtonDown((Raylib_cs.MouseButton)i);
        }

        // Mouse scroll
        io.MouseWheel += Raylib.GetMouseWheelMove();

        // Mouse position
        var mousePosition = io.MousePos;
        bool focused = Raylib.IsWindowFocused();

        if (focused)
        {
            if (io.WantSetMousePos)
                Raylib.SetMousePosition((int)mousePosition.X, (int)mousePosition.Y);
            else
                io.MousePos = Raylib.GetMousePosition();
        }

        // Mouse cursor state
        if ((io.ConfigFlags & ImGuiConfigFlags.NoMouseCursorChange) == 0 || Raylib.IsCursorHidden())
        {
            var cursor = ImGui.GetMouseCursor();
            if (cursor == ImGuiMouseCursor.None || io.MouseDrawCursor)
                Raylib.HideCursor();
            else
                Raylib.ShowCursor();
        }
    }

    /// <summary>
    /// Gets the geometry as set up by ImGui and sends it to the graphics device
    /// </summary>
    public static void Draw()
    {
        ImGui.Render();
        RenderCommandLists(ImGui.GetDrawData());
    }

    // Returns a Color struct from hexadecimal value
    private static Raylib_cs.Color GetColor(uint hexValue)
    {
        Raylib_cs.Color color =
            new(
                (byte)(hexValue & 0xFF),
                (byte)(hexValue >> 8 & 0xFF),
                (byte)(hexValue >> 16 & 0xFF),
                (byte)(hexValue >> 24 & 0xFF)
            );
        return color;
    }

    private static void DrawTriangleVertex(ImDrawVertPtr idxVert)
    {
        var c = GetColor(idxVert.col);
        Rlgl.Color4ub(c.R, c.G, c.B, c.A);
        Rlgl.TexCoord2f(idxVert.uv.X, idxVert.uv.Y);
        Rlgl.Vertex2f(idxVert.pos.X, idxVert.pos.Y);
    }

    // Draw the imgui triangle data
    private static void DrawTriangles(
        uint count,
        int idxOffset,
        int vtxOffset,
        ImVector<ushort> idxBuffer,
        ImPtrVector<ImDrawVertPtr> idxVert,
        nint textureId
    )
    {
        if (Rlgl.CheckRenderBatchLimit((int)count * 3))
            Rlgl.DrawRenderBatchActive();

        Rlgl.Begin(DrawMode.Triangles); // RL_TRIANGLES
        Rlgl.SetTexture((uint)textureId);

        for (var i = 0; i <= count - 3; i += 3)
        {
            var index = idxBuffer[idxOffset + i];
            var vertex = idxVert[vtxOffset + index];
            DrawTriangleVertex(vertex);

            index = idxBuffer[idxOffset + i + 1];
            vertex = idxVert[vtxOffset + index];
            DrawTriangleVertex(vertex);

            index = idxBuffer[idxOffset + i + 2];
            vertex = idxVert[vtxOffset + index];
            DrawTriangleVertex(vertex);
        }
        Rlgl.End();
    }

    private static void RenderCommandLists(ImDrawDataPtr data)
    {
        // Scale coordinates for retina displays (screen coordinates != framebuffer coordinates)
        var fbWidth = (int)(data.DisplaySize.X * data.FramebufferScale.X);
        var fbHeight = (int)(data.DisplaySize.Y * data.FramebufferScale.Y);

        // Avoid rendering if display is minimized or if the command list is empty
        if (fbWidth <= 0 || fbHeight <= 0 || data.CmdListsCount == 0)
            return;

        Rlgl.DrawRenderBatchActive();
        Rlgl.DisableBackfaceCulling();
        Rlgl.EnableScissorTest();

        data.ScaleClipRects(ImGui.GetIO().DisplayFramebufferScale);

        for (var n = 0; n < data.CmdListsCount; n++)
        {
            var idxOffset = 0;
            var cmdList = data.CmdLists[n];

            // Vertex buffer and index buffer generated by DearImGui
            var vtxBuffer = cmdList.VtxBuffer;
            var idxBuffer = cmdList.IdxBuffer;

            for (var cmdi = 0; cmdi < cmdList.CmdBuffer.Size; cmdi++)
            {
                var pcmd = cmdList.CmdBuffer[cmdi];

                // Scissor rect
                var pos = data.DisplayPos;
                var rectX = (int)((pcmd.ClipRect.X - pos.X) * data.FramebufferScale.X);
                var rectY = (int)((pcmd.ClipRect.Y - pos.Y) * data.FramebufferScale.Y);
                var rectW = (int)((pcmd.ClipRect.Z - rectX) * data.FramebufferScale.Y);
                var rectH = (int)((pcmd.ClipRect.W - rectY) * data.FramebufferScale.Y);
                Rlgl.Scissor(rectX, Raylib.GetScreenHeight() - (rectY + rectH), rectW, rectH);

                if (pcmd.UserCallback != nint.Zero)
                    idxOffset += (int)pcmd.ElemCount;
                else
                {
                    DrawTriangles(
                        pcmd.ElemCount,
                        idxOffset,
                        (int)pcmd.VtxOffset,
                        idxBuffer,
                        vtxBuffer,
                        pcmd.TextureId
                    );
                    idxOffset += (int)pcmd.ElemCount;
                    Rlgl.DrawRenderBatchActive();
                }
            }
        }

        Rlgl.SetTexture(0);
        Rlgl.DisableScissorTest();
        Rlgl.EnableBackfaceCulling();
    }

    /// <summary>
    /// Render Raylib Texture2D to ImGui
    /// </summary>
    /// <param name="image">Raylib Texture2D</param>
    /// <param name="width">Destination Width</param>
    /// <param name="height">Destination Height</param>
    /// <param name="source">Source Rectangle</param>
    public static void ImageRect(Texture2D image, int width, int height, Rect source)
    {
        var uv0 = new Vec2();
        var uv1 = new Vector2();

        if (source.Width < 0)
        {
            uv0.X = -(source.X / image.Width);
            uv1.X = uv0.X - MathF.Abs(source.Width) / image.Width;
        }
        else
        {
            uv0.X = source.X / image.Width;
            uv1.X = uv0.X + source.Width / image.Width;
        }

        if (source.Height < 0)
        {
            uv0.Y = -(source.Y / image.Height);
            uv1.Y = uv0.Y - MathF.Abs(source.Height) / image.Height;
        }
        else
        {
            uv0.Y = source.Y / image.Height;
            uv1.Y = uv0.Y + source.Height / image.Height;
        }

        ImGui.Image(new nint(image.Id), new Vector2(width, height), uv0, uv1);
    }

    /// <summary>
    /// Render Raylib RenderTexture2D to ImGui
    /// </summary>
    /// <param name="image">Raylib RenderTexture2D</param>
    /// <param name="center">Center RenderTexture2D</param>
    public static void ImageRenderTexture(RenderTexture2D image, bool center = true)
    {
        var area = ImGui.GetContentRegionAvail();

        var scale = area.X / image.Texture.Width;

        var y = image.Texture.Height * scale;
        if (y > area.Y)
            scale = area.Y / image.Texture.Height;

        var sizeX = (int)(image.Texture.Width * scale);
        var sizeY = (int)(image.Texture.Height * scale);

        if (center)
        {
            ImGui.SetCursorPosX(0);
            ImGui.SetCursorPosX(area.X / 2 - sizeX / 2f);
            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + (area.Y / 2 - sizeY / 2f));
        }

        ImageRect(
            image.Texture,
            sizeX,
            sizeY,
            new Rect(0, 0, image.Texture.Width, -image.Texture.Height)
        );
    }
}
