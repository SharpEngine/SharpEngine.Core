using SharpEngine.Core;
using SharpEngine.Core.Data.Save;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;
using SharpEngine.Core.Utils;

namespace Testing;

internal static class Program
{
    private static void Main()
    {
        var window = new Window(1280, 920, "SE Raylib", Color.CornflowerBlue, null, true, true, true)
        {
            RenderImGui = DebugManager.CreateSeImGuiWindow
        };
        
        window.TextureManager.AddTexture("outline", "resources/outline.png");
        
        window.ShaderManager.AddShader("outline", "", "resources/outline.frag");
        
        window.ShaderManager.SetShaderValue("outline", "textureSize", new Vec2(128));
        window.ShaderManager.SetShaderValue("outline", "outlineSize", 4f);
        window.ShaderManager.SetShaderValue("outline", "outlineColor", Color.Gold);
        
        window.AddScene(new MyScene());
        
        window.Run();
    }
}

