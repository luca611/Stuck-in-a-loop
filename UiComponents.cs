using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Stuck_in_a_loop_challange;

/// <summary>
/// class to handle the UI components and the pause menu
/// </summary>
public static class UiComponents
{
    //------------------------------VARIABLES--------------------------------------
    /// <summary>
    /// <c>bool</c> Flag to check if the game is paused
    /// </summary>
    public static bool IsPaused;
    
    //-----------------------------------CODE--------------------------------------
    /// <summary>
    /// Method to toggle the pause of the game
    /// </summary>
    public static void TogglePause()
    {
        if(IsKeyPressed(KeyboardKey.P)) IsPaused = !IsPaused;
    }
    
    /// <summary>
    /// Draw the pause menu of the game
    /// </summary>
    public static void DrawPauseMenu()
    {
        if(IsPaused)
        {
            DrawRectangle(0, 0, GetScreenWidth(), GetScreenHeight(), Fade(Color.Black, 0.5f));
            DrawText("PAUSED", GetScreenWidth() / 2 - MeasureText("PAUSED", 40) / 2, GetScreenHeight() / 2 - 20, 40, Color.White);
        }
    }
}