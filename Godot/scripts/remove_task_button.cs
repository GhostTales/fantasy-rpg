using Godot;
using System;

public partial class remove_task_button : Button
{
	private Timer delayTimer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		delayTimer =  GetNode<Timer>("Timer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_pressed()
	{
	GetParent().QueueFree();
	}

void _on_timer_timeout()
{
	GetParent().QueueFree();
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

