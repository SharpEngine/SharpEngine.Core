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
        AddWidget(new ProgressBar(new Vec2(600, 100), new Vec2(500, 100), Color.Green, 48));
        AddWidget(new ProgressBar(new Vec2(400), new Vec2(50, 200), Color.Green, 48, false));
        AddWidget(new Slider(new Vec2(600, 800), new Vec2(500, 100), Color.Green, 48));
        AddWidget(new Slider(new Vec2(800, 400), new Vec2(50, 200), Color.Green, 48, false));
    }
}
