using GameTool;
using Raylib_cs;
using static Raylib_cs.Raylib;
public class SyncSnake
{
    public static void Main()
    {
        Console.WriteLine("Hello, WORLD !");
        InitWindow(1200, 800, "SyncSnake");
        GameTimer.Set(3);
        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.Black);
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