using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Raylib_cs;
using SharpEngine.Core.Math;
using SharpEngine.Core.Utils.EventArgs;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class, which display a selector
/// </summary>
[UsedImplicitly]
public class Selector : Widget
{
    /// <summary>
    /// Values of Selector
    /// </summary>
    [UsedImplicitly]
    public List<string> Values { get; }

    /// <summary>
    /// Selected Index of Selector
    /// </summary>
    [UsedImplicitly]
    public int SelectedIndex { get; set; }

    /// <summary>
    /// Left Button of Selector
    /// </summary>
    [UsedImplicitly]
    public Button LeftButton { get; }

    /// <summary>
    /// Right Button of Selector
    /// </summary>
    [UsedImplicitly]
    public Button RightButton { get; }

    /// <summary>
    /// Label of Selector
    /// </summary>
    [UsedImplicitly]
    public Label LabelValue { get; }

    /// <summary>
    /// Font of Selector
    /// </summary>
    [UsedImplicitly]
    public string Font { get; set; }

    /// <summary>
    /// Font Size of Selector
    /// </summary>
    [UsedImplicitly]
    public int? FontSize { get; set; }

    /// <summary>
    /// Event trigger when the value is changed
    /// </summary>
    [UsedImplicitly]
    public event EventHandler<ValueEventArgs<string>>? ValueChanged;

    /// <summary>
    /// Selected Value
    /// </summary>
    [UsedImplicitly]
    public string Selected => Values[SelectedIndex];

    /// <summary>
    /// Create Selector
    /// </summary>
    /// <param name="position">Position</param>
    /// <param name="values">Values of Selector</param>
    /// <param name="font">Font of Selector</param>
    /// <param name="currentIndex">Current Index of Selector</param>
    /// <param name="fontSize">Font Size of Selector</param>
    /// <param name="zLayer">Z Layer</param>
    public Selector(
        Vec2 position,
        List<string>? values = null,
        string font = "",
        int currentIndex = 0,
        int? fontSize = null,
        int zLayer = 0
    )
        : base(position, zLayer)
    {
        Values = values ?? [];
        SelectedIndex = currentIndex;
        Font = font;
        FontSize = fontSize;

        LabelValue = new Label(Vec2.Zero, Selected, font, fontSize: fontSize, zLayer: zLayer);
        LeftButton = new Button(
            Vec2.Zero,
            "<",
            font,
            new Vec2(30),
            fontSize: fontSize,
            zLayer: zLayer
        );
        RightButton = new Button(
            Vec2.Zero,
            ">",
            font,
            new Vec2(30),
            fontSize: fontSize,
            zLayer: zLayer
        );

        AddChild(LabelValue);
        AddChild(LeftButton).Clicked += LeftButtonClick;
        AddChild(RightButton).Clicked += RightButtonClick;
    }

    private void LeftButtonClick(object? sender, EventArgs e)
    {
        var old = Selected;
        if (SelectedIndex == 0)
            SelectedIndex = Values.Count - 1;
        else
            SelectedIndex--;
        LabelValue.Text = Selected;

        ValueChanged?.Invoke(
            this,
            new ValueEventArgs<string> { OldValue = old, NewValue = Selected }
        );
    }

    private void RightButtonClick(object? sender, EventArgs e)
    {
        var old = Selected;
        if (SelectedIndex == Values.Count - 1)
            SelectedIndex = 0;
        else
            SelectedIndex++;
        LabelValue.Text = Selected;

        ValueChanged?.Invoke(
            this,
            new ValueEventArgs<string> { OldValue = old, NewValue = Selected }
        );
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        var font = Scene?.Window?.FontManager.GetFont(Font);

        if (font != null)
        {
            var fontSize = FontSize ?? font.Value.BaseSize;
            var maxSizeX = 0f;
            foreach (var value in Values)
            {
                var size = Raylib.MeasureTextEx(font.Value, value, fontSize, 2);
                if (maxSizeX < size.X)
                    maxSizeX = size.X;
            }

            LeftButton.Position = new Vec2(-maxSizeX / 2 - 30, 0);
            RightButton.Position = new Vec2(maxSizeX / 2 + 30, 0);
        }
    }
}
