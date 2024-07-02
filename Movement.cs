using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Stuck_in_a_loop_challange;

public static class Movement
{
    private static Vector2 _playerSpeed = new Vector2(0, 0);
    private const float Gravity = 0.5f; //change this value to change the gravity force

    public static int Direction = 0;    // 0 = right, 1 = left
    /*
     * Update the player movement if pressing any key (right, left, space bar or shift for speed)
     * in: player to move and the floor to apply the physic (i don't want the ppl to fall in the hell, even if it would be fun ngl)
     * out: updated position on the screen
     */
    public static Vector2 UpdateMovement(Vector2 player , Rectangle floor)
    {   
        //----------------x movement--------------------------------
        if (IsKeyDown(KeyboardKey.Right) || IsKeyDown(KeyboardKey.D))
        {
            Direction = 0;
        }
        else if(IsKeyDown(KeyboardKey.Left) || IsKeyDown(KeyboardKey.A))
        {
            Direction = 1;
            _playerSpeed.X = IsKeyDown(KeyboardKey.LeftShift) ? -6.0f : -4.0f;
        }
        else
        {
            _playerSpeed.X = 0;
        }
        
        //----------------y movement--------------------------------
        if (IsPlayerJumping(player, floor)) _playerSpeed.Y = -10; // apply leg force (better start training)
        
        //----------------unwanted forces----------------------------
        _playerSpeed.Y += Gravity; // apply gravity (no space gravity?😰) 
        
        //----------------update player position---------------------
        player += _playerSpeed;
        
        //----------------basic floor collision----------------------
        if (CheckCollisionCircleRec(player, 50, floor))
        {
            _playerSpeed.Y = 0; // Stop vertical velocity
            player.Y = floor.Y - 50; // Correct position
        }

        return player;
    }
    
    /*
     * it does what you would expect checks if ur jumping (pressinf the space bar) (and if the player is on the floor no double jump)
     * in: player position and the floor to check if the player is on the floor
     * out: true if the player is jumping, false otherwise
     */
    private static bool IsPlayerJumping(Vector2 player,Rectangle floor)
    {
        if (IsKeyPressed(KeyboardKey.Space) && CheckCollisionCircleRec(player, 50, floor)) return true;
        return false;
    }


}





