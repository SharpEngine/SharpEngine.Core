using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Raylib_cs;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;
using SharpEngine.Core.Utils.EventArgs;
using SharpEngine.Core.Utils.SeImGui;
using static System.Formats.Asn1.AsnWriter;
using Color = SharpEngine.Core.Utils.Color;

namespace SharpEngine.Core;

/// <summary>
/// Class which represents and create Window
/// </summary>
public class Window
{
    /// <summary>
    /// Title of Window
    /// </summary>
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            Raylib.SetWindowTitle(value);
        }
    }

    /// <summary>
    /// Size of Window
    /// </summary>
    public Vec2 ScreenSize
    {
        get => _screenSize;
        set
        {
            _screenSize = value;
            Raylib.SetWindowSize((int)value.X, (int)value.Y);
            RenderScale = MathF.Min(_screenSize.X / _renderSize.X, _screenSize.Y / _renderSize.Y);
        }
    }

    /// <summary>
    /// Size of Render
    /// </summary>
    public Vec2 RenderSize
    {
        get => _renderSize;
        set
        {
            _renderSize = value;
            UpdateRenderSize();
        }
    }

    /// <summary>
    /// Position of Window
    /// </summary>
    public static Vec2 Position
    {
        get => Raylib.GetWindowPosition();
        set => Raylib.SetWindowPosition((int)value.X, (int)value.Y);
    }

    /// <summary>
    /// Background Color used in Window
    /// </summary>
    public Color BackgroundColor { get; set; }

    /// <summary>
    /// Event which be called in Start of Window (can stop start by set result to false)
    /// </summary>
    public event EventHandler<BoolEventArgs>? StartCallback;

    /// <summary>
    /// Event which be called in Stop of Window (can stop stop by set result to false)
    /// </summary>
    public event EventHandler<BoolEventArgs>? StopCallback;

    /// <summary>
    /// Function which be called to render something in ImGui
    /// </summary>
    public Action<Window>? RenderImGui { get; set; }

    /// <summary>
    /// Manage Debug Mode of Window
    /// </summary>
    public bool Debug
    {
        get => _debug;
        set
        {
            _debug = value;
            DebugManager.SetLogLevel(value ? LogLevel.LogAll : LogLevel.LogInfo);
        }
    }

    /// <summary>
    /// Camera Manager of Window
    /// </summary>
    public CameraManager CameraManager { get; }

    /// <summary>
    /// Texture Manager of Window
    /// </summary>
    public TextureManager TextureManager { get; }

    /// <summary>
    /// Shader Manager of Window
    /// </summary>
    public ShaderManager ShaderManager { get; }

    /// <summary>
    /// Font Manager of Window
    /// </summary>
    public FontManager FontManager { get; }

    /// <summary>
    /// Sound Manager of Window
    /// </summary>
    public SoundManager SoundManager { get; }

    /// <summary>
    /// Music Manager of Window
    /// </summary>
    public MusicManager MusicManager { get; }

    /// <summary>
    ///Timer Manager of Window
    /// </summary>
    public TimerManager TimerManager { get; }

    /// <summary>
    /// ImGui Management of Window
    /// </summary>
    public SeImGui SeImGui { get; }

    /// <summary>
    /// Scale of Render
    /// </summary>
    public float RenderScale { get; private set; }

    /// <summary>
    /// Index of Current Scene
    /// </summary>
    public int IndexCurrentScene
    {
        get => _internalIndexCurrentScene;
        set
        {
            if (_internalIndexCurrentScene != -1)
                Scenes[_internalIndexCurrentScene].CloseScene();
            _internalIndexCurrentScene = value;
            Scenes[_internalIndexCurrentScene].OpenScene();
        }
    }

    /// <summary>
    /// Current Scene
    /// </summary>
    public Scene CurrentScene
    {
        get => Scenes[_internalIndexCurrentScene];
        set
        {
            if (_internalIndexCurrentScene != 1)
                Scenes[_internalIndexCurrentScene].CloseScene();
            _internalIndexCurrentScene = Scenes.IndexOf(value);
            Scenes[_internalIndexCurrentScene].OpenScene();
        }
    }

    /// <summary>
    /// Get All Scenes
    /// </summary>
    public List<Scene> Scenes { get; } = [];

    private Vec2 _screenSize;
    private Vec2 _renderSize;
    private string _title;
    private bool _closeWindow;
    private int _internalIndexCurrentScene = -1;
    private bool _debug;
    private RenderTexture2D _targetTexture;
    internal bool _imguiDisplayWindow = false;
    internal bool _imguiDisplayConsole = false;

    private static bool ConsoleLog { get; set; } = true;
    private static bool FileLog { get; set; } = false;

    /// <summary>
    /// Create and Init Window
    /// </summary>
    /// <param name="width">Width of Window</param>
    /// <param name="height">Height of Window</param>
    /// <param name="title">Title of Window</param>
    /// <param name="backgroundColor">Background Color of Window (Black)</param>
    /// <param name="fps">Number of FPS (60)</param>
    /// <param name="debug">Debug Mode (false)</param>
    /// <param name="consoleLog">Log in Console</param>
    /// <param name="fileLog">Log in File (log.txt)</param>
    public Window(
        int width,
        int height,
        string title,
        Color? backgroundColor = null,
        int? fps = 60,
        bool debug = false,
        bool consoleLog = true,
        bool fileLog = false
    )
        : this(new Vec2(width, height), title, backgroundColor, fps, debug, consoleLog, fileLog) { }

    /// <summary>
    /// Create and Init Window
    /// </summary>
    /// <param name="screenSize">Size of Window</param>
    /// <param name="title">Title of Window</param>
    /// <param name="backgroundColor">Background Color of Window (Black)</param>
    /// <param name="fps">Number of FPS (60)</param>
    /// <param name="debug">Debug Mode (false)</param>
    /// <param name="consoleLog">Log in Console</param>
    /// <param name="fileLog">Log in File (log.txt)</param>
    public Window(
        Vec2 screenSize,
        string title,
        Color? backgroundColor = null,
        int? fps = 60,
        bool debug = false,
        bool consoleLog = true,
        bool fileLog = false
    )
    {
        InputManager.InternalWindow = this;
        RenderImGui = DebugManager.SeRenderImGui;
        ConsoleLog = consoleLog;
        FileLog = fileLog;
        _title = title;
        _screenSize = screenSize;
        _renderSize = screenSize;
        BackgroundColor = backgroundColor ?? Color.Black;
        Debug = debug;

        if (FileLog && File.Exists("log.txt"))
            File.Delete("log.txt");

        unsafe
        {
            Raylib.SetTraceLogCallback(&LogCustom);
        }

        Raylib.InitWindow((int)screenSize.X, (int)screenSize.Y, title);
        Raylib.InitAudioDevice();

        SeImGui = new SeImGui();
        SeImGui.Load((int)screenSize.X, (int)screenSize.Y);

        TextureManager = new TextureManager();
        ShaderManager = new ShaderManager();
        FontManager = new FontManager();
        CameraManager = new CameraManager();
        SoundManager = new SoundManager();
        MusicManager = new MusicManager();
        TimerManager = new TimerManager();
        CameraManager.SetScreenSize(screenSize);

        if (fps != null)
            Raylib.SetTargetFPS(fps.Value);

        _targetTexture = Raylib.LoadRenderTexture((int)screenSize.X, (int)screenSize.Y);
        RenderScale = MathF.Min(_screenSize.X / _renderSize.X, _screenSize.Y / _renderSize.Y);
    }

    /// <summary>
    /// Take a screenshot and save it
    /// </summary>
    /// <param name="path">Path of saved screenshot</param>
    public static void TakeScreenshot(string path) => Raylib.TakeScreenshot(path);

    /// <summary>
    /// Set master volume
    /// </summary>
    /// <param name="volume">Volume (0 to 1)</param>
    public static void SetMasterVolume(float volume) => Raylib.SetMasterVolume(volume);

    /// <summary>
    /// Add Scene to Window and Set Current Scene to it
    /// </summary>
    /// <param name="scene">Scene which be added</param>
    public void AddScene(Scene scene)
    {
        scene.Window = this;
        Scenes.Add(scene);
        _internalIndexCurrentScene = Scenes.Count - 1;
    }

    /// <summary>
    /// Get Scene by Index
    /// </summary>
    /// <param name="index">Index of Scene</param>
    /// <returns>Scene</returns>
    public Scene GetScene(int index) => Scenes[index];

    /// <summary>
    /// Get Scene cast as T
    /// </summary>
    /// <param name="index">Index of Scene</param>
    /// <typeparam name="T">Type as Scene</typeparam>
    /// <returns>Scene cast as T</returns>
    public T GetScene<T>(int index)
        where T : Scene => (T)Scenes[index];

    /// <summary>
    /// Get Current Scene cast as T
    /// </summary>
    /// <typeparam name="T">Type as Scene</typeparam>
    /// <returns>Current Scene cast as T</returns>
    public T GetCurrentScene<T>()
        where T : Scene => (T)Scenes[_internalIndexCurrentScene];

    /// <summary>
    /// Run Window
    /// </summary>
    public void Run()
    {
        var args = new BoolEventArgs();
        StartCallback?.Invoke(this, args);
        if (!args.Result)
            return;

        if (Scenes.Count == 0)
        {
            DebugManager.Log(LogLevel.LogError, "SE: There are no scenes in window.");
            return;
        }

        #region Load

        DebugManager.Log(LogLevel.LogInfo, "SE: Loading Scenes...");
        foreach (var scene in Scenes)
            scene.Load();
        DebugManager.Log(LogLevel.LogInfo, "SE: Scenes loaded !");

        #endregion

        CurrentScene.OpenScene();

        while (!Raylib.WindowShouldClose() && !_closeWindow)
        {
            #region Update Input and ScreenSize

            InputManager.UpdateInput();

            if (_screenSize.X != Raylib.GetScreenWidth() || _screenSize.Y != Raylib.GetScreenHeight())
            {
                _screenSize = new Vec2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
                RenderScale = MathF.Min(_screenSize.X / _renderSize.X, _screenSize.Y / _renderSize.Y);
            }

            #endregion

            #region Update

            var delta = Raylib.GetFrameTime();

            SeImGui.Update(delta);

            CurrentScene.Update(delta);
            CameraManager.Update(delta);
            TimerManager.Update(delta);

            #endregion

            #region Draw

            if (Debug)
                RenderImGui?.Invoke(this);
            CurrentScene.Draw();

            {
                Raylib.BeginTextureMode(_targetTexture);
                Raylib.ClearBackground(BackgroundColor);

                SERender.Draw(this);

                if (Debug)
                    SeImGui.Draw();

                Raylib.EndTextureMode();
            }

            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                var dest = new Rectangle(
                    (_screenSize.X - (_renderSize.X * RenderScale)) * 0.5f,
                    (_screenSize.Y - (_renderSize.Y * RenderScale)) * 0.5f,
                    _renderSize.X * RenderScale,
                    _renderSize.Y * RenderScale
                );

                Raylib.DrawTexturePro(_targetTexture.Texture, new Rectangle(0, 0, _targetTexture.Texture.Width, -_targetTexture.Texture.Height),
                    dest, new Vector2(0, 0), 0, Color.White);

                Raylib.EndDrawing();
            }

            #endregion
        }

        #region Unload

        DebugManager.Log(LogLevel.LogInfo, "SE: Unloading Scenes...");
        foreach (var scene in Scenes)
            scene.Unload();
        DebugManager.Log(LogLevel.LogInfo, "SE: Scenes unloaded !");

        DebugManager.Log(LogLevel.LogInfo, "SE: Unloading Textures...");
        Raylib.UnloadRenderTexture(_targetTexture);
        TextureManager.Unload();
        DebugManager.Log(LogLevel.LogInfo, "SE: Textures unloaded !");
        DebugManager.Log(LogLevel.LogInfo, "SE: Unloading Fonts...");
        FontManager.Unload();
        DebugManager.Log(LogLevel.LogInfo, "SE: Fonts unloaded !");
        DebugManager.Log(LogLevel.LogInfo, "SE: Unloading Shaders...");
        ShaderManager.Unload();
        DebugManager.Log(LogLevel.LogInfo, "SE: Shaders unloaded !");
        DebugManager.Log(LogLevel.LogInfo, "SE: Unloading SeImGui...");
        SeImGui.Dispose();
        DebugManager.Log(LogLevel.LogInfo, "SE: SeImGui unloaded !");

        DebugManager.Log(LogLevel.LogInfo, "SE: Closing Window.");
        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();

        #endregion
    }

    /// <summary>
    /// Stop Window
    /// </summary>
    public void Stop()
    {
        var args = new BoolEventArgs();
        StopCallback?.Invoke(this, args);
        if (args.Result)
            _closeWindow = true;
    }

    private void UpdateRenderSize()
    {
        CameraManager.SetScreenSize(_renderSize);
        SeImGui.Resize((int)_renderSize.X, (int)_renderSize.Y);
        RenderScale = MathF.Min(_screenSize.X / _renderSize.X, _screenSize.Y / _renderSize.Y);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void LogCustom(int logLevel, sbyte* text, sbyte* args)
    {
        var message = Logging.GetLogMessage(new IntPtr(text), new IntPtr(args));
        message = (LogLevel)logLevel switch
        {
            LogLevel.LogTrace => $"TRACE: {message}",
            LogLevel.LogAll => $"ALL: {message}",
            LogLevel.LogDebug => $"DEBUG: {message}",
            LogLevel.LogInfo => $"INFO: {message}",
            LogLevel.LogWarning => $"WARNING: {message}",
            LogLevel.LogError => $"ERROR: {message}",
            LogLevel.LogFatal => $"FATAL: {message}",
            _ => message
        };

        message = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - {message}";

        if (ConsoleLog)
            Console.WriteLine(message);
        if (FileLog)
            File.AppendAllText("log.txt", message + "\n");
    }
}
