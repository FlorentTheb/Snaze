using GameTool;
using Raylib_cs;
using static Raylib_cs.Raylib;
public class SyncSnake
{
    public static void Main()
    {
        InitWindow(1200, 800, "SyncSnake");
        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.Gray);
            SceneManager.Update();
            SceneManager.Draw();
            EndDrawing();
        }
    }
}