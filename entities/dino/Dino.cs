using Godot;
using System;
using System.Security.Cryptography;

public partial class Dino : CharacterBody2D
{
    [Export] float JumpPower { get; set; } = 75;
    [Export] float GroundLevel { get; set; } = 74f;
    [Export] float Gravity { get; set; } = 120f;
    enum States
    {
        Alive,
        Dead,
    }
    States CurrentState = States.Alive;
    Vector2 NewVelocity;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        NewVelocity = Velocity;
        switch (CurrentState)
        {
            case States.Alive:
                HandleAliveState(delta);
                break; 
            case States.Dead:
                HandleDeadState(delta);
                break;
        }
        if (IsOnFloor())
        {
            Position = Position with { Y = GroundLevel };
            if (NewVelocity.Y > 0)
            {
                NewVelocity.Y = 0;
            }
        }
        else
        {
            NewVelocity.Y += Gravity * (float)delta;
        }
        Velocity = NewVelocity;
        MoveAndSlide();
    }
    private void HandleAliveState(double delta)
    {
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            NewVelocity.Y -= JumpPower;
        }
    }
    private void HandleDeadState(double delta)
    {
        return;
    }
    private new bool IsOnFloor() // we're replacing the original method because we don't have a collision for floor.
    {
        if (Position.Y >= GroundLevel)
        {  
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnArea2DAreaEntered(Area2D Area)
    {
        if (Area.IsInGroup("death"))
        {
            QueueFree();
        }
    }

}

