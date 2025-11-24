using Godot;
using System;

public partial class Game : Node2D
{
	int CactusCount;
	float GameSpeed = 1f;
    float ScrollSpeed = -40f;
    [Export]
    public PackedScene CactusScene { get; set; } // Apparently there is no preload() in c#, so we have to do this.

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		SpawnLoop();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override async void _Process(double delta)
	{
		GameSpeed += (float)delta * 0.0025f;
        ScrollSpeed -= (float)delta * 0.1f;
        CactusCount = GetTree().GetNodesInGroup("death").Count;
	}

	private async void SpawnLoop()
	{
		while (true)
		{
            await ToSignal(GetTree().CreateTimer((2f + GD.RandRange(0.1, 0.5)) / GameSpeed), Timer.SignalName.Timeout);
            SpawnCactus();
        }
	}

	private async void SpawnCactus()
	{
		if (CactusCount <= Mathf.RoundToInt(2 * GameSpeed))
		{
			Cactus cactusInstance = (Cactus)CactusScene.Instantiate();
			cactusInstance.Position = new Vector2(245f, 75f);
			cactusInstance.Scroll = ScrollSpeed;
			GetNode<Node2D>("ThyDeathIsNow").AddChild(cactusInstance);
		}
    }
}
