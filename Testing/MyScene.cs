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
        AddWidget(new LineInput(new Vec2(500), font: "RAYLIB_DEFAULT", fontSize: 25));
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (InputManager.IsKeyPressed(Key.A))
        {
            DebugManager.Log(LogLevel.LogInfo, ((LineInput)Widgets[0]).Text);
        }

        if(InputManager.IsKeyPressed(Key.Z))
        {
            ((LineInput)Widgets[0]).Secret = !((LineInput)Widgets[0]).Secret;
        }
    }
}
