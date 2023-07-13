namespace Com.LaytonCommunity.RavensCurse;

public static class ExtentionAnimationPlayer
{
	public static void PlayAndAdvance(this AnimationPlayer animation, StringName name, double advanceDelta, double customBlend = -1, float customSpeed = 1, bool fromEnd = false)
	{
		animation.Play(name, customBlend, customSpeed, fromEnd);
		animation.Advance(advanceDelta);
	}
}

