namespace Com.LaytonCommunity.RavensCurse;

public static class ExtentionString
{
	public static T InstantiateFromPath<T>(this string str) where T : class
	{
		if (string.IsNullOrEmpty(str))
		{
			throw new Exception($"The file is not specified! (string is null or empty)");
		}
		
		if (str.GetExtension() != "tscn")
		{
			throw new Exception($"The file at path \"{str}\" is not a scene!");
		}
		
		if (!ResourceLoader.Exists(str))
		{
			throw new Exception($"The file at path \"{str}\" does not exist!");
		}
		
		return ResourceLoader.Load<PackedScene>(str).Instantiate<T>();
	}
	
	public static Node InstantiateFromPath(this string str)
	{
		return InstantiateFromPath<Node>(str);
	}
}

