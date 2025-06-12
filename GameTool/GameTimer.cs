using Raylib_cs;
using static Raylib_cs.Raylib;

namespace GameTool;

public static class GameTimer
{
    public static float TimerLimit { get; private set; } = 0f;
    public static bool IsRunning { get; private set; } = false;
    public static float TimeElapsed { get; private set; }
    public static float GetDeltaFrame() => GetFrameTime();
    public static void Set(float limit)
    {
        IsRunning = true;
        TimeElapsed = 0f;
        TimerLimit = limit;
        Console.WriteLine($"Time left => {(int)TimerLimit} seconds");
    }

    public static void Pause() => IsRunning = false;
    public static void Resume()
    {
        if (TimerLimit == 0f)
            throw new InvalidOperationException($"Timer method 'Set' was not called => '{nameof(TimerLimit)}' is NULL");
        IsRunning = true;
    }

    public static void Stop()
    {
        IsRunning = false;
        TimeElapsed = 0f;
        TimerLimit = 0f;
    }

    public static void Reset()
    {
        IsRunning = true;
        TimeElapsed = 0;
    }

    public static bool Update()
    {
        if (TimeElapsed >= TimerLimit)
        {
            IsRunning = false;
            return true;
        }
        TimeElapsed += GetDeltaFrame();
        return false;
    }

    public static float GetRemainingTime()
    {
        if (TimeElapsed < TimerLimit)
            return TimerLimit - TimeElapsed;
        else return 0f;
    }
}
