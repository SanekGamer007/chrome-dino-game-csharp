using Godot;
using System;

public partial class Cactus : Area2D
{
	Vector2 NewPosition;
	float Scroll = -40f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Scroll -= (float)delta * 0.1f;
        NewPosition = Position;
		NewPosition.X += Scroll * (float)delta;
		Position = NewPosition;

		if (Position.X <= 0 - GetNode<CollisionShape2D>("CollisionShape2D").Shape.GetRect().Size.X) // universal offscreen killer based on size.
		{
			QueueFree();
		}
	}
}
