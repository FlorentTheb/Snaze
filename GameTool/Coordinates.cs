using System;
using System.Numerics;

namespace GameTool;

public static class Coordinates
{
    public static readonly Vector2 Right = new(1, 0);
    public static readonly Vector2 Left = new(-1, 0);
    public static readonly Vector2 Up = new(0, -1);
    public static readonly Vector2 Down = new(0, 1);
    public static Vector2 StringToVector(string direction)
    {

        return direction switch
        {
            "Right" => Right,
            "Left" => Left,
            "Up" => Up,
            "Down" => Down,
            _ => throw new ArgumentException($"Direction '{direction}' is invalid. Must be 'Right', 'Left', 'Up', or 'Down'")
        };
    }

    public static float StringToAngle(string direction)
    {
        return direction switch
        {
            "Right" => 0f,
            "Left" => 90f,
            "Up" => 180f,
            "Down" => 270f,
            _ => throw new ArgumentException($"Direction '{direction}' is invalid. Must be 'Right', 'Left', 'Up', or 'Down'")
        };
    }

    public static string VectorToString(Vector2 direction)
    {
        if (direction == new Vector2(1, 0)) return "Right";
        if (direction == new Vector2(0, 1)) return "Down";
        if (direction == new Vector2(-1, 0)) return "Left";
        if (direction == new Vector2(0, -1)) return "Up";
        throw new ArgumentException($"Direction '{direction}' is invalid. Must be a valid vector with X and Y");
    }
    public static float VectorToAngle(Vector2 direction)
    {
        if (direction == new Vector2(1, 0)) return 0f;
        if (direction == new Vector2(0, 1)) return 90f;
        if (direction == new Vector2(-1, 0)) return 180f;
        if (direction == new Vector2(0, -1)) return 270f;
        throw new ArgumentException($"Direction '{direction}' is invalid. Must be a valid vector with X and Y");
    }
}
