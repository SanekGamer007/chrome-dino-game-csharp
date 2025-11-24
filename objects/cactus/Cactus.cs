using Godot;
using System;
using System.Drawing;

public partial class Cactus : Area2D
{
	Vector2 NewPosition;
	public float Scroll = -40f;
	int CactusSize = 0; // 0 = small, 1 = medium, 2 = big

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        CactusSize = GD.RandRange(0, 2);
		switch (CactusSize)
		{
			case 0:
				SetSize(new Vector2(4f, 8f));
				break;
			case 1:
				SetSize(new Vector2(6f, 12f));
				break;
			case 2:
				SetSize(new Vector2(8f, 18f));
				break;
		}
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
	private void SetSize(Vector2 newsize)
	{
		GetNode<CollisionShape2D>("CollisionShape2D").Shape = new RectangleShape2D { Size = newsize };
    }
}
