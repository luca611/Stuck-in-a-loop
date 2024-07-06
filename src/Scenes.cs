using System.Numerics;
using Raylib_cs;

namespace Stuck_in_a_loop_challange;

/// <summary>
/// Class to handle the scenes
/// </summary>
public static class Scenes
{
    //------------------------------VARIABLES--------------------------------------
    /// <summary>
    /// <c>Color[]</c> Array of colors representing the scenes
    /// </summary>
    public static readonly Color[] SceneList = new Color[] { Color.Red, Color.Brown, Color.Blue };
    
    /// <summary>
    /// <c>int</c> Index of the current scene (default is 0)
    /// </summary>
    public static int CurrentScene;
    
    //-----------------------------------CODE--------------------------------------
    
    /// <summary>
    /// Update the scene if the player is out of the screen
    /// </summary>
    /// <param name="player"><c>Vector2</c> instance of the player</param>
    /// <returns><c>Vector2</c> updated position of the player (positioned at the sides of the screen)</returns>
    public static Vector2 UpdateScene(Vector2 player)
    {
        if (player.X > GameWindow.ScreenWidth) // If the player is out of the screen (right)
        {
            CurrentScene = (CurrentScene + 1) % SceneList.Length; // Switch to the next scene
            player.X = 0;                                         // Reset player position
        }
        else if (player.X < 0) // If the player is out of the screen (left)
        {
            CurrentScene = (CurrentScene - 1 + SceneList.Length) % SceneList.Length; // Switch to the previous scene
            player.X = GameWindow.ScreenWidth;                                      // Reset player position
        }
        
        return player;
    }
    
    /// <summary>
    /// Update the bullet position if the bullet is out of the screen
    /// </summary>
    /// <param name="bullet"><c>Projectile</c> the bullet instance to update</param>
    /// <returns><c>Projectile</c> the updated bullet</returns>
    public static Projectile UpdateBulletPosition(Projectile bullet)
    {
        switch (bullet.Position.X)
        {
            case > GameWindow.ScreenWidth:
                bullet.CurrentProjectileScene = (bullet.CurrentProjectileScene + 1) % SceneList.Length;
                bullet.Position = bullet.Position with { X = 0 };                               // Reset position to the start of the next scene
                break;
            case < 0:
                bullet.CurrentProjectileScene = (bullet.CurrentProjectileScene - 1 + SceneList.Length) % SceneList.Length;
                bullet.Position = bullet.Position with { X = GameWindow.ScreenWidth };         // Reset position to the end of the previous scene
                break;
        }

        //---the visibility is handled outside of this function---
        return bullet;
    }
    
    /// <summary>
    /// Update the enemy position if the enemy is out of the screen
    /// </summary>
    /// <param name="badGuy"><c>Enemy</c> the enemy instance to update</param>
    /// <returns><c>Enemy</c> the updated Enemy</returns>
    public static Enemy UpdateEnemyPosition(Enemy badGuy)
    {
        switch (badGuy.Position.X)
        {
            case > GameWindow.ScreenWidth:
                badGuy.EnemyScene = (badGuy.EnemyScene + 1) % SceneList.Length;
                badGuy.Position = badGuy.Position with { X = 0 };                               // Reset position to the start of the next scene
                break;
            case < 0:
                badGuy.EnemyScene = (badGuy.EnemyScene - 1 + SceneList.Length) % SceneList.Length;
                badGuy.Position = badGuy.Position with { X = GameWindow.ScreenWidth };         // Reset position to the end of the previous scene
                break;
        }

        //---the visibility is handled outside of this function---
        return badGuy;
    }
    
}