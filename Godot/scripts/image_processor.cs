using Godot;
using System;
using Emgu.CV;
using ProcessImage;
using System.Drawing;
public partial class image_processor : Node
{
	VideoCapture _capture;
	private Mat _frame;

	ImageProcessor processor = new ImageProcessor(System.Drawing.Color.FromArgb(0, 168, 181, 138), 15);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		_capture = new VideoCapture(0);
		_frame = new Mat();
		if (_capture != null)
		{
			try
			{
				_capture.Start();
			}
			catch (Exception ex)
			{
				GD.Print(ex.Message);
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_capture.ImageGrabbed += ProcessFrame;
	}

	private void ProcessFrame(object sender, EventArgs e)
	{
		if (_capture != null && _capture.Ptr != IntPtr.Zero)
		{
			_capture.Retrieve(_frame, 0);
			processor._frame = _frame;
			processor.ProcessImage();

			GD.Print($"{processor.avgX}, {processor.avgY}");
		}
	}


}
