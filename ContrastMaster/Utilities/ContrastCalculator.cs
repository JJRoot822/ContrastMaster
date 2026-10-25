using ContrastMaster.Models;

namespace ContrastMaster.Utilities;

public static class ContrastCalculator
{
    public static double CalculateContrastRatio(RGBColor foreground, RGBColor background)
    {
        double foregroundLuminance = CalculateRelativeLuminance(foreground);
        double backgroundLuminance = CalculateRelativeLuminance(background);

        double lighter = Math.Max(foregroundLuminance, backgroundLuminance);
        double darker = Math.Min(foregroundLuminance, backgroundLuminance);

        return (lighter + 0.05) / (darker + 0.05);
    }

    public static bool PassesAAText(double contrastRatio, bool isBoldText, double fontSize)
    {
        bool isLargeText = IsLargeText(isBoldText, fontSize);

        return contrastRatio >= (isLargeText ? 3.0 : 4.5);
    }

    public static bool PassesAANonText(double contrastRatio)
    {
        return contrastRatio >= 3.0;
    }

    public static bool PassesAAAText(double contrastRatio, bool isBoldText, double fontSize)
    {
        bool isLargeText = IsLargeText(isBoldText, fontSize);

        return contrastRatio >= (isLargeText ? 4.5 : 7.0);
    }

    private static bool IsLargeText(bool isBoldText, double fontSize)
    {
        return isBoldText ? fontSize >= 14.0 : fontSize >= 18.0;
    }

    private static double CalculateRelativeLuminance(RGBColor color)
    {
        double red = LinearizeChannel(color.Red);
        double green = LinearizeChannel(color.Green);
        double blue = LinearizeChannel(color.Blue);

        return (0.2126 * red) + (0.7152 * green) + (0.0722 * blue);
    }

    private static double LinearizeChannel(int channel)
    {
        double normalized = channel / 255.0;

        return normalized <= 0.04045 ? normalized / 12.92 : Math.Pow((normalized + 0.055) / 1.055, 2.4);
    }
}
