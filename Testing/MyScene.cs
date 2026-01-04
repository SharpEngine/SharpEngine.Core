using SharpEngine.Core;
using SharpEngine.Core.Component;
using SharpEngine.Core.Entity;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;
using SharpEngine.Core.Utils;
using SharpEngine.Core.Utils.Tween;

namespace Testing;

public class MyScene : Scene
{
    public MyScene()
    {
        var e = new Entity();
        e.AddComponent(new TransformComponent(Vec2.Zero, Vec2.One));
        e.AddComponent(new RectComponent(Color.AliceBlue, new Vec2(50)));
        e.AddComponent(new ControlComponent());
        e.AddComponent(new CollisionComponent(new Vec2(50), drawDebug: true));
        AddEntity(e);

        var e2 = new Entity();
        e2.AddComponent(new TransformComponent(new Vec2(300), Vec2.One));
        e2.AddComponent(new RectComponent(Color.Gray, new Vec2(100)));
        e2.AddComponent(new CollisionComponent(new Vec2(25), new Vec2(-25), drawDebug: true));
        e2.AddComponent(new CollisionComponent(new Vec2(25), new Vec2(25), drawDebug: true));
        e2.AddComponent(new CollisionComponent(new Vec2(25), new Vec2(-25, 25), drawDebug: true));
        e2.AddComponent(new CollisionComponent(new Vec2(25), new Vec2(25, -25), drawDebug: true));
        AddEntity(e2);

        var e3 = new Entity();
        e3.AddComponent(new TransformComponent(new Vec2(800, 300), Vec2.One));
        e3.AddComponent(new RectComponent(Color.AliceBlue, new Vec2(50)));
        e3.AddComponent(new AutoComponent(new Vec2(-20, 0)));
        e3.AddComponent(new CollisionComponent(new Vec2(50), drawDebug: true));
        AddEntity(e3);
    }

    public override void OpenScene()
    {
        base.OpenScene();

        Window?.TweenManager.Tweens.Add(new Tween([
            new TweenStep(5)
                .Float(Entities[0].GetComponentAs<TransformComponent>()!, x => ((TransformComponent)x).LocalRotation, 360, 5)
                .Float(Entities[0].GetComponentAs<TransformComponent>()!.LocalPosition, x => ((Vec2)x).X, 100, 5)
                .Float(Entities[0].GetComponentAs<TransformComponent>()!.LocalPosition, x => ((Vec2)x).Y, 100, 5),
            new TweenStep(10)
                .Float(Entities[0].GetComponentAs<TransformComponent>()!, x => ((TransformComponent)x).LocalRotation, 720, 5)
                .Float(Entities[0].GetComponentAs<TransformComponent>()!.LocalPosition, x => ((Vec2)x).X, 0, 5)
                .Float(Entities[0].GetComponentAs<TransformComponent>()!.LocalPosition, x => ((Vec2)x).Y, 0, 5),
        ], () => DebugManager.Log(LogLevel.Info, "FIN DU TWEEN")));
    }
}
