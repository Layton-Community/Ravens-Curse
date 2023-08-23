using Com.LaytonCommunity.RavensCurse.Components;

namespace Com.LaytonCommunity.RavensCurse;

public partial class Singleton : Node
{
	// Enums
	
	// Signal
	
	// Export variables
	
	// Member variables
	protected Logger logger;
	
	public override void _Ready()
	{
		logger = new Logger();
		
		AddChild(logger, true);
	}
}
