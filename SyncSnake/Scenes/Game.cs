using System.Drawing;
using System.Numerics;
using GameTool;

public class Game : IScene
{
    private int CurrentLevel;
    private List<Grid> Grids = [];
    private List<Snake> Snakes = [];

    public Game()
    {
        CurrentLevel = 1;
        Grids.Add(new Grid(0));
        Grids.Add(new Grid(1));
        InitCells();
        Snakes.Add(new Snake(Grids[0], "Blue"));
        Snakes.Add(new Snake(Grids[1], "Pink"));
        GameTimer.Set(3);
    }

    private void InitCells()
    {
        LoadLevel();
    }

    public void Update()
    {
        CheckMouseInputs();

        if (!GameTimer.Update())
            InputsManager.CheckKeyboard();
        else
        {
            for (int snakeIndex = 0; snakeIndex < Snakes.Count; snakeIndex++) // Check extend and reduce first
            {
                string currentDirection = InputsManager.direction != "" ? InputsManager.direction : Coordinates.VectorToString(Snakes[snakeIndex].SnakeParts[0].Direction);
                Snakes[1 - snakeIndex].UpdateNextLength(Snakes[snakeIndex].CheckHeadNextMove(currentDirection));
            }
            GameTimer.Reset();
            InputsManager.Reset();
        }
        foreach (var snake in Snakes)
        {
            snake.Update();
        }
    }

    public void Draw()
    {
        DrawGrids();
        DrawSnakes();
        DrawResult();
    }

    public void DrawResult()
    {
        string result = "Temporary result";
        int height = 30;
        int width = Graphics.GetTextWidth(result, height);
        Graphics.DrawText(result, Screen.GetWidth() / 2 - width / 2, 2 * height, height, "Green");
        float timeRemaining = GameTimer.GetRemainingTime();
        string timeRemainingString = timeRemaining.ToString("n1");
        int width2 = Graphics.GetTextWidth(timeRemainingString, height);
        string wishedDirection = InputsManager.direction != "" ? InputsManager.direction : "Default";
        int width3 = Graphics.GetTextWidth(wishedDirection, height);
        Graphics.DrawText(timeRemainingString, Screen.GetWidth() / 2 - width2 / 2, 4 * height, height, "Green");
        Graphics.DrawText(wishedDirection, Screen.GetWidth() / 2 - width3 / 2, 5 * height, height, "Green");
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

    private void CheckMouseInputs()
    {
        if (Mouse.IsLeftPressed())
        {
            Point? clickedGridIndexes;
            int gridIndex = 0;
            clickedGridIndexes = Grids[gridIndex].GetCellIndexesFromPixels(Mouse.GetX(), Mouse.GetY());

            if (clickedGridIndexes == null)
                gridIndex = 1;
            clickedGridIndexes = Grids[gridIndex].GetCellIndexesFromPixels(Mouse.GetX(), Mouse.GetY());

            if (clickedGridIndexes != null)
            {
                int row = clickedGridIndexes.Value.Y;
                int column = clickedGridIndexes.Value.X;
                if (Grids[gridIndex].GetCell(row, column) == CellType.Empty)
                    Grids[gridIndex].SetCell(row, column, CellType.Wall);
                else if (Grids[gridIndex].GetCell(row, column) == CellType.Wall)
                    Grids[gridIndex].SetCell(row, column, CellType.Empty);
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

                Grids[0].SetCell(6, 6, CellType.Apple);
                Grids[0].SetCell(7, 6, CellType.Apple);
                Grids[0].SetCell(8, 6, CellType.Apple);
                Grids[0].SetCell(9, 6, CellType.Apple);
                Grids[0].SetCell(10, 6, CellType.Apple);

                Grids[0].UpdateGoals(new Point(7, 7), new Point(11, 10));
                Grids[1].UpdateGoals(new Point(11, 7), new Point(11, 11));
            }
        }
    }
}
