namespace Com.LaytonCommunity.RavensCurse;

public static class Print
{
	public static void Error(string what)
	{
		GD.PrintErr(what);
	}
	
	public static void Warn(string what)
	{
		GD.PrintRich("[color=ffdd65][b]•[/b] " + what + "[/color]");
	}
	
	public static void Info(string what)
	{
		GD.Print(what);
	}
}
