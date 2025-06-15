using System;
using GameTool;
using Raylib_cs;

public class PauseScene : IScene
{
    List<IButton> lButtons = [];
    private ButtonsManager BM;
    private ScenesManager SM;
    private Rectangle Area;
    public PauseScene()
    {
        BM = ServiceLocator.GetService<ButtonsManager>();
        SM = ServiceLocator.GetService<ScenesManager>();
        Area = new Rectangle(Screen.GetWidth() / 4, 100, Screen.GetWidth() / 2, Screen.GetHeight() / 2);
        lButtons.Add(new NeonButton("Back", new Rectangle(Screen.GetWidth() / 2, Screen.GetHeight() / 2, 100, 50), "Pink", SM.PopScene));
    }
    public void Draw()
    {
        Graphics.DrawRectangle(Area, "Blue");
        BM.Draw(lButtons);
    }

    public void Update()
    {
        BM.Update(lButtons);
    }
}
