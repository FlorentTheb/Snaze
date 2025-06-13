using System;
public static class SceneManager
{
    public static IScene CurrentScene { get; private set; } = new Game();
    public static void Update() => CurrentScene.Update();
    public static void Draw() => CurrentScene.Draw();
    public static void ChangeScene<T>() where T : IScene, new()
    {
        CurrentScene = new T();
    }
}
