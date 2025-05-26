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
    };
    public static void DrawRectangle(int posX, int posY, int width, int height, string color)
    {
        if (!ColorMap.TryGetValue(color, out Color RaylibColor))
            throw new ArgumentException($"Color {color} not found. Must be given in Pascal Case");
        if (width <= 0 || height <= 0)
            throw new ArgumentOutOfRangeException($"Width and height should be positive. '{nameof(width)}' = {width} | '{nameof(height)}' = {height}");
        Raylib.DrawRectangle(posX, posY, width, height, RaylibColor);
    }
    public static void DrawSquare(int posX, int posY, int sideLength, string color)
    {
        DrawRectangle(posX, posY, sideLength, sideLength, color);
    }
}
