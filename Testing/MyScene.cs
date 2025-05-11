using SharpEngine.Core;
using SharpEngine.Core.Component;
using SharpEngine.Core.Entity;
using SharpEngine.Core.Input;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;
using SharpEngine.Core.Widget;

namespace Testing;

public class MyScene : Scene
{
    private SpriteSheetComponent _sprite;
    private TextComponent _text;

    public MyScene()
    {
        var e = new Entity();
        e.AddComponent(new TransformComponent(Vec2.Zero, Vec2.One, 0));
        e.AddComponent(new RectComponent(Color.AliceBlue, new Vec2(50)));
        e.AddComponent(new ControlComponent());
        e.AddComponent(new CollisionComponent(new Vec2(50), drawDebug: true));
        AddEntity(e);

        var e2 = new Entity();
        e2.AddComponent(new TransformComponent(new Vec2(300), Vec2.One, 0));
        e2.AddComponent(new RectComponent(Color.Gray, new Vec2(100)));
        e2.AddComponent(new CollisionComponent(new Vec2(25), new Vec2(-25), drawDebug: true));
        e2.AddComponent(new CollisionComponent(new Vec2(25), new Vec2(25), drawDebug: true));
        e2.AddComponent(new CollisionComponent(new Vec2(25), new Vec2(-25, 25), drawDebug: true));
        e2.AddComponent(new CollisionComponent(new Vec2(25), new Vec2(25, -25), drawDebug: true));
        AddEntity(e2);
    }
}
