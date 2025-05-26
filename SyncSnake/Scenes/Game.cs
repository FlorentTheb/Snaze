using System;
using System.Runtime.CompilerServices;
using GameTool;

public class Game : IScene
{
    private const int CellNumber = 19;
    private List<Grid<int>> Grids = new List<Grid<int>>();
    private List<Snake> Snakes = new List<Snake>();

    public Game()
    {
        Grids[0] = new Grid<int>(CellNumber, CellNumber);
        Grids[1] = new Grid<int>(CellNumber, CellNumber);
        InitCells();
        Snakes.Add(new Snake());
        Snakes.Add(new Snake());
    }

    private void InitCells()
    {
        for (int row = 0; row < Grids[0].Rows; row++)
        {
            for (int column = 0; column < Grids[0].Columns; column++)
            {
                Grids[0].SetCell(row, column, 0);
                Grids[1].SetCell(row, column, 0);
            }
        }
    }

    public void Update()
    {

    }

    public void Draw()
    {
        DrawGrid();
        DrawSnake();
    }

    private void DrawGrid()
    {
        for (int index = 0; index < 2; index++)
        {
            for (int row = 0; row < Grids[0].Rows; row++)
            {
                for (int column = 0; column < Grids[0].Columns; column++)
                {
                    int cell = Grids[index].GetCell(row, column);
                    int posX, posY;
                    if (index == 0)
                        posX = 20 + 30 * column;
                    else
                        posX = 610 + 30 * column;
                    posY = 210 + 30 * row;
                    DrawCell(cell, posX, posY, 30);
                }
            }
        }
    }

    private void DrawCell(int value, int posX, int posY, int sideLength)
    {
        string color;
        if (value == 0)
            color = "DarkGray";
        else color = "White";

        Graphics.DrawSquare(posX, posY, sideLength, "Black");
        Graphics.DrawSquare(posX, posY, sideLength - 1, color);
    }

    private void DrawSnake()
    {

    }
}
