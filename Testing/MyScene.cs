using SharpEngine.Core;
using SharpEngine.Core.Component;
using SharpEngine.Core.Entity;
using SharpEngine.Core.Input;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;
using SharpEngine.Core.Utils;
using SharpEngine.Core.Widget;

namespace Testing;

public class MyScene : Scene
{
    private SpriteSheetComponent _sprite;
    private TextComponent _text;

    public MyScene()
    {
        var movable = new Entity();
        movable.AddComponent(new TransformComponent(new Vec2(100)));
        movable.AddComponent(new SpriteSheetComponent("portal", new Vec2(100), [
            new("animation", Enumerable.Range(0, 10).Select(x => Convert.ToUInt32(x)).ToList(), 0.2f, false)
        ], "animation"));
        movable.AddComponent(new ControlComponent());
        AddEntity(movable);

        var entity = new Entity();
        entity.AddComponent(new TransformComponent(new Vec2(300)));
        _sprite = entity.AddComponent(new SpriteSheetComponent("portal", new Vec2(100), [
            new("animation", Enumerable.Range(0, 10).Select(x => Convert.ToUInt32(x)).ToList(), 0.2f, false)
        ], "animation"));
        _sprite.AnimationEnded += (_, _) => Console.WriteLine($"Animation Ended : {_sprite.Anim}");
        _text = entity.AddComponent(new TextComponent("0", offset: new Vec2(0, 50), fontSize: 25));
        movable.AddChild(entity);

        var button = new Button(
            new Vec2(100, 100),
            "Hello World",
            "RAYLIB_DEFAULT",
            new Vec2(200, 50),
            Color.White,
            Color.Blue,
            20
         );

        button.Clicked += (_, _) => Console.WriteLine("Button Clicked");

        AddWidget(button);
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (InputManager.IsKeyPressed(Key.A))
        {
            Widgets[^1].Displayed = false;
        }
        if(InputManager.IsKeyPressed(Key.Z))
        {
            _sprite.Replay();
        }

        _text.Text = _sprite.CurrentImage.ToString();
    }
}
