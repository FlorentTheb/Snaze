using Raylib_cs;
using static Raylib_cs.Raylib;
using scenes;
using services;
using services.managers;
public class SyncSnake
{
    public static void Main()
    {
        InitWindow(1330, 900, "SyncSnake");

        InitServiceLocator();
        ScenesManager SM = ServiceLocator.GetService<ScenesManager>();
        SM.ChangeScene<MenuScene>();
        while (!WindowShouldClose() && !SM.WillClose)
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
        _ = new ButtonsManager();
        _ = new InputsManager();
        _ = new AssetsManager();
        _ = new ScenesManager();
    }
}