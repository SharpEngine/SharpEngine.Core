using System;
using System.Collections.Generic;
using System.Linq;
using Raylib_cs;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer.Instructions;

namespace SharpEngine.Core.Renderer;

/// <summary>
/// Static class which used to render textures, texts or rectangles
/// </summary>
public static class SERender
{
    /// <summary>
    /// Number of Last Instructions
    /// </summary>
    public static int LastInstructionsNumber { get; set; }

    /// <summary>
    /// Number of Last Entity Instructions
    /// </summary>
    public static int LastEntityInstructionsNumber { get; set; }

    /// <summary>
    /// Number of Last UI Instructions
    /// </summary>
    public static int LastUIInstructionsNumber { get; set; }

    /// <summary>
    /// Current Instructions to be rendered
    /// </summary>
    private static List<Instruction> Instructions = [];

    internal static void DrawInstructions(List<Instruction> instructions)
    {
        foreach (var instruction in instructions)
        {
            LastInstructionsNumber++;
            if (instruction.Source == InstructionSource.Entity)
                LastEntityInstructionsNumber++;
            else
                LastUIInstructionsNumber++;
            instruction.Execute();
        }
    }

    /// <summary>
    /// Draw all instructions in Window
    /// </summary>
    /// <param name="window">Window</param>
    public static void Draw(Window window)
    {
        LastInstructionsNumber = 0;
        LastEntityInstructionsNumber = 0;
        LastUIInstructionsNumber = 0;
        List<Instruction> entityInstructions = [];
        List<Instruction> uiInstructions = [];

        foreach (var instruction in Instructions)
        {
            switch (instruction.Source)
            {
                case InstructionSource.Entity:
                    entityInstructions.Add(instruction);
                    break;
                case InstructionSource.UI:
                    uiInstructions.Add(instruction);
                    break;
                default:
                    throw new ArgumentException("Unknown instruction source");
            }
        }

        entityInstructions.Sort((i1, i2) => i1.ZLayer.CompareTo(i2.ZLayer));
        uiInstructions.Sort((i1, i2) => i1.ZLayer.CompareTo(i2.ZLayer));

        Raylib.BeginMode2D(window.CameraManager.Camera2D);
        DrawInstructions(entityInstructions);
        Raylib.EndMode2D();

        DrawInstructions(uiInstructions);

        Instructions.Clear();
    }

    /// <summary>
    /// Add Shader Mode Instructions
    /// </summary>
    /// <param name="shader">Shader</param>
    /// <param name="source">Instruction Source</param>
    /// <param name="zLayer">Z Layer</param>
    /// <param name="shaderAction">Function which render in shader mode</param>
    public static void ShaderMode(
        Shader shader,
        InstructionSource source,
        float zLayer,
        Action shaderAction
    )
    {
        var instructions = new List<Instruction>(Instructions);
        Instructions.Clear();
        shaderAction();
        var instruction = new ShaderMode
        {
            Source = source,
            ZLayer = zLayer,
            Parameters = [shader]
        };
        instruction.Parameters.AddRange(Instructions.Select(x => (object)x));
        Instructions = instructions;
        Instructions.Add(instruction);
    }

    /// <summary>
    /// Add Scissor Mode Instructions
    /// </summary>
    /// <param name="posX">Position X</param>
    /// <param name="posY">Position Y</param>
    /// <param name="width">Width</param>
    /// <param name="height">Height</param>
    /// <param name="source">Instruction Source</param>
    /// <param name="zLayer">Z Layer</param>
    /// <param name="scissorAction">Function which render in scissor mode</param>
    public static void ScissorMode(
        float posX,
        float posY,
        float width,
        float height,
        InstructionSource source,
        float zLayer,
        Action scissorAction
    )
    {
        var instructions = new List<Instruction>(Instructions);
        Instructions.Clear();
        scissorAction();
        var instruction = new ScissorMode
        {
            Source = source,
            ZLayer = zLayer,
            Parameters = [posX, posY, width, height]
        };
        instruction.Parameters.AddRange(Instructions.Select(x => (object)x));
        Instructions = instructions;
        Instructions.Add(instruction);
    }

    /// <summary>
    /// Add Draw Rectangle Pro Instruction
    /// </summary>
    /// <param name="rectangle">Rectangle</param>
    /// <param name="origin">Origin</param>
    /// <param name="rotation">Rotation</param>
    /// <param name="color">Color</param>
    /// <param name="source">Instruction Source</param>
    /// <param name="zLayer">Z Layer</param>
    public static void DrawRectangle(
        Rect rectangle,
        Vec2 origin,
        float rotation,
        Utils.Color color,
        InstructionSource source,
        float zLayer
    )
    {
        Instructions.Add(
            new DrawRectanglePro
            {
                Source = source,
                ZLayer = zLayer,
                Parameters = [rectangle, origin, rotation, color]
            }
        );
    }

    /// <summary>
    /// Add Draw Rectangle Instruction
    /// </summary>
    /// <param name="posX">Position X</param>
    /// <param name="posY">Position Y</param>
    /// <param name="width">Width</param>
    /// <param name="height">Height</param>
    /// <param name="color">Color</param>
    /// <param name="source">Instruction Source</param>
    /// <param name="zLayer">Z Layer</param>
    public static void DrawRectangle(
        float posX,
        float posY,
        float width,
        float height,
        Utils.Color color,
        InstructionSource source,
        float zLayer
    )
    {
        Instructions.Add(
            new DrawRectangle
            {
                Source = source,
                ZLayer = zLayer,
                Parameters = [posX, posY, width, height, color]
            }
        );
    }

    /// <summary>
    /// Add Draw Rectangle Lines Ex Instruction
    /// </summary>
    /// <param name="rect">Rectangle</param>
    /// <param name="borderSize">Border Size</param>
    /// <param name="borderColor">Border Color</param>
    /// <param name="source">Instruction Source</param>
    /// <param name="zLayer">Z Layer</param>
    public static void DrawRectangleLines(
        Rect rect,
        float borderSize,
        Utils.Color borderColor,
        InstructionSource source,
        float zLayer
    )
    {
        Instructions.Add(
            new DrawRectangleLinesEx
            {
                Source = source,
                ZLayer = zLayer,
                Parameters = [rect, borderSize, borderColor]
            }
        );
    }

    /// <summary>
    /// Add Draw Circle Lines Instruction
    /// </summary>
    /// <param name="posX">Position X</param>
    /// <param name="posY">Position Y</param>
    /// <param name="radius">Radius</param>
    /// <param name="borderColor">Border Color</param>
    /// <param name="source">Instruction Source</param>
    /// <param name="zLayer">Z Layer</param>
    public static void DrawCircleLines(
        float posX,
        float posY,
        float radius,
        Utils.Color borderColor,
        InstructionSource source,
        float zLayer
    )
    {
        Instructions.Add(
            new DrawCircleLines
            {
                Source = source,
                ZLayer = zLayer,
                Parameters = [posX, posY, radius, borderColor]
            }
        );
    }

    /// <summary>
    /// Add Draw Texture Pro Instruction
    /// </summary>
    /// <param name="texture">Texture</param>
    /// <param name="src">Rectangle Source</param>
    /// <param name="dest">Rectangle Destination</param>
    /// <param name="origin">Origin</param>
    /// <param name="rotation">Rotation</param>
    /// <param name="tint">Color Tint</param>
    /// <param name="source">Instruction Source</param>
    /// <param name="zLayer">Z Layer</param>
    public static void DrawTexture(
        Texture2D texture,
        Rect src,
        Rect dest,
        Vec2 origin,
        float rotation,
        Utils.Color tint,
        InstructionSource source,
        float zLayer
    )
    {
        Instructions.Add(
            new DrawTexturePro
            {
                Source = source,
                ZLayer = zLayer,
                Parameters = [texture, src, dest, origin, rotation, tint]
            }
        );
    }

    /// <summary>
    /// Add Draw Text Pro Instruction
    /// </summary>
    /// <param name="font">Font</param>
    /// <param name="text">Text</param>
    /// <param name="position">Position</param>
    /// <param name="origin">Origin</param>
    /// <param name="rotation">Rotation</param>
    /// <param name="fontSize">Font Size</param>
    /// <param name="spacing">Spacing</param>
    /// <param name="color">Color</param>
    /// <param name="source">Instruction Source</param>
    /// <param name="zLayer">Z Layer</param>
    public static void DrawText(
        Font font,
        string text,
        Vec2 position,
        Vec2 origin,
        float rotation,
        float fontSize,
        float spacing,
        Utils.Color color,
        InstructionSource source,
        float zLayer
    )
    {
        Instructions.Add(
            new DrawTextPro
            {
                Source = source,
                ZLayer = zLayer,
                Parameters =
                [
                    font,
                    text,
                    position,
                    origin,
                    rotation,
                    fontSize,
                    spacing,
                    color
                ]
            }
        );
    }

    /// <summary>
    /// Add Draw Text Ex Instruction
    /// </summary>
    /// <param name="font">Font</param>
    /// <param name="text">Text</param>
    /// <param name="position">Position</param>
    /// <param name="fontSize">Font Size</param>
    /// <param name="spacing">Spacing</param>
    /// <param name="color">Color</param>
    /// <param name="source">Instruction Source</param>
    /// <param name="zLayer">Z Layer</param>
    public static void DrawText(
        Font font,
        string text,
        Vec2 position,
        float fontSize,
        float spacing,
        Utils.Color color,
        InstructionSource source,
        float zLayer
    )
    {
        Instructions.Add(
            new DrawTextEx
            {
                Source = source,
                ZLayer = zLayer,
                Parameters = [font, text, position, fontSize, spacing, color]
            }
        );
    }
}
