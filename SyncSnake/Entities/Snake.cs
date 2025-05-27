using System;
using System.Data;
using System.Drawing;
using Raylib_cs;
using static Raylib_cs.Raylib;
public class Snake
{
    private int Length;
    private List<Point> SnakeCoordinates = new List<Point>();
    private int RowPosition;
    private int ColumnPosition;
    public Snake()
    {
        Length = 3;
        SnakeCoordinates.Add(new Point(9, 8));
        SnakeCoordinates.Add(new Point(9, 9));
        SnakeCoordinates.Add(new Point(9, 10));
    }

}
