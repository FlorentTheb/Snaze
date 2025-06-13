using System;
public class ScenesManager
{
    public ScenesManager()
    {
        ServiceLocator.Register(this);
    }
    public static IScene CurrentScene { get; private set; } = new MenuScene();
    public static void Update() => CurrentScene.Update();
    public static void Draw() => CurrentScene.Draw();
    public static void ChangeScene<T>() where T : IScene, new()
    {
        CurrentScene = new T();
    }
}
