﻿using System.Numerics;
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
    
    private float _immunityTimeLeft; // Immunity time left in seconds

    public Texture2D PlayerTexture;
    
    private static int _frames = 0;
    private const int ShootingDuration = 45; // Duration in frames
    private const int WalkingFrames = 30;
    private const int RunningFrames = 15;
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
        PlayerTexture = LoadTexture("./resources/Player/IdleR.png");
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
        else PlayerTexture = LoadTexture(Movement.Direction == 0 ? "./resources/Player/IdleR.png" : "./resources/Player/IdleL.png");
        

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
        if (_frames <30) PlayerTexture = LoadTexture(Movement.Direction == 0 ? "./resources/Player/ShootingRF.png" : "./resources/Player/ShootingLF.png");
        else PlayerTexture = LoadTexture(Movement.Direction == 0 ? "./resources/Player/ShootingR.png" : "./resources/Player/ShootingL.png");

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
        if (_frames < RunningFrames/2) PlayerTexture = LoadTexture(Movement.Direction == 0 ? "./resources/Player/RunningR.png" : "./resources/Player/RunningL.png");
        else PlayerTexture = LoadTexture(Movement.Direction == 0 ? "./resources/Player/IdleR.png" : "./resources/Player/IdleL.png");
        if (_frames >= RunningFrames) _frames = 0;
    }
    
    /// <summary>
    /// Updates the walking animation 
    /// </summary>
    private void UpdateWalkingAnimation()
    {
        if (!Movement.IsWalking) return;
        
        _frames++;
        if (_frames < WalkingFrames/2) PlayerTexture = LoadTexture(Movement.Direction == 0 ? "./resources/Player/runningR.png" : "./resources/Player/RunningL.png");
        else PlayerTexture = LoadTexture(Movement.Direction == 0 ? "./resources/Player/IdleR.png" : "./resources/Player/IdleL.png");
            
        if (_frames >= WalkingFrames) _frames = 0;
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
        var healthText = $"Health: {Health}";
        var fontSize = 20; 
        var paddingRight = 10; 
        var paddingTop = 10; 
        var textWidth = MeasureText(healthText, fontSize);
        var textX = BasicWindow.ScreenWidth - textWidth - paddingRight;
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

        DrawHeath();
    }
}