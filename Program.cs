using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Stuck_in_a_loop_challange
{
    public class BasicWindow
    {
        //------screen settings---------
        public const int ScreenWidth = 800;
        public const int ScreenHeight = 450;

        //-------the floor---
        public static Rectangle Floor = new(0, ScreenHeight - 100, ScreenWidth, 100);
        
        
        //-------the main character------
        public static Vector2 Player = new((float)ScreenWidth / 2, (float)ScreenHeight / 2);
        
        public static int Main()
        {
            LightSystem lights = new();
            InitWindow(ScreenWidth, ScreenHeight, "The hospital");
            SetTargetFPS(60); // ⚠ ️the game speed is based on this value ⚠  ️
            //--------actual game loop-------
            while (!WindowShouldClose())
            {
                if (!UiComponents.IsPaused)
                {
                    DrawText("developing state\nlast addition: basic enemy system", 10, 10, 20, Raylib_cs.Color.White);
                    //---updating player position---
                    Player = Movement.UpdateMovement();
                    //---updating the scene---------
                    Player = Scenes.UpdateScene(Player);
                    //---check if the player likes to shoot-------
                    Shooting.HandleShooting(Player);
                    //---generate and move the enemies-----
                    EnemyEngine.SummonEnemy();
                    EnemyEngine.Update();
                }
                LightSystem.UpdateLightSystem();
                //---ui Interactions-----
                UiComponents.TogglePause();
                
                //---drawing the game frame-----
                BeginDrawing();
                ClearBackground(Scenes.SceneList[Scenes.CurrentScene]);
                DrawRectangleRec(Floor, Raylib_cs.Color.Green);
                LightSystem.DrawBrake();
                EnemyEngine.Draw();
                Shooting.Draw();
                DrawCircleV(Player, 50, Color.Maroon);
                LightSystem.UpdateAndDrawWarningText();
                LightSystem.DrawLights();
                //---draw ui components---
                UiComponents.DrawPauseMenu();
                EndDrawing();
            }

            CloseWindow();

            return 0;
        }
    }
}