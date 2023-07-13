#if TOOLS
namespace Com.LaytonCommunity.RavensCurse.Addons;

[Tool]
public partial class DebugTools : BaseAddons
{
	public override void _EnterTree()
	{
		scenePath = "addons/debug_tools/dock.tscn";
		
		base._EnterTree();
	}
}
#endif
