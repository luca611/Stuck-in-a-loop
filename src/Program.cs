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
        //public static Vector2 Player = new((float)ScreenWidth / 2, (float)ScreenHeight / 2);
        
        public static int Main()
        {
            new LightSystem();
            //-----main character--------
            InitWindow(ScreenWidth, ScreenHeight, "The hospital");
            
            Player mainCharac = new Player(10, 5, ScreenWidth / 2, 0);
            SetTargetFPS(60); // ⚠ ️the game speed is based on this value ⚠  ️
            //--------actual game loop-------
            while (!WindowShouldClose())
            {
                PropSystem.InitProps();
                if (!UiComponents.IsPaused)
                {
                    DrawText("developing state\nlast addition: Light system", 10, 10, 20, Color.White);
                    //---update the player-----
                    mainCharac.UpdatePlayer();
                    //---generate and move the enemies-----
                    EnemyEngine.SummonEnemy(mainCharac.Position);
                    EnemyEngine.Update(mainCharac);
                }
                LightSystem.UpdateLightSystem(mainCharac);
                //---ui Interactions-----
                UiComponents.TogglePause();
                
                //---drawing the game frame-----
                BeginDrawing();
                ClearBackground(Scenes.SceneList[Scenes.CurrentScene]);
                DrawRectangleRec(Floor, Color.Green);
                PropSystem.DrawProps();
                LightSystem.DrawBrake();
                EnemyEngine.Draw();
                Shooting.Draw();
                mainCharac.DrawPlayer();
                //DrawRectangle((int)Player.X, (int)Player.Y, 50, 100,Color.Maroon);
                LightSystem.UpdateAndDrawWarningText();
                LightSystem.DrawLights();
                //---draw ui components---
                UiComponents.DrawUiComponents();
                EndDrawing();
            }

            CloseWindow();

            return 0;
        }
    }
}