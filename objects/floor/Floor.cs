using Godot;
using System;

public partial class Floor : Parallax2D
{
    float NewAutoScrollX;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        NewAutoScrollX = Autoscroll.X;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        NewAutoScrollX -= (float)delta * 0.1f;
        Autoscroll = new(NewAutoScrollX, 0f);

    }
}
