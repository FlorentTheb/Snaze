using System;
public class ScenesManager
{
    private IScene CurrentScene;
    public ScenesManager()
    {
        ServiceLocator.Register(this);
        CurrentScene = new MenuScene();
    }
    public void Update() => CurrentScene.Update();
    public void Draw() => CurrentScene.Draw();
    public void ChangeScene<T>() where T : IScene, new()
    {
        CurrentScene = new T();
    }
}
