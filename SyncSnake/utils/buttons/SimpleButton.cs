using System;
using System.Numerics;
using Raylib_cs;

public abstract class SimpleButton : IButton
{
    protected Rectangle Box;

    protected Color DefaultColor;
    protected Color HoverColor;
    protected Color PressedColor;

    public bool IsHovered { get; protected set; } = false;
    public bool IsPressed { get; protected set; } = false;
    public bool IsClicked { get; protected set; } = false;

    public Action OnClickCallback;

    public SimpleButton(Rectangle box, Action action)
    {
        Box = box;
        OnClickCallback = action;
    }
    public abstract void Draw();

    public virtual void OnClick()
    {
        OnClickCallback?.Invoke();
    }

    public virtual bool Update()
    {
        Vector2 mousePos = Raylib.GetMousePosition();

        IsHovered = Raylib.CheckCollisionPointRec(mousePos, Box);

        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            IsPressed = IsHovered;


        if (Raylib.IsMouseButtonReleased(MouseButton.Left))
        {
            IsClicked = IsPressed && IsHovered;
            IsPressed = false;
        }
        else IsClicked = false;

        return IsClicked;
    }
}
