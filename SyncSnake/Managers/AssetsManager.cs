using System;
using System.Numerics;
using Raylib_cs;
using GameTool;

namespace Managers;

public static class AssetsManager
{

    private static readonly List<Texture2D> SnakeSprites = new()
    {
        Raylib.LoadTexture("Assets/sprites/snake_head.png"),
        Raylib.LoadTexture("Assets/sprites/snake_body_straight.png"),
        Raylib.LoadTexture("Assets/sprites/snake_body_corner_right.png"),
        Raylib.LoadTexture("Assets/sprites/snake_body_corner_left.png"),
        Raylib.LoadTexture("Assets/sprites/snake_tail.png"),
        Raylib.LoadTexture("Assets/sprites/head_goal.png"),
        Raylib.LoadTexture("Assets/sprites/tail_goal.png")
    };

    public static void DrawSprite(int spriteIndex, int posX, int posY, float spriteAngle, string color, int alpha) =>
        Graphics.DrawImage(posX, posY, color, SnakeSprites[spriteIndex], spriteAngle, alpha);

}
