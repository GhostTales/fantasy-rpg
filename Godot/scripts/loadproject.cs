using Godot;
using System;
using System.IO;

public partial class loadproject : Button
{
	private string loadFilePath;
	private TextureRect textureRect;

	public override void _Ready()
	{
		// Set the file path where you want to load the screenshot from
		loadFilePath = "AppData/Roaming/project_touchscreen/screenshot.png";

	
	}

	private void LoadScreenshot()
	{
		// Load the image from the file
		Image screenshot = new Image();
		Error err = screenshot.Load(loadFilePath);
		if (err == Error.Ok)
		{
			// Create an ImageTexture and set the image
			ImageTexture imageTexture = new ImageTexture();
			imageTexture = ImageTexture.CreateFromImage(screenshot);

			// Assign the texture to the TextureRect
			textureRect.Texture = imageTexture;

			GD.Print("Screenshot loaded successfully!");
		}
		else
		{
			GD.Print("Failed to load screenshot: " + err.ToString());
		}
	}

	private void _on_pressed()
	{
		LoadScreenshot();
	}
}
