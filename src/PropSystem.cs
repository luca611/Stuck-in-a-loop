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

    //--------------------------------RESOURCES-------------------------------------
    
    private static readonly Texture2D ChairTexture = LoadTexture("./resources/Props/chair.png");
    private static readonly Texture2D DoorTexture = LoadTexture("./resources/Props/door.png");
    private static readonly Texture2D VineTexture = LoadTexture("./resources/Props/vine.png");
    public static Texture2D LightOffTexture = LoadTexture("./resources/Props/light.png");
    public static Texture2D LightOnTexture = LoadTexture("./resources/Props/lightOn.png");
    public static Texture2D BrakeOn= LoadTexture("./resources/Props/brakeOn.png");
    public static Texture2D BrakeOff=LoadTexture("./resources/Props/brakeOff.png");
    
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
    var random = new Random();

    for (var i = 0; i < MaxDoors; i++)
    {
        bool positionIsValid;
        Vector2 newPosition;
        int scene;

        do
        {
            positionIsValid = true;
            var randomX = random.Next(0, GameWindow.ScreenWidth); 
            scene = random.Next(0, Scenes.SceneList.Length); 
            newPosition = new Vector2(randomX, GameWindow.Floor.Y - 150);
            
            var tempDoor = new Prop(newPosition, DoorTexture, scene, false);
            if (IsPropClipping(tempDoor))
            {
                positionIsValid = false;
            }
        } while (!positionIsValid);

        //--if ot made until here create the prop--
        Doors[i] = new Prop(newPosition, DoorTexture, scene, false);
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
    var random = new Random();

    for (var i = 0; i < MaxLights; i++)
    {
        bool positionIsValid;
        Vector2 newPosition;
        int scene;

        do
        {
            positionIsValid = true;
            var randomX = random.Next(0, GameWindow.ScreenWidth); 
            scene = random.Next(0, Scenes.SceneList.Length); 
            newPosition = new Vector2(randomX, 40);

            
            var tempLight = new Prop(newPosition, LightOnTexture, scene, true);
            if (IsPropClipping(tempLight)) positionIsValid = false;
            
        } while (!positionIsValid);
        //--if ot made until here create the prop--
        Lights[i] = new Prop(newPosition, LightOnTexture, scene, true);
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
        var random = new Random();

        for (var i = 0; i < MaxVines; i++)
        {
            bool positionIsValid;
            Vector2 newPosition;
            int scene;

            do
            {
                positionIsValid = true;
                var randomX = random.Next(0, GameWindow.ScreenWidth);
                scene = random.Next(0, Scenes.SceneList.Length); 
                newPosition = new Vector2(randomX, GameWindow.Floor.Y - 60);
                
                var tempVine = new Prop(newPosition, VineTexture, scene, false);
                if (IsPropClipping(tempVine)) positionIsValid = false;
                
            } while (!positionIsValid);
            //--if ot made until here create the prop--
            Vines[i] = new Prop(newPosition, VineTexture, scene, false);
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
        var random = new Random();
        //--Generate the chairs--
        for (var i = 0; i < MaxChairs; i++)
        {
            bool positionIsValid;
            Vector2 newPosition;
            int scene;
            do
            {
                positionIsValid = true;
                var randomX = random.Next(0, GameWindow.ScreenWidth); 
                scene = random.Next(0, Scenes.SceneList.Length); 
                newPosition = new Vector2(randomX, GameWindow.Floor.Y - 45);

                var tempChair = new Prop(newPosition, ChairTexture, scene, false);
                if (IsPropClipping(tempChair)) positionIsValid = false;
                
            } while (!positionIsValid);
            //--if ot made until here create the prop--
            Chairs[i] = new Prop(newPosition, ChairTexture, scene, false);
        }
    }

    /// <summary>
    /// Draw the chairs
    /// </summary>
    private static void DrawChairs()
    {
        foreach (var chair in Chairs) chair.DrawProp();
    }
    
    
    /// <summary>
    /// Check if a prop is clipping with any other prop
    /// </summary>
    /// <param name="propToCheck"> <c>Prop</c> prop to check</param>
    /// <returns> <c>bool</c> true if the prop is clipping with another prop false if not</returns>
    private static bool IsPropClipping(Prop propToCheck)
    {
        //minimum distance for clipping
        const float clippingDistance = 75.0f; 

        //--easy way to check if the prop is clipping with any other prop--
        var allProps = Doors.Concat(Lights).Concat(Vines).Concat(Chairs).ToArray();

        foreach (var prop in allProps)
        {
            if (prop == null || prop == propToCheck) continue;
            var distance = Vector2.Distance(prop.GetPos(), propToCheck.GetPos());
            //--check if the distance is less than the clipping distance--
            if (distance < clippingDistance) return true;
        }
        return false;
    }
}