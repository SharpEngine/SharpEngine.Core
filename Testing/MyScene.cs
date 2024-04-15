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
    public MyScene()
    {
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
    }
}
