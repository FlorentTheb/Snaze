using interfaces;

namespace services.managers;

public class ScenesManager
{
    private readonly Stack<IScene> SceneStack = new();

    public bool WillClose { get; private set; } = false;
    public ScenesManager()
    {
        ServiceLocator.Register(this);
    }

    public void PushScene<T>() where T : IScene, new()
    {
        SceneStack.Push(new T());
    }

    public void PopScene()
    {
        if (SceneStack.Count == 0)
            throw new InvalidOperationException("Scene stack is empty. Cannot pop scene.");

        SceneStack.Pop();
    }
    public void ClearScenes()
    {
        SceneStack.Clear();
    }

    public void Update()
    {
        IScene? CurrentScene = SceneStack.Count > 0 ? SceneStack.Peek() : null;
        CurrentScene?.Update();
    }
    public void Draw()
    {
        foreach (var scene in SceneStack.Reverse())
        {
            scene.Draw();
        }
    }
    public void ChangeScene<T>() where T : IScene, new()
    {
        ClearScenes();
        PushScene<T>();
    }

    public void ShouldClose()
    {
        WillClose = true;
    }
}
