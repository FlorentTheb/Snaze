using System;
using GameTool;
using Raylib_cs;

public class CreditsScene : IScene
{
    private readonly List<IButton> lButtons = [];
    private readonly ButtonsManager BM;
    private readonly ScenesManager SM;
    public CreditsScene()
    {
        BM = ServiceLocator.GetService<ButtonsManager>();
        SM = ServiceLocator.GetService<ScenesManager>();
        lButtons.Add(new NeonButton("Menu", new Rectangle(Screen.GetWidth() / 2 - 50, Screen.GetHeight() - 200, 100, 50), "Pink", SM.ChangeScene<MenuScene>));
    }
    public void Draw()
    {
        Raylib.DrawText("Game made by Flowfi", Screen.GetWidth() / 2 - 320, 200, 80, Color.White);
        Raylib.DrawText("Check out my other games :\n\nhttps://flowfi.itch.io", Screen.GetWidth() / 2 - 250, 400, 40, Color.White);
        BM.Draw(lButtons);
    }

    public void Update()
    {
        BM.Update(lButtons);
    }
}
