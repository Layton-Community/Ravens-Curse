#if TOOLS
namespace Com.LaytonCommunity.RavensCurse.Addons;

[Tool]
public partial class BaseAddons : EditorPlugin
{
	protected string scenePath;
	private Control dock;

	public override void _EnterTree()
	{
		Print("Enabled!");
		
		dock = (Control)GD.Load<PackedScene>("addons/debug_tools/dock.tscn").Instantiate();
		AddControlToDock(DockSlot.LeftBr, dock);
	}

	public override void _ExitTree()
	{
		Print("Disabled!");
		
		RemoveControlFromDocks(dock);
		dock.Free();
	}
	
	protected void Print(string what)
	{
		GD.PrintRich($"[color=8b8b8b][{GetType().Name}] {what}[/color]");
	}
}
#endif
