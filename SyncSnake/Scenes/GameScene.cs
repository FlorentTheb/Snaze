using System.Drawing;
using System.Numerics;
using GameTool;
using Managers;

public class GameScene : IScene
{
    List<IButton> lButtons = [];
    private int CurrentLevel;
    private ScenesManager SM;
    private ButtonsManager BM;
    private bool IsCurrentLevelResolved = false;
    private List<Grid> Grids = [];
    private List<Snake> Snakes = [];

    public GameScene()
    {
        SM = ServiceLocator.GetService<ScenesManager>();
        BM = ServiceLocator.GetService<ButtonsManager>();
        CurrentLevel = 1;
        InitButtons();
        InitCells();
    }

    private void InitButtons()
    {
        lButtons.Add(new NeonButton("Restart", new Raylib_cs.Rectangle(200, 100, 100, 50), "Pink", ResetLevel));
        lButtons.Add(new NeonButton("Back", new Raylib_cs.Rectangle(200, 170, 100, 50), "Blue", SM.ChangeScene<MenuScene>));
    }

    private void InitCells()
    {
        LoadLevel();
    }

    public void Update()
    {
        CheckMouseInputs();
        BM.Update(lButtons);
        if (Grids[0].isResolved && Grids[1].isResolved)
        {
            IsCurrentLevelResolved = true;
            GameTimer.Stop();
        }
        else
        {
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
    }

    public void Draw()
    {
        DrawGrids();
        DrawSnakes();
        DrawResult();
        BM.Draw(lButtons);
    }

    public void DrawResult()
    {
        string result = IsCurrentLevelResolved ? "Victory" : "Resolving ...";
        int height = 30;
        int width = Graphics.GetTextWidth(result, height);
        Graphics.DrawText(result, new Raylib_cs.Rectangle(Screen.GetWidth() * 3 / 4, 100, 100, 50), "Green");
        float timeRemaining = GameTimer.GetRemainingTime();
        string timeRemainingString = timeRemaining.ToString("n1");
        int width2 = Graphics.GetTextWidth(timeRemainingString, height);
        string wishedDirection = InputsManager.direction != "" ? InputsManager.direction : "Default";
        int width3 = Graphics.GetTextWidth(wishedDirection, height);
        Graphics.DrawText(timeRemainingString, new Raylib_cs.Rectangle(Screen.GetWidth() / 2, 100, 100, 50), "Green");
        Graphics.DrawText(wishedDirection, new Raylib_cs.Rectangle(Screen.GetWidth() / 2, 200, 100, 50), "Green");
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
        Grids.Add(new Grid(0, new Point(12, 9), new Point(10, 9), "Right", "Right"));
        Grids.Add(new Grid(1, new Point(11, 8), new Point(10, 9), "Right", "Up"));
        Snakes.Add(new Snake(Grids[0]));
        Snakes.Add(new Snake(Grids[1]));
        GameTimer.Set(2);
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

                Grids[0].SetCell(0, 0, CellType.Empty);
                Grids[0].SetCell(8, 10, CellType.Wall);
                Grids[1].SetCell(7, 10, CellType.Wall);

                Grids[0].SetCell(6, 6, CellType.Apple);
                Grids[0].SetCell(7, 6, CellType.Apple);
                Grids[0].SetCell(8, 6, CellType.Apple);
                Grids[0].SetCell(9, 6, CellType.Apple);
                Grids[0].SetCell(10, 6, CellType.Apple);
                Grids[1].SetCell(12, 12, CellType.Bomb);
            }
        }
    }

    public void ResetLevel()
    {
        Console.WriteLine("CHECK RESET");
        Grids.Clear();
        Snakes.Clear();
        GameTimer.Stop();
        IsCurrentLevelResolved = false;
        LoadLevel();
    }
}
