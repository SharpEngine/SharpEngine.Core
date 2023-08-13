using SharpEngine.Core;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Utils;

namespace Testing;

internal static class Program
{
    private static void Main()
    {
        var window = new Window(1280, 920, "SE Raylib", Color.CornflowerBlue, null, true, true, true)
        {
            RenderImGui = win => DebugManager.CreateSeImGuiWindow(win, true)
        };
        
        window.AddScene(new MyScene());
        
        window.Run();
    }
}

