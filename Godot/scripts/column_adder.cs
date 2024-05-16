using Godot;
using System;

public partial class column_adder : Button
{
	private Timer delayTimer;

	[Export]
	PackedScene scene;
	[Export]
	Node row;
	public override void _Ready()
	{
		row.AddChild(scene.Instantiate());
		delayTimer = GetNode<Timer>("Timer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	void _on_timer_timeout()
	{
		row.AddChild(scene.Instantiate());
		GD.PrintRich($"added [b]{scene.Instantiate().Name}[/b] to [b]{row.Name}[/b]");
	}
	public void _on_pressed()
	{
		row.AddChild(scene.Instantiate());
		GD.PrintRich($"added [b]{scene.Instantiate().Name}[/b] to [b]{row.Name}[/b]");
	}
	private void _on_area_2d_body_entered(Node2D node)
	{
		delayTimer.Start();
	}

	private void _on_area_2d_body_exited(Node2D node)
	{
		delayTimer.Stop();
	}

}
