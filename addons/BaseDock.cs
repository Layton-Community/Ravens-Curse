#if TOOLS
namespace Com.LaytonCommunity.RavensCurse.Addons;

[Tool]
public partial class BaseDock : Control
{
	protected void Print(string what)
	{
		var fullName = GetType().Name;
		var name = fullName.Remove(fullName.IndexOf("Dock"));
		
		GD.PrintRich($"[color=8b8b8b][{name}] {what}[/color]");
	}
}
#endif
