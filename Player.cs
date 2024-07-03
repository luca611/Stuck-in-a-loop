using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Stuck_in_a_loop_challange;

public class Player
{
    //------------------------------VARIABLES--------------------------------------
    
    /// <summary>
    /// <c>int</c> Health of the player
    /// </summary>
    private int Health { get; set; }
    
    /// <summary>
    /// <c>float</c> Movement speed of the player
    /// </summary>
    private float MovementSpeed { get; }
    
    /// <summary>
    /// <c>Vector2</c> Position of the player
    /// </summary>
    public Vector2 Position { get;  set; }
    
    /// <summary>
    /// <c>Rectangle</c> Size of the player
    /// </summary>
    public static Rectangle Size { get; } = new Rectangle(0, 0, 50, 100); // All players share the same size
    
    /// <summary>
    /// <c>bool</c> Flag to check if the player is alive
    /// </summary>
    public bool IsAlive = true;
    
    private int _direction; // Direction of the player
    
    //-----------------------------------CODE--------------------------------------

    /// <summary>
    /// constructor of the player
    /// </summary>
    /// <param name="health"><c>int</c> health of the player</param>
    /// <param name="movementSpeed"><c>float</c> speed of the player </param>
    /// <param name="playerScene"><c>int</c> starting scene of the player</param>
    public Player( int health, float movementSpeed)
    {
        Position =  new Vector2 (0, 250);
        Health = health;
        MovementSpeed = movementSpeed;
    }
}