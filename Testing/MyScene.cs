using SharpEngine.Core;
using SharpEngine.Core.Component;
using SharpEngine.Core.Entity;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;
using SharpEngine.Core.Utils;

namespace Testing;

public class MyScene: Scene
{
    public MyScene()
    {
        var e = new Entity();
        e.AddComponent(new TransformComponent(new Vec2(100)));
        e.AddComponent(new RectComponent(Color.Black, new Vec2(50)));
        e.AddComponent(new ControlComponent(speed: 200, useGamePad: true, gamePadIndex: 0));
        e.AddComponent(new CollisionComponent(new Vec2(50)));
        AddEntity(e);
        
        var e2 = new Entity();
        e2.AddComponent(new TransformComponent(new Vec2(300)));
        e2.AddComponent(new RectComponent(Color.Red, new Vec2(50)));
        e2.AddComponent(new CollisionComponent(new Vec2(50)));
        AddEntity(e2);
    }
}