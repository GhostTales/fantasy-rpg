using Godot;
using System;

public partial class task_adder : ColorRect
{
	// Called when the node enters the scene tree for the first time.


	Button add_button;

	[Export]
	PackedScene scene;
	[Export]
	Node column;
	public override void _Ready()
	{
		add_button = GetNode<Button>("Button");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_button_pressed()
	{
		column.AddChild(scene.Instantiate());
		GD.Print($"added {scene.Instantiate().Name} to {column.Name}");
	}
}
