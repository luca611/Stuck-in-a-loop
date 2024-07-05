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
public class Prop(Vector2 position, Vector2 size, Color color, int scene)
{
    //---------------------------------CODE-------------------------------------
    
    /// <summary>
    /// draw the prop
    /// </summary>
    public void DrawProp()
    {
        if(scene != Scenes.CurrentScene) return;
        DrawRectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y, color);
    }
}