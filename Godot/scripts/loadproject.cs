using Godot;
using System;

public partial class LoadButton2 : Button
{
// Note: This can be called from anywhere inside the tree. This function is
// path independent.
public void LoadGame()
{
	if (!FileAccess.FileExists("user://savegame.save"))
	{
		return; // Error! We don't have a save to load.
	}

	// We need to revert the game state so we're not cloning objects during loading.
	// This will vary wildly depending on the needs of a project, so take care with
	// this step.
	// For our example, we will accomplish this by deleting saveable objects.
	var saveNodes = GetTree().GetNodesInGroup("Persist");
	foreach (Node saveNode in saveNodes)
	{
		saveNode.QueueFree();
	}

	// Load the file line by line and process that dictionary to restore the object
	// it represents.
	using var saveGame = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Read);

	while (saveGame.GetPosition() < saveGame.GetLength())
	{
		var jsonString = saveGame.GetLine();

		// Creates the helper class to interact with JSON
		var json = new Json();
		var parseResult = json.Parse(jsonString);
		if (parseResult != Error.Ok)
		{
			GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");
			continue;
		}

		// Get the data from the JSON object
		var nodeData = new Godot.Collections.Dictionary<string, Variant>((Godot.Collections.Dictionary)json.Data);

		// Firstly, we need to create the object and add it to the tree and set its position.
		var newObjectScene = GD.Load<PackedScene>(nodeData["Filename"].ToString());
		var newObject = newObjectScene.Instantiate<Node>();
		GetNode(nodeData["Parent"].ToString()).AddChild(newObject);
		newObject.Set(Node2D.PropertyName.Position, new Vector2((float)nodeData["PosX"], (float)nodeData["PosY"]));

		// Now we set the remaining variables.
		foreach (var (key, value) in nodeData)
		{
			if (key == "Filename" || key == "Parent" || key == "PosX" || key == "PosY")
			{
				continue;
			}
			newObject.Set(key, value);
		}
	}
}

private void _on_pressed()
{
	LoadGame();	// Replace with function body.
}
}

