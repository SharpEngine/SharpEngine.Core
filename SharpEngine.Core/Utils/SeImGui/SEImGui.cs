﻿using System;
using System.Numerics;
using ImGuiNET;
using Raylib_cs;
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

        io.KeyMap[(int)ImGuiKey.Tab] = (int)KeyboardKey.Tab;
        io.KeyMap[(int)ImGuiKey.LeftArrow] = (int)KeyboardKey.Left;
        io.KeyMap[(int)ImGuiKey.RightArrow] = (int)KeyboardKey.Right;
        io.KeyMap[(int)ImGuiKey.UpArrow] = (int)KeyboardKey.Up;
        io.KeyMap[(int)ImGuiKey.DownArrow] = (int)KeyboardKey.Down;
        io.KeyMap[(int)ImGuiKey.PageUp] = (int)KeyboardKey.PageUp;
        io.KeyMap[(int)ImGuiKey.PageDown] = (int)KeyboardKey.PageDown;
        io.KeyMap[(int)ImGuiKey.Home] = (int)KeyboardKey.Home;
        io.KeyMap[(int)ImGuiKey.End] = (int)KeyboardKey.End;
        io.KeyMap[(int)ImGuiKey.Insert] = (int)KeyboardKey.Insert;
        io.KeyMap[(int)ImGuiKey.Delete] = (int)KeyboardKey.Delete;
        io.KeyMap[(int)ImGuiKey.Backspace] = (int)KeyboardKey.Backspace;
        io.KeyMap[(int)ImGuiKey.Space] = (int)KeyboardKey.Space;
        io.KeyMap[(int)ImGuiKey.Enter] = (int)KeyboardKey.Enter;
        io.KeyMap[(int)ImGuiKey.Escape] = (int)KeyboardKey.Escape;
        io.KeyMap[(int)ImGuiKey.A] = (int)KeyboardKey.A;
        io.KeyMap[(int)ImGuiKey.C] = (int)KeyboardKey.C;
        io.KeyMap[(int)ImGuiKey.V] = (int)KeyboardKey.V;
        io.KeyMap[(int)ImGuiKey.X] = (int)KeyboardKey.X;
        io.KeyMap[(int)ImGuiKey.Y] = (int)KeyboardKey.Y;
        io.KeyMap[(int)ImGuiKey.Z] = (int)KeyboardKey.Z;
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

        // Modifiers are not reliable across systems
        io.KeyCtrl =
            io.KeysDown[(int)KeyboardKey.LeftControl]
            || io.KeysDown[(int)KeyboardKey.RightControl];
        io.KeyShift =
            io.KeysDown[(int)KeyboardKey.LeftShift]
            || io.KeysDown[(int)KeyboardKey.RightShift];
        io.KeyAlt =
            io.KeysDown[(int)KeyboardKey.LeftAlt]
            || io.KeysDown[(int)KeyboardKey.RightAlt];
        io.KeySuper =
            io.KeysDown[(int)KeyboardKey.LeftSuper]
            || io.KeysDown[(int)KeyboardKey.RightSuper];

        // Key states
        for (var i = (int)KeyboardKey.Space; i < (int)KeyboardKey.KeyboardMenu + 1; i++)
            io.KeysDown[i] = Raylib.IsKeyDown((KeyboardKey)i);

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
