using System.Drawing;
using System.Numerics;
using GameTool;

public class Game : IScene
{
    private const int CellNumber = 19;
    private const int CellSize = 30;
    private List<Grid<int>> Grids = new List<Grid<int>>();
    private List<Snake> Snakes = new List<Snake>();

    public Game()
    {
        Grids.Add(new Grid<int>(CellNumber, CellNumber));
        Grids.Add(new Grid<int>(CellNumber, CellNumber));
        InitCells();
        Snakes.Add(new Snake(Grids[0]));
        Snakes.Add(new Snake(Grids[1]));
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
        Grids[0].SetCell(10, 10, 2);
        Grids[1].SetCell(15, 7, 2);
        Grids[0].SetCell(4, 15, 3);
        Grids[0].SetCell(3, 5, 3);
        Grids[1].SetCell(7, 7, 4);
    }

    public void Update()
    {
        CheckInputs();
        foreach (var snake in Snakes)
        {
            snake.Update();
        }
    }

    public void Draw()
    {
        DrawGrid();
    }

    private void DrawGrid()
    {
        for (int index = 0; index < 2; index++)
        {
            for (int row = 0; row < Grids[0].Rows; row++)
            {
                for (int column = 0; column < Grids[0].Columns; column++)
                {
                    DrawCell(Grids[index].GetCell(row, column), GetGridCoordinates(index, row, column));
                }
            }
        }
    }

    private void DrawCell(int value, Point coordinates)
    {
        string color;
        if (value == 0)
            color = "DarkGray";
        else if (value == 1)
            color = "Green";
        else if (value == 2)
            color = "White";
        else if (value == 3)
            color = "Red";
        else color = "Brown";

        Graphics.DrawSquare(coordinates.X, coordinates.Y, CellSize, "Black");
        Graphics.DrawSquare(coordinates.X, coordinates.Y, CellSize - 1, color);
    }

    private Point GetGridCoordinates(int gridIndex, int rowIndex, int columnIndex)
    {
        int offSet = gridIndex == 0 ? 20 : 610;
        int posX = offSet + columnIndex * CellSize;
        int posY = 210 + rowIndex * CellSize;
        return new Point(posX, posY);
    }

    private void CheckInputs()
    {
        List<string> nextMoveResults = new();
        if (Keyboard.IsKeyPressed("Z"))
        {
            nextMoveResults.Add(Snakes[0].CheckNextMove(new Vector2(0, -1)));
            nextMoveResults.Add(Snakes[1].CheckNextMove(new Vector2(0, -1)));
        }
        else if (Keyboard.IsKeyPressed("Q"))
        {
            nextMoveResults.Add(Snakes[0].CheckNextMove(new Vector2(-1, 0)));
            nextMoveResults.Add(Snakes[1].CheckNextMove(new Vector2(-1, 0)));
        }
        else if (Keyboard.IsKeyPressed("S"))
        {
            nextMoveResults.Add(Snakes[0].CheckNextMove(new Vector2(0, 1)));
            nextMoveResults.Add(Snakes[1].CheckNextMove(new Vector2(0, 1)));
        }
        else if (Keyboard.IsKeyPressed("D"))
        {
            nextMoveResults.Add(Snakes[0].CheckNextMove(new Vector2(1, 0)));
            nextMoveResults.Add(Snakes[1].CheckNextMove(new Vector2(1, 0)));
        }
        if (nextMoveResults.Count > 0) // A key has been pressed
        {
            for (int snakeIndex = 0; snakeIndex < 2; snakeIndex++) // Check extend and reduce first
            {
                string result = nextMoveResults[snakeIndex];
                if (result == "Apple") // Current snake can move, we check other snake
                {
                    Snakes[snakeIndex].HasToExtend = true;
                    Snakes[1 - snakeIndex].HasToExtend = true;
                }
                else if (result == "Bomb")
                {
                    Snakes[1 - snakeIndex].Reduce();
                }

                if (result == "Empty" || result == "Apple" || result == "Bomb")
                    Snakes[snakeIndex].HasToMove = true;
            }
        }
    }
}
