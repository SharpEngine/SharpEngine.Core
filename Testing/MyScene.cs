using SharpEngine.Core;
using SharpEngine.Core.Component;
using SharpEngine.Core.Entity;
using SharpEngine.Core.Input;
using SharpEngine.Core.Math;

namespace Testing;

public class MyScene : Scene
{
    public MyScene()
    {
        var e = new Entity();
        e.AddComponent(new TransformComponent(new Vec2(640, 480)));
        e.AddComponent(new SpriteComponent("outline", shader: "outline"));
        e.AddComponent(new ControlComponent(controlType: ControlType.MouseFollow));
        AddEntity(e);
    }

    public override void Load()
    {
        base.Load();

        Window?.TimerManager.AddTimer(
            "outlineTimer",
            new SharpEngine.Core.Utils.Timer(
                2,
                () =>
                {
                    Window?.ShaderManager.SetShaderValue("outline", "outlineSize", 8f);
                    Window?.TimerManager.AddTimer(
                        "outlineTimer2",
                        new SharpEngine.Core.Utils.Timer(
                            2,
                            () =>
                            {
                                Window?.ShaderManager.SetShaderValue("outline", "outlineSize", 2f);
                            }
                        )
                    );
                }
            )
        );
    }
}
