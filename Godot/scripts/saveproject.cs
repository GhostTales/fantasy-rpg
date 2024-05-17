using Godot;
using System;
using System.IO;

public partial class saveproject : Button
{
	private string saveFilePath;

	public override void _Ready()
	{
		// Resolve the environment variable and set the file path
		saveFilePath = System.Environment.GetEnvironmentVariable("APPDATA") + "/project_touchscreen";

		// Ensure the directory exists
		if (!Directory.Exists(saveFilePath))
		{
			Directory.CreateDirectory(saveFilePath);
		}
	}

	private void SaveScreenshot()
	{
		// Capture the current scene as an image
		Viewport rootViewport = GetViewport();
		Image screenshot = rootViewport.GetTexture().GetImage();


		// Generate a unique file name for the screenshot (e.g., based on timestamp)
		string screenshotName = "screenshot_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

		// Construct the full file path for saving
		string screenshotPath = Path.Combine(saveFilePath, screenshotName);

		// Save the screenshot image to the file path
		Error err = screenshot.SavePng(screenshotPath);
		if (err != Error.Ok)
		{
			GD.PrintErr("Failed to save screenshot: " + err.ToString());
		}
		else GD.Print("Saved screenshot");
	}
		
	private void _on_pressed()
	{
		SaveScreenshot();
	}
}
