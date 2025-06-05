using System;
using System.Drawing;
using GameTool;

public class Grid : GenericGrid<CellType>
{
    public const int CellNumber = 19;
    public const int CellSize = 30;
    private readonly Point OffSet;
    public Grid(Point offset) : base(CellNumber, CellNumber)
    {
        OffSet = offset;
    }

    public Point GetCellPixelsFromIndexes(int columnIndex, int rowIndex)
    {
        int posX = OffSet.X + columnIndex * CellSize + (int)(0.5 * CellSize);
        int posY = OffSet.Y + rowIndex * CellSize + (int)(0.5 * CellSize);
        return new Point(posX, posY);
    }

    public Point? GetCellIndexesFromPixels(int posX, int posY)
    {
        if (posY > OffSet.Y && posY <= OffSet.Y + CellNumber * CellSize)
        {
            if (posX > OffSet.X && posX <= OffSet.X + CellNumber * CellSize)
            {
                int column = (posX - OffSet.X) / CellSize;
                int row = (posY - OffSet.Y) / CellSize;
                return new Point(column, row);
            }
        }
        return null;
    }

    public void Draw()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                string color = "";
                if ((GetCell(row, column) == CellType.Empty) || (GetCell(row, column) == CellType.Snake))
                    color = "DarkGray";
                else if (GetCell(row, column) == CellType.Wall)
                    color = "White";
                else if (GetCell(row, column) == CellType.Apple)
                    color = "Red";
                else if (GetCell(row, column) == CellType.Bomb)
                    color = "Brown";

                Point coords = GetCellPixelsFromIndexes(column, row);
                Graphics.DrawSquare(coords.X, coords.Y, CellSize, "Black");
                if (color != "")
                    Graphics.DrawSquare(coords.X, coords.Y, CellSize - 1, color);
            }
        }
    }
}
