using System;
using Raylib_cs;
public interface IButton
{
    Rectangle Bounds { get; }

    string Text { get; }

    Color DefaultColor { get; }
    Color HoverColor { get; }
    Color PressedColor { get; }

    bool IsHovered { get; }
    bool IsPressed { get; }

    void Draw();
    void Update();
    void OnClick();
}
