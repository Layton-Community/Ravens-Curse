namespace Com.LaytonCommunity.RavensCurse;

public static class ExtentionTexture2D
{
	public static string ResourceFileName(this Texture2D texture)
	{
		return texture.ResourcePath.GetFile().GetBaseName();
	}
}

