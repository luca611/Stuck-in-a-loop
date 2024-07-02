using System.Numerics;
using Raylib_cs;
using the_hospital;
using static Raylib_cs.Raylib;

namespace Stuck_in_a_loop_challange;

public static class Shooting
{
    public static readonly List<Projectile>
        ActiveProjectiles = new List<Projectile>(); //list of all the active projectiles
    
    private static bool _reloadTextVisible = true; 
    private static double _lastToggleTime = 0; //if 1 means that the text is visible, if 0 means that the text is not visible
    private const double ToggleInterval = 0.5; 


    private static double _lastShotTime = 0; // Time when the last shot was fired
    private static double _shotDelay = 0.35; // Delay between shots in seconds
    private static int Ammo = 10;

    /*
     * Check if the player is shooting and handle the shooting
     * in: player position
     * out: none
     */
    public static void HandleShooting(Vector2 player)
    {
        if (IsPlayerShooting() && GetTime() - _lastShotTime >= _shotDelay)
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
        //---update the ammo count---
        if(Ammo == 0) return; // no ammo? no shoot :( 
        Ammo--;               // Decrease ammo count, remove this if u like to play with infinite ammo
        Vector2 projectileSpeed;
        
        //----giving the direction to the projectile----
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
        ActiveProjectiles.Add(new Projectile(player, projectileSpeed, projectileSize, 100));
    }
    
    /*
     * Reload the gun, i mean, what did you expect?
     * in: none
     * out: none
     */
    private static void Reload()
    {
        Ammo = 10;
    }
    
    /*
     * Check if the player wants to reload the gun really easy, yeah i did a method for it and what?
     * in: none
     * out: none
     */
    private static void CheckReload()
    {
        if (IsKeyPressed(KeyboardKey.R))
        {
            Reload();
        }
    }
    
    /*
     * Draw the reload text in the middle of the screem, (ik ik i could have done it in the main draw function but i wanted to keep it clean)
     * in: none
     * out: none
     */
    private static void DrawReloadText()
    {
        string reloadText = "Reload [R]";
        int textWidth = MeasureText(reloadText, 20);
        int centerX = BasicWindow.ScreenWidth / 2 - textWidth / 2;
        int centerY = BasicWindow.ScreenHeight - (BasicWindow.ScreenHeight - 100);
        DrawText(reloadText, centerX, centerY, 20, Color.White);
    }
    
    /*
     * Draw all the active projectiles (if they are in the same scene as the current scene) and update them
     * in: none
     * out: none
     */
    public static void Draw()
    {
        //update and draw all the active projectiles (if they are in the same scene as the current scene)
        for (int i = ActiveProjectiles.Count - 1; i >= 0; i--)
        {
            ActiveProjectiles[i] = Scenes.UpdateBulletPosition(ActiveProjectiles[i]);
            var projectile = ActiveProjectiles[i];

            if (projectile.CurrentProjectileScene == Scenes.CurrentScene)
            {
                DrawRectangleRec(projectile.Size, Raylib_cs.Color.Black);
            }

            if (projectile.Update())
            {
                ActiveProjectiles.RemoveAt(i);
            }
        }
    
        //---reload text and actual reload---
        if (Ammo == 0)
        {
            //timing the blink text 
            if (GetTime() - _lastToggleTime > ToggleInterval)
            {
                _reloadTextVisible = !_reloadTextVisible; // Toggle visibility
                _lastToggleTime = GetTime(); // Update last time it was toggled
            }
            
            //drawing the text if the circumstances allows it
            if (_reloadTextVisible)
            {
                DrawReloadText();
            }
        }
        else
        {
            //don't want to see the text if the player has ammo
            _reloadTextVisible = false;
        }

        CheckReload();
    }
}