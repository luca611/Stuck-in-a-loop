using System.Numerics;
using Raylib_cs;
using the_hospital;
using static Raylib_cs.Raylib;

namespace Stuck_in_a_loop_challange;

/// <summary>
/// class to handle the single enemy action 
/// </summary>
public class Enemy
{
    //------------------------------VARIABLES--------------------------------------
    
    /// <summary>
    /// <c>int</c> Health of the enemy 
    /// </summary>
    public int Health { get; private set; }
    
    /// <summary>
    /// <c>float</c> Movement speed of the enemy
    /// </summary>
    public float MovementSpeed { get; }
    
    /// <summary>
    /// <c>Vector2</c> Position of the enemy
    /// </summary>
    public Vector2 Position { get;  set; }
    
    /// <summary>
    /// <c>Rectangle</c> Size of the enemy
    /// </summary>
    public static Rectangle Size { get; } = new Rectangle(0, 0, 50, 100); // All enemies share the same size
    
    /// <summary>
    /// <c>bool</c> Flag to check if the enemy is summoned to avoid multiple generations
    /// </summary>
    private bool _enemySummoned = false;
    
    /// <summary>
    /// <c>bool</c> Flag to check if the enemy is alive
    /// </summary>
    public bool IsAlive = true;
    
    //-----------------------------------CODE--------------------------------------
    
    /// <summary>
    /// constructor of the enemy
    /// </summary>
    /// <param name="health"><c>int</c> health of the enemy</param>
    /// <param name="movementSpeed"><c>float</c> speed of the enemy </param>
    public Enemy( int health, float movementSpeed)
    {
        Position =  new Vector2 (0, 250);
        Health = health;
        MovementSpeed = movementSpeed;
    }
    
    /// <summary>
    /// Spawns the enemy at a random position
    /// </summary>
    public void SummonEnemy()
    {
        //---check if the enemy is already summoned---
        if (_enemySummoned) return;
        
        //---randomize the position of the enemy---
        var random = new Random();
        float randomX = random.Next(0, BasicWindow.ScreenWidth);
        
        if (Math.Abs(randomX - BasicWindow.Player.X) < 100)
        {
            randomX += 100 * (randomX < BasicWindow.Player.X ? -1 : 1);
            randomX = Math.Clamp(randomX, 0, BasicWindow.ScreenWidth - Enemy.Size.Width);
        }
        
        //---set the position of the enemy---
        Position = new Vector2(randomX, 250);
        _enemySummoned = true;
    }
    
    /// <summary>
    /// Update the enemy position
    /// </summary>
    public void Update()
    {
        //----get the player position like so they will follow him----
        var direction = new Vector2(BasicWindow.Player.X - Position.X, 0);
        
        //----move the enemy towards the player and avoid "disappearing adding 1px of "personal space"----
        if (!(Math.Abs(direction.X) > 1)) return;
        
        direction = Vector2.Normalize(direction);
        var movement = direction * MovementSpeed;
        
        //----update the position of the enemy----
        Position = new Vector2(Position.X + movement.X, Position.Y);
    }
    
    /// <summary>
    /// Function to get the enemy hit (this it's gonna hurt him a lot)
    /// </summary>
    public void GetHit()
    {
        Health--;
        //---if the enemy is dead set the flag to false---
        if (Health <= 0) IsAlive = false;
    }
    
    /// <summary>
    /// draw the enemy on the screen if he's alive
    /// </summary>
    public void Draw()
    {
        if(IsAlive) DrawRectangleRec(new Rectangle(Position.X, Position.Y, Enemy.Size.Width, Enemy.Size.Height), new Color(0, 100, 0, 255)); // Dark green
    }
    
    /*
     * #TODO: Add a method to check if the enemy is hitting the player and update the player health (in case despawn the player)
     */
    
}