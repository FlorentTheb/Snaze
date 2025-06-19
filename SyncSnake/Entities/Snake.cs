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
    private AssetsManager AM;
    public List<SnakePart> SnakeParts = new();
    public bool CanMove = false;
    public int LengthModifier = 0;
    private readonly Grid Grid;
    public Snake(Grid grid)
    {
        AM = ServiceLocator.GetService<AssetsManager>();
        Grid = grid;
        SnakeParts.Add(new SnakePart(new Point(10, 9), "Right"));
        SnakeParts.Add(new SnakePart(new Point(9, 9)));
        SnakeParts.Add(new SnakePart(new Point(8, 9)));
        UpdatePartsDirections();
        UpdatePartsSpriteIndex();
        UpdatePartsColor();
        UpdateSnakeCells();
    }

    public void UpdateNextLength(int newLength)
    {
        LengthModifier += newLength;
    }

    public bool AreElementsInExactSamePosition(Point coords_1, Point coords_2, Vector2 direction_1, Vector2 direction_2)
    {
        bool sameCoords = coords_1.Equals(coords_2);
        bool sameDirection = direction_1.Equals(direction_2);
        return sameCoords && sameDirection;
    }

    public void UpdatePuzzleResult()
    {
        bool headResult = AreElementsInExactSamePosition(SnakeParts[0].Coordinates, Grid.Objectives[0].Coordinates, SnakeParts[0].Direction, Grid.Objectives[0].Direction);
        bool tailResult = AreElementsInExactSamePosition(SnakeParts[^1].Coordinates, Grid.Objectives[1].Coordinates, SnakeParts[^1].Direction, Grid.Objectives[1].Direction);
        Grid.IsResolved = headResult && tailResult;
    }

    public void Update()
    {
        if (LengthModifier < 0)
            Reduce();
        else if (LengthModifier > 0 && CanMove)
            Extend();

        if (CanMove)
            Move();

        UpdatePartsColor();
        UpdatePartsSpriteIndex();
        UpdatePuzzleResult();
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
            Grid.SetCell(SnakeParts[^1].Coordinates.Y, SnakeParts[^1].Coordinates.X, CellType.Empty);

            SnakeParts.RemoveAt(SnakeParts.Count - 1);
            LengthModifier++;
        }
        else
            LengthModifier = 0;
    }

    public bool IsNextCellAvailable(Vector2 nextDirection)
    {
        int nextPosX = SnakeParts[0].Coordinates.X + (int)nextDirection.X;
        int nextPosY = SnakeParts[0].Coordinates.Y + (int)nextDirection.Y;
        if (!Grid.IsInBounds(nextPosY, nextPosX))
            return false;
        else
        {
            CellType gridValue = Grid.GetCell(nextPosY, nextPosX);
            switch (gridValue)
            {
                case CellType.Snake:
                    if (SnakeParts[^1].Coordinates.X == nextPosX && SnakeParts[^1].Coordinates.Y == nextPosY)
                        return true;
                    else return false;
                case CellType.Apple:
                case CellType.Bomb:
                case CellType.Empty:
                    return true;
                case CellType.Wall:
                default:
                    return false;
            }
        }
    }

    public int CheckHeadNextMove(string direction)
    {
        Vector2 wishedDirection = Coordinates.StringToVector(direction);
        bool nextCellEmpty = false;
        if (IsNextCellAvailable(wishedDirection))
        {
            SnakeParts[0].Direction = wishedDirection;
            nextCellEmpty = true;
        }
        else if (IsNextCellAvailable(SnakeParts[0].Direction))
            nextCellEmpty = true;

        if (nextCellEmpty)
        {
            CanMove = true;
            int nextPosX = SnakeParts[0].Coordinates.X + (int)SnakeParts[0].Direction.X;
            int nextPosY = SnakeParts[0].Coordinates.Y + (int)SnakeParts[0].Direction.Y;

            CellType gridValue = Grid.GetCell(nextPosY, nextPosX);
            switch (gridValue)
            {
                case CellType.Empty: // empty
                case CellType.Snake: // snake
                    return 0;
                case CellType.Apple: // apple
                    UpdateNextLength(1);
                    return 1;
                case CellType.Bomb: // bomb
                    return -1;
                default:
                    return 0;
            }
        }
        return 0;
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

        SnakeParts[0].SpriteId = SpriteId.SnakeHead;
        SnakeParts[^1].SpriteId = SpriteId.SnakeTail;
        for (int partIndex = 1; partIndex < SnakeParts.Count - 1; partIndex++)
        {
            var prevCoord = SnakeParts[partIndex - 1].Coordinates;
            var currentCoord = SnakeParts[partIndex].Coordinates;
            var nextCoord = SnakeParts[partIndex + 1].Coordinates;
            if (prevCoord.X == nextCoord.X || prevCoord.Y == nextCoord.Y)
                SnakeParts[partIndex].SpriteId = SpriteId.SnakeBodyStraight;
            else
            {
                if ((prevCoord.X > nextCoord.X && prevCoord.X == currentCoord.X && prevCoord.Y > nextCoord.Y)
                || (prevCoord.X < nextCoord.X && prevCoord.Y == currentCoord.Y && prevCoord.Y > nextCoord.Y)
                || (prevCoord.X < nextCoord.X && prevCoord.X == currentCoord.X && prevCoord.Y < nextCoord.Y)
                || (prevCoord.X > nextCoord.X && prevCoord.Y == currentCoord.Y && prevCoord.Y < nextCoord.Y))
                    SnakeParts[partIndex].SpriteId = SpriteId.SnakeBodyCornerRight;
                else SnakeParts[partIndex].SpriteId = SpriteId.SnakeBodyCornerLeft;
            }
        }
    }

    public void UpdatePartsColor()
    {
        int snakeIndex = Grid.Index;

        for (int partIndex = 0; partIndex < SnakeParts.Count; partIndex++)
        {
            if (partIndex == 0)
                SnakeParts[partIndex].Color = snakeIndex == 0 ? "DarkBlue" : "DarkPurple";
            else if (partIndex == SnakeParts.Count - 1)
                SnakeParts[partIndex].Color = snakeIndex == 0 ? "SkyBlue" : "Purple";
            else
                SnakeParts[partIndex].Color = snakeIndex == 0 ? "Blue" : "Violet";


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
            float angle = Coordinates.VectorToAngle(snakePart.Direction);
            Point pixelCoords = Grid.GetCellPixelsFromIndexes(snakePart.Coordinates.X, snakePart.Coordinates.Y);
            Graphics.DrawImage(pixelCoords.X, pixelCoords.Y, snakePart.Color, AM.GetSprite(snakePart.SpriteId), angle, 255);
        }
    }
}