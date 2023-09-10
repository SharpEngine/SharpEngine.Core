using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Raylib_cs;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;
using SharpEngine.Core.Utils.EventArgs;
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
            CameraManager.SetScreenSize(value);
            Raylib.SetWindowSize((int)value.X, (int)value.Y);
        }
    }

    /// <summary>
    /// Position of Window
    /// </summary>
    public Vec2 Position
    {
        get => Raylib.GetWindowPosition();
        set => Raylib.SetWindowPosition((int)value.X, (int)value.Y);
    }

    /// <summary>
    /// Background Color used in Window
    /// </summary>
    public Color BackgroundColor;

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
    public Action<Window>? RenderImGui;

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
    public List<Scene> Scenes { get; } = new();

    private Vec2 _screenSize;
    private string _title;
    private readonly SeImGui _seImGui;
    private bool _closeWindow;
    private int _internalIndexCurrentScene = -1;
    private bool _debug;
    private static bool _consoleLog = true;
    private static bool _fileLog = false;

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
    public Window(int width, int height, string title, Color? backgroundColor = null, int? fps = 60, bool debug = false,
        bool consoleLog = true, bool fileLog = false) :
        this(new Vec2(width, height), title, backgroundColor, fps, debug, consoleLog, fileLog)
    {
    }

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
    public Window(Vec2 screenSize, string title, Color? backgroundColor = null, int? fps = 60, bool debug = false,
        bool consoleLog = true, bool fileLog = false)
    {
        _consoleLog = consoleLog;
        _fileLog = fileLog;
        _title = title;
        _screenSize = screenSize;
        BackgroundColor = backgroundColor ?? Color.Black;
        Debug = debug;

        if (_fileLog && File.Exists("log.txt"))
            File.Delete("log.txt");

        unsafe
        {
            Raylib.SetTraceLogCallback(&LogCustom);
        }

        Raylib.InitWindow((int)screenSize.X, (int)screenSize.Y, title);
        Raylib.InitAudioDevice();

        _seImGui = new SeImGui();
        _seImGui.Load((int)screenSize.X, (int)screenSize.Y);

        TextureManager = new TextureManager();
        ShaderManager = new ShaderManager();
        FontManager = new FontManager();
        CameraManager = new CameraManager();
        SoundManager = new SoundManager();
        MusicManager = new MusicManager();
        CameraManager.SetScreenSize(screenSize);

        if (fps != null)
            Raylib.SetTargetFPS(fps.Value);
    }

    /// <summary>
    /// Take a screenshot and save it
    /// </summary>
    /// <param name="path">Path of saved screenshot</param>
    public void TakeScreenshot(string path) => Raylib.TakeScreenshot(path);

    /// <summary>
    /// Set master volume
    /// </summary>
    /// <param name="volume">Volume (0 to 1)</param>
    public void SetMasterVolume(float volume) => Raylib.SetMasterVolume(volume);

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
    /// Get Current Scene cast as T
    /// </summary>
    /// <typeparam name="T">Type as Scene</typeparam>
    /// <returns>Current Scene cast as T</returns>
    public T GetCurrentScene<T>() where T : Scene => (T)Scenes[_internalIndexCurrentScene];

    /// <summary>
    /// Get Scene cast as T
    /// </summary>
    /// <param name="index">Index of Scene</param>
    /// <typeparam name="T">Type as Scene</typeparam>
    /// <returns>Scene cast as T</returns>
    public T GetScene<T>(int index) where T : Scene => (T)Scenes[index];

    /// <summary>
    /// Run Window
    /// </summary>
    public void Run()
    {
        var args = new BoolEventArgs();
        StartCallback?.Invoke(this, args);
        if(!args.Result)
            return;

        #region Load

        DebugManager.Log(LogLevel.LogInfo, "SE: Loading Scenes...");
        foreach (var scene in Scenes)
            scene.Load();
        DebugManager.Log(LogLevel.LogInfo, "SE: Scenes loaded !");

        #endregion
        
        CurrentScene.OpenScene();

        while (!Raylib.WindowShouldClose() && !_closeWindow)
        {
            #region Update Pressed Keys and Chars
            
            InputManager.InternalPressedChars.Clear();
            InputManager.InternalPressedKeys.Clear();

            var key = Raylib.GetKeyPressed();
            while (key > 0)
            {
                InputManager.InternalPressedKeys.Add(key);
                key = Raylib.GetKeyPressed();
            }
            
            var charGot = Raylib.GetCharPressed();
            while (charGot > 0)
            {
                InputManager.InternalPressedChars.Add(charGot);
                charGot = Raylib.GetCharPressed();
            }

            #endregion

            #region Update

            var delta = Raylib.GetFrameTime();
            
            _seImGui.Update(delta);
            
            CurrentScene.Update(delta);
            CameraManager.Update(delta);

            #endregion

            #region Draw
            
            if(Debug)
                RenderImGui?.Invoke(this);
            
            CurrentScene.Draw();
            
            Raylib.BeginDrawing();
            Raylib.ClearBackground(BackgroundColor);
            
            SERender.Draw(this);

            if (Debug)
                _seImGui.Draw();

            Raylib.EndDrawing();

            #endregion
        }

        #region Unload
        
        DebugManager.Log(LogLevel.LogInfo, "SE: Unloading Scenes...");
        foreach (var scene in Scenes)
            scene.Unload();
        DebugManager.Log(LogLevel.LogInfo, "SE: Scenes unloaded !");
        
        DebugManager.Log(LogLevel.LogInfo, "SE: Unloading Textures...");
        TextureManager.Unload();
        DebugManager.Log(LogLevel.LogInfo, "SE: Textures unloaded !");
        DebugManager.Log(LogLevel.LogInfo, "SE: Unloading Fonts...");
        FontManager.Unload();
        DebugManager.Log(LogLevel.LogInfo, "SE: Fonts unloaded !");
        DebugManager.Log(LogLevel.LogInfo, "SE: Unloading Shaders...");
        ShaderManager.Unload();
        DebugManager.Log(LogLevel.LogInfo, "SE: Shaders unloaded !");
        
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
        if(args.Result)
            _closeWindow = true;
    }
    
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl)})]
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
        
        if(_consoleLog)
            Console.WriteLine(message);
        if(_fileLog)
            File.AppendAllText("log.txt", message+"\n");
            
    }
}