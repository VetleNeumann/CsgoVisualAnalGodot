using Godot;

public partial class TimeScale : Node
{
	private Playback _playback;
	private HSlider _slider;
	private Label _label;
	private bool ignoreNextSliderValue = false;

	public override void _Ready()
	{
		_playback = GetNode<Playback>("/root/Playback");
		_slider = GetNode("HBoxContainer/Slider") as HSlider;
		_label = GetNode("HBoxContainer/Label") as Label;
	}

	private void SetTimeScale(float timeScale)
	{
		timeScale = Mathf.Clamp(timeScale, 0f, 4f);
		if (_playback.TimeScale == timeScale) return;

		_playback.TimeScale = timeScale;
		UpdateCounters(timeScale);
	}

	private void UpdateCounters(float timeScale)
	{
		float percentage = timeScale * 100;
		ignoreNextSliderValue = true;
		_slider.Value = percentage;
		_label.Text = string.Format("{0:d}%", Mathf.RoundToInt(percentage));
	}

	private void _on_quarter_button_down()
	{
		SetTimeScale(0.25f);
	}

	private void _on_half_button_down()
	{
		SetTimeScale(0.5f);
	}

	private void _on_normal_button_down()
	{
		SetTimeScale(1f);
	}

	private void _on_twice_button_down()
	{
		SetTimeScale(2f);
	}

	private void _on_quadruple_button_down()
	{
		SetTimeScale(4f);
	}

	private void _on_slider_value_changed(double value)
	{
		if (ignoreNextSliderValue)
		{
			ignoreNextSliderValue = false;
			return;
		}
		SetTimeScale((float)value / 100f);
	}
}


