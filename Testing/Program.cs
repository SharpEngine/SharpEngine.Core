using Raylib_cs;
using SharpEngine.Core;
using SharpEngine.Core.Manager;
using Color = SharpEngine.Core.Utils.Color;

namespace Testing;

internal static class Program
{
    private static void Main()
    {
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);

        var window = new Window(
            1280,
            920,
            "SE Raylib",
            Color.CornflowerBlue,
            null,
            true,
            true,
            true
        );

        window.TextureManager.AddTexture("portal", "resources/portal.png");

        window.AddScene(new MyScene());

        DebugManager.AddConsoleCommand(new TestConsoleCommand());

        window.Run();
    }
}
