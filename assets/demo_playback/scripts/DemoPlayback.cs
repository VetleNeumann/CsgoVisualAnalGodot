using Godot;

public partial class DemoPlayback : PanelContainer
{
	private Vector2? _dragPosition = null;

	private Playback _playback;
	private Label _timeCounter;
	private Label _tickCounter;

	public override void _Ready()
	{
		_playback = GetNode<Playback>("/root/Playback");
		_timeCounter = GetNode("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/TimeCounter") as Label;
		_tickCounter = GetNode("MarginContainer/VBoxContainer/HBoxContainer2/TickCounter") as Label;
	}

	public override void _Process(double delta)
	{
		UpdateCounters();
	}

	public override void _GuiInput(InputEvent @event)
	{
		// Handle dragging the window
		if (@event is InputEventMouseButton eventMouseButton)
		{
			if (eventMouseButton.Pressed)
			{
				_dragPosition = GetGlobalMousePosition() - GlobalPosition;
				if (_dragPosition.Value.Y > 25)
				{
					_dragPosition = null;
				}
			}
			else
			{
				_dragPosition = null;
			}
		}
		if (@event is InputEventMouseMotion eventMouseMotion && _dragPosition is not null)
		{
			Vector2 newPosition = GetGlobalMousePosition() - _dragPosition.Value;
			Vector2 max = GetViewportRect().End - Size;
			newPosition = newPosition.Clamp(Vector2.Zero, max);
			SetGlobalPosition(newPosition);
		}
	}

	private void UpdateCounters()
	{
		int minutes = Mathf.FloorToInt(_playback.Seconds / 60f);
		int seconds = Mathf.FloorToInt(_playback.Seconds) % 60;
		_timeCounter.Text = string.Format("Time: {0:d2}:{1:d2} / 00:00", minutes, seconds);
		_tickCounter.Text = string.Format("Tick: {0:d} / 0", _playback.Tick);
	}

	private void _on_load_button_button_down()
	{
		var demoBrowser = GetNode("../DemoBrowser") as FileDialog;
		demoBrowser.PopupCentered();
	}
}


