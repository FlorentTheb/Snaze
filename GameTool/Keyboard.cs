using Raylib_cs;
using static Raylib_cs.Raylib;

namespace GameTool;

public static class Keyboard
{
    private static readonly Dictionary<string, KeyboardKey> KeyMap = new()
    {
        { "Z", KeyboardKey.W },
        { "Q", KeyboardKey.A },
        { "S", KeyboardKey.S },
        { "D", KeyboardKey.D },
        { "Up", KeyboardKey.Up },
        { "Left", KeyboardKey.Left },
        { "Down", KeyboardKey.Down },
        { "Right", KeyboardKey.Right },
    };
    public static bool IsKeyPressed(string keyString)
    {
        if (!KeyMap.TryGetValue(keyString, out KeyboardKey key))
            throw new ArgumentException($"Key {keyString} not found.");
        return Raylib.IsKeyPressed(key);
    }
    public static bool IsKeyDown(string keyString)
    {
        if (!KeyMap.TryGetValue(keyString, out KeyboardKey key))
            throw new ArgumentException($"Key {keyString} not found.");
        return Raylib.IsKeyPressed(key);
    }
}
