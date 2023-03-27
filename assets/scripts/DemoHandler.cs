using CsgoVisualAnalGodot.assets.demo_playback.scripts;
using DemoTracker.Structs;
using Godot;
using System.IO;

public partial class DemoHandler : Node
{
	public uint FinalTick = 0;

	[Signal]
	public delegate void DemoLoadedEventHandler(string filename);

	private DemoTracker.DemoSummary _demoSummary;
	private PackedScene _loadingScreen;

	private struct CachedTickSummary
	{
		public readonly uint Tick;
		public readonly TickSummary TickSummary;

		public CachedTickSummary(uint tick, TickSummary tickSummary)
		{
			Tick = tick;
			TickSummary = tickSummary;
		}
	}
	CachedTickSummary? _cachedTick;

	public override void _Ready()
	{
		_loadingScreen = GD.Load<PackedScene>("res://assets/loading_screen/loading_screen.tscn");
	}

	public void ProcessDemo(string path)
	{
		path = ProjectSettings.GlobalizePath(path);
		_demoSummary = new DemoTracker.DemoSummary(path);

		// Show loading screen
		Node GUI = GetNode("/root/Main/GUI");
		Node instance = _loadingScreen.Instantiate();
		GUI.AddChild(instance);

		_demoSummary.Process();
		string filename = Path.GetFileName(path);
		EmitSignal(SignalName.DemoLoaded, filename);

		// Remove loading screen
		instance.QueueFree();
	}

	public TickSummary GetTick(uint tick)
	{
		if (_cachedTick.HasValue && _cachedTick.Value.Tick == tick)
		{
			return _cachedTick.Value.TickSummary;
		}
		
		TickSummary tickSummary = _demoSummary.GetTickSummary((int)tick);
		_cachedTick = new CachedTickSummary(tick, tickSummary);
		return tickSummary;
	}
}
