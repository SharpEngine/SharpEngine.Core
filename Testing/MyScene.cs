using SharpEngine.Core;
using SharpEngine.Core.Component;
using SharpEngine.Core.Entity;
using SharpEngine.Core.Math;

namespace Testing;

public class MyScene: Scene
{
    public MyScene()
    {
        var e = new Entity();
        e.AddComponent(new TransformComponent(new Vec2(640, 480)));
        e.AddComponent(new SpriteComponent("outline", shader: "outline"));
        AddEntity(e);
    }
}