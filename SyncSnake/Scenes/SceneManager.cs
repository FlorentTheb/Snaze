using System;
public static class SceneManager
{
    public static IScene Scene { get; private set; } = new Game();
    public static void Update() => Scene.Update();
    public static void Draw() => Scene.Draw();
    public static void ChangeScene(IScene scene) => Scene = scene;
}
