using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Stuck_in_a_loop_challange
{
    /// <summary>
    /// Class for the projectiles
    /// </summary>
    public class Projectile
    {
        //---------------------------------VARIABLES-------------------------------------
        /// <summary>
        /// <c>Vector2</c> position on the screen of the projectile
        /// </summary>
        public Vector2 Position { get; set; }
        
        /// <summary>
        /// <c>Vector2</c> speed of the projectile
        /// </summary>
        private Vector2 Speed { get; set; }
        
        /// <summary>
        /// <c>Rectangle</c> size of the projectile
        /// </summary>
        public Rectangle Size { get; private set; } 
        
        /// <summary>
        /// <c>int</c> scene where the projectile is
        /// </summary>
        public int CurrentProjectileScene { get; set; } 
        
        /// <summary>
        /// <c>float</c> distance travelled by the projectile
        /// </summary>
        private float DistanceTravelled { get; set; } 
        
        /// <summary>
        /// <c>float</c> amount of px before gravity starts to apply to the projectile
        /// </summary>
        private float GravityResistence { get; set; } 
        
        //---------------------------------CODE-------------------------------------
        
        /// <summary>
        /// Constructor of the projectile
        /// </summary>
        /// <param name="position"><c>Vector2</c> Starting position of the projectile</param>
        /// <param name="speed"><c>Vector2</c> Speed of the projectile</param>
        /// <param name="size"><c>Rectangle</c> Size of the projectile</param>
        /// <param name="gravityResistence"><c>float</c> Gravity resistence of the projectile</param>
        public Projectile(Vector2 position, Vector2 speed, Rectangle size, float gravityResistence)
        {
            Position = position;
            Speed = speed;
            Size = size;
            DistanceTravelled = 0;
            GravityResistence = gravityResistence;
            CurrentProjectileScene = Scenes.CurrentScene;
        }
        
        /// <summary>
        /// Update the projectile position
        /// </summary>
        /// <returns><c>bool</c> true if the projectile should be removed false if not</returns>
        public bool Update()
        {
            if(UiComponents.IsPaused) return false; // Don't update if the game is paused (to avoid the projectile moving while the game is paused
            //--update the position of the projectile--
            Position += Speed;
            DistanceTravelled += Speed.Length();

            // Start falling after travelling it's gravity resistence
            if(DistanceTravelled > GravityResistence)
            {
                Speed = Speed with { Y = 1.0f }; // Gravity force
            }

            //--refreshing position of the projectile--
            if (IsHittingGround())
            {
                return true; // Indicate that the projectile should be removed
            }
            Size = Size with { X = Position.X, Y = Position.Y };

            return false;    // Indicate that the projectile should not be removed
        }
        
        /// <summary>
        /// Check if the projectile is hitting the ground (what did you expect from the name?)
        /// </summary>
        /// <returns><c>bool</c> true if the projectile is hitting the ground false if not</returns>
        private bool IsHittingGround()
        {
            return CheckCollisionRecs(Size, BasicWindow.Floor);
        }
    }
}