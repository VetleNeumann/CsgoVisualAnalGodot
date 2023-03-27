using Godot;
using System;

public partial class Camera : Camera3D
{
	private DemoHandler _demoHander;
	private PlayerModel _playerModel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_playerModel != null)
		{
			if (Time.GetTicksMsec() % 50 == 0)
			{
				_playerModel = GetNode("../PlayerVisualizer/PlayerModel") as PlayerModel;
				this.Position = _playerModel.Position;
				this.Rotation = _playerModel.Rotation;
			}
			return;
		}
	}

	public Vector3 FindFirstPlayerPosition()
	{
		for (int tick = 0; tick < _demoHander.FinalTick; tick++)
		{

		}
	}
}
