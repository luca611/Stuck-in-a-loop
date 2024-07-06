using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Stuck_in_a_loop_challange;

/// <summary>
/// class to handle the player movement
/// </summary>
public static class Movement
{
    //------------------------------VARIABLES--------------------------------------
    /// <summary>
    /// <c>Vector2</c> Speed of the player
    /// </summary>
    private static Vector2 _playerSpeed = new Vector2(0, 0);
    
    /// <summary>
    /// <c>float</c> Gravity force applied to the player
    /// </summary>
    private const float Gravity = 0.5f; 
    
    /// <summary>
    /// <c>int</c> Direction of the player (0 = right, 1 = left)
    /// </summary>
    public static int Direction;
    
    public static bool IsJumping;
    
    public static bool IsRunning;
    
    public static bool IsWalking;
    //-----------------------------------CODE--------------------------------------
    /*
     
     * in: player to move and the floor to apply the physic (I don't want the ppl to fall in the hell, even if it would be fun ngl)
     * out: updated position on the screen
     */
    
    /// <summary>
    /// update the player movement if pressing any key (right, left, space bar or shift for speed)
    /// </summary>
    /// <returns> <c>Vector2</c> Updated player position</returns>
    public static Vector2 UpdateMovement(Player player)
    {
        if (Shooting.IsShooting) return player.Position;
        //--reset flags--
        IsRunning = false;
        IsWalking = false;
        //----------------x movement--------------------------------
        if (IsKeyDown(KeyboardKey.Right) || IsKeyDown(KeyboardKey.D))
        {
            IsWalking = true;
            Direction = 0;
            IsRunning = IsKeyDown(KeyboardKey.LeftShift);
            _playerSpeed.X = IsRunning ? +6.0f : +4.0f;
        }
        else if(IsKeyDown(KeyboardKey.Left) || IsKeyDown(KeyboardKey.A))
        {
            IsWalking = true;
            Direction = 1;
            IsRunning = IsKeyDown(KeyboardKey.LeftShift);
            _playerSpeed.X = IsRunning ? -6.0f: -4.0f;
        }
        else _playerSpeed.X = 0;
        
        //----------------y movement--------------------------------
        if (IsPlayerJumping(player)) _playerSpeed.Y = -10; // apply leg force (better start training)
        
        //----------------unwanted forces----------------------------
        _playerSpeed.Y += Gravity; // apply gravity (no space gravity?😰) 
        
        //----------------update player position---------------------
        player.Position += _playerSpeed;
        
        //----------------basic floor collision----------------------
        var floorHitbox = new Rectangle(GameWindow.Floor.X, GameWindow.Floor.Y+12, GameWindow.Floor.Width, GameWindow.Floor.Height);
        if (!CheckCollisionRecs(new Rectangle(player.Position.X, player.Position.Y, 
                player.Size.X, player.Size.Y),floorHitbox)) return player.Position;
        IsJumping = false;
        _playerSpeed.Y = 0; // Stop vertical velocity
        player.Position = player.Position with { Y = floorHitbox.Y-player.Size.Y}; 

        return player.Position;
    }
    
    /// <summary>
    /// Check if the player is jumping (pressing the space bar) and if the player is on the floor (no midair jump)
    /// </summary>
    /// <returns><c>bool</c>true if jumping false if not</returns>
    private static bool IsPlayerJumping(Player player)
    {
        if (!IsKeyPressed(KeyboardKey.Space) || CheckCollisionRecs(new Rectangle(player.Position.X,
                player.Position.Y + (player.Size.Y / 2)+10,
                player.Size.X, player.Size.Y+10), GameWindow.Floor) != true) return false;
        IsJumping = true;
        return true;
    }
}





