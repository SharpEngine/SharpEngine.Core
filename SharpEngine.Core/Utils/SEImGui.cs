﻿using System;
using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;

namespace SharpEngine.Core.Utils;

/// <summary>
/// Class which control ImGui
/// </summary>
public class SeImGui : IDisposable
{
    private readonly IntPtr _context;
    private Texture2D _fontTexture;
    private readonly Vector2 _scaleFactor = Vector2.One;

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
        var data = new IntPtr(pixels);
        var image = new Image
        {
            Data = (void*)data,
            Width = width,
            Height = height,
            Mipmaps = 1,
            Format = PixelFormat.PIXELFORMAT_UNCOMPRESSED_R8G8B8A8,
        };
        _fontTexture = Raylib.LoadTextureFromImage(image);

        // Store texture id in imgui font
        io.Fonts.SetTexID(new IntPtr(_fontTexture.Id));

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
    }

    private static ImGuiKey MapKey(KeyboardKey key)
    {
        return key switch
        {
            KeyboardKey.KEY_TAB => ImGuiKey.Tab,
            KeyboardKey.KEY_LEFT => ImGuiKey.LeftArrow,
            KeyboardKey.KEY_RIGHT => ImGuiKey.RightArrow,
            KeyboardKey.KEY_UP => ImGuiKey.UpArrow,
            KeyboardKey.KEY_DOWN => ImGuiKey.DownArrow,
            KeyboardKey.KEY_PAGE_UP => ImGuiKey.PageUp,
            KeyboardKey.KEY_PAGE_DOWN => ImGuiKey.PageDown,
            KeyboardKey.KEY_HOME => ImGuiKey.Home,
            KeyboardKey.KEY_END => ImGuiKey.End,
            KeyboardKey.KEY_INSERT => ImGuiKey.Insert,
            KeyboardKey.KEY_DELETE => ImGuiKey.Delete,
            KeyboardKey.KEY_BACKSPACE => ImGuiKey.Backspace,
            KeyboardKey.KEY_SPACE => ImGuiKey.Space,
            KeyboardKey.KEY_ENTER => ImGuiKey.Enter,
            KeyboardKey.KEY_ESCAPE => ImGuiKey.Escape,
            KeyboardKey.KEY_A => ImGuiKey.A,
            KeyboardKey.KEY_C => ImGuiKey.C,
            KeyboardKey.KEY_V => ImGuiKey.V,
            KeyboardKey.KEY_X => ImGuiKey.X,
            KeyboardKey.KEY_Y => ImGuiKey.Y,
            KeyboardKey.KEY_Z => ImGuiKey.Z,
            _ => (ImGuiKey)key,
        };
    }

    /// <summary>
    /// Update imgui internals(input, frameData)
    /// </summary>
    /// <param name="dt"></param>
    public void Update(float dt)
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

        // Modifiers are not reliable across systems
        io.KeyCtrl = Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_CONTROL) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT_CONTROL);
        io.KeyShift = Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT_SHIFT);
        io.KeyAlt = Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_ALT) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT_ALT);
        io.KeySuper = Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SUPER) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT_SUPER);

        // Key states
        for (var i = (int)KeyboardKey.KEY_SPACE; i < (int)KeyboardKey.KEY_KB_MENU + 1; i++)
            io.AddKeyEvent(MapKey((KeyboardKey)i), Raylib.IsKeyDown((KeyboardKey)i));

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
            io.MouseDown[i] = Raylib.IsMouseButtonDown((MouseButton)i);
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
    public void Draw()
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
                (byte)((hexValue >> 8) & 0xFF),
                (byte)((hexValue >> 16) & 0xFF),
                (byte)((hexValue >> 24) & 0xFF)
            );
        return color;
    }

    private void DrawTriangleVertex(ImDrawVertPtr idxVert)
    {
        var c = GetColor(idxVert.col);
        Rlgl.Color4ub(c.R, c.G, c.B, c.A);
        Rlgl.TexCoord2f(idxVert.uv.X, idxVert.uv.Y);
        Rlgl.Vertex2f(idxVert.pos.X, idxVert.pos.Y);
    }

    // Draw the imgui triangle data
    private void DrawTriangles(
        uint count,
        int idxOffset,
        int vtxOffset,
        ImVector<ushort> idxBuffer,
        ImPtrVector<ImDrawVertPtr> idxVert,
        IntPtr textureId
    )
    {
        if (Rlgl.CheckRenderBatchLimit((int)count * 3))
            Rlgl.DrawRenderBatchActive();

        Rlgl.Begin(DrawMode.TRIANGLES); // RL_TRIANGLES
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

    private void RenderCommandLists(ImDrawDataPtr data)
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

                if (pcmd.UserCallback != IntPtr.Zero)
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

        ImGui.Image(new IntPtr(image.Id), new Vector2(width, height), uv0, uv1);
    }

    /// <summary>
    /// Render Raylib RenderTexture2D to ImGui
    /// </summary>
    /// <param name="image">Raylib RenderTexture2D</param>
    /// <param name="center">Center RenderTexture2D</param>
    public void ImageRenderTexture(RenderTexture2D image, bool center = true)
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
