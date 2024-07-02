using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Stuck_in_a_loop_challange;

public static class Shooting
{
    public static readonly List<Projectile> ActiveProjectiles = new List<Projectile>(); //list of all the active projectiles
    private static double _lastShotTime = 0; // Time when the last shot was fired
    private static double _shotDelay = 0.4; // Delay between shots in seconds
    
    /*
     * Check if the player is shooting and handle the shooting
     * in: player position
     * out: none
     */
    public static void HandleShooting(Vector2 player)
    {
        if(IsPlayerShooting() && GetTime() - _lastShotTime >= _shotDelay) 
        {
            Shoot(player);
            _lastShotTime = GetTime();
        }
    }
    
    /*
     * Detect if the player is shooting
     * in: none
     * out: bool true if the player is shooting false otherwise
     */
    private static bool IsPlayerShooting()
    {
        if (IsKeyDown(KeyboardKey.One) || IsMouseButtonDown(MouseButton.Left)) return true;
        return false; 
    }
    
    /*
     * Create a new projectile and add it to the list of active projectiles
     * in: player position
     * out: none
     */
    private static void Shoot(Vector2 player)
    {
        Vector2 projectileSpeed;
        //----giving the direcvtion to the projectile----
        if (Movement.Direction == 0)
        {
            projectileSpeed = new Vector2(10, 0);
        }
        else
        {
            projectileSpeed = new Vector2(-10, 0);
        }
        
        //---for now all the projectle has the same size but if i'll add gun more I'll change this---
        Rectangle projectileSize = new Rectangle(player.X, player.Y, 5, 5);
        ActiveProjectiles.Add(new Projectile(player, projectileSpeed, projectileSize,100));
    }
    
    /*
     * Draw all the active projectiles
     * in: none
     * out: none
     */
    public static void Draw()
    {
        for (int i = ActiveProjectiles.Count - 1; i >= 0; i--)
        {
            var projectile = ActiveProjectiles[i];
            DrawRectangleRec(projectile.Size, Raylib_cs.Color.Black);
            if (projectile.Update())
            {
                ActiveProjectiles.RemoveAt(i); // Remove the projectile if Update() returned true
            }
        }
    }
}