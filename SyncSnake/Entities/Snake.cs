using System;
using System.Data;
using System.Drawing;
using System.Numerics;
using GameTool;
using Raylib_cs;
using Managers;

using static Raylib_cs.Raylib;
public class Snake
{
    public string Color;
    public List<SnakePart> SnakeParts = new();
    public bool CanMove = false;
    public int LengthModifier = 0;
    private readonly Grid Grid;
    public Snake(Grid grid, string color)
    {
        Grid = grid;
        Color = color;
        SnakeParts.Add(new SnakePart(new Point(10, 9), "Right"));
        SnakeParts.Add(new SnakePart(new Point(9, 9)));
        SnakeParts.Add(new SnakePart(new Point(8, 9)));
        UpdatePartsDirections();
        UpdatePartsSpriteIndex();
        UpdateSnakeCells();
    }

    public void UpdateNextLength(int newLength)
    {
        LengthModifier += newLength;
    }

    public void Update()
    {
        if (LengthModifier < 0)
            Reduce();
        else if (LengthModifier > 0 && CanMove)
            Extend();

        if (CanMove)
            Move();

        UpdatePartsSpriteIndex();
    }

    public void Extend()
    {
        SnakeParts.Add(new SnakePart(SnakeParts[^1].Coordinates));
        LengthModifier--;
    }

    public void Reduce()
    {
        if (SnakeParts.Count > 3)
        {
            SnakeParts.RemoveAt(SnakeParts.Count - 1);
            LengthModifier++;
        }
        else
            LengthModifier = 0;
    }

    public int CheckHeadNextMove(string direction)
    {
        Vector2 wishedDirection = Coordinates.StringToVector(direction);
        int nextPosX = SnakeParts[0].Coordinates.X + (int)wishedDirection.X;
        int nextPosY = SnakeParts[0].Coordinates.Y + (int)wishedDirection.Y;

        if (!Grid.IsInBounds(nextPosY, nextPosX))
        {
            CanMove = false;
            return 0;
        }
        else
        {
            CellType gridValue = Grid.GetCell(nextPosY, nextPosX);
            switch (gridValue)
            {
                case CellType.Empty: // empty
                    SnakeParts[0].Direction = wishedDirection;
                    CanMove = true;
                    return 0;
                case CellType.Snake: // snake
                    if (SnakeParts[^1].Coordinates.X == nextPosX && SnakeParts[^1].Coordinates.Y == nextPosY)
                    {
                        SnakeParts[0].Direction = wishedDirection;
                        CanMove = true;
                    }
                    else CanMove = false;
                    return 0;
                case CellType.Wall: // wall
                    CanMove = false;
                    return 0;
                case CellType.Apple: // apple
                    SnakeParts[0].Direction = wishedDirection;
                    CanMove = true;
                    UpdateNextLength(1);
                    return 1;
                case CellType.Bomb: // bomb
                    SnakeParts[0].Direction = wishedDirection;
                    CanMove = true;
                    return -1;
                default:
                    CanMove = false;
                    return 0;
            }
        }
    }

    public void Move()
    {
        ResetSnakeCells();
        UpdatePartsPositions();
        UpdatePartsDirections();
        CanMove = false;
    }

    public void UpdatePartsPositions()
    {
        for (int partIndex = SnakeParts.Count - 1; partIndex > 0; partIndex--)
        {
            SnakeParts[partIndex].Coordinates = SnakeParts[partIndex - 1].Coordinates;
        }
        SnakeParts[0].Coordinates.Y = SnakeParts[0].Coordinates.Y + (int)SnakeParts[0].Direction.Y;
        SnakeParts[0].Coordinates.X = SnakeParts[0].Coordinates.X + (int)SnakeParts[0].Direction.X;
        UpdateSnakeCells();
    }

    public void UpdatePartsDirections()
    {
        for (int partIndex = 1; partIndex < SnakeParts.Count; partIndex++)
        {
            SnakeParts[partIndex].Direction.X = SnakeParts[partIndex - 1].Coordinates.X - SnakeParts[partIndex].Coordinates.X;
            SnakeParts[partIndex].Direction.Y = SnakeParts[partIndex - 1].Coordinates.Y - SnakeParts[partIndex].Coordinates.Y;
        }
    }

    public void UpdatePartsSpriteIndex()
    {
        SnakeParts[0].SpriteIndex = 0;
        SnakeParts[^1].SpriteIndex = 4;
        for (int partIndex = 1; partIndex < SnakeParts.Count - 1; partIndex++)
        {
            var prevCoord = SnakeParts[partIndex - 1].Coordinates;
            var currentCoord = SnakeParts[partIndex].Coordinates;
            var nextCoord = SnakeParts[partIndex + 1].Coordinates;
            if (prevCoord.X == nextCoord.X || prevCoord.Y == nextCoord.Y)
                SnakeParts[partIndex].SpriteIndex = 1;
            else
            {
                if ((prevCoord.X > nextCoord.X && prevCoord.X == currentCoord.X && prevCoord.Y > nextCoord.Y)
                || (prevCoord.X < nextCoord.X && prevCoord.Y == currentCoord.Y && prevCoord.Y > nextCoord.Y)
                || (prevCoord.X < nextCoord.X && prevCoord.X == currentCoord.X && prevCoord.Y < nextCoord.Y)
                || (prevCoord.X > nextCoord.X && prevCoord.Y == currentCoord.Y && prevCoord.Y < nextCoord.Y))
                    SnakeParts[partIndex].SpriteIndex = 2;
                else SnakeParts[partIndex].SpriteIndex = 3;
            }
        }
    }

    public void ResetSnakeCells()
    {
        foreach (var part in SnakeParts)
        {
            Grid.SetCell(part.Coordinates.Y, part.Coordinates.X, CellType.Empty);
        }
    }

    public void UpdateSnakeCells()
    {
        foreach (var part in SnakeParts)
        {
            Grid.SetCell(part.Coordinates.Y, part.Coordinates.X, CellType.Snake);
        }
    }

    public void Draw()
    {
        foreach (var snakePart in SnakeParts)
        {
            float angle = GameTool.Coordinates.VectorToAngle(snakePart.Direction);
            Point pixelCoords = Grid.GetCellPixelsFromIndexes(snakePart.Coordinates.X, snakePart.Coordinates.Y);
            AssetsManager.DrawSprite(snakePart.SpriteIndex, pixelCoords.X, pixelCoords.Y, angle, Color);
        }
    }
}

public class SnakePart
{
    public int SpriteIndex;
    public Point Coordinates;
    public Vector2 Direction;

    public SnakePart(Point coordinates, string direction)
    {
        Coordinates = coordinates;
        Direction = GameTool.Coordinates.StringToVector(direction);
    }

    public SnakePart(Point coordinates)
    {
        Coordinates = coordinates;
    }
}
