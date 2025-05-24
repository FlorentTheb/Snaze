using Raylib_cs;
using static Raylib_cs.Raylib;

namespace ToolKit;

public static class GameTimer
{
    public static float? TimerLimit { get; private set; } = null;
    public static bool IsRunning { get; private set; } = false;
    public static float TimeElapsed { get; private set; }
    public static float OneSecondTreshold { get; private set; }
    public static float GetDeltaFrame() => GetFrameTime();
    public static void Set(float limit)
    {
        IsRunning = true;
        TimeElapsed = 0f;
        OneSecondTreshold = 0f;
        TimerLimit = limit;
        Console.WriteLine($"Time left => {(int)TimerLimit} seconds");
    }

    public static void Pause() => IsRunning = false;
    public static void Resume()
    {
        if (TimerLimit is null)
            throw new InvalidOperationException($"Timer method 'Set' was not called => '{nameof(TimerLimit)}' is NULL");
        IsRunning = true;
    }

    public static void Stop()
    {
        IsRunning = false;
        TimeElapsed = 0f;
        TimerLimit = null;
    }

    public static bool Update()
    {
        if (!IsRunning || TimerLimit is null)
            return false;


        if (TimeElapsed >= TimerLimit)
        {
            IsRunning = false;
            return true;
        }
        if (OneSecondTreshold >= 1)
        {
            OneSecondTreshold = 0;
            Console.WriteLine($"Time left => {(int)TimerLimit - (int)TimeElapsed} seconds");
        }
        TimeElapsed += GetDeltaFrame();
        OneSecondTreshold += GetDeltaFrame();
        return false;
    }
}
