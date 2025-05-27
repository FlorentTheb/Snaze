using System.Drawing;
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
    }

    public void Update()
    {

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
        else color = "Green";

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
}
