namespace Com.LaytonCommunity.RavensCurse;

/// <summary> Use this class only for non-instanciable objects, for others use the Logger component! </summary>
[Tool]
public static class Print
{
	public static void Info(string scriptName, string what)
	{
		GD.PrintRich($"[color=8b8b8b][{scriptName}] {what}[/color]");
	}
	
	public static void Warn(string scriptName, string what)
	{
		GD.PrintRich($"[color=FF8C00][{scriptName}] {what}[/color]");
	}
	
	public static void Error(string scriptName, string what)
	{
		GD.PrintErr($"[{scriptName}] {what}");
	}
}
