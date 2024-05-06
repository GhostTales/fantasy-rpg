using Godot;
using System;

public partial class column_remover : Panel
{
	// Called when the node enters the scene tree for the first time.

	[Export]
	Node column;
	[Export]
	Node remove;
	[Export]
	LineEdit lineEdit;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_button_pressed()
	{
		if (lineEdit.Text != "")
			GD.PrintRich($"removed [b][hint=name = {lineEdit.Text}]{column.Name}[/hint][/b]");
		else
			GD.PrintRich($"removed [b]{column.Name}[/b]");

		remove.QueueFree();
	}
}
