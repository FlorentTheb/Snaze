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
        { "DarkBlue", Color.DarkBlue },
        { "Blue", Color.Blue },
        { "SkyBlue", Color.SkyBlue },
        { "DarkPurple", Color.DarkPurple },
        { "Violet", Color.Violet },
        { "Purple", Color.Purple },
        { "LightGray", Color.LightGray },
        { "DarkGray", Color.DarkGray },
        { "Gray", Color.Gray },
        { "Brown", Color.Brown },
        { "Pink", Color.Pink },
        { "Yellow", Color.Yellow },
        { "Beige", Color.Beige },
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

    public static void DrawRectangle(Rectangle rec, string color)
    {
        if (rec.Width <= 0 || rec.Height <= 0)
            throw new ArgumentOutOfRangeException($"Width and height should be positive");

        Color raylibColor = GetColorFromString(color);
        Raylib.DrawRectangleRec(rec, raylibColor);
    }
    public static void DrawSquare(int posX, int posY, int sideLength, string color)
    {
        DrawRectangle(posX, posY, sideLength, sideLength, color);
    }

    public static void DrawImage(int posX, int posY, string color, Texture2D image, float angle, int alpha)
    {
        Color raylibColor = GetColorFromString(color);

        Rectangle source = new Rectangle(0, 0, image.Width, image.Height);
        float originX = image.Width / 2f;
        float originY = image.Height / 2f;
        Rectangle dest = new Rectangle(
            posX + originX - 1,
            posY + originY - 1,
            image.Width,
            image.Height
        );
        Vector2 origin = new Vector2(originX, originY);

        raylibColor.A = (byte)alpha;
        Raylib.DrawTexturePro(image, source, dest, origin, angle, raylibColor);
    }

    public static void DrawText(string text, Rectangle box, string color)
    {
        Color raylibColor = GetColorFromString(color);
        int centerX = (int)(box.X + box.Width / 2);
        int centerY = (int)(box.Y + box.Height / 2);
        int textHeight = (int)box.Height / 2;
        int textWidth = GetTextWidth(text, textHeight);
        Raylib.DrawText(text, centerX - textWidth / 2, centerY - textHeight / 2, textHeight, raylibColor);
    }

    public static int GetTextWidth(string text, int fontSize)
    {
        return Raylib.MeasureText(text, fontSize);
    }
}
