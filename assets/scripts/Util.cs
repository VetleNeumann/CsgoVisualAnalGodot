namespace CsgoVisualAnalGodot.assets.scripts
{
	internal class Util
	{
		public static Godot.Vector3 ToGodotVector3(System.Numerics.Vector3 vector)
		{
			return new Godot.Vector3(vector.X, vector.Y, vector.Z);
		}

		public static Godot.Vector2 ToGodotVector2(System.Numerics.Vector2 vector)
		{
			return new Godot.Vector2(vector.X, vector.Y);
		}
	}
}
