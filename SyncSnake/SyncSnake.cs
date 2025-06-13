using GameTool;
using Managers;
using Raylib_cs;
using static Raylib_cs.Raylib;
public class SyncSnake
{
    public static void Main()
    {
        InitWindow(1330, 900, "SyncSnake");

        InitServiceLocator();
        ScenesManager SM = ServiceLocator.GetService<ScenesManager>();

        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.Black);
            SM.Update();
            SM.Draw();
            EndDrawing();
        }
    }

    public static void InitServiceLocator()
    {
        new ButtonsManager();
        new InputsManager();
        new AssetsManager();
        new ScenesManager();
    }
}