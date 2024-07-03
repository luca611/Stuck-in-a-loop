namespace Stuck_in_a_loop_challange;

/// <summary>
/// Class made to manage the enemies system
/// </summary>
public static class EnemyEngine
{
    //------------------------------VARIABLES--------------------------------------
    /// <summary>
    /// <c>List[Enemy]</c>List of all the active enemies
    /// </summary>
    public static readonly List<Enemy> ActiveEnemies = [];
    
    /// <summary>
    /// <c>int</c> max amount of enemies that can be active at the same time by default
    /// </summary>
    private const int MaxEnemies = 5;
    
    /// <summary>
    /// <c>int</c> difficulty of the game
    /// </summary>
    private static int _difficulty = 1;
    
    //-----------------------------------CODE--------------------------------------
    
    /// <summary>
    /// Method to summon an enemy if possible (for now if the max amount of enemies is not reached)
    /// </summary>
    public static void SummonEnemy()
    {
        //----can't summon if the max was reached----
        if (ActiveEnemies.Count >= MaxEnemies * _difficulty) return;
        
        //----randomize the health and speed of the enemy based on the diff.----
        var random = new Random();
        var health = random.Next(1, 1+_difficulty);
        float movementSpeed = random.Next(1, _difficulty);
            
        //----create the enemy and add it to the list----
        var enemy = new Enemy(health, movementSpeed);
        enemy.SummonEnemy();
        ActiveEnemies.Add(enemy);
    }
    
    /// <summary>
    /// Update the position of the enemies
    /// </summary>
    public static void Update()
    {
        //---create a copy of the list to avoid concurrent modification---
        var temp = ActiveEnemies.ToArray();
        //----update the enemies----
        foreach (Enemy enemy in temp)
        {
            //----if the enemy is dead remove it from the list----
            //⚠ this condition shouldn't be touched to avoid concurrent modification ⚠ 
            if(enemy.IsAlive == false) ActiveEnemies.Remove(enemy);
            else enemy.Update();
        }
    }
    
    /// <summary>
    /// Draw the enemies on screen
    /// </summary>
    public static void Draw()
    {
        foreach (Enemy enemy in ActiveEnemies) enemy.Draw();
    }
}