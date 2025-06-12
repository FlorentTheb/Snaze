using System;
using GameTool;

public static class InputsManager
{
    public static string direction { get; private set; } = "";
    private static void Register(string newDirection)
    {
        direction = newDirection;
    }

    public static void Reset()
    {
        direction = "";
    }

    public static void CheckKeyboard()
    {
        string[] keys = { "Z", "Q", "S", "D", "Up", "Left", "Down", "Right" };

        foreach (var key in keys)
        {
            if (Keyboard.IsKeyPressed(key))
            {
                Register(GetDirectionFromKeyboard(key));
            }
        }
    }

    public static string GetDirectionFromKeyboard(string key)
    {
        return key switch
        {
            "Z" => "Up",
            "Q" => "Left",
            "S" => "Down",
            "D" => "Right",
            "Up" => "Up",
            "Left" => "Left",
            "Down" => "Down",
            "Right" => "Right",
            _ => "_"
        };
    }
}
