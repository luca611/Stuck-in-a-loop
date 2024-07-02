using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Stuck_in_a_loop_challange;

public static class Scenes
{
    public static Color[] SceneList = new Color[] { Raylib_cs.Color.Red, Raylib_cs.Color.Brown, Raylib_cs.Color.Blue };
    public static int CurrentScene = 0;
    
    /*
     * Update the scene if the player is out of the screen
     * in: player position, the floor and the screen width
     * out: updated player position
     */
    public static Vector2 UpdateScene(Vector2 player, Rectangle floor, int screenWidth)
    {
        if (player.X > screenWidth) // If the player is out of the screen (right)
        {
            CurrentScene = (CurrentScene + 1) % SceneList.Length; // Switch to the next scene
            player.X = 0;                                         // Reset player position
        }
        else if (player.X < 0) // If the player is out of the screen (left)
        {
            CurrentScene = (CurrentScene - 1 + SceneList.Length) % SceneList.Length; // Switch to the previous scene
            player.X = screenWidth;                                                  // Reset player position
        }
        
        return player;
    }
}