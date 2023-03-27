using CsgoVisualAnalGodot.assets.demo_playback.scripts;
using Godot;

public partial class Playback : Node
{
	public PlaybackState State { get; private set; } = PlaybackState.Stopped;
	public double Seconds { get; private set; } = 0f;
	public uint Tick { get; private set; } = 0;

	private float _timeScale = 1f;
	public float TimeScale
	{
		get => _timeScale;
		set
		{
			_timeScale = Mathf.Clamp(value, 0f, 4f);
		}
	}

	private float _tickRate = 16f;
	public float TickRate
	{
		get => _tickRate;
		private set
		{
			_tickRate = Mathf.Max(0, value);
		}
	}

	[Signal]
	public delegate void PlaybackStateChangedEventHandler(PlaybackState state);

	private DemoHandler _demoHandler;

	public override void _Ready()
	{
		_demoHandler = GetNode<DemoHandler>("/root/DemoHandler");
		_demoHandler.DemoLoaded += _on_demo_handler_demo_loaded;
	}

	public override void _Process(double delta)
	{
		UpdateTick(delta);
	}

	public void PlayPauseToggle()
	{
		switch (State)
		{
			case PlaybackState.Playing:
				SetState(PlaybackState.Paused);
				break;
			case PlaybackState.Paused:
				SetState(PlaybackState.Playing);
				break;
			case PlaybackState.Stopped:
			default:
				break;
		}
	}

	private void SetState(PlaybackState state)
	{
		State = state;
		EmitSignal(SignalName.PlaybackStateChanged, Variant.From<PlaybackState>(state));
	}

	private void UpdateTick(double delta)
	{
		if (State != PlaybackState.Playing)
			return;

		Seconds += delta * TimeScale;
		Tick = TimeToTick(Seconds);
	}

	private uint TimeToTick(double time)
	{
		return (uint)Mathf.Max(0, Mathf.RoundToInt(time * TickRate));
	}

	private void _on_demo_handler_demo_loaded(string demoName)
	{
		SetState(PlaybackState.Paused);
	}
}
