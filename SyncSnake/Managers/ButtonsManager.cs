using System;
public class ButtonsManager
{
    private List<IButton> BList = [];
    public ButtonsManager()
    {
        ServiceLocator.Register(this);
    }

    public void RegisterButtons(List<IButton> buttonsList)
    {
        BList = buttonsList;
    }

    public void Update()
    {
        
    }
}
