using Godot;
using System;

public partial class CalibrationButton : Control
{
    [Export]
    String scene;



    public void _on_button_pressed()
    {
        GetTree().ChangeSceneToFile(scene);
    }
}
