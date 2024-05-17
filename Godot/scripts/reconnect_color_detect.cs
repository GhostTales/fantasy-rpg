using Godot;
using System;

public partial class reconnect_color_detect : Control
{
    [Export]
    WebSocketClient Client;

    public void _on_reconnect_pressed()
    {
        Client.Connect();
    }
}
