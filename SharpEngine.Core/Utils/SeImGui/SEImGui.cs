using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ImGuiNET;
using JetBrains.Annotations;
using Raylib_cs;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;

namespace SharpEngine.Core.Utils.SeImGui;

/// <summary>
/// Class which control ImGui
/// </summary>
[UsedImplicitly]
public class SeImGui : IDisposable
{
    private readonly nint _context;
    private Texture2D _fontTexture;
    private readonly Vector2 _scaleFactor = Vector2.One;
    private static readonly Dictionary<KeyboardKey, ImGuiKey> KeyMap = [];

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

    /// <inheritdoc cref="Dispose" />
    protected virtual void Dispose(bool disposing)
    {
        if (!disposing) return;
        
        ImGui.DestroyContext(_context);
        Raylib.UnloadTexture(_fontTexture);
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

        KeyMap.Add(KeyboardKey.Apostrophe, ImGuiKey.Apostrophe);
        KeyMap.Add(KeyboardKey.Comma, ImGuiKey.Comma); 
        KeyMap.Add(KeyboardKey.Minus, ImGuiKey.Minus);
        KeyMap.Add(KeyboardKey.Period, ImGuiKey.Period);
        KeyMap.Add(KeyboardKey.Slash, ImGuiKey.Slash);
        KeyMap.Add(KeyboardKey.Zero, ImGuiKey._0);
        KeyMap.Add(KeyboardKey.One, ImGuiKey._1);
        KeyMap.Add(KeyboardKey.Two, ImGuiKey._2);
        KeyMap.Add(KeyboardKey.Three, ImGuiKey._3);
        KeyMap.Add(KeyboardKey.Four, ImGuiKey._4);
        KeyMap.Add(KeyboardKey.Five, ImGuiKey._5);
        KeyMap.Add(KeyboardKey.Six, ImGuiKey._6);
        KeyMap.Add(KeyboardKey.Seven, ImGuiKey._7);
        KeyMap.Add(KeyboardKey.Eight, ImGuiKey._8);
        KeyMap.Add(KeyboardKey.Nine, ImGuiKey._9);
        KeyMap.Add(KeyboardKey.Semicolon, ImGuiKey.Semicolon);
        KeyMap.Add(KeyboardKey.Equal, ImGuiKey.Equal);
        KeyMap.Add(KeyboardKey.A, ImGuiKey.A);
        KeyMap.Add(KeyboardKey.B, ImGuiKey.B);
        KeyMap.Add(KeyboardKey.C, ImGuiKey.C);
        KeyMap.Add(KeyboardKey.D, ImGuiKey.D);
        KeyMap.Add(KeyboardKey.E, ImGuiKey.E);
        KeyMap.Add(KeyboardKey.F, ImGuiKey.F);
        KeyMap.Add(KeyboardKey.G, ImGuiKey.G);
        KeyMap.Add(KeyboardKey.H, ImGuiKey.H);
        KeyMap.Add(KeyboardKey.I, ImGuiKey.I);
        KeyMap.Add(KeyboardKey.J, ImGuiKey.J);
        KeyMap.Add(KeyboardKey.K, ImGuiKey.K);
        KeyMap.Add(KeyboardKey.L, ImGuiKey.L);
        KeyMap.Add(KeyboardKey.M, ImGuiKey.M);
        KeyMap.Add(KeyboardKey.N, ImGuiKey.N);
        KeyMap.Add(KeyboardKey.O, ImGuiKey.O);
        KeyMap.Add(KeyboardKey.P, ImGuiKey.P);
        KeyMap.Add(KeyboardKey.Q, ImGuiKey.Q);
        KeyMap.Add(KeyboardKey.R, ImGuiKey.R);
        KeyMap.Add(KeyboardKey.S, ImGuiKey.S);
        KeyMap.Add(KeyboardKey.T, ImGuiKey.T);
        KeyMap.Add(KeyboardKey.U, ImGuiKey.U);
        KeyMap.Add(KeyboardKey.V, ImGuiKey.V);
        KeyMap.Add(KeyboardKey.W, ImGuiKey.W);
        KeyMap.Add(KeyboardKey.X, ImGuiKey.X);
        KeyMap.Add(KeyboardKey.Y, ImGuiKey.Y);
        KeyMap.Add(KeyboardKey.Z, ImGuiKey.Z);
        KeyMap.Add(KeyboardKey.Space, ImGuiKey.Space);
        KeyMap.Add(KeyboardKey.Escape, ImGuiKey.Escape);
        KeyMap.Add(KeyboardKey.Enter, ImGuiKey.Enter);
        KeyMap.Add(KeyboardKey.Tab, ImGuiKey.Tab);
        KeyMap.Add(KeyboardKey.Backspace, ImGuiKey.Backspace);
        KeyMap.Add(KeyboardKey.Insert, ImGuiKey.Insert);
        KeyMap.Add(KeyboardKey.Delete, ImGuiKey.Delete);
        KeyMap.Add(KeyboardKey.Right, ImGuiKey.RightArrow);
        KeyMap.Add(KeyboardKey.Left, ImGuiKey.LeftArrow);
        KeyMap.Add(KeyboardKey.Down, ImGuiKey.DownArrow);
        KeyMap.Add(KeyboardKey.Up, ImGuiKey.UpArrow);
        KeyMap.Add(KeyboardKey.PageUp, ImGuiKey.PageUp);
        KeyMap.Add(KeyboardKey.PageDown, ImGuiKey.PageDown);
        KeyMap.Add(KeyboardKey.Home, ImGuiKey.Home);
        KeyMap.Add(KeyboardKey.End, ImGuiKey.End);
        KeyMap.Add(KeyboardKey.CapsLock, ImGuiKey.CapsLock);
        KeyMap.Add(KeyboardKey.ScrollLock, ImGuiKey.ScrollLock);
        KeyMap.Add(KeyboardKey.NumLock, ImGuiKey.NumLock);
        KeyMap.Add(KeyboardKey.PrintScreen, ImGuiKey.PrintScreen);
        KeyMap.Add(KeyboardKey.Pause, ImGuiKey.Pause);
        KeyMap.Add(KeyboardKey.F1, ImGuiKey.F1);
        KeyMap.Add(KeyboardKey.F2, ImGuiKey.F2);
        KeyMap.Add(KeyboardKey.F3, ImGuiKey.F3);
        KeyMap.Add(KeyboardKey.F4, ImGuiKey.F4);
        KeyMap.Add(KeyboardKey.F5, ImGuiKey.F5);
        KeyMap.Add(KeyboardKey.F6, ImGuiKey.F6);
        KeyMap.Add(KeyboardKey.F7, ImGuiKey.F7);
        KeyMap.Add(KeyboardKey.F8, ImGuiKey.F8);
        KeyMap.Add(KeyboardKey.F9, ImGuiKey.F9);
        KeyMap.Add(KeyboardKey.F10, ImGuiKey.F10);
        KeyMap.Add(KeyboardKey.F11, ImGuiKey.F11);
        KeyMap.Add(KeyboardKey.F12, ImGuiKey.F12);
        KeyMap.Add(KeyboardKey.LeftShift, ImGuiKey.LeftShift);
        KeyMap.Add(KeyboardKey.LeftControl, ImGuiKey.LeftCtrl);
        KeyMap.Add(KeyboardKey.LeftAlt, ImGuiKey.LeftAlt);
        KeyMap.Add(KeyboardKey.LeftSuper, ImGuiKey.LeftSuper);
        KeyMap.Add(KeyboardKey.RightShift, ImGuiKey.RightShift);
        KeyMap.Add(KeyboardKey.RightControl, ImGuiKey.RightCtrl);
        KeyMap.Add(KeyboardKey.RightAlt, ImGuiKey.RightAlt);
        KeyMap.Add(KeyboardKey.RightSuper, ImGuiKey.RightSuper);
        KeyMap.Add(KeyboardKey.KeyboardMenu, ImGuiKey.Menu);
        KeyMap.Add(KeyboardKey.LeftBracket, ImGuiKey.LeftBracket);
        KeyMap.Add(KeyboardKey.Backslash, ImGuiKey.Backslash);
        KeyMap.Add(KeyboardKey.RightBracket, ImGuiKey.RightBracket);
        KeyMap.Add(KeyboardKey.Grave, ImGuiKey.GraveAccent);
        KeyMap.Add(KeyboardKey.Kp0, ImGuiKey.Keypad0);
        KeyMap.Add(KeyboardKey.Kp1, ImGuiKey.Keypad1);
        KeyMap.Add(KeyboardKey.Kp2, ImGuiKey.Keypad2);
        KeyMap.Add(KeyboardKey.Kp3, ImGuiKey.Keypad3);
        KeyMap.Add(KeyboardKey.Kp4, ImGuiKey.Keypad4);
        KeyMap.Add(KeyboardKey.Kp5, ImGuiKey.Keypad5);
        KeyMap.Add(KeyboardKey.Kp6, ImGuiKey.Keypad6);
        KeyMap.Add(KeyboardKey.Kp7, ImGuiKey.Keypad7);
        KeyMap.Add(KeyboardKey.Kp8, ImGuiKey.Keypad8);
        KeyMap.Add(KeyboardKey.Kp9, ImGuiKey.Keypad9);
        KeyMap.Add(KeyboardKey.KpDecimal, ImGuiKey.KeypadDecimal);
        KeyMap.Add(KeyboardKey.KpDivide, ImGuiKey.KeypadDivide);
        KeyMap.Add(KeyboardKey.KpMultiply, ImGuiKey.KeypadMultiply);
        KeyMap.Add(KeyboardKey.KpSubtract, ImGuiKey.KeypadSubtract);
        KeyMap.Add(KeyboardKey.KpAdd, ImGuiKey.KeypadAdd);
        KeyMap.Add(KeyboardKey.KpEnter, ImGuiKey.KeypadEnter);
        KeyMap.Add(KeyboardKey.KpEqual, ImGuiKey.KeypadEqual);
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
    }

    private static void UpdateKeyboard()
    {
        var io = ImGui.GetIO();

        // Key states
        foreach(var keys in KeyMap)
        {
            if(Raylib.IsKeyPressed(keys.Key))
                io.AddKeyEvent(keys.Value, true);
            if (Raylib.IsKeyReleased(keys.Key))
                io.AddKeyEvent(keys.Value, false);
        }

        // Key input
        foreach (var charGot in InputManager.InternalPressedChars.Where(charGot => charGot != 0))
            io.AddInputCharacter((uint)charGot);
    }

    private static void UpdateMouse()
    {
        var io = ImGui.GetIO();

        // Store button states
        for (var i = 0; i < io.MouseDown.Count; i++)
            io.MouseDown[i] = Raylib.IsMouseButtonDown((MouseButton)i);

        // Mouse scroll
        io.MouseWheel += Raylib.GetMouseWheelMove();

        // Mouse position
        var mousePosition = io.MousePos;
        bool focused = Raylib.IsWindowFocused();

        if (focused)
        {
            if (io.WantSetMousePos)
                InputManager.SetMousePosition(mousePosition);
            else
                io.MousePos = InputManager.GetMousePosition();
        }

        // Mouse cursor state
        if ((io.ConfigFlags & ImGuiConfigFlags.NoMouseCursorChange) != 0 && !Raylib.IsCursorHidden()) return;
        
        var cursor = ImGui.GetMouseCursor();
        if (cursor == ImGuiMouseCursor.None || io.MouseDrawCursor)
            Raylib.HideCursor();
        else
            Raylib.ShowCursor();
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
                Rlgl.Scissor(rectX, (int)InputManager.InternalWindow.RenderSize.Y - (rectY + rectH), rectW, rectH);

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
    private static void ImageRect(Texture2D image, int width, int height, Rect source)
    {
        var uv0 = new Vector2();
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
    [UsedImplicitly]
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
