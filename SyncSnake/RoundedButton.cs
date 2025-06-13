using System;
using GameTool;
using Raylib_cs;

public class RoundedButton : SimpleButton
{
    private readonly float Roundness = 1.0f;
    private readonly float Thickness = 8.0f;
    private readonly int Segments = 60;
    private Color EdgeColor1;
    private Color EdgeColor2;
    public RoundedButton(string text, Rectangle bounds, Action action) : base(text, bounds, action)
    {
        DefaultColor = new Color
        {
            R = 0,
            G = 0,
            B = 0,
            A = 255
        };
        HoverColor = new Color
        {
            R = 255,
            G = 255,
            B = 255,
            A = 255
        };
        PressedColor = new Color
        {
            R = 153,
            G = 0,
            B = 153,
            A = 255
        };
        EdgeColor1 = new Color
        {
            R = 255,
            G = 255,
            B = 255,
            A = 150
        };
        EdgeColor2 = new Color
        {
            R = 255,
            G = 102,
            B = 255,
            A = 255
        };
    }

    public override void Draw()
    {
        Color currentColor = IsPressed ? PressedColor : IsHovered ? HoverColor : DefaultColor;
        Color textColor = IsHovered ? new Color(255, 102, 255, 255) : Color.White;
        Color neonOutsideColor = new Color(255, 102, 255, 150);
        Raylib.DrawRectangleRoundedLinesEx(Bounds, Roundness, Segments, Thickness * 3 / 2, neonOutsideColor);
        Raylib.DrawRectangleRounded(Bounds, Roundness, Segments, currentColor);
        Graphics.DrawText(Text, Bounds, textColor);
        Raylib.DrawRectangleRoundedLinesEx(Bounds, Roundness, Segments, Thickness, EdgeColor2);
        Rectangle neonInside = new Rectangle(Bounds.X - Thickness / 4, Bounds.Y - Thickness / 4, Bounds.Width + Thickness / 2, Bounds.Height + Thickness / 2);
        Raylib.DrawRectangleRoundedLinesEx(neonInside, Roundness, Segments, Thickness / 2, EdgeColor1);
    }
}
