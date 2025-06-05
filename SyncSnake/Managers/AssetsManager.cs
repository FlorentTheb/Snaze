using System;
using System.Numerics;
using Raylib_cs;
using GameTool;

namespace Managers;

public static class AssetsManager
{

    private static readonly List<Texture2D> SnakeSprites = new()
    {
        Raylib.LoadTexture("images/snake_head.png"),
        Raylib.LoadTexture("images/snake_body_straight.png"),
        Raylib.LoadTexture("images/snake_body_corner_right.png"),
        Raylib.LoadTexture("images/snake_body_corner_left.png"),
        Raylib.LoadTexture("images/snake_tail.png")
    };

    public static void DrawSprite(int spriteIndex, int posX, int posY, float spriteAngle, string color) =>
        Graphics.DrawImage(posX, posY, color, SnakeSprites[spriteIndex], spriteAngle);

}
