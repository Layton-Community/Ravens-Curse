namespace Com.LaytonCommunity.RavensCurse;

public static class Assert
{
	public static void _(bool value)
	{
		if (!value)
		{
			throw new Exception("Assert false");
		}
	}
}
