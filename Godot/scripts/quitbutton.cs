using Godot;
using System;

public partial class quitbutton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	private void _on_pressed()
	{
	GetTree().Quit();// Replace with function body.
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
