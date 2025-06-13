using System;
using Raylib_cs;
using GameTool;
public class MenuScene : IScene
{
    private ButtonsManager BM;
    private ScenesManager SM;
    private List<IButton> ButtonList = [];
    public MenuScene()
    {
        BM = ServiceLocator.GetService<ButtonsManager>();
        SM = ServiceLocator.GetService<ScenesManager>();
        List<string> labelList = new()
        {
            "Play",
            "Tutorial",
            "Credits"
        };
        InitButtons(labelList);
    }

    private void InitButtons(List<String> labels)
    {
        int nbButtons = labels.Count;
        for (int buttonIndex = 0; buttonIndex < nbButtons; buttonIndex++)
        {
            int posX = Screen.GetWidth() / 2;
            int posY = Screen.GetHeight() / nbButtons * buttonIndex + Screen.GetHeight() / nbButtons / 2;
            int labelHeight = 50;
            int labelWidth = Graphics.GetTextWidth(labels[buttonIndex], labelHeight);
            int buttonWidth = (int)((float)labelWidth * 1.5f);
            int buttonHeight = (int)((float)labelHeight * 1.5f);
            Rectangle currentButtonBounds = new Rectangle(posX - buttonWidth / 2, posY - buttonHeight / 2, buttonWidth, buttonHeight);
            ButtonList.Add(new RoundedButton(labels[buttonIndex], currentButtonBounds, OnClick));
        }
    }
    public void Draw()
    {
        foreach (var button in ButtonList)
        {
            button.Draw();
        }
    }

    public void Update()
    {

        foreach (var button in ButtonList)
        {
            button.Update();
        }
    }

    public void OnClick()
    {
        SM.ChangeScene<GameScene>();
    }
}
