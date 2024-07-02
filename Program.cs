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
        //------screen settings---------
        public const int ScreenWidth = 800;
        public const int ScreenHeight = 450;

        //-------the floor---
        public static Rectangle Floor = new Rectangle(0, ScreenHeight - 100, ScreenWidth, 100);
        public static int Main()
        {
            InitWindow(ScreenWidth, ScreenHeight, "The hospital");
            SetTargetFPS(60); // ⚠ ️the game speed is based on this value ⚠  ️

            //-------the main character------
            Vector2 player = new((float)ScreenWidth / 2, (float)ScreenHeight / 2);
            
            
            //--------actual game loop-------
            while (!WindowShouldClose())
            {
                DrawText("developing state\nlast addition: shooting heheheheh", 10, 10, 20, Raylib_cs.Color.White);
                //---updating player position---
                player = Movement.UpdateMovement(player, Floor);
                //---updating the scene---------
                player = Scenes.UpdateScene(player, Floor);
                //---check if the player likes to shoot-------
                Shooting.HandleShooting(player);
                
                
                //---drawing the game frame-----
                BeginDrawing();
                ClearBackground(Scenes.SceneList[Scenes.CurrentScene]);
                DrawRectangleRec(Floor, Raylib_cs.Color.Green);
                DrawCircleV(player, 50, Raylib_cs.Color.Maroon);
                Shooting.Draw();
                EndDrawing();
            }

            CloseWindow();

            return 0;
        }
    }
}