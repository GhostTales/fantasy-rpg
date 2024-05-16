using Godot;
using System;

public partial class task_adder : Panel
{
	private Timer delayTimer;
	
	[Export]
	PackedScene scene;
	[Export]
	Node column;
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
		column.AddChild(scene.Instantiate());

		if (lineEdit.Text != "")
			GD.PrintRich($"added [b]{scene.Instantiate().Name}[/b] to [b][hint=name = {lineEdit.Text}]{column.Name}[/hint][/b]");
		else
			GD.PrintRich($"added [b]{scene.Instantiate().Name}[/b] to [b]{column.Name}[/b]");

	}
	
	void _on_timer_timeout()
{
	column.AddChild(scene.Instantiate());

		if (lineEdit.Text != "")
			GD.PrintRich($"added [b]{scene.Instantiate().Name}[/b] to [b][hint=name = {lineEdit.Text}]{column.Name}[/hint][/b]");
		else
			GD.PrintRich($"added [b]{scene.Instantiate().Name}[/b] to [b]{column.Name}[/b]");

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




