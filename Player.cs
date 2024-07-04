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
    
    private float _immunityTimeLeft; // Immunity time left in seconds
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
        Size = Size with { X = 50 };
        Size = Size with { Y = 90 };
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
        if (_immunityTimeLeft > 0)
        {
            _immunityTimeLeft -= GetFrameTime(); // Decrease immunity time by the frame time
        }
        
        Position = Movement.UpdateMovement(this);
        Position = Scenes.UpdateScene(Position);
        Shooting.HandleShooting(Position);
    }
    
    /// <summary>
    /// Method to handle player damaging
    /// </summary>
    public void GetHit()
    {
        if (!(_immunityTimeLeft <= 0)) return; // Check if the player is immune
        
        Health--;
        _immunityTimeLeft = 5; // Set immunity time to 5 seconds
        if (Health > 0) return;// Check if the player is still alive
        
        IsAlive = false;
        UiComponents.ToggleGameOver();
    }
    
    /// <summary>
    /// method to draw the player health
    /// </summary>
    private void DrawHeath()
    {
        string healthText = $"Health: {Health}";
        int fontSize = 20; 
        int paddingRight = 10; 
        int paddingTop = 10; 
        int textWidth = MeasureText(healthText, fontSize);
        int textX = BasicWindow.ScreenWidth - textWidth - paddingRight;
        int textY = paddingTop;

        DrawText(healthText, textX, textY, fontSize, Color.White);
    }
    
    /// <summary>
    /// Draw the player on the screen
    /// </summary>
    public void DrawPlayer()
    {
        DrawRectangle((int)Position.X, (int)Position.Y, (int)Size.Width, (int)Size.Height,Color.Maroon);
        DrawHeath();
    }
}