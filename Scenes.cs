using System.Numerics;
using Raylib_cs;
using the_hospital;
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
    public static Vector2 UpdateScene(Vector2 player, Rectangle floor)
    {
        if (player.X > BasicWindow.ScreenWidth) // If the player is out of the screen (right)
        {
            CurrentScene = (CurrentScene + 1) % SceneList.Length; // Switch to the next scene
            player.X = 0;                                         // Reset player position
        }
        else if (player.X < 0) // If the player is out of the screen (left)
        {
            CurrentScene = (CurrentScene - 1 + SceneList.Length) % SceneList.Length; // Switch to the previous scene
            player.X = BasicWindow.ScreenWidth;                                      // Reset player position
        }
        
        return player;
    }
    
    /*
     * Update the bullet position if the bullet is out of the screen
     * in: the bullet (pew pew)
     * out: updated bullet position
     */
    public static Projectile UpdateBulletPosition(Projectile bullet)
    {
        if (bullet.Position.X > BasicWindow.ScreenWidth)
        {
            bullet.CurrentProjectileScene = (bullet.CurrentProjectileScene + 1) % SceneList.Length;
            bullet.Position = bullet.Position with { X = 0 };                               // Reset position to the start of the next scene
        }
        else if (bullet.Position.X < 0)
        {
            bullet.CurrentProjectileScene = (bullet.CurrentProjectileScene - 1 + SceneList.Length) % SceneList.Length;
            bullet.Position = bullet.Position with { X = BasicWindow.ScreenWidth };         // Reset position to the end of the previous scene
        }
        //---the visibility is handled outside of this function (the projectle was coming out of the roof otherwise, don't know why yet)---
        return bullet;
    }
}