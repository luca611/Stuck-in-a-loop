using System;
using System.Numerics;
using Raylib_cs;
using the_hospital;
using static Raylib_cs.Raylib;

namespace Stuck_in_a_loop_challange;

public class Enemy
{
    public int Health { get; private set; }
    public float MovementSpeed { get; private set; }
    public Vector2 Position { get;  set; }
    public static Rectangle Size { get; } = new Rectangle(0, 0, 50, 100); // All enemies share the same size
    private static bool _enemySummoned = false; 
    
    public Enemy( int health, float movementSpeed)
    {
        Position =  new Vector2 (0, 250);
        Health = health;
        MovementSpeed = movementSpeed;
    }
    
    /*
     * Summon the enemy at a random x position 
     * in: none
     * out: the enemy at a random position
     */
    public void SummonEnemy()
    {
        if (!_enemySummoned)
        {
            Random random = new Random();

            float randomX = random.Next(0, BasicWindow.ScreenWidth);
            if (Math.Abs(randomX - BasicWindow.Player.X) < 100)
            {
                randomX += 100 * (randomX < BasicWindow.Player.X ? -1 : 1);
                randomX = Math.Clamp(randomX, 0, BasicWindow.ScreenWidth - Enemy.Size.Width);
            }
            
            Position = new Vector2(randomX, 250);
            _enemySummoned = true;
        }
    }
    
    /*
     * Update the enemy position
     * in: none
     * out: the updated position of the enemy
     */
    public void Update()
    {
        //----get the player position like so they will follow him----
        Vector2 direction = new Vector2(BasicWindow.Player.X - Position.X, 0);
        
        //----move the enemy towards the player and avoid "disappearing adding 1px of "personal space"----
        if (Math.Abs(direction.X) > 1) 
        {
            direction = Vector2.Normalize(direction);
            Vector2 movement = direction * MovementSpeed;
            Position = new Vector2(Position.X + movement.X, Position.Y);
        }
    }
    
    // Draw the enemy: probably gonna change it later
    public void Draw()
    {
        DrawRectangleRec(new Rectangle(Position.X, Position.Y, Enemy.Size.Width, Enemy.Size.Height), new Color(0, 100, 0, 255)); // Dark green
    }
    
    /*
     * #TODO: Add a method to check if the enemy is hit by a projectile and update the health (in case despawn the enemy)
     * #TODO: Add a method to check if the enemy is hitting the player and update the player health (in case despawn the player)
     */
    
}