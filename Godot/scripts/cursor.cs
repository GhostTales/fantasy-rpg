using Godot;
using System;

public partial class cursor : CharacterBody2D
{
    [Export]
    WebSocketClient Client;
    [Export]
    float speed = 25;


    Vector2 coords = new Vector2(0, 0);
    string[] packetParts;

    public override void _Process(double delta)
    {
        if (Client.packet != null)
            if (Client.packet.Length != 0)
            {
                packetParts = Client.packet.Split(",");
                coords = new Vector2(packetParts[0].ToFloat() * 2.5f, packetParts[1].ToFloat() * 2.5f);

            }
            else GD.Print("Packet length is 0");
        else GD.Print("Packet is null");

        Position = Position.MoveToward(coords, (1 + (float)delta) * speed);

    }


}
