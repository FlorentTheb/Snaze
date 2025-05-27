using System;
using System.Data;
using System.Drawing;
using System.Numerics;
using GameTool;
using Raylib_cs;
using static Raylib_cs.Raylib;
public class Snake
{
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
        UpdateGrid();
    }

    public void UpdateGrid()
    {
        for (int i = 0; i < SnakeCoordinates.Count(); i++)
        {
            Grid.SetCell(SnakeCoordinates[i].Y, SnakeCoordinates[i].X, 1);
        }
    }

}
