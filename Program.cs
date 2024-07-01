using System.Numerics;
using System;
using System.Text;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Examples.Core
{
    public class BasicWindow
    {
        public static int Main()
        {
            const int screenWidth = 800;
            const int screenHeight = 450;

            InitWindow(screenWidth, screenHeight, "The hospital");

            SetTargetFPS(60);

            Vector2 ballPosition = new((float)screenWidth / 2, (float)screenHeight / 2);
            Vector2 ballVelocity = new Vector2(0, 0);
            float gravity = 0.5f;

            // Define the floor
            Rectangle floor = new Rectangle(0, screenHeight - 100, screenWidth, 100);

            // Define the scenes
            Color[] scenes = new Color[] { Raylib_cs.Color.Red, Raylib_cs.Color.Green, Raylib_cs.Color.Blue };
            int currentScene = 0;

            while (!WindowShouldClose())
            {
                if (IsKeyDown(Raylib_cs.KeyboardKey.Right) || IsKeyDown(KeyboardKey.D))
                {
                    ballVelocity.X = IsKeyDown(KeyboardKey.LeftShift) || IsKeyDown(KeyboardKey.RightShift) ? 6.0f : 4.0f; // Set constant speed to the right
                }
                else if (IsKeyDown(Raylib_cs.KeyboardKey.Left) || IsKeyDown(KeyboardKey.A))
                {
                    ballVelocity.X = IsKeyDown(KeyboardKey.LeftShift) || IsKeyDown(KeyboardKey.RightShift) ? -6.0f : -4.0f; // Set constant speed to the left
                }
                else
                {
                    ballVelocity.X = 0; // Stop moving when no key is pressed
                }

                // Apply gravity
                ballVelocity.Y += gravity;

                // Update ball position
                ballPosition += ballVelocity;

                // Collision detection with the floor
                if (CheckCollisionCircleRec(ballPosition, 50, floor))
                {
                    ballVelocity.Y = 0; // Stop vertical velocity
                    ballPosition.Y = floor.Y - 50; // Correct position
                }

                // Jumping
                if (IsKeyPressed(KeyboardKey.Space) && CheckCollisionCircleRec(ballPosition, 50, floor))
                {
                    ballVelocity.Y = -15; // Apply an upward force
                }

                // Scene switching
                if (ballPosition.X > screenWidth)
                {
                    currentScene = (currentScene + 1) % scenes.Length; // Switch to the next scene
                    ballPosition.X = 0; // Reset ball position
                }
                else if (ballPosition.X < 0)
                {
                    currentScene = (currentScene - 1 + scenes.Length) % scenes.Length; // Switch to the previous scene
                    ballPosition.X = screenWidth; // Reset ball position
                }

                BeginDrawing();
                ClearBackground(scenes[currentScene]);

                DrawText("move the ball with arrow keys", 10, 10, 20, Raylib_cs.Color.DarkGray);

                // Draw the floor
                DrawRectangleRec(floor, Raylib_cs.Color.Green);

                DrawCircleV(ballPosition, 50, Raylib_cs.Color.Maroon);

                EndDrawing();
            }

            CloseWindow();

            return 0;
        }
    }
}