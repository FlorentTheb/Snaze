using System;
using System.Collections;
using GameTool;
using Raylib_cs;

public class NeonButton : SimpleButton
{
    public string Text = "";
    private readonly float Roundness = 1.0f;
    private readonly float Thickness = 8.0f;
    private readonly int Segments = 60;
    private Color InnerBorderColor;
    private Color OuterBorderColor;
    public NeonButton(string text, Rectangle box, string neonTheme, Action action) : base(box, action)
    {
        Text = text;
        SetColorsTheme(neonTheme);
    }

    private void SetColorsTheme(string theme)
    {
        switch (theme)
        {
            case "Blue":
                SetBlueTheme();
                break;
            default:
                SetPinkTheme();
                break;
        }
    }

    private void SetPinkTheme()
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
        InnerBorderColor = new Color
        {
            R = 255,
            G = 255,
            B = 255,
            A = 180
        };
        OuterBorderColor = new Color
        {
            R = 255,
            G = 102,
            B = 255,
            A = 255
        };
    }
    private void SetBlueTheme()
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
            R = 0,
            G = 77,
            B = 128,
            A = 255
        };
        InnerBorderColor = new Color
        {
            R = 255,
            G = 255,
            B = 255,
            A = 180
        };
        OuterBorderColor = new Color
        {
            R = 0,
            G = 153,
            B = 255,
            A = 255
        };
    }

    public override void Draw()
    {
        Color currentColor = IsPressed ? PressedColor : IsHovered ? HoverColor : DefaultColor;
        Color textColor = IsPressed ? Color.Black : IsHovered ? OuterBorderColor : Color.White;

        Raylib.DrawRectangleRounded(Box, Roundness, Segments, currentColor);
        Graphics.DrawText(Text, Box, textColor);
        Raylib.DrawRectangleRoundedLinesEx(Box, Roundness, Segments, Thickness, OuterBorderColor);
        Rectangle neonInside = new Rectangle(Box.X - Thickness / 4, Box.Y - Thickness / 4, Box.Width + Thickness / 2, Box.Height + Thickness / 2);
        Raylib.DrawRectangleRoundedLinesEx(neonInside, Roundness, Segments, Thickness / 2, InnerBorderColor);
    }
}
