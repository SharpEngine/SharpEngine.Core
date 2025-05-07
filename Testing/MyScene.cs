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
        AddWidget(new LineInput(new Vec2(500), font: "RAYLIB_DEFAULT", fontSize: 25));
        AddWidget(new MultiLineInput(new Vec2(500, 200), font: "RAYLIB_DEFAULT", fontSize: 25));

        AddWidget(new TextureButton(new Vec2(200), texture: "portal"));
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

    public override void Draw()
    {
        base.Draw();

        SERender.DrawRectangle(500, 5, 98, 123, Color.Black, InstructionSource.UI, 8);
        SERender.DrawRectangle(5.4f, 5.1f, 98, 123f, Color.Red, InstructionSource.UI, 8);
    }
}
