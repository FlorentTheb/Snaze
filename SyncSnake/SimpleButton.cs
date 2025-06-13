using System;
using System.Numerics;
using Raylib_cs;

public abstract class SimpleButton : IButton
{
    public Rectangle Bounds { get; protected set; }
    public string Text { get; protected set; } = "";

    public Color DefaultColor { get; protected set; }
    public Color HoverColor { get; protected set; }
    public Color PressedColor { get; protected set; }

    public bool IsHovered { get; protected set; } = false;
    public bool IsPressed { get; protected set; } = false;

    public Action OnClickCallback { get; protected set; }

    public SimpleButton(string text, Rectangle bounds, Action action)
    {
        Text = text;
        Bounds = bounds;
        OnClickCallback = action;
    }
    public abstract void Draw();

    public virtual void OnClick()
    {
        OnClickCallback?.Invoke();
    }

    public virtual void Update()
    {
        Vector2 mousePos = Raylib.GetMousePosition();
        IsHovered = Raylib.CheckCollisionPointRec(mousePos, Bounds);

        if (!IsHovered)
            IsPressed = false;

        if (IsHovered && Raylib.IsMouseButtonPressed(MouseButton.Left))
            IsPressed = true;

        if (IsPressed)
            Console.WriteLine("PRESSED IN");

        if (IsPressed && Raylib.IsMouseButtonReleased(MouseButton.Left))
        {
            OnClick();
            IsPressed = false;
        }


        if (!Raylib.IsMouseButtonDown(MouseButton.Left))
            IsPressed = false;

    }
}
