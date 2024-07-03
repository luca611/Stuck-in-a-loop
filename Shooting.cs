using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Stuck_in_a_loop_challange;

/// <summary>
/// Class to handle the shooting engine of the game
/// </summary>
public static class Shooting
{
    //------------------------------VARIABLES--------------------------------------
    /// <summary>
    /// <c>List[Projectile]</c> List of all the active projectiles
    /// </summary>
    private static readonly List<Projectile> ActiveProjectiles = [];

    /// <summary>
    /// <c>bool</c> Variable to toggle the visibility of the reload text
    /// </summary>
    private static bool _reloadTextVisible = true;

    /// <summary>
    /// <c>double</c> Last state of the toggle 1 means that the text is visible, 0 means that the text is not visible
    /// </summary>
    private static double _lastToggleTime;

    /// <summary>
    /// <c>const</c> <c>double</c> Interval between toggling the text
    /// </summary>
    private const double ToggleInterval = 0.5;

    /// <summary>
    /// <c> double</c> Time when the last shot was fired
    /// </summary>
    private static double _lastShotTime;

    /// <summary>
    /// <c>const</c> <c>double</c> Delay between shots in seconds
    /// </summary>
    private const double ShotDelay = 0.35;

    /// <summary>
    /// <c>int</c> Ammo count
    /// </summary>
    private static int _ammo = 10;

    //-----------------------------------CODE--------------------------------------

    /// <summary>
    /// check if the player is shooting and in that case handle the shooting
    /// </summary>
    /// <param name="player"><c>Vector2</c> player (it only uses its position)</param>
    public static void HandleShooting(Vector2 player)
    {
        if (!IsPlayerShooting() || !(GetTime() - _lastShotTime >= ShotDelay)) return;
        Shoot(player);
        _lastShotTime = GetTime();
    }

    /// <summary>
    /// Detect if the player is shooting
    /// </summary>
    /// <returns><c>bool</c> true if the player is shooting false otherwise</returns>
    private static bool IsPlayerShooting()
    {
        return IsKeyDown(KeyboardKey.One) || IsMouseButtonDown(MouseButton.Left);
    }

    /// <summary>
    /// create a new projectile and add it to the list of active projectiles
    /// </summary>
    /// <param name="player"> <c>Vector2</c> Player (uses only its position) </param>
    private static void Shoot(Vector2 player)
    {
        //---update the ammo count---
        if (_ammo == 0) return; // no ammo? no shoot :( 
        _ammo--; // Decrease ammo count (remove = inf ammo)

        //----giving the direction to the projectile----
        var projectileSpeed = Movement.Direction == 0 ? new Vector2(10, 0) : new Vector2(-10, 0);

        //---for now all the projectile has the same size but if I'll add gun more I'll change this---
        var projectileSize = new Rectangle(player.X, player.Y, 5, 5);
        ActiveProjectiles.Add(new Projectile(player, projectileSpeed, projectileSize, 200));
    }

    /// <summary>
    /// reload the gun (literally what the name says wow)
    /// </summary>
    private static void Reload()
    {
        _ammo = 10;
    }

    /// <summary>
    /// check if the player wants to reload the gun (yes it's that simple)
    /// </summary>
    private static void CheckReload()
    {
        if (IsKeyPressed(KeyboardKey.R)) Reload();
    }

    /// <summary>
    /// Draw the reload text (ik what ur thinking and yes I made a method for drawing a text,I like to keep things clean)
    /// </summary>
    private static void DrawReloadText()
    {
        var reloadText = "Reload [R]";
        var textWidth = MeasureText(reloadText, 20);
        var centerX = BasicWindow.ScreenWidth / 2 - textWidth / 2;
        var centerY = BasicWindow.ScreenHeight - (BasicWindow.ScreenHeight - 100);
        DrawText(reloadText, centerX, centerY, 20, Color.White);
    }
    
    /// <summary>
    /// function to check if the bullet has hit a general enemy (and if so apply the damage)
    /// </summary>
    /// <param name="bullet"><c>Projectile</c> bullet to check</param>
    /// <returns></returns>
    private static bool HasShootedAnEnemy(Projectile bullet)
    {
        foreach (var enemy in EnemyEngine.ActiveEnemies.Where(enemy => CheckEnemyHit(bullet, enemy)))
        {
            enemy.GetHit();
            return true;
        }
        return false;
    }
    
    /// <summary>
    /// method to check if the bullet has hit a specific enemy
    /// </summary>
    /// <param name="bullet"> <c>Projectile</c> bullet to check</param>
    /// <param name="badGuy"> <c>Enemy</c> Enemy to check</param>
    /// <returns><c>bool</c> true if it was hit false otherwise</returns>
    private static bool CheckEnemyHit(Projectile bullet, Enemy badGuy)
    {
        if(bullet.CurrentProjectileScene != badGuy.EnemyScene) return false; // Check if the projectile is in the same scene as the enemy (if not don't bother checking for collision)
        // if it is in the same scene check if the bullet is hitting the enemy
        return CheckCollisionRecs(bullet.Size, new Rectangle(badGuy.Position.X, badGuy.Position.Y, Enemy.Size.Width, Enemy.Size.Height));
    }

    /// <summary>
    /// Draw the projectiles and the reload text if needed
    /// </summary>
    public static void Draw()
    {
        //update and draw all the active projectiles (if they are in the same scene as the current scene)
        for (int i = ActiveProjectiles.Count - 1; i >= 0; i--)
        {
            ActiveProjectiles[i] = Scenes.UpdateBulletPosition(ActiveProjectiles[i]);
            var projectile = ActiveProjectiles[i];

            if (projectile.CurrentProjectileScene == Scenes.CurrentScene)
                DrawRectangleRec(projectile.Size, Color.Black);

            if (HasShootedAnEnemy(projectile)) 
                ActiveProjectiles.RemoveAt(i);
            
            else if (projectile.Update()) 
                ActiveProjectiles.RemoveAt(i);
            
        }

        //---check if the player is fine---
        if (_ammo != 0)
        {
            //don't want to see the text if the player has ammo
            _reloadTextVisible = false;
            //no need to do anything else if the player has ammo
            return;
        }

        //---reload text and actual reload---

        //timing the blink text 
        if (GetTime() - _lastToggleTime > ToggleInterval)
        {
            _reloadTextVisible = !_reloadTextVisible; // Toggle visibility
            _lastToggleTime = GetTime(); // Update last time it was toggled
        }

        //drawing the text if the circumstances allows it
        if (_reloadTextVisible) DrawReloadText();

        //check if the player wants to reload
        CheckReload();
    }
}