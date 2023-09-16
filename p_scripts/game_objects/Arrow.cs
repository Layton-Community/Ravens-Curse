using System.ComponentModel.DataAnnotations;
using Com.LaytonCommunity.RavensCurse.Components;

namespace Com.LaytonCommunity.RavensCurse.Game;

[Tool]
[GlobalClass]
public partial class Arrow : TextureButton
{
	// Constants
	public const string GROUP = "arrow";
	
	private const string ANIM_IDLE = "anim_arrow/idle";
	private const string ANIM_PRESSED = "anim_arrow/pressed";

	// Enums
	public enum Direction { Up, Down, Left, Right, Back, BackLeft, BackRight, Hand }
	
	// Signal
	
	// Export variables
	[Export(PropertyHint.File, "*.tscn")] private string pathArrowScene;
	[Export] private Direction ArrowDirection
	{
		get { return arrowDirection; }
		set { SetDirection(value); }
	}

	[ExportGroup("Imports")]
	[Export] private ArrowDictionary arrowDictionary;
	[Export] private AnimationPlayer animation;
	[Export] private Logger logger;
	
	// Member variables
	private Direction arrowDirection;
	private Location location;
	
	
	
	private void SetDirection(Direction newDirection)
	{
		arrowDirection = newDirection;
		
		// If the script is not in the engine, return
		if (!Engine.IsEditorHint() || arrowDictionary == null) return;
		
		if (TextureNormal != null)
		{
			string dirSnakeCase = newDirection.ToString().ToSnakeCase();
			string textureName = TextureNormal.ResourceFileName();
			
			// If the direction enum is the same as the texture, return
			if (textureName.Find(dirSnakeCase) != -1) return;
		}
		
		TextureNormal = arrowDictionary.GetValue(arrowDirection).Item1;
	}
	
	
	
	public void OnLocation_ShowArrows(Location instance)
	{
		if (!animation.IsPlaying())
		{
			Pressed += OnThis_Pressed;
			
			location = instance;
			
			Show();
			animation.Play(ANIM_IDLE);
		}
	}
	
	private void OnThis_Pressed()
	{
		Pressed -= OnThis_Pressed;
		animation.AnimationFinished += OnAnimationPressed_Finished;
		
		animation.Play(ANIM_PRESSED);
	}
	
	private void OnAnimationPressed_SetSprite(int id)
	{
		var value = arrowDictionary.GetValue(arrowDirection);
		
		TextureNormal = id == 0 ? value.Item1 : value.Item2;
	}

	private void OnAnimationPressed_Finished(StringName _)
	{
		animation.AnimationFinished -= OnAnimationPressed_Finished;
		
		if (string.IsNullOrEmpty(pathArrowScene))
		{
			logger.Error("The path to the scene is empty!");
			return;
		}
		
		location.OnArrow_Pressed(pathArrowScene);
	}
}

/*
	private void OnAnimationPressed_Finished(StringName _)
	{
		if (string.IsNullOrEmpty(pathArrowScene))
		{
			logger.Error("The path to the scene is empty!");
			return;
		}
		
		GetTree().ChangeSceneToFile(pathArrowScene);
	}
*/
