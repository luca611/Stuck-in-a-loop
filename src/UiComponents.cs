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

    /// <summary>
    /// <c>bool</c> Flag to check if the game is over
    /// </summary>
    private static bool _isGameOver;

    /// <summary>
    /// <c>int</c> Number of frames  for the overlay where the player has been hit
    /// </summary>
    private static int _hitFrames;

    //--------------------------------RESOURCES-------------------------------------

    private static readonly Texture2D OverlayPlayerHitted = LoadTexture("./resources/Ui/HittedOverlay.png");

    private static readonly Texture2D OverlayPlayerHitted2 = LoadTexture("./resources/Ui/HittedOverlay2.png");

    //-----------------------------------CODE--------------------------------------
    /// <summary>
    /// Method to toggle the pause of the game
    /// </summary>
    public static void TogglePause()
    {
        if (IsKeyPressed(KeyboardKey.P) && !_isGameOver) IsPaused = !IsPaused;
    }

    /// <summary>
    /// Draw the pause menu of the game
    /// </summary>
    private static void DrawPauseMenu()
    {
        if (!IsPaused) return;

        DrawRectangle(0, 0, GetScreenWidth(), GetScreenHeight(), Fade(Color.Black, 0.5f));
        DrawText("PAUSED", GetScreenWidth() / 2 - MeasureText("PAUSED", 40) / 2, GetScreenHeight() / 2 - 20, 40,
            Color.White);
    }

    /// <summary>
    /// Method to toggle the game over
    /// </summary>
    public static void ToggleGameOver()
    {
        _isGameOver = true;
    }

    /// <summary>
    /// Draw the game over screen
    /// </summary>
    private static void DrawGameOver()
    {
        if (!_isGameOver) return;

        IsPaused = true;
        DrawRectangle(0, 0, GetScreenWidth(), GetScreenHeight(), Fade(Color.Black, 0.5f));
        DrawText("GAME OVER", GetScreenWidth() / 2 - MeasureText("GAME OVER", 40) / 2, GetScreenHeight() / 2 - 20, 40,
            Color.White);
    }

    /// <summary>
    /// method to draw all the UI components
    /// </summary>
    public static void DrawUiComponents()
    {
        if (_isGameOver)
        {
            DrawGameOver();
            return;
        }

        DrawPauseMenu();
    }

    /// <summary>
    /// Method to draw the overlay when the player has been hit
    /// </summary>
    /// <param name="player"> <c>Player</c> player to check if it has been hit</param>
    public static void DrawHitOverlay(Player player)
    {
        if (!player.wasHit) return;

        switch (_hitFrames)
        {
            case >= 0 and < 15:
                // Render small overlay
                DrawTexture(OverlayPlayerHitted2, 0, 0, Color.White);
                break;
            case >= 15 and < 45:
                // Render big overlay
                DrawTexture(OverlayPlayerHitted, 0, 0, Color.White);
                break;
            case >= 45 and < 60:
                // Render small overlay again
                DrawTexture(OverlayPlayerHitted2, 0, 0, Color.White);
                break;
            default:
                // Reset conditions
                player.wasHit = false;
                _hitFrames = 0;
                return;
        }

        _hitFrames++;
    }
}