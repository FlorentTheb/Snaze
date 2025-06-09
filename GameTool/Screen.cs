using System;
using Raylib_cs;

namespace GameTool;

public static class Screen
{
    public static int GetWidth() => Raylib.GetScreenWidth();
    public static int GetHeight() => Raylib.GetScreenHeight();
}
