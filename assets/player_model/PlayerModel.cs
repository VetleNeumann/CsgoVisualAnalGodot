using CsgoVisualAnalGodot.assets.scripts;
using DemoInfo;
using DemoTracker.Structs;
using Godot;
using System;

public partial class PlayerModel : Node3D
{
	private Team _team = Team.Terrorist;
	private Material _materialTerrorist;
	private Material _materialCounterTerrorist;

	private CsgBox3D _agent;
	private Node3D _weapon;

	public override void _Ready()
	{
		_materialTerrorist = GD.Load<Material>("res://assets/player_model/materials/terrorist.tres");
		_materialCounterTerrorist = GD.Load<Material>("res://assets/player_model/materials/counter_terrorist.tres");

		_agent = GetNode<CsgBox3D>("Agent");
		_weapon = GetNode<Node3D>("Weapon");
	}

	public void VisualizeTick(PlayerTickSummary tick)
	{
		if (tick.Position is null || tick.ViewDirection is null)
		{
			if (this.Visible)
				this.Hide();
			return;
		}
		else if (!this.Visible)
		{
			this.Show();
		}

		Vector3 position = Util.ToGodotVector3(tick.Position.Value);
		Vector2 viewDirection = Util.ToGodotVector2(tick.ViewDirection.Value);
		SetTeam(tick.Team.Value);

		this.Position = position;
		this.Basis = new Basis(Quaternion.FromEuler(new Vector3(0, -viewDirection.X, 0)));
	}

	private void SetTeam(Team team)
	{
		if (team == _team)
			return;

		_team = team;
		switch (_team)
		{
			case Team.Terrorist:
				this.Show();
				_agent.Material = _materialTerrorist;
				break;
			case Team.CounterTerrorist:
				this.Show();
				_agent.Material = _materialCounterTerrorist;
				break;
			case Team.Spectate:
			default:
				_agent.Material = null;
				break;
		}
	}
}
