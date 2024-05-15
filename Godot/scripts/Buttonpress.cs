using Godot;
using System;

public partial class Buttonpress : Area2D
{
	private Timer delayTimer;
	
	[Export]
	PackedScene scene;
	[Export]
	Node row;
	public override void _Ready()
	{
		row.AddChild(scene.Instantiate());
		delayTimer =  GetNode<Timer>("Timer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	 void _on_timer_timeout()
			{
			GD.Print("Button pressed");
			row.AddChild(scene.Instantiate());
		GD.PrintRich($"added [b]{scene.Instantiate().Name}[/b] to [b]{row.Name}[/b]");
			}
	
	private void _on_area_entered(Area2D area)
{
	// Replace with function body.
	GD.Print("Mouse entered the button area...");
	delayTimer.Start();
}

private void _on_area_exited(Area2D area)
{
	GD.Print("Mouse exit on button area.."); // Replace with function body.
	delayTimer.Stop();
}
}

