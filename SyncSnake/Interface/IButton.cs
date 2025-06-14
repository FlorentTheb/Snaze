using System;
using System.Drawing;
using System.Numerics;
using Raylib_cs;
public interface IButton
{
    bool IsHovered { get; }
    bool IsPressed { get; }
    bool IsClicked { get; }

    void Draw();
    bool Update();
    void OnClick();
}
