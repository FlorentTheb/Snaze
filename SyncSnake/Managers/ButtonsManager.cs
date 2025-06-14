using System;
using Raylib_cs;
public class ButtonsManager
{
    private List<IButton> ButtonList = [];
    public ButtonsManager()
    {
        ServiceLocator.Register(this);
    }

    public void RegisterButtons(List<IButton> buttonList)
    {
        Clear();
        ButtonList = buttonList;
    }

    public void Clear()
    {
        ButtonList.Clear();
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
        bool isOneButtonHovered = false;
        foreach (var button in ButtonList)
        {
            if (button.Update())
            {
                button.OnClick();
                return;
            }

            isOneButtonHovered = button.IsHovered || isOneButtonHovered;
        }

        if (isOneButtonHovered)
            Raylib.SetMouseCursor(MouseCursor.PointingHand);
        else
            Raylib.SetMouseCursor(MouseCursor.Arrow);
    }
}
