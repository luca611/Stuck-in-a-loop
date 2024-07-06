using System.Numerics;
using Raylib_cs;
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
    private int Health { get; set; }
    
    /// <summary>
    /// <c>float</c> Movement speed of the enemy
    /// </summary>
    private float MovementSpeed { get; }
    
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
    private bool _enemySummoned;
    
    /// <summary>
    /// <c>bool</c> Flag to check if the enemy is alive
    /// </summary>
    public bool IsAlive = true;
    
    /// <summary>
    /// <c>int</c> Scene where the enemy is
    /// </summary>
    public int EnemyScene; 
    
    /// <summary>
    /// <c>int</c> Direction of the enemy
    /// </summary>
    private int _direction;
    
    /// <summary>
    /// <c>int</c> Frame of the animation
    /// </summary>
    private int _animationFrame;
    
    //-----------------------------------CODE--------------------------------------

    /// <summary>
    /// constructor of the enemy
    /// </summary>
    /// <param name="health"><c>int</c> health of the enemy</param>
    /// <param name="movementSpeed"><c>float</c> speed of the enemy </param>
    /// <param name="enemyScene"><c>int</c> starting scene of the enemy</param>
    public Enemy( int health, float movementSpeed, int enemyScene)
    {
        var random = new Random();
        float randomY = random.Next(255, 270);
        Position =  new Vector2 (0, randomY);
        Health = health;
        MovementSpeed = movementSpeed;
        EnemyScene = enemyScene;
    }
    
    /// <summary>
    /// Spawns the enemy at a random position
    /// </summary>
    public void SummonEnemy(Vector2 playerPosition)
    {
        //---check if the enemy is already summoned---
        if (_enemySummoned) return;
        
        //---randomize the position of the enemy---
        var random = new Random();
        float randomX = random.Next(0, GameWindow.ScreenWidth);
        
        if (Math.Abs(randomX - playerPosition.X) < 100)
        {
            randomX += 100 * (randomX < playerPosition.X ? -1 : 1);
            randomX = Math.Clamp(randomX, 0, GameWindow.ScreenWidth - Enemy.Size.Width);
        }
        
        //---set the position of the enemy---
        Position = new Vector2(randomX, Position.Y);
        _enemySummoned = true;
    }
    
    /// <summary>
    /// Update the enemy position
    /// </summary>
    public void Update(Vector2 playerPosition)
    {
        //---if the enemy is in the same scene as the player move towards him---
        if (EnemyScene == Scenes.CurrentScene)
        {
            //----get the player position, so they will follow him----
            var direction = new Vector2(playerPosition.X - Position.X, 0);
        
            //----move the enemy towards the player and avoid "disappearing adding 1px of "personal space"----
            if (!(Math.Abs(direction.X) > 1)) return;
        
            direction = Vector2.Normalize(direction);
            _direction= direction.X > 0 ? 1 : -1;
            var movement = direction * MovementSpeed;
        
            //----update the position of the enemy----
            Position = new Vector2(Position.X + movement.X,Position.Y);
        }
        //---if the enemy is not in the same scene as the player move towards the first player's scene direction---
        else
        {
            if(EnemyScene<Scenes.CurrentScene || EnemyScene == Scenes.SceneList.Length) _direction = 1;
            else _direction = -1;
            
            var movement = new Vector2(_direction * MovementSpeed, 0);
            Position = new Vector2(Position.X + movement.X,Position.Y);
        }
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
        if(EnemyScene != Scenes.CurrentScene) return;
        Texture2D textureToDraw;

        // Determine the direction and select the appropriate texture for the current frame
        if (_direction > 0) textureToDraw = _animationFrame < 30 ? EnemyEngine.ZombieRight : EnemyEngine.ZombieRight2;
        else textureToDraw = _animationFrame < 30 ? EnemyEngine.ZombieLeft : EnemyEngine.ZombieLeft2;
        
        // Draw the selected texture
        DrawTexture(textureToDraw, (int)Position.X, (int)Position.Y, Color.White);
        _animationFrame++;
        if (_animationFrame >= 60) _animationFrame = 0;
    }
    
    /// <summary>
    /// method to check if the enemy has hit the player and apply the damage if so
    /// </summary>
    /// <param name="player"></param>
    public void HitPlayer(Player player)
    {
        //---check if the player is in the same scene as the enemy---
        if (EnemyScene == Scenes.CurrentScene)
        {
            //---check if the player is hit---
            if (CheckCollisionRecs(new Rectangle((int)Position.X, (int)Position.Y, (int)Enemy.Size.Width, (int)Enemy.Size.Height), new Rectangle((int)player.Position.X, (int)player.Position.Y, (int)player.Size.Width, (int)player.Size.Height)))
            {
                player.GetHit();
            }
        }
    }
    
}