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
    
    //-----------------------------------CODE--------------------------------------
    /*
     
     * in: player to move and the floor to apply the physic (I don't want the ppl to fall in the hell, even if it would be fun ngl)
     * out: updated position on the screen
     */
    
    /// <summary>
    /// update the player movement if pressing any key (right, left, space bar or shift for speed)
    /// </summary>
    /// <returns> <c>Vector2</c> Updated player position</returns>
    public static Vector2 UpdateMovement()
    {   
        //----------------x movement--------------------------------
        if (IsKeyDown(KeyboardKey.Right) || IsKeyDown(KeyboardKey.D))
        {
            Direction = 0;
            _playerSpeed.X = IsKeyDown(KeyboardKey.LeftShift) ? +6.0f : +4.0f;
        }
        else if(IsKeyDown(KeyboardKey.Left) || IsKeyDown(KeyboardKey.A))
        {
            Direction = 1;
            _playerSpeed.X = IsKeyDown(KeyboardKey.LeftShift) ? -6.0f : -4.0f;
        }
        else _playerSpeed.X = 0;
        
        
        //----------------y movement--------------------------------
        if (IsPlayerJumping()) _playerSpeed.Y = -10; // apply leg force (better start training)
        
        //----------------unwanted forces----------------------------
        _playerSpeed.Y += Gravity; // apply gravity (no space gravity?😰) 
        
        //----------------update player position---------------------
        BasicWindow.Player += _playerSpeed;
        
        //----------------basic floor collision----------------------
        if (!CheckCollisionCircleRec(BasicWindow.Player, 50, BasicWindow.Floor)) return BasicWindow.Player;
        
        _playerSpeed.Y = 0; // Stop vertical velocity
        BasicWindow.Player.Y = BasicWindow.Floor .Y - 50; // Correct position

        return BasicWindow.Player;
    }
    
    /// <summary>
    /// Check if the player is jumping (pressing the space bar) and if the player is on the floor (no midair jump)
    /// </summary>
    /// <returns><c>bool</c>true if jumping false if not</returns>
    private static bool IsPlayerJumping()
    {
        return IsKeyPressed(KeyboardKey.Space) && CheckCollisionCircleRec(BasicWindow.Player, 50, BasicWindow.Floor);
    }
}





