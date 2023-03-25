using Godot;

public partial class LoadingScreen : Label
{
	int _dots = 0;

	private void UpdateText()
	{
		Text = "Loading" + new string('.', _dots);
	}

	private void _on_timer_timeout()
	{
		_dots++;
		if (_dots > 3)
		{
			_dots = 0;
		}
		UpdateText();
	}
}
