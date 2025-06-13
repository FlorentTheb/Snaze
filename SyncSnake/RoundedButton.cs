using System;
using GameTool;
using Raylib_cs;

public class RoundedButton : SimpleButton
{
    private readonly float Roundness = 1.0f;
    private readonly float Thickness = 8.0f;
    private readonly int Segments = 60;
    private Color EdgeColor;
    public RoundedButton(string text, Rectangle bounds, Action action) : base(text, bounds, action)
    {
        DefaultColor = new Color
        {
            R = 51,
            G = 204,
            B = 51,
            A = 255
        };
        HoverColor = new Color
        {
            R = 112,
            G = 219,
            B = 112,
            A = 255
        };
        PressedColor = new Color
        {
            R = 41,
            G = 163,
            B = 41,
            A = 255
        };
        EdgeColor = new Color
        {
            R = 102,
            G = 255,
            B = 255,
            A = 255
        };
    }

    public override void Draw()
    {
        Color currentColor = IsPressed ? PressedColor : IsHovered ? HoverColor : DefaultColor;
        Raylib.DrawRectangleRounded(Bounds, Roundness, Segments, currentColor);
        Graphics.DrawText(Text, Bounds, "Black");
        Raylib.DrawRectangleRoundedLinesEx(Bounds, Roundness, Segments, Thickness, EdgeColor);
    }
}
