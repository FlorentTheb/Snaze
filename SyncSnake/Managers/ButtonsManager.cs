using Raylib_cs;
public class ButtonsManager
{
    public ButtonsManager()
    {
        ServiceLocator.Register(this);
    }

    public void Update(List<IButton> buttons)
    {
        bool isOneButtonHovered = false;

        foreach (var button in buttons)
        {
            if (button.Update())
            {
                button.OnClick();
                return;
            }
            isOneButtonHovered |= button.IsHovered;
        }

        Raylib.SetMouseCursor(isOneButtonHovered ? MouseCursor.PointingHand : MouseCursor.Arrow);
    }

    public void Draw(List<IButton> buttons)
    {
        foreach (var button in buttons)
            button.Draw();
    }
}
