using CsgoVisualAnalGodot.assets.demo_playback.scripts;
using DemoTracker.Structs;
using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class PlayerVisualizer : Node
{
	private DemoHandler _demo;
	private Playback _playback;

	private PackedScene _playerModel;
	private System.Collections.Generic.Dictionary<long, PlayerModel> _playerModelsBySteamID;

	private uint lastVisualizedTick = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_demo = GetNode<DemoHandler>("/root/DemoHandler");
		_playback = GetNode<Playback>("/root/Playback");
		_playback.PlaybackStateChanged += _on_playback_state_changed;

		_playerModel = GD.Load<PackedScene>("res://assets/player_model/player_model.tscn");
		_playerModelsBySteamID = new System.Collections.Generic.Dictionary<long, PlayerModel>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_playback.State == PlaybackState.Stopped)
			return;

		if (_playback.Tick != lastVisualizedTick)
		{
			VisualizeTick(_demo.GetTick(_playback.Tick));
			lastVisualizedTick = _playback.Tick;
		}
	}

	private void VisualizeTick(TickSummary tick)
	{
		long[] playersInTick = tick.Players.Select(x => x.SteamID).ToArray();
		UpdateDictionary(playersInTick);

		foreach (var playerTick in tick.Players)
		{
			var playerModel = _playerModelsBySteamID[playerTick.SteamID];
			playerModel.VisualizeTick(playerTick);
		}
	}

	private void UpdateDictionary(long[] playersInTick)
	{
		var playersMissingModel = playersInTick.Except(_playerModelsBySteamID.Keys);
		foreach (var steamId in playersMissingModel)
		{
			// Instantiate playerModel
			var instance = _playerModel.Instantiate<PlayerModel>();
			this.AddChild(instance);
			_playerModelsBySteamID[steamId] = instance;
		}

		var playersNotInTick = _playerModelsBySteamID.Keys.Except(playersInTick);
		foreach (var steamId in playersNotInTick)
		{
			// Remove playerModel
			_playerModelsBySteamID[steamId].QueueFree();
			_playerModelsBySteamID.Remove(steamId);
		}
	}

	private void _on_playback_state_changed(PlaybackState state)
	{
		if (state == PlaybackState.Stopped)
		{

		}
	}
}
