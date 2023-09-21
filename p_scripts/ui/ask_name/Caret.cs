namespace Com.LaytonCommunity.RavensCurse.Ui;

public partial class Caret : ColorRect
{
	// Constants
	
	// Enums
	
	// Signal
	
	// Export variables
	
	// Member variables
	private double count = 0;

	public override void _Process(double delta)
	{
		count += delta;
		Visible = Mathf.Sin(Mathf.Pi * count) > 0;
	}
	
	public void HideCaret()
	{
		SetProcess(false);
		Hide();
	}
	
	public void ShowCaret()
	{
		count = 0;
		
		SetProcess(true);
		Show();
	}
}

