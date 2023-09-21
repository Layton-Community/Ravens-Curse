namespace Com.LaytonCommunity.RavensCurse;

using static Com.LaytonCommunity.RavensCurse.Game.Arrow;

[Tool]
[GlobalClass]
public partial class ArrowSprite : Resource
{
	// Constants
	
	// Enums
	
	// Signal
	
	// Export variables
	[Export] public Direction key;
	[Export] public Texture2D value0;
	[Export] public Texture2D value1;
	
	// Member variables
	
	public ArrowSprite() : this(Direction.Up, null, null) {}

	public ArrowSprite(Direction key, Texture2D value0, Texture2D value1)
	{
		this.key = key;
		this.value0 = value0;
		this.value1 = value1;
	}
}
