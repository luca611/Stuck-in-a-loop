using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;

namespace Stuck_in_a_loop_challange;

/// <summary>
/// class to handle the light system (brake)
/// </summary>
public class LightSystem
{
    //------------------------------VARIABLES--------------------------------------
    
    /// <summary>
    /// <c>bool</c> Flag to check if the brake is on
    /// </summary>
    private static bool _isBrakeOn = true;

    /// <summary>
    /// <c>Vector2</c> position of the brake
    /// </summary>
    private static Vector2 _position;
    
    /// <summary>
    /// <c>int</c> Scene where the brake is
    /// </summary>
    private static int _brakeScene;
    
    /// <summary>
    /// <c>Random</c> Random object to generate random numbers
    /// </summary>
    private static readonly Random Random = new Random();
    
    /// <summary>
    /// <c>double</c> Last time when the brake was toggled
    /// </summary>
    private static double _lastToggleTime = GetTime();
    
    /// <summary>
    /// <c>const</c> <c>double</c> Interval between toggling the brake
    /// </summary>
    private static readonly double ToggleInterval = 25;
    
    /// <summary>
    /// <c>bool</c> Variable to toggle the visibility of the warning text
    /// </summary>
    private static bool _textVisible = true;
    
    /// <summary>
    /// <c>double</c> Last time when the brake warning text was toggled
    /// </summary>
    private static double _lastToggleTextTime = GetTime();
    
    /// <summary>
    /// <c>const</c> <c>double</c> Interval between toggling the warning text
    /// </summary>
    private static double _toggleInterval = 0.5; // Blink every 0.5 seconds

    //-----------------------------------CODE--------------------------------------
    
    /// <summary>
    /// Constructor of the light system
    /// </summary>
    public LightSystem()
    {
        _position = new Vector2(Random.Next(0, BasicWindow.ScreenWidth), BasicWindow.ScreenHeight - 190); // Random X, fixed Y
        _brakeScene = Random.Next(0, Scenes.SceneList.Length); // Random scene selection
    }
    
    /// <summary>
    /// Method to check if the brake is turned on
    /// </summary>
    public static void checkTunOn()
    {
        //---if the brake is already on or the key is not pressed return---
        if (!IsKeyPressed(KeyboardKey.B) || _isBrakeOn) return; 
        //--reset variables--
        _lastToggleTime = GetTime();
        _isBrakeOn = true;
        //---reset the difficulty---
        EnemyEngine.Difficulty = 1;
    }
    
    /// <summary>
    /// Method to toggle the brake after a time interval
    /// </summary>
    private static void ToggleBrake()
    {
        if (!_isBrakeOn) return; //the brake was alr off
        if (GetTime() - _lastToggleTime > ToggleInterval) _isBrakeOn = false;
    }


    /// <summary>
    /// Method to update the light system
    /// </summary>
    public static void UpdateLightSystem()
    {
        ToggleBrake();
        checkTunOn();
        if (!_isBrakeOn) EnemyEngine.Difficulty += (int)(2 * _lastToggleTime / 5);
    }
    
    /// <summary>
    /// Method to draw the brake
    /// </summary>
    public static void DrawBrake()
    {
        if (_brakeScene == Scenes.CurrentScene) DrawRectangle((int)_position.X, (int)_position.Y, 50, 50, Color.Gold);
    }
    
    /// <summary>
    /// Method to draw the lights
    /// </summary>
    public static void DrawLights()
    {
        if (_isBrakeOn) return;//if the brake is on don't draw the lights
        Color overlayColor = new Color(80, 80, 80, 150);
        DrawRectangle(0, 0, BasicWindow.ScreenWidth, BasicWindow.ScreenHeight, overlayColor);
    }
    
    /// <summary>
    /// Method to update and draw the warning text
    /// </summary>
    public static void UpdateAndDrawWarningText()
    {
        if (_isBrakeOn) return; //no need to draw the text if the brake is on
        //--blink the text--
        if (GetTime() - _lastToggleTextTime > _toggleInterval)
        {
            _textVisible = !_textVisible;
            _lastToggleTextTime = GetTime();
        }

        if (!_textVisible) return;
        //--draw the text--
        var warningText = "Warning: Brake is off!\n"; //could be a const, but it's only used here sooo 
        var textWidth = MeasureText(warningText, 20);
        var centerX = BasicWindow.ScreenWidth / 2 - textWidth / 2;
        var centerY = BasicWindow.ScreenHeight - 300;
        DrawText(warningText, centerX, centerY, 20, Color.Red);
    }
}