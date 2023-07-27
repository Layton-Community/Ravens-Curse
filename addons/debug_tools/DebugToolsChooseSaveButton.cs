#if TOOLS
namespace Com.LaytonCommunity.RavensCurse.Addons;

[Tool]
public partial class DebugToolsChooseSaveButton : Button
{
	// Constants

	// Enums

	// Signal
	[Signal] public delegate void PressedIdEventHandler(uint id);
	
	// Properties variables
	public uint Id { get; private set; }
	
	// Export variables
	
	// Member variables
	
	public override void _Ready()
	{
		Pressed += On_Pressed;
		
		Id = uint.Parse(Name.ToString().Split('_')[^1]);
	}

	private void On_Pressed()
	{
		EmitSignal(SignalName.PressedId, Id);
	}
}
#endif
