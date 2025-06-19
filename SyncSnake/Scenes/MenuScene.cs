using Raylib_cs;
using GameTool;

public class MenuScene : IScene
{
    private readonly List<IButton> lButtons = [];
    private readonly ButtonsManager BM;
    private readonly ScenesManager SM;
    public MenuScene()
    {
        BM = ServiceLocator.GetService<ButtonsManager>();
        SM = ServiceLocator.GetService<ScenesManager>();
        InitButtons();
    }

    private void InitButtons()
    {
        List<string> labels =
        [
            "Play",
            "Credits",
            "Quit"
        ];
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
            if (labels[buttonIndex].Equals("Play"))
                lButtons.Add(new NeonButton(labels[buttonIndex], currentButtonBounds, "Pink", SM.ChangeScene<TutorialScene>));
            else if (labels[buttonIndex].Equals("Credits"))
                lButtons.Add(new NeonButton(labels[buttonIndex], currentButtonBounds, "Pink", SM.ChangeScene<CreditsScene>));
            else
                lButtons.Add(new NeonButton(labels[buttonIndex], currentButtonBounds, "Blue", SM.ShouldClose));
        }
    }
    public void Draw()
    {
        BM.Draw(lButtons);
    }

    public void Update()
    {
        BM.Update(lButtons);
    }
}
