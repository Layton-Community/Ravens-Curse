namespace Com.LaytonCommunity.RavensCurse.Ui;

public partial class LineEdit : Godot.LineEdit
{
	// Constants
	
	// Enums
	
	// Signal
	
	// Export variables
	[ExportGroup("Imports")]
	[Export] private Caret caret;
	[Export] private AudioStreamPlayer sound;

	// Member variables

	public override void _Ready() => TextChanged += UpdateCaret;

	private void UpdateCaret(string newText)
	{		
		if (newText.Length == 10)
		{
			caret.HideCaret();
		}
		else
		{
			Vector2 newPos = new(newText.Length * (45 + 3) + caret.Size.X / 3 - 2, caret.Position.Y);
			caret.Position = newPos;
			
			caret.ShowCaret();
		}
		
		CaretColumn = Text.Length;
		
		sound.Play();
	}
}

