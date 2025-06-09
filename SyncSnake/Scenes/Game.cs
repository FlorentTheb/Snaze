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
        DrawResult();
    }

    public void DrawResult()
    {
        string result = "Temporary result";
        int height = 30;
        int width = Graphics.GetTextWidth(result, height);
        Graphics.DrawText(result, Screen.GetWidth() / 2 - width / 2, 2 * height, height, "Green");
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

                Grids[0].UpdateGoals(new Point(7, 7), new Point(11, 10));
                Grids[1].UpdateGoals(new Point(11, 7), new Point(11, 11));
            }
        }
    }
}
