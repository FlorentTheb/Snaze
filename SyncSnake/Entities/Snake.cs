using System;
using System.Data;
using System.Drawing;
using System.Numerics;
using GameTool;
using Raylib_cs;
using static Raylib_cs.Raylib;
public class Snake
{
    public bool HasToExtend = false;
    public bool HasToMove = false;
    private Grid<int> Grid;
    private Vector2 HeadDirection;
    private List<Point> SnakeCoordinates = new List<Point>();
    public Snake(Grid<int> grid)
    {
        Grid = grid;
        HeadDirection = new Vector2(0, -1);
        SnakeCoordinates.Add(new Point(9, 8));
        SnakeCoordinates.Add(new Point(9, 9));
        SnakeCoordinates.Add(new Point(9, 10));
        Update();
    }

    public void Update()
    {
        if (HasToMove)
            Move();
        for (int i = 0; i < SnakeCoordinates.Count(); i++)
        {
            Grid.SetCell(SnakeCoordinates[i].Y, SnakeCoordinates[i].X, 1);
        }
    }

    public void Extend(Point bodyPart)
    {
        if (HasToExtend)
        {
            SnakeCoordinates.Add(bodyPart);
            HasToExtend = false;
        }
    }

    public void Reduce()
    {
        int size = SnakeCoordinates.Count;
        if (size > 2)
        {
            Grid.SetCell(SnakeCoordinates[size - 1].Y, SnakeCoordinates[size - 1].X, 0);
            SnakeCoordinates.RemoveAt(size - 1);
        }
    }

    public string CheckNextMove(Vector2 direction)
    {
        HeadDirection = direction;
        int nextPosY = SnakeCoordinates[0].Y + (int)HeadDirection.Y;
        int nextPosX = SnakeCoordinates[0].X + (int)HeadDirection.X;
        if (!Grid.IsInBounds(nextPosY, nextPosX))
            return "Wall";
        else
        {
            return Grid.GetCell(nextPosY, nextPosX) switch
            {
                0 => "Empty",
                1 => "Snake",
                3 => "Apple",
                4 => "Bomb",
                _ => "Wall"
            };
        }
    }

    public void Move()
    {
        HasToMove = false;

        int nextPosY = SnakeCoordinates[0].Y + (int)HeadDirection.Y;
        int nextPosX = SnakeCoordinates[0].X + (int)HeadDirection.X;

        ResetSnakeCells();
        Extend(UpdatePosition(nextPosY, nextPosX));
    }

    public Point UpdatePosition(int nextPosY, int nextPosX)
    {
        Point tail = SnakeCoordinates[^1];
        for (int i = SnakeCoordinates.Count - 1; i >= 1; i--)
        {
            SnakeCoordinates[i] = SnakeCoordinates[i - 1];
        }
        SnakeCoordinates[0] = new Point(nextPosX, nextPosY);
        return tail;
    }

    public void ResetSnakeCells()
    {
        foreach (var coord in SnakeCoordinates)
        {
            Grid.SetCell(coord.Y, coord.X, 0);
        }
    }
}
