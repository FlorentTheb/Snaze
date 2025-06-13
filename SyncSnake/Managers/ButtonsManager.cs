using System;
public class ButtonsManager
{
    private List<IButton> ButtonList = [];
    public ButtonsManager()
    {
        ServiceLocator.Register(this);
    }

    public void RegisterButtons(List<IButton> buttonList)
    {
        ButtonList = buttonList;
    }

    public void Update()
    {

    }
}
