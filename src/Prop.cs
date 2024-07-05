using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Stuck_in_a_loop_challange;

/// <summary>
/// class to create props
/// </summary>
/// <param name="position"> <c>Vector2</c> position of the prop</param>
/// <param name="size"> <c>Vector2</c> size of the prop</param>
/// <param name="color"> <c>Color</c> color of the prop</param>
/// <param name="scene"> <c>int</c> scene where the prop is</param>
public class Prop(Vector2 position, Texture2D texture, int scene, bool IsLight)
{
    //---------------------------------CODE-------------------------------------
    
    /// <summary>
    /// draw the prop
    /// </summary>
    public void DrawProp()
    {
        if(scene != Scenes.CurrentScene) return;
        if (!IsLight)
        {
            DrawTexture(texture, (int)position.X, (int)position.Y, Color.White);
            return;
        }
        
        if(LightSystem.IsBrakeOn)  DrawTexture(PropSystem.LightOnTexture, (int)position.X, (int)position.Y, Color.White);
        else DrawTexture(PropSystem.LightOffTexture, (int)position.X, (int)position.Y, Color.White);
    }

    public Vector2 GetPos()
    {
        return position;
    }
    
    public int GetScene()
    {
        return scene;
    }
}