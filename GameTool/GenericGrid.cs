
using System.Data.Common;

namespace GameTool;

public abstract class GenericGrid<T>
{
    public int Columns { get; protected set; }
    public int Rows { get; protected set; }
    protected T[,] cells;
    public GenericGrid(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        cells = new T[rows, columns];
    }

    public void SetCell(int row, int column, T value)
    {
        if (IsInBounds(row, column))
        {
            cells[row, column] = value;
        }
    }

    public T? GetCell(int row, int column)
    {
        if (IsInBounds(row, column))
        {
            return cells[row, column];
        }
        return default(T);
    }
    public bool IsInBounds(int row, int column)
    {
        return row >= 0 && row < Rows && column >= 0 && column < Columns;
    }
}
