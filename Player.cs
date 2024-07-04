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
    public Rectangle Size { get; set; } = new(0, 0, 50, 90); // All players share the same size
    
    /// <summary>
    /// <c>bool</c> Flag to check if the player is alive
    /// </summary>
    public bool IsAlive = true;
    
    //-----------------------------------CODE--------------------------------------

    /// <summary>
    /// constructor of the player
    /// </summary>
    /// <param name="health"><c>int</c> health of the player</param>
    /// <param name="movementSpeed"><c>float</c> speed of the player </param>
    /// <param name="startingX"><c>int</c> Starting x position of the player</param>
    /// <param name="startingY"><c>int</c> Starting Y position of the player</param>
    public Player( int health, float movementSpeed, int startingX, int startingY)
    {
        Position =  new Vector2 (0, 250);
        Health = health;
        MovementSpeed = movementSpeed;
        Position = Position with { X = startingX };
        Position = Position with { Y = startingY };
    }

    /// <summary>
    /// Update the player (Movement, scene, shooting)
    /// </summary>
    public void UpdatePlayer()
    {
        Position = Movement.UpdateMovement(this);
        Position = Scenes.UpdateScene(Position);
        Shooting.HandleShooting(Position);
    }
    
    public void GetHit()
    {
        Health--;
        if (Health <= 0)
        {
            IsAlive = false;
        }
    }
    
    /// <summary>
    /// Draw the player on the screen
    /// </summary>
    public void DrawPlayer()
    {
        DrawRectangle((int)Position.X, (int)Position.Y, (int)Size.Width, (int)Size.Height,Color.Maroon);
    }
}