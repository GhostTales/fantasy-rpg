using Godot;
using System;

public partial class reconnect_color_detect : Control
{
    [Export]
    PackedScene Client;
    [Export]
    Node parent;



    public void _on_reconnect_pressed()
    {
        var c = GD.Load<PackedScene>(Client.ResourcePath);
        parent.GetChild<Node>(2).QueueFree();
        parent.AddChild(c.Instantiate());
    }
}
