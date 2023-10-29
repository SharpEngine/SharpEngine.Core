using SharpEngine.Core;
using SharpEngine.Core.Component;
using SharpEngine.Core.Entity;
using SharpEngine.Core.Input;
using SharpEngine.Core.Math;
using SharpEngine.Core.Utils;

namespace Testing;

public class MyScene : Scene
{
    public MyScene()
    {
        var e = new Entity();
        e.AddComponent(new TransformComponent(new Vec2(640, 480)));
        e.AddComponent(
            new SpriteSheetComponent(
                "portal",
                new Vec2(100),
                new List<Animation>
                {
                    new("idle", Enumerable.Range(0, 41).Select(x => (uint)x).ToList(), 0.01f)
                },
                currentAnim: "idle",
                zLayerOffset: 1
            )
        );
        AddEntity(e);
    }
}
