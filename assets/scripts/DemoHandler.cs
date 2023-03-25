using Godot;

public partial class DemoHandler : Node
{
	private DemoTracker.DemoSummary _demoSummary;
	private PackedScene _loadingScreen;

	public override void _Ready()
	{
		_loadingScreen = GD.Load<PackedScene>("res://assets/loading_screen/loading_screen.tscn");
	}

	public void ProcessDemo(string path)
	{
		_demoSummary = new DemoTracker.DemoSummary("demos/demo1.dem");

		// Show loading screen
		Node GUI = GetNode("/root/Main/GUI");
		Node instance = _loadingScreen.Instantiate();
		GUI.AddChild(instance);
		
		_demoSummary.Process();

		// Remove loading screen
		GUI.RemoveChild(instance);
	}
}
