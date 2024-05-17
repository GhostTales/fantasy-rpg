using Godot;
using System;

public partial class quitbutton : Button
{
	private Timer delayTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		delayTimer = GetNode<Timer>("Timer");
	}
	private void _on_pressed()
	{
		GD.Print("quit");
		GetTree().Quit();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_timer_timeout()
	{
		GD.Print("quit");
		GetTree().Quit();
	}


	private void _on_area_2d_body_entered(Node2D body)
	{
		delayTimer.Start();
	}


	private void _on_area_2d_body_exited(Node2D body)
	{
		delayTimer.Stop();
	}
}



