using System.Drawing;
using System.Numerics;
using GameTool;

public class Game : IScene
{
    private int CurrentLevel;
    private const int CellNumber = 19;
    private const int CellSize = 30;
    private List<Grid> Grids = [];
    private List<Snake> Snakes = [];

    public Game()
    {
        CurrentLevel = 1;
        Grids.Add(new Grid(new Point(20, 210)));
        Grids.Add(new Grid(new Point(610, 210)));
        InitCells();
        Snakes.Add(new Snake(Grids[0], "Blue"));
        Snakes.Add(new Snake(Grids[1], "Pink"));
    }

    private void InitCells()
    {
        LoadLevel();
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
        DrawGrids();
        DrawSnakes();
    }

    public void DrawGrids()
    {
        foreach (var grid in Grids)
        {
            grid.Draw();
        }
    }

    public void DrawSnakes()
    {
        foreach (var snake in Snakes)
        {
            snake.Draw();
        }
    }

    private void CheckInputs()
    {
        // if (Mouse.IsLeftPressed())
        // {
        //     (int, int, int)? gridIndexCellValuePair = GetGridCell(Mouse.GetX(), Mouse.GetY());
        //     if (gridIndexCellValuePair != null)
        //     {
        //         int gridIndex = gridIndexCellValuePair.Value.Item1;
        //         int row = gridIndexCellValuePair.Value.Item2;
        //         int column = gridIndexCellValuePair.Value.Item3;

        //         if (Grids[gridIndex].GetCell(row, column) == 0)
        //             Grids[gridIndex].SetCell(row, column, 2);
        //         else if (Grids[gridIndex].GetCell(row, column) == 2)
        //             Grids[gridIndex].SetCell(row, column, 0);
        //     }

        // }
        List<string> nextMoveResults = new();
        string direction = "";
        if (Keyboard.IsKeyPressed("Z"))
            direction = "Up";
        else if (Keyboard.IsKeyPressed("Q"))
            direction = "Left";
        else if (Keyboard.IsKeyPressed("S"))
            direction = "Down";
        else if (Keyboard.IsKeyPressed("D"))
            direction = "Right";

        if (direction != "") // A key has been pressed
        {
            for (int snakeIndex = 0; snakeIndex < Snakes.Count; snakeIndex++) // Check extend and reduce first
            {
                Snakes[snakeIndex].UpdateNextLength(Snakes[1 - snakeIndex].CheckHeadNextMove(direction));
            }
        }
    }

    public void LoadLevel()
    {
        if (CurrentLevel == 1)
            LoadLevelOne();
    }

    public void LoadLevelOne()
    {

        for (int row = 0; row < Grids[0].Rows; row++)
        {
            for (int column = 0; column < Grids[0].Columns; column++)
            {
                if (row < 6 || row > Grids[0].Rows - 7 || column < 6 || column > Grids[0].Columns - 7)
                {
                    Grids[0].SetCell(row, column, CellType.Wall);
                    Grids[1].SetCell(row, column, CellType.Wall);
                }
                else
                {
                    Grids[0].SetCell(row, column, CellType.Empty);
                    Grids[1].SetCell(row, column, CellType.Empty);
                }

                Grids[0].SetCell(8, 10, CellType.Wall);
                Grids[1].SetCell(7, 10, CellType.Wall);

                Grids[0].SetCell(7, 7, CellType.Apple);
                Grids[1].SetCell(11, 7, CellType.Apple);
                Grids[1].SetCell(11, 10, CellType.Apple);
                Grids[0].SetCell(11, 10, CellType.Bomb);
            }
        }
    }
}
