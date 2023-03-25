using Godot;
using System;

public partial class DemoBrowser : FileDialog
{
	private DemoHandler _demoHandler;

	public override void _EnterTree()
	{
		_demoHandler = GetNode<DemoHandler>("/root/DemoHandler");
		this.FileSelected += _demoHandler.ProcessDemo;
	}

	public override void _ExitTree()
	{
		this.FileSelected -= _demoHandler.ProcessDemo;
	}
}
