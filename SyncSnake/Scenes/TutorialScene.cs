using GameTool;
using Raylib_cs;

public class TutorialScene : IScene
{
    private readonly ButtonsManager BM;
    private readonly ScenesManager SM;

    private int TutorialPageIndex = 0;

    private List<IButton> lButtons = [];

    private List<string> TutorialTexts = [
        "Welcome to SyncSnake\n\n\nIn this variant of the world famous game 'Snake'\n\nYou will not control 1 but 2 snakes... Simutaneously !\n\n\nLike in a classic Snake, you control the snakes in 4 directions :\n\nZ or Up Arrow : Snakes goes up\nS or Down Arrow : Snakes goes down\nQ or Left Arrow : Snakes goes left\nD or Right Arrow : Snakes goes right\n\n\nA timer will be displayed for the remaining time left to chose a direction\nIf none is pressed, the snakes will move in their default direction",
        "The rules :\n\n\nEach snake have a head, and a tail right ?\n\n\nYour goal is to position their head and tail in a specific cell :\nThe position AND the angle to match the objective cells matters\nso ... Be strategic ;)\n\nIf you succeed to do so,\nyou complete the level and go to a more difficult one :)\n\n\nTheses objective cells will have a specific image as a background,\nrecognizable as the head and the tail",
        "Each snake will have its own play area\n\n\nOf course to spice it up a bit :\n\nThe walls\n\nThe apples\n\nThe bombs\n\n\nWill be on different position among the 2 grids !",
        "About the different cells :\n\nWall : if a snake try to go towards a wall,\nit will continue in its default direction it was before the direction wished\n\nApple : Both snakes will grow an extra segment\n\nBomb : The other snake will have a segment of its body removed",
        "A restart button makes everything goes back\nas the beginning of the current level.\n\n\nYou can press Escape to open Pause Menu",
        "Good luck, and have fun!"
    ];

    public TutorialScene()
    {
        BM = ServiceLocator.GetService<ButtonsManager>();
        SM = ServiceLocator.GetService<ScenesManager>();

        Rectangle nextRect = new Rectangle(Screen.GetWidth() - 150, 50, 100, 50);
        lButtons.Add(new NeonButton("Next", nextRect, "Blue", GoNext));
    }

    public void Update()
    {
        BM.Update(lButtons);
    }

    public void Draw()
    {
        Raylib.DrawText(TutorialTexts[TutorialPageIndex], 100, 200, 30, Color.White);
        BM.Draw(lButtons);
    }

    private void GoNext()
    {
        TutorialPageIndex++;

        if (TutorialPageIndex == 1 && lButtons.Count == 1)
        {
            Rectangle backRect = new Rectangle(50, 50, 100, 50);
            lButtons.Add(new NeonButton("Back", backRect, "Pink", GoBack));
        }

        if (TutorialPageIndex >= TutorialTexts.Count - 1)
        {
            var nextButton = lButtons.OfType<NeonButton>().FirstOrDefault(b => b.Text == "Next");
            if (nextButton != null)
            {
                nextButton.Text = "Go";
                nextButton.OnClickCallback = SM.ChangeScene<GameScene>;
            }
        }
    }

    private void GoBack()
    {
        if (TutorialPageIndex > 0)
            TutorialPageIndex--;

        if (TutorialPageIndex == 0)
            lButtons.RemoveAll(b => (b as NeonButton)?.Text == "Back");

        var nextButton = lButtons.OfType<NeonButton>().FirstOrDefault(b => b.Text == "Go");
        if (nextButton != null)
        {
            nextButton.Text = "Next";
            nextButton.OnClickCallback = GoNext;
        }
    }
}