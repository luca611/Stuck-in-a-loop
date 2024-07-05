using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace Stuck_in_a_loop_challange;

/// <summary>
/// class to handle the prop system
/// </summary>
public static class PropSystem
{
    //---------------------------------VARIABLES-------------------------------------
    /// <summary>
    /// <c>bool</c> Flag to check if the props are generated for the first time
    /// </summary>
    private static bool _isFirstGeneration = true;
    
    /// <summary>
    /// <c>int</c> Max number of doors
    /// </summary>
    private const int MaxDoors = 4; 
    
    /// <summary>
    /// <c>int</c> Max number of lights
    /// </summary>
    private const int MaxLights = 8;
    
    /// <summary>
    /// <c>int</c> Max number of veins
    /// </summary>
    private const int MaxVines = 4;
    
    /// <summary>
    /// <c>int</c> Max number of props
    /// </summary>
    private const int MaxChairs = 2; // Max number of props
    
    /// <summary>
    /// <c>Prop[]</c> Array of doors
    /// </summary>
    private static readonly Prop[] Doors = new Prop[MaxDoors]; 
    
    /// <summary>
    /// <c>Prop[]</c> Array of lights
    /// </summary>
    private static readonly Prop[] Lights = new Prop[MaxLights]; 
    
    /// <summary>
    /// <c>Prop[]</c> Array of vines
    /// </summary>
    private static readonly Prop[] Vines = new Prop[MaxVines]; 
    
    /// <summary>
    /// <c>Prop[]</c> Array of chairs
    /// </summary>
    private static readonly Prop[] Chairs = new Prop[MaxChairs];
    
    private static Texture2D _currentlightTexture;
    
    //--------------------------------RESOURCES-------------------------------------
    
    private static Texture2D _chairTexture = LoadTexture("./resources/Props/chair.png");
    private static Texture2D _doorTexture = LoadTexture("./resources/Props/door.png");
    private static Texture2D _vineTexture = LoadTexture("./resources/Props/vine.png");
    public static Texture2D LightOffTexture = LoadTexture("./resources/Props/light.png");
    public static Texture2D LightOnTexture = LoadTexture("./resources/Props/lightOn.png");
    
    //-----------------------------------CODE--------------------------------------
    
    /// <summary>
    /// Initialize the props
    /// </summary>
    public static void InitProps()
    {
        //--Generate the props only once-- (i forgot about this the firs time and doors were spinning all around)
        if (!_isFirstGeneration) return;
        
        //--Generate the props--
        GenerateChairs();
        GenerateDoors();
        GenerateLights();
        GenerateVines();
        _isFirstGeneration = false;
    }
    
    /// <summary>
    /// Draw the props
    /// </summary>
    public static void DrawProps()
    {
        DrawVines();
        DrawDoors();
        DrawChairs();
        DrawLights();
    }
    
    /// <summary>
    /// Generate the doors
    /// </summary>
    private static void GenerateDoors()
    {
        //---random generator---
        var random = new Random();
        var doorColor = Color.Pink; //will be replaced with the texture

        for (var i = 0; i < MaxDoors; i++)
        {
            var randomX = random.Next(0, BasicWindow.ScreenWidth); // Random X-coordinate
            var scene = random.Next(0, Scenes.SceneList.Length); // Random scene
            //---create the door---
            Doors[i] = new Prop(new Vector2(randomX, BasicWindow.Floor.Y-130), _doorTexture, scene, false);
        }
    }
    
    /// <summary>
    /// Draw the doors
    /// </summary>
    private static void DrawDoors()
    {
        foreach (var door in Doors) door.DrawProp();
    }
    
    /// <summary>
    /// Generate the lights
    /// </summary>
    private static void GenerateLights()
    {
        //---random generator---
        var random = new Random();
        var lightColor = Color.Yellow; //will be replaced with the texture

        for (var i = 0; i < MaxLights; i++)
        {
            var randomX = random.Next(0, BasicWindow.ScreenWidth); // Random X-coordinate
            var scene = random.Next(0, Scenes.SceneList.Length); // Random scene
            //---create the light---
            Lights[i] = new Prop(new Vector2(randomX, 40), LightOnTexture , scene, true);
        }
    }
    
    /// <summary>
    /// Draw the lights
    /// </summary>
    private static void DrawLights()
    {
        foreach (var light in Lights) light.DrawProp();
    }
    
    /// <summary>
    /// Generate the veins
    /// </summary>
    private static void GenerateVines()
    {
        //---random generator---
        var random = new Random();
        var vineColor = Color.DarkGreen; //will be replaced with the texture

        for (var i = 0; i < MaxVines; i++)
        {
            var randomX = random.Next(0, BasicWindow.ScreenWidth); // Random X-coordinate
            var scene = random.Next(0, Scenes.SceneList.Length); // Random scene
            //---create the vine---
            Vines[i] = new Prop(new Vector2(randomX, BasicWindow.Floor.Y-60), _vineTexture, scene,false);
        }
    }

    /// <summary>
    /// Draw the vines
    /// </summary>
    private static void DrawVines()
    {
        foreach (var vine in Vines) vine.DrawProp();
    }
    
    /// <summary>
    /// Generate the chairs
    /// </summary>
    private static void GenerateChairs()
    {
        //---random generator---
        var random = new Random();
        var chairColor = Color.DarkBrown; //will be replaced with the texture

        for (var i = 0; i < MaxChairs; i++)
        {
            var randomX = random.Next(0, BasicWindow.ScreenWidth); // Random X-coordinate
            var scene = random.Next(0, Scenes.SceneList.Length); // Random scene
            //---create the chair---
            Chairs[i] = new Prop(new Vector2(randomX, BasicWindow.Floor.Y-50), _chairTexture, scene, false);
        }
    }

    /// <summary>
    /// Draw the chairs
    /// </summary>
    private static void DrawChairs()
    {
        foreach (var chair in Chairs) chair.DrawProp();
    }
}