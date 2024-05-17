using Godot;
using System;

public partial class cursor : CharacterBody2D
{
	[Export]
	WebSocketClient Client;
	[Export]
	float speed = 25;


	Vector2 coords = new Vector2(0, 0);
	float[,] corner;
	float verticalLength;
	float horizontalLength;
	string[] packetParts;

	public override void _Process(double delta)
	{
		if (Client.packet != null)
			if (Client.packet.Length != 0)
			{
				packetParts = Client.packet.Split(",");

				corner = new float[,] {
					{packetParts[2].ToFloat(), packetParts[3].ToFloat()}, // top left
					{packetParts[4].ToFloat(), packetParts[5].ToFloat()}, // top left
					{packetParts[6].ToFloat(), packetParts[7].ToFloat()}, // bottom left
					{packetParts[8].ToFloat(), packetParts[9].ToFloat()}  // bottom right
					};

				verticalLength = (corner[2, 1] - corner[0, 1] + corner[3, 1] - corner[1, 1]) / 2f;
				horizontalLength = (corner[1, 0] - corner[0, 0] + corner[3, 0] - corner[2, 0]) / 2f;

				float V_Scale = 900 / verticalLength;
				float H_Scale = 1600 / horizontalLength;


				coords = new Vector2((packetParts[0].ToFloat() - 85) * H_Scale, (packetParts[1].ToFloat() - 50) * V_Scale);
				//GD.Print($"{coords.ToString()}, {H_Scale}, {V_Scale}, {horizontalLength}, {verticalLength}");
			}
			else GD.Print("Packet length is 0");
		//else 
		//GD.Print("Packet is null");

		Position = Position.MoveToward(coords, (1 + (float)delta) * speed);

	}


}
