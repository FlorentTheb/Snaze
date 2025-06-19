using GameTool;
using Raylib_cs;
using utils.buttons;
using interfaces;
using services;
using services.managers;

namespace scenes;

public class VictoryScene : IScene
{
    private readonly List<IButton> lButtons = [];
    private readonly ButtonsManager BM;
    private readonly ScenesManager SM;
    public VictoryScene()
    {
        BM = ServiceLocator.GetService<ButtonsManager>();
        SM = ServiceLocator.GetService<ScenesManager>();
        lButtons.Add(new NeonButton("Menu", new Rectangle(Screen.GetWidth() / 2 - 50, Screen.GetHeight() - 200, 100, 50), "Pink", SM.ChangeScene<MenuScene>));
    }
    public void Draw()
    {
        Raylib.DrawText("Victory", Screen.GetWidth() / 2 - 170, 200, 100, Color.White);
        Raylib.DrawText("More levels incoming ...", Screen.GetWidth() / 2 - 150, 400, 30, Color.White);
        BM.Draw(lButtons);
    }

    public void Update()
    {
        BM.Update(lButtons);
    }
}
