using System;
using System.Numerics;
using Raylib_cs;
using the_hospital;
using static Raylib_cs.Raylib;

namespace Stuck_in_a_loop_challange
{
    public class Projectile
    {
        public Vector2 Position { get; set; }          //position on the screen of the projectile
        public Vector2 Speed { get; set; }             //speed of the projectile
        public Rectangle Size { get; set; }            //size of the projectile
        private float DistanceTravelled { get; set; } //total distance travelled by the projectile
        private float GravityResistence { get; set; } //amount of px before gravity hits the projectile

        /*
         * Create a new projectile
         * in: position of the projectile, speed of the projectile and the size of the projectile
         * out: the projectile
         */
        public Projectile(Vector2 position, Vector2 speed, Rectangle size, float gravityResistence)
        {
            Position = position;
            Speed = speed;
            Size = size;
            DistanceTravelled = 0;
            GravityResistence = gravityResistence;
        }

        /*
         * Update the projectile position
         * in: none
         * out: the updated size of the projectile
         */
        public bool Update()
        {
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

            return false; // Indicate that the projectile should not be removed
        }
        
        /*
         * does it need explenation?
         * in: none
         * out: bool true if the projectile is hitting the ground false otherwise
         */
        private bool IsHittingGround()
        {
            return CheckCollisionRecs(Size, BasicWindow.Floor);
        }
    }
}