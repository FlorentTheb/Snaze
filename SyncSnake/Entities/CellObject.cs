using System;
using System.Drawing;
using System.Numerics;
public abstract class CellObject
{
    public string Color = "";
    public Point Coordinates;
    public Vector2 Direction;
    public CellObject(Point coordinates, string direction)
    {
        Coordinates = coordinates;
        Direction = GameTool.Coordinates.StringToVector(direction);
    }
    public CellObject(Point coordinates)
    {
        Coordinates = coordinates;
    }
}

public class Objective : CellObject
{
    public string ObjectiveType = "";
    public Objective(Point coordinates, string direction, int gridIndex, string objectiveType) : base(coordinates, direction)
    {
        ObjectiveType = objectiveType;
        if (gridIndex == 0)
            Color = objectiveType == "Head" ? "DarkBlue" : "SkyBlue";
        else
            Color = objectiveType == "Head" ? "DarkPurple" : "Purple";
    }
}

public class SnakePart : CellObject
{
    public int SpriteIndex;
    public SnakePart(Point coordinates, string direction) : base(coordinates, direction) { }

    public SnakePart(Point coordinates) : base(coordinates) { }
}