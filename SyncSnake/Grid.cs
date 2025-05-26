using System;

public class Grid
{
    private int[,] Cells;
    public Grid(int rows, int columns)
    {
        if (rows > 0 && columns > 0)
            Cells = new int[rows, columns];
        else
            throw new ArgumentException("row and column number must be positive.");
    }
}
