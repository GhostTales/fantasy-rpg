using Godot;
using System;

public partial class column_adder : Button
{
	[Export]
	PackedScene scene;
	[Export]
	Node row;
	public override void _Ready()
	{
		row.AddChild(scene.Instantiate());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_pressed()
	{
		row.AddChild(scene.Instantiate());
		GD.Print($"added {scene.Instantiate().Name} to {row.Name}");
	}
}
