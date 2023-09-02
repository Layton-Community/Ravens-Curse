namespace Com.LaytonCommunity.RavensCurse;

public static class ExtentionGodotArray
{
	public static List<T> ToList<T>(this Godot.Collections.Array<Node> array)
	where
		T : Node
	{
		return array.Cast<T>().ToList();
	}
	
	public static List<T> ToList<T>(this Node[] array)
	where
		T : Node
	{
		return array.Cast<T>().ToList();
	}
}

