using System.Numerics;
using Raylib_cs;

namespace GameTool;

public static class Graphics
{
    private static readonly Dictionary<string, Color> ColorMap = new()
    {
        { "Black", Color.Black },
        { "White", Color.White },
        { "Red", Color.Red },
        { "Green", Color.Green },
        { "Blue", Color.Blue },
        { "LightGray", Color.LightGray },
        { "DarkGray", Color.DarkGray },
        { "Gray", Color.Gray },
        { "Brown", Color.Brown },
        { "Pink", Color.Pink },
    };

    public static Color GetColorFromString(string color)
    {
        if (!ColorMap.TryGetValue(color, out Color raylibColor))
            throw new ArgumentException($"Color {color} not found. Must be given in Pascal Case");
        return raylibColor;
    }
    public static void DrawRectangle(int posX, int posY, int width, int height, string color)
    {
        if (width <= 0 || height <= 0)
            throw new ArgumentOutOfRangeException($"Width and height should be positive. '{nameof(width)}' = {width} | '{nameof(height)}' = {height}");

        Color raylibColor = GetColorFromString(color);
        Raylib.DrawRectangle(posX, posY, width, height, raylibColor);
    }
    public static void DrawSquare(int posX, int posY, int sideLength, string color)
    {
        DrawRectangle(posX, posY, sideLength, sideLength, color);
    }

    public static void DrawImage(int posX, int posY, string color, Texture2D image, float angle)
    {
        Color raylibColor = GetColorFromString(color);

        Rectangle source = new Rectangle(0, 0, image.Width, image.Height);
        Rectangle dest = new Rectangle(
            posX + image.Width / 2f,
            posY + image.Height / 2f,
            image.Width,
            image.Height
        );
        Vector2 origin = new Vector2(image.Width / 2f, image.Height / 2f);

        Raylib.DrawTexturePro(image, source, dest, origin, angle, raylibColor);

        // Raylib.UnloadTexture(image);
    }

    public static void DrawText(string text, int posX, int posY, int fontSize, string color)
    {
        Color raylibColor = GetColorFromString(color);

        Raylib.DrawText(text, posX, posY, fontSize, raylibColor);
    }

    public static int GetTextWidth(string text, int fontSize)
    {
        return Raylib.MeasureText(text, fontSize);
    }
}
