using System.Drawing;
using GameTool;
using utils.types;
using utils.buttons;
using interfaces;
using services;
using services.managers;
using entities;

namespace scenes;

public class GameScene : IScene
{
    private readonly List<IButton> lButtons = [];
    private int CurrentLevel;
    private readonly ScenesManager SM;
    private readonly ButtonsManager BM;
    private bool IsCurrentLevelResolved = false;
    private readonly List<Grid> Grids = [];
    private readonly List<Snake> Snakes = [];

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
        lButtons.Add(new NeonButton("Restart", new Raylib_cs.Rectangle(50, 50, 100, 50), "Pink", ResetLevel));
        lButtons.Add(new NeonButton("Menu", new Raylib_cs.Rectangle(50, 120, 100, 50), "Blue", SM.ChangeScene<MenuScene>));
    }

    private void InitCells()
    {
        LoadLevel();
    }

    public void Update()
    {
        CheckMouseInputs();
        BM.Update(lButtons);
        if (Grids[0].IsResolved && Grids[1].IsResolved && !IsCurrentLevelResolved)
        {
            IsCurrentLevelResolved = true;
            GameTimer.Stop();

            Raylib_cs.Rectangle nextRect = new Raylib_cs.Rectangle(Screen.GetWidth() - 200, 50, 150, 50);
            if (CurrentLevel == 3)
                lButtons.Add(new NeonButton("Next Level", nextRect, "Blue", SM.ChangeScene<VictoryScene>));
            else
                lButtons.Add(new NeonButton("Next Level", nextRect, "Blue", GoNextLevel));
        }
        else if (!IsCurrentLevelResolved)
        {
            if (!GameTimer.Update())
                InputsManager.CheckKeyboard();
            else
            {
                for (int snakeIndex = 0; snakeIndex < Snakes.Count; snakeIndex++) // Check extend and reduce first
                {
                    string currentDirection = InputsManager.Direction != "" ? InputsManager.Direction : Coordinates.VectorToString(Snakes[snakeIndex].SnakeParts[0].Direction);
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
        Graphics.DrawText(result, new Raylib_cs.Rectangle(Screen.GetWidth() * 3 / 4, 100, 200, 100), "Green");
        float timeRemaining = GameTimer.GetRemainingTime();
        string timeRemainingString = timeRemaining.ToString("n1");
        string wishedDirection = InputsManager.Direction != "" ? InputsManager.Direction : "Default";
        Graphics.DrawText(timeRemainingString, new Raylib_cs.Rectangle(Screen.GetWidth() / 2 - 50, 100, 100, 50), "Green");
        Graphics.DrawText("Direction : " + wishedDirection, new Raylib_cs.Rectangle(Screen.GetWidth() / 2 - 50, 200, 100, 50), "Green");
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
        switch (CurrentLevel)
        {
            case 1:
                LoadLevelOne();
                break;
            case 2:
                LoadLevelTwo();
                break;
            case 3:
                LoadLevelThree();
                break;
            default:
                break;
        }
    }

    private void GoNextLevel()
    {
        CurrentLevel++;
        ResetLevel();
    }

    public void LoadLevelOne()
    {
        Grids.Add(new Grid(0, new Point(13, 9), new Point(11, 9), "Right", "Right"));
        Grids.Add(new Grid(1, new Point(12, 8), new Point(11, 9), "Up", "Right"));
        Snakes.Add(new Snake(Grids[0]));
        Snakes.Add(new Snake(Grids[1]));
        GameTimer.Set(2);
        for (int row = 0; row < Grids[0].Rows; row++)
        {
            for (int column = 0; column < Grids[0].Columns; column++)
            {
                if (row < 7 || row > Grids[0].Rows - 8 || column < 7 || column > Grids[0].Columns - 6)
                {
                    Grids[0].SetCell(row, column, CellType.Wall);
                    Grids[1].SetCell(row, column, CellType.Wall);
                }
                else
                {
                    Grids[0].SetCell(row, column, CellType.Empty);
                    Grids[1].SetCell(row, column, CellType.Empty);
                }

                Grids[0].SetCell(8, 12, CellType.Wall);
            }
        }
    }

    public void LoadLevelTwo()
    {
        Grids.Add(new Grid(0, new Point(13, 9), new Point(12, 9), "Down", "Up"));
        Grids.Add(new Grid(1, new Point(13, 11), new Point(11, 10), "Right", "Down"));
        Snakes.Add(new Snake(Grids[0]));
        Snakes.Add(new Snake(Grids[1]));
        GameTimer.Set(2);
        for (int row = 0; row < Grids[0].Rows; row++)
        {
            for (int column = 0; column < Grids[0].Columns; column++)
            {
                if (row < 7 || row > Grids[0].Rows - 8 || column < 7 || column > Grids[0].Columns - 6)
                {
                    Grids[0].SetCell(row, column, CellType.Wall);
                    Grids[1].SetCell(row, column, CellType.Wall);
                }
                else
                {
                    Grids[0].SetCell(row, column, CellType.Empty);
                    Grids[1].SetCell(row, column, CellType.Empty);
                }

                Grids[0].SetCell(10, 11, CellType.Wall);
                Grids[1].SetCell(11, 12, CellType.Apple);
            }
        }
    }

    public void LoadLevelThree()
    {
        Grids.Add(new Grid(0, new Point(9, 11), new Point(9, 9), "Down", "Down"));
        Grids.Add(new Grid(1, new Point(12, 10), new Point(13, 8), "Left", "Down"));
        Snakes.Add(new Snake(Grids[0]));
        Snakes.Add(new Snake(Grids[1]));
        GameTimer.Set(2);
        for (int row = 0; row < Grids[0].Rows; row++)
        {
            for (int column = 0; column < Grids[0].Columns; column++)
            {
                if (row < 7 || row > Grids[0].Rows - 8 || column < 7 || column > Grids[0].Columns - 5)
                {
                    Grids[0].SetCell(row, column, CellType.Wall);
                    Grids[1].SetCell(row, column, CellType.Wall);
                }
                else
                {
                    Grids[0].SetCell(row, column, CellType.Empty);
                    Grids[1].SetCell(row, column, CellType.Empty);
                }

                Grids[0].SetCell(8, 10, CellType.Apple);
                Grids[0].SetCell(7, 10, CellType.Wall);
                Grids[0].SetCell(10, 8, CellType.Wall);
                Grids[1].SetCell(8, 11, CellType.Wall);
                Grids[1].SetCell(8, 12, CellType.Bomb);
            }
        }
    }
    public void ResetLevel()
    {
        lButtons.RemoveAll(b => b is NeonButton { Text: "Next Level" });
        Grids.Clear();
        Snakes.Clear();
        GameTimer.Stop();
        IsCurrentLevelResolved = false;
        LoadLevel();
    }
}
