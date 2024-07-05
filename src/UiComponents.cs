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
    
    public static bool isGameOver;
    
    //-----------------------------------CODE--------------------------------------
    /// <summary>
    /// Method to toggle the pause of the game
    /// </summary>
    public static void TogglePause()
    {
        if(IsKeyPressed(KeyboardKey.P) && !isGameOver) IsPaused = !IsPaused;
    }
    
    /// <summary>
    /// Draw the pause menu of the game
    /// </summary>
    private static void DrawPauseMenu()
    {
        if(IsPaused)
        {
            DrawRectangle(0, 0, GetScreenWidth(), GetScreenHeight(), Fade(Color.Black, 0.5f));
            DrawText("PAUSED", GetScreenWidth() / 2 - MeasureText("PAUSED", 40) / 2, GetScreenHeight() / 2 - 20, 40, Color.White);
        }
    }
    
    /// <summary>
    /// Method to toggle the game over
    /// </summary>
    public static void ToggleGameOver()
    {
        isGameOver = true;
    }
    /// <summary>
    /// Draw the game over screen
    /// </summary>
    private static void DrawGameOver()
    {
        if(isGameOver)
        {
            IsPaused = true;
            DrawRectangle(0, 0, GetScreenWidth(), GetScreenHeight(), Fade(Color.Black, 0.5f));
            DrawText("GAME OVER", GetScreenWidth() / 2 - MeasureText("GAME OVER", 40) / 2, GetScreenHeight() / 2 - 20, 40, Color.White);
        }
    }
    
    /// <summary>
    /// method to draw all the UI components
    /// </summary>
    public static void DrawUiComponents()
    {
        if (isGameOver)
        {
            DrawGameOver();
            return;
        }
        DrawPauseMenu();
    }
    
}