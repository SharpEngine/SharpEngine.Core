﻿using System;
using System.Collections.Generic;
using SharpEngine.Core.Input;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component which represents Controls
/// </summary>
public class ControlComponent : Component
{
    /// <summary>
    /// Type of Control
    /// </summary>
    public ControlType ControlType { get; set; }

    /// <summary>
    /// Speed of Control
    /// </summary>
    public int Speed { get; set; }

    /// <summary>
    /// If Control use GamePad
    /// </summary>
    public bool UseGamePad { get; set; }

    /// <summary>
    /// Index of GamePad
    /// </summary>
    public int GamePadIndex { get; set; }

    /// <summary>
    /// Jump force
    /// </summary>
    public float JumpForce { get; set; }

    /// <summary>
    /// If Entity is moving
    /// </summary>
    public bool IsMoving { get; protected set; }

    /// <summary>
    /// If Entity can jump
    /// </summary>
    public bool CanJump { get; protected set; }

    /// <summary>
    /// Direction of Control
    /// </summary>
    public Vec2 Direction { get; protected set; }

    private readonly Dictionary<ControlKey, Key> _keys;
    private TransformComponent? _transform;
    private CollisionComponent? _basicPhysics;

    /// <summary>
    /// Create Control Component
    /// </summary>
    /// <param name="controlType">Control Type (FourDirection)</param>
    /// <param name="speed">Speed (300)</param>
    /// <param name="jumpForce">Jump Force (3)</param>
    /// <param name="useGamePad">Use Game Pad (false)</param>
    /// <param name="gamePadIndex">Game Pad Index (1)</param>
    public ControlComponent(
        ControlType controlType = ControlType.FourDirection,
        int speed = 300,
        float jumpForce = 2,
        bool useGamePad = false,
        int gamePadIndex = 1
    )
    {
        ControlType = controlType;
        Speed = speed;
        IsMoving = false;
        UseGamePad = useGamePad;
        GamePadIndex = gamePadIndex;
        JumpForce = jumpForce;
        CanJump = true;
        _keys = new Dictionary<ControlKey, Key>
        {
            { ControlKey.Up, Key.Up },
            { ControlKey.Down, Key.Down },
            { ControlKey.Left, Key.Left },
            { ControlKey.Right, Key.Right }
        };
    }

    /// <summary>
    /// Get Key for a Control
    /// </summary>
    /// <param name="controlKey">Key Control</param>
    /// <returns>Key</returns>
    public Key GetKey(ControlKey controlKey) => _keys[controlKey];

    /// <summary>
    /// Set Key for a Control
    /// </summary>
    /// <param name="controlKey">Key Control</param>
    /// <param name="key">Key</param>
    public void SetKey(ControlKey controlKey, Key key) => _keys[controlKey] = key;

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        _transform = Entity?.GetComponentAs<TransformComponent>();
        _basicPhysics = Entity?.GetComponentAs<CollisionComponent>();
    }

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);

        IsMoving = false;

        if (_transform == null)
            return;

        var move = GetMovement();
        if (move.IsZero())
            return;

        IsMoving = true;
        Direction = move.Normalized();
        var newPos = new Vec2(
            _transform.Position.X + Direction.X * Speed * delta,
            _transform.Position.Y + Direction.Y * Speed * delta
        );
        var newPosX = new Vec2(
            _transform.Position.X + Speed * delta * (Direction.X < 0 ? -1 : 1),
            _transform.Position.Y
        );
        var newPosY = new Vec2(
            _transform.Position.X,
            _transform.Position.Y + Speed * delta * (Direction.Y < 0 ? -1 : 1)
        );
        if (_basicPhysics == null || _basicPhysics.CanGo(newPos))
            _transform.Position = newPos;
        else if (Direction.X != 0 && _basicPhysics.CanGo(newPosX))
        {
            _transform.Position = newPosX;
            Direction = new Vec2(Direction.X < 0 ? -1 : 1, 0);
        }
        else if (Direction.Y != 0 && _basicPhysics.CanGo(newPosY))
        {
            _transform.Position = newPosY;
            Direction = new Vec2(0, Direction.Y < 0 ? -1 : 1);
        }
        else
            IsMoving = false;
    }

    private Vec2 GetMovement()
    {
        return ControlType switch
        {
            ControlType.MouseFollow => GetMouseFollowMovement(),
            ControlType.LeftRight => GetLeftRightMovement(),
            ControlType.UpDown => GetUpDownMovement(),
            ControlType.FourDirection => GetFourDirectionMovement(),
            _ => throw new ArgumentException("Unknown Control Type")
        };
    }

    private Vec2 GetMouseFollowMovement()
    {
        var mp = InputManager.GetMousePosition();
        return mp - _transform!.Position;
    }

    private Vec2 GetFourDirectionMovement()
    {
        if (UseGamePad)
            return new Vec2(InputManager.GetGamePadAxis(GamePadIndex, GamePadAxis.LeftX), InputManager.GetGamePadAxis(GamePadIndex, GamePadAxis.LeftY));
        else
        {
            var dirX = 0;
            var dirY = 0;
            if (InputManager.IsKeyDown(_keys[ControlKey.Left]))
                dirX--;
            if (InputManager.IsKeyDown(_keys[ControlKey.Right]))
                dirX++;
            if (InputManager.IsKeyDown(_keys[ControlKey.Up]))
                dirY++;
            if (InputManager.IsKeyDown(_keys[ControlKey.Down]))
                dirY--;
            return new Vec2(dirX, dirY);
        }
    }

    private Vec2 GetLeftRightMovement()
    {
        if (UseGamePad)
            return new Vec2(InputManager.GetGamePadAxis(GamePadIndex, GamePadAxis.LeftX), 0);
        else
        {
            var dirX = 0;
            if (InputManager.IsKeyDown(_keys[ControlKey.Left]))
                dirX--;
            if (InputManager.IsKeyDown(_keys[ControlKey.Right]))
                dirX++;
            return new Vec2(dirX, 0);
        }
    }

    private Vec2 GetUpDownMovement()
    {
        if (UseGamePad)
            return new Vec2(0, InputManager.GetGamePadAxis(GamePadIndex, GamePadAxis.LeftY));
        else
        {
            var dirY = 0;
            if (InputManager.IsKeyDown(_keys[ControlKey.Up]))
                dirY++;
            if (InputManager.IsKeyDown(_keys[ControlKey.Down]))
                dirY--;
            return new Vec2(0, dirY);
        }
    }
}
