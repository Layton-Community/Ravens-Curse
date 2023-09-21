namespace Com.LaytonCommunity.RavensCurse.Components;

[Tool]
[GlobalClass]
[Icon("res://a_sprites/icons/node_custom.svg")]
public partial class Logger : NodeCS
{
	// Constants
	
	// Enums
	
	// Signal
	
	// Export variables
	[Export] public bool enabled = false;
	
	// Member variables
	private string parentScriptName;

	protected override void ReadyComponent()
	{
		parentScriptName = parent.GetType().Name;
	}
	
	public void Info(string what)
	{
		if (enabled)
		{
			Print.Info(parentScriptName, what);
		}
	}
	
	public void Warn(string what)
	{
		Print.Warn(parentScriptName, what);
	}
	
	public void Error(string what)
	{
		Print.Error(parentScriptName, what);
	}
}
