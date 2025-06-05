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
        { "Brown", Color.Brown },
        { "Pink", Color.Pink },
    };
    public static void DrawRectangle(int posX, int posY, int width, int height, string color)
    {
        if (!ColorMap.TryGetValue(color, out Color raylibColor))
            throw new ArgumentException($"Color {color} not found. Must be given in Pascal Case");
        if (width <= 0 || height <= 0)
            throw new ArgumentOutOfRangeException($"Width and height should be positive. '{nameof(width)}' = {width} | '{nameof(height)}' = {height}");
        Raylib.DrawRectangle(posX, posY, width, height, raylibColor);
    }
    public static void DrawSquare(int posX, int posY, int sideLength, string color)
    {
        DrawRectangle(posX, posY, sideLength, sideLength, color);
    }

    public static void DrawImage(int posX, int posY, string color, Texture2D image, float angle)
    {
        if (!ColorMap.TryGetValue(color, out Color raylibColor))
            throw new ArgumentException($"Color {color} not found. Must be given in Pascal Case");

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
}
