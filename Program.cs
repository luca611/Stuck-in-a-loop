using System.Numerics;
using System;
using System.Text;
using Raylib_cs;
using Stuck_in_a_loop_challange;
using static Raylib_cs.Raylib;

namespace the_hospital
{
    public class BasicWindow
    {
        public static int Main()
        {
            //------screen settings---------
            const int screenWidth = 800;
            const int screenHeight = 450;

            InitWindow(screenWidth, screenHeight, "The hospital");
            SetTargetFPS(60); // ⚠ ️the game speed is based on this value ⚠  ️

            //-------the main character------
            Vector2 player = new((float)screenWidth / 2, (float)screenHeight / 2);
            
            //-------the floor and objects---
            Rectangle floor = new Rectangle(0, screenHeight - 100, screenWidth, 100);

            //--------actual game loop-------
            while (!WindowShouldClose())
            {
                DrawText("developing state\nlast addition: movement", 10, 10, 20, Raylib_cs.Color.White);
                //---updating player position---
                player = Movement.UpdateMovement(player, floor);
                //---updating the scene---------
                player = Scenes.UpdateScene(player, floor, screenWidth);
                
                //---drawing the game frame-----
                BeginDrawing();
                ClearBackground(Scenes.SceneList[Scenes.CurrentScene]);
                DrawRectangleRec(floor, Raylib_cs.Color.Green);
                DrawCircleV(player, 50, Raylib_cs.Color.Maroon);

                EndDrawing();
            }

            CloseWindow();

            return 0;
        }
    }
}