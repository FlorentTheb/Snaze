using ToolKit;
using Raylib_cs;
using static Raylib_cs.Raylib;
public class Game
{
    public static void Main()
    {
        Console.WriteLine("Hello, WORLD !");
        InitWindow(800, 800, "SyncSnake");
        GameTimer.Set(3);
        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.White);
            Graphics.DrawSquare(400, 400, 25, "Green");
            if (GameTimer.Update())
            {
                Console.WriteLine($"Time left => 0 seconds ==> TIMER STOP");
                Console.WriteLine($"Mouse recorded in [{Mouse.GetX()}, {Mouse.GetY()}]");
            }
            EndDrawing();
        }
    }
}