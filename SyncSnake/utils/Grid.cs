using System;
using System.Drawing;
using GameTool;

public class Grid : GenericGrid<CellType>
{
    private const int CellNumber = 19;
    private const int CellSize = 29;
    private const int CellOffset = 2;
    private const int Size = CellNumber * CellSize + (CellNumber - 1) * CellOffset;
    private Point Start;
    private Point[] Objectives = new Point[2];
    private readonly int Index;
    public Grid(int index) : base(CellNumber, CellNumber)
    {
        Index = index;
        int totalGridsWidth = 2 * Size;
        int totalSpacing = Screen.GetWidth() - totalGridsWidth;
        int sideMargin = totalSpacing / 3;
        Start.X = index == 0 ? sideMargin : 2 * sideMargin + Size;
        Start.Y = Screen.GetHeight() - (Size + sideMargin);
    }

    public void UpdateGoals(Point headGoal, Point tailGoal)
    {
        Objectives[0] = headGoal;
        Objectives[1] = tailGoal;
    }

    public Point GetCellPixelsFromIndexes(int columnIndex, int rowIndex)
    {
        int posX = Start.X + columnIndex * (CellSize + CellOffset);
        int posY = Start.Y + rowIndex * (CellSize + CellOffset);
        return new Point(posX, posY);
    }

    public Point? GetCellIndexesFromPixels(int posX, int posY)
    {
        if (posY > Start.Y && posY <= Start.Y + Size)
        {
            if (posX > Start.X && posX <= Start.X + Size)
            {
                int column = (posX - Start.X) / (CellSize + CellOffset);
                int row = (posY - Start.Y) / (CellSize + CellOffset);
                return new Point(column, row);
            }
        }
        return null;
    }

    private void DrawObjectives()
    {
        for (int obj = 0; obj < Objectives.Length; obj++)
        {
            Point coords = GetCellPixelsFromIndexes(Objectives[obj].X, Objectives[obj].Y);
            string color = obj == 0 ? "Green" : "Yellow";
            Graphics.DrawSquare(coords.X - 1, coords.Y - 1, CellSize + CellOffset, color);
        }
    }

    public void Draw()
    {
        DrawObjectives();
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                Point coords = GetCellPixelsFromIndexes(column, row);
                string color;
                switch (GetCell(row, column))
                {
                    case CellType.Empty:
                    case CellType.Snake:
                        color = "Gray";
                        break;
                    case CellType.Wall:
                        color = "DarkGray";
                        break;
                    case CellType.Apple:
                        color = "Red";
                        break;
                    case CellType.Bomb:
                        color = "Brown";
                        break;
                    default:
                        color = "Black";
                        break;
                }
                Graphics.DrawSquare(coords.X, coords.Y, CellSize, color);
            }
        }
    }
}
