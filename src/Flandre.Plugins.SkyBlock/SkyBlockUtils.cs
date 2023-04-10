namespace Flandre.Plugins.SkyBlock;

public static class SkyBlockUtils
{
    internal static string ConvertItemId(string input)
    {
        return input.ToLower().Replace("_", "");
    }
}