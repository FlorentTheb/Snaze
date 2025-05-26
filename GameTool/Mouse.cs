using Raylib_cs;
using static Raylib_cs.Raylib;

namespace GameTool;

public static class Mouse
{
    public static int GetX() => GetMouseX();
    public static int GetY() => GetMouseY();
    public static bool IsLeftDown() => IsMouseButtonDown(MouseButton.Left);
    public static bool IsRightDown() => IsMouseButtonDown(MouseButton.Right);
}
