using Godot;
using System;

public partial class task_adder : Panel
{
	// Called when the node enters the scene tree for the first time.


	Button add_button;

	[Export]
	PackedScene scene;
	[Export]
	Node column;
	[Export]
	LineEdit lineEdit;
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

		if (lineEdit.Text != "")
			GD.PrintRich($"added [b]{scene.Instantiate().Name}[/b] to [b][hint=name = {lineEdit.Text}]{column.Name}[/hint][/b]");
		else
			GD.PrintRich($"added [b]{scene.Instantiate().Name}[/b] to [b]{column.Name}[/b]");

	}
}
