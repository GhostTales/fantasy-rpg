using Godot;
using System;

public partial class column_remover : Panel
{
	// Called when the node enters the scene tree for the first time.
	private Timer delayTimer;

	[Export]
	Node column;
	[Export]
	Node remove;
	[Export]
	LineEdit lineEdit;
	public override void _Ready()
	{
		delayTimer =  GetNode<Timer>("Timer");
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
	
	void _on_timer_timeout()
{
	if (lineEdit.Text != "")
			GD.PrintRich($"removed [b][hint=name = {lineEdit.Text}]{column.Name}[/hint][/b]");
		else
			GD.PrintRich($"removed [b]{column.Name}[/b]");

		remove.QueueFree();
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

