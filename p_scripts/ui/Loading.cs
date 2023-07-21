global using Godot;
global using System;
global using System.Linq;
global using System.Collections.Generic;

namespace Com.LaytonCommunity.RavensCurse.Ui;

public partial class Loading : UiBase
{
	// Constants
	public const double FIRST_LOAD_WAIT = 0.5;
	private const string BG_LAYTON = "BgLayton";

	// Enums

	// Signal

	// Export variables
	[Export(PropertyHint.Range, "0,10,0.1")] private double logoShowTime = 1.5;
	[ExportGroup("Imports")]
	[Export(PropertyHint.File,"*.tscn")] private string sceneTitle;
	[Export] private AudioStreamPlayer sfxBgLayton;
	[Export] private Node logosContainer;
	
	// Member variables
	private int index = 0;
	private List<CanvasItem> logos;
	
	public override async void _Ready()
	{
		base._Ready();
		animations.Stop();
		
		await ToSignal(GetTree().CreateTimer(FIRST_LOAD_WAIT), Timer.SignalName.Timeout);
		
		logos = logosContainer.GetChildren().Cast<CanvasItem>().ToList();
 		logos.ForEach((logo) => logo.Hide());
		logos.First().Show();
		
		animations.AnimationFinished += Wait;
		animations.AnimationFinished += NextLogo;
		animations.Play();
	}
	
	private void Wait(StringName name)
	{
		if (name == ANIM_FADE_IN)
		{
			GetTree().CreateTimer(logoShowTime).Timeout += () => animations.Play(ANIM_FADE_OUT);
		}
	}
	
	private void NextLogo(StringName name)
	{
		if (name == ANIM_FADE_OUT)
		{
			logos[index].Hide();
			
			index += 1;
			
			if (index < logos.Count)
			{
				CanvasItem logo = logos[index];
				
				logo.Show();
				animations.Play(ANIM_FADE_IN);
				
				if (logo.Name == BG_LAYTON)
				{
					GetTree().CreateTimer(logoShowTime * .75).Timeout += () => sfxBgLayton.Play();
				}
			}
			else
			{
				AddSibling(sceneTitle.InstantiateFromPath(), true);
				QueueFree();
			}
		}
	}
}
