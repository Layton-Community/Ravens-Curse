namespace Com.LaytonCommunity.RavensCurse;

using static Com.LaytonCommunity.RavensCurse.Game.Arrow;
using DictionaryArrowSprite = Dictionary<Game.Arrow.Direction, (Texture2D, Texture2D)>;

[Tool]
[GlobalClass]
public partial class ArrowDictionary : Resource
{
	// Constants
	
	// Enums
	
	// Signal
	
	// Export variables
	[Export] private ArrowSprite[] values;
	
	// Member variables
	private DictionaryArrowSprite dictionary;
	
	public ArrowDictionary() : this(null) {}

	public ArrowDictionary(ArrowSprite[] values)
	{
		this.values = values;
		
		InitDictionary();
	}

	public (Texture2D, Texture2D) GetValue(Direction key)
	{
		InitDictionary();
		
		dictionary.TryGetValue(key, out (Texture2D, Texture2D) maybeValue);
		
		return maybeValue;
	}

	private void InitDictionary()
	{
		if (values != null && dictionary == null)
		{
			dictionary = new();
			
			foreach (var item in values)
			{
				dictionary.Add(item.key, (item.value0, item.value1));
			}
		}
	}
	
	public void PrintDbg()
	{
		foreach (var fieldInfo in typeof(ArrowDictionary).GetFields())
		{
			Print.Info(nameof(ArrowDictionary), string.Format("{0,-20}{1}", $"{fieldInfo.Name}:", fieldInfo.GetValue(this)));
		}
		
		GD.Print();
	}
}
