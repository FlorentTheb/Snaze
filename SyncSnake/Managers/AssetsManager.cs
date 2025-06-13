using System;
using System.Numerics;
using Raylib_cs;
using GameTool;

namespace Managers;

public class AssetsManager
{
    private readonly Dictionary<SpriteId, Texture2D> Sprites = [];
    public AssetsManager()
    {
        ServiceLocator.Register(this);
        LoadSprites();
        LoadSFX();
        LoadMusic();
    }

    public void LoadSprites()
    {
        Sprites[SpriteId.SnakeHead] = Raylib.LoadTexture("Assets/sprites/snake_head.png");
        Sprites[SpriteId.SnakeBodyStraight] = Raylib.LoadTexture("Assets/sprites/snake_body_straight.png");
        Sprites[SpriteId.SnakeBodyCornerRight] = Raylib.LoadTexture("Assets/sprites/snake_body_corner_right.png");
        Sprites[SpriteId.SnakeBodyCornerLeft] = Raylib.LoadTexture("Assets/sprites/snake_body_corner_left.png");
        Sprites[SpriteId.SnakeTail] = Raylib.LoadTexture("Assets/sprites/snake_tail.png");
        Sprites[SpriteId.HeadGoal] = Raylib.LoadTexture("Assets/sprites/head_goal.png");
        Sprites[SpriteId.TailGoal] = Raylib.LoadTexture("Assets/sprites/tail_goal.png");
        Sprites[SpriteId.Apple] = Raylib.LoadTexture("Assets/sprites/apple.png");
        Sprites[SpriteId.Bomb] = Raylib.LoadTexture("Assets/sprites/bomb.png");
    }

    public void LoadSFX()
    {

    }

    public void LoadMusic()
    {

    }

    public Texture2D GetSprite(SpriteId id)
    {
        if (!Sprites.TryGetValue(id, out var texture))
            throw new ArgumentException($"Sprite {id} not loaded.");
        return texture;
    }

    public void Dispose()
    {
        foreach (var texture in Sprites.Values)
            Raylib.UnloadTexture(texture);
        Sprites.Clear();
    }

}
