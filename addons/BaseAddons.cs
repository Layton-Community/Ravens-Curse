#if TOOLS
namespace Com.LaytonCommunity.RavensCurse.Addons;

[Tool]
public partial class BaseAddons : EditorPlugin
{
	protected string scenePath;
	private Control dock;

	public override void _EnterTree()
	{
		PrintInfo("Enabled!");
		
		dock = (Control)GD.Load<PackedScene>(scenePath).Instantiate();
		AddControlToDock(DockSlot.LeftUr, dock);
	}

	public override void _ExitTree()
	{
		PrintInfo("Disabled!");
		
		RemoveControlFromDocks(dock);
		dock.Free();
	}
	
	protected void PrintInfo(string what)
	{
		Print.Info(GetType().Name, what);
	}
}
#endif
