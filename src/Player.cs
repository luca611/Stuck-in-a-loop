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
    private float MovementSpeed { get; set; }
    
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
    
    /// <summary>
    /// <c>float</c> Immunity time left in seconds
    /// </summary>
    private float _immunityTimeLeft; // Immunity time left in seconds

    /// <summary>
    /// <c>Texture2D</c> Texture of the player
    /// </summary>
    public Texture2D PlayerTexture;
    
    /// <summary>
    /// <c>int</c> Frames counter for the animations
    /// </summary>
    private static int _frames = 0;
    
    /// <summary>
    /// <c>const</c> <c>int</c> Duration of the shooting animation in frames
    /// </summary>
    private const int ShootingDuration = 45; 
    
    /// <summary>
    /// <c>const</c> <c>int</c> Number of frames for the walking animation
    /// </summary>
    private const int WalkingFrames = 30;
    
    /// <summary>
    /// <c>const</c> <c>int</c> Number of frames for the running animation
    /// </summary>
    private const int RunningFrames = 15;

    public bool wasHit;
    //-----------------------------------RESOURCES--------------------------------------
    
    private readonly Texture2D _idleR = LoadTexture("./resources/Player/IdleR.png") ;
    private readonly Texture2D _idleL = LoadTexture("./resources/Player/IdleL.png") ;
    private readonly Texture2D _runningR = LoadTexture("./resources/Player/RunningR.png") ;
    private readonly Texture2D _runningL = LoadTexture("./resources/Player/RunningL.png") ;
    private readonly Texture2D _shootingR = LoadTexture("./resources/Player/ShootingR.png") ;
    private readonly Texture2D _shootingL = LoadTexture("./resources/Player/ShootingL.png") ;
    private readonly Texture2D _shootingRf = LoadTexture("./resources/Player/ShootingRF.png") ;
    private readonly Texture2D _shootingLf = LoadTexture("./resources/Player/ShootingLF.png") ;
    
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
        Size = Size with { Y = 110 };
        Health = health;
        MovementSpeed = movementSpeed;
        Position = Position with { X = startingX };
        Position = Position with { Y = startingY };
        PlayerTexture = _idleR;
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
        
        //---update the player animations---
        if (Shooting.IsShooting) UpdateShootingAnimation();
        else if(Movement.IsRunning) UpdateRunningAnimation();
        else if(Movement.IsWalking) UpdateWalkingAnimation();
        else PlayerTexture = Movement.Direction == 0 ? _idleR : _idleL;
        

        Position = Movement.UpdateMovement(this);
        Position = Scenes.UpdateScene(Position);
        Shooting.HandleShooting(this);
    }
    
    /// <summary>
    /// Updates the shooting animation
    /// </summary>
    private void UpdateShootingAnimation()
    {
        //--player might shoot multiple times in a row, so I need to reset the frames counter--
        if(Shooting.ShotReset)
        {
            _frames = 0;
            Shooting.ShotReset = false;
        }
        
        _frames++;
        if (_frames <30) PlayerTexture = Movement.Direction == 0 ? _shootingRf : _shootingLf;
        else PlayerTexture = Movement.Direction == 0 ? _shootingR : _shootingL;

        if (_frames < ShootingDuration) return; // Check if the shooting animation is over
        
        Shooting.IsShooting = false;
        _frames = 0;
    }
    
    /// <summary>
    /// Updates the running animation
    /// </summary>
    private void UpdateRunningAnimation()
    {
        if (!Movement.IsRunning) return;
        
        _frames++;
        if (_frames < RunningFrames/2) PlayerTexture = Movement.Direction == 0 ? _runningR : _runningL;
        else PlayerTexture = Movement.Direction == 0 ? _idleR : _idleL;
        if (_frames >= RunningFrames) _frames = 0;
    }
    
    /// <summary>
    /// Updates the walking animation 
    /// </summary>
    private void UpdateWalkingAnimation()
    {
        if (!Movement.IsWalking) return;
        
        _frames++;
        if (_frames < WalkingFrames/2) PlayerTexture = Movement.Direction == 0 ? _runningR : _runningL;
        else PlayerTexture = Movement.Direction == 0 ? _idleR : _idleL;
            
        if (_frames >= WalkingFrames) _frames = 0;
    }
    
    /// <summary>
    /// Method to handle player damaging
    /// </summary>
    public void GetHit()
    {
        if (!(_immunityTimeLeft <= 0)) return; // Check if the player is immune
        
        wasHit = true;
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
        var healthText = $"Health: {Health}";
        var fontSize = 20; 
        var paddingRight = 10; 
        var paddingTop = 10; 
        var textWidth = MeasureText(healthText, fontSize);
        var textX = GameWindow.ScreenWidth - textWidth - paddingRight;
        var textY = paddingTop;

        DrawText(healthText, textX, textY, fontSize, Color.White);
    }
    
    /// <summary>
    /// method to draw the player difficulty
    /// </summary>
    private void DrawDifficulty()
    {
        var healthText = $"Difficulty: {(int)EnemyEngine.Difficulty}";
        var fontSize = 20; 
        var paddingTop = 10; 
        var textWidth = MeasureText(healthText, fontSize);
        var textX = GameWindow.ScreenWidth/2 - textWidth/2;
        var textY = paddingTop;

        DrawText(healthText, textX, textY, fontSize, Color.White);
    }

    /// <summary>
    /// Draw the killed enemies on the screen
    /// </summary>
    private static void DrawKilledEnemies()
    {
        var healthText = $"Killed Enemy: {EnemyEngine.KilledEnemies}";
        var fontSize = 20; 
        var paddingRight = 10; 
        var paddingTop = 10; 
        var textWidth = MeasureText(healthText, fontSize);
        var textX = paddingRight;
        var textY = paddingTop;

        DrawText(healthText, textX, textY, fontSize, Color.White);
    }
    
    /// <summary>
    /// Draw the player on the screen
    /// </summary>
    public void DrawPlayer()
    {
        // Draw the image at the player's position
        DrawTexture(PlayerTexture, (int)Position.X, (int)Position.Y, Color.White);
        DrawKilledEnemies();
        DrawDifficulty();
        DrawHeath();
    }
}