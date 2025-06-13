using System;
using System.Drawing;
using System.Numerics;
public abstract class CellObject
{
    public SpriteId SpriteId;
    public string Color = "";
    public Point Coordinates;
    public Vector2 Direction;
    public CellObject(Point coordinates, string direction, SpriteId spriteId)
    {
        SpriteId = spriteId;
        Coordinates = coordinates;
        Direction = GameTool.Coordinates.StringToVector(direction);
    }
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
    public Objective(Point coordinates, string direction, int gridIndex, SpriteId spriteId) : base(coordinates, direction, spriteId)
    {
        if (gridIndex == 0)
            Color = SpriteId == SpriteId.HeadGoal ? "DarkBlue" : "SkyBlue";
        else
            Color = SpriteId == SpriteId.HeadGoal ? "DarkPurple" : "Purple";
    }
}

public class SnakePart : CellObject
{
    public SnakePart(Point coordinates, string direction) : base(coordinates, direction) { }

    public SnakePart(Point coordinates) : base(coordinates) { }
}