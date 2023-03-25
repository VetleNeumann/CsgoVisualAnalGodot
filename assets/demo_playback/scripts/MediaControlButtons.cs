using CsgoVisualAnalGodot.assets.demo_playback.scripts;
using Godot;
using System;

public partial class MediaControlButtons : HBoxContainer
{
	private Playback _playback;
	private Button _skipBackward;
	private Button _back;
	private Button _playPauseToggle;
	private Button _forward;
	private Button _skipForward;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_skipBackward = GetNode("SkipBackward") as Button;
		_back = GetNode("Back") as Button;
		_playPauseToggle = GetNode("PlayPauseToggle") as Button;
		_forward = GetNode("Forward") as Button;
		_skipForward = GetNode("SkipForward") as Button;

		_playback = GetNode<Playback>("/root/Playback");
		UdatePlayPauseToggleButton(_playback.State);
		_playback.PlaybackStateChanged += _on_playback_state_changed;
	}

	private void UdatePlayPauseToggleButton(PlaybackState state)
	{
		switch (state)
		{
			case PlaybackState.Playing:
				_playPauseToggle.Disabled = false;
				_playPauseToggle.Text = "Pause";
				break;
			case PlaybackState.Paused:
				_playPauseToggle.Disabled = false;
				_playPauseToggle.Text = "Play";
				break;
			case PlaybackState.Stopped:
			default:
				_playPauseToggle.Disabled = true;
				_playPauseToggle.Text = "Play";
				break;
		}
	}

	private void _on_playback_state_changed(PlaybackState state)
	{
		UdatePlayPauseToggleButton(state);
	}

	private void _on_play_pause_toggle_button_down()
	{
		_playback.PlayPauseToggle();
	}
}
