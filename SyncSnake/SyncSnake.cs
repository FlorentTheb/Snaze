using GameTool;
using Raylib_cs;
using static Raylib_cs.Raylib;
public class SyncSnake
{
    public static void Main()
    {
        InitWindow(1330, 900, "SyncSnake");
        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.Black);
            SceneManager.Update();
            SceneManager.Draw();
            EndDrawing();
        }
    }
}