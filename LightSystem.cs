using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace Stuck_in_a_loop_challange;

public class LightSystem
{
    public static bool IsBrakeOn = true;

    public static Vector2 Position;
    public static int BrakeScene;
    private static Random random = new Random();
    private static double _lastToggleTime = GetTime();
    private static double ToggleInterval = 25;

    private static bool _textVisible = true;
    private static double _lastToggleTextTime = GetTime();
    private static double _toggleInterval = 0.5; // Blink every 0.5 seconds

    public LightSystem()
    {
        Position = new Vector2(random.Next(0, BasicWindow.ScreenWidth),
            BasicWindow.ScreenHeight - 190); // Random X, fixed Y
        BrakeScene = random.Next(0, Scenes.SceneList.Length); // Random scene selection
    }

    public static void checkTunOn()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.B) && !IsBrakeOn)
        {
            _lastToggleTime = GetTime();
            IsBrakeOn = true;
            EnemyEngine.Difficulty = 1;
        }
    }

    private static void toggleBrake()
    {
        if (!IsBrakeOn) return;
        if (GetTime() - _lastToggleTime > ToggleInterval) IsBrakeOn = false;
    }


    public static void UpdateLightSystem()
    {
        toggleBrake();
        checkTunOn();
        if (!IsBrakeOn) EnemyEngine.Difficulty += (int)(2 * _lastToggleTime / 5);
    }

    public static void drawBrake()
    {
        if (BrakeScene == Scenes.CurrentScene) DrawRectangle((int)Position.X, (int)Position.Y, 50, 50, Color.Gold);
    }

    public static void DrawLights()
    {
        if (IsBrakeOn) return;

        Color overlayColor = new Color(80, 80, 80, 150);
        DrawRectangle(0, 0, BasicWindow.ScreenWidth, BasicWindow.ScreenHeight, overlayColor);
    }

    public static void UpdateAndDrawWarningText()
    {
        if (!IsBrakeOn)
        {
            if (GetTime() - _lastToggleTextTime > _toggleInterval)
            {
                _textVisible = !_textVisible;
                _lastToggleTextTime = GetTime();
            }

            if (_textVisible)
            {
                var warningText = "Warning: Brake is off!\n";
                var textWidth = MeasureText(warningText, 20);
                var centerX = BasicWindow.ScreenWidth / 2 - textWidth / 2;
                var centerY = BasicWindow.ScreenHeight - 300;
                DrawText(warningText, centerX, centerY, 20, Color.Red);
            }
        }
    }
}