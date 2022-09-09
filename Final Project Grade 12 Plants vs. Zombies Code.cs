//Author: Lyam Katz
//File Name: Account.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: An account class

//Import namespace
using System;

class Account
{
    //Store the account stats
    private int? maxGold;
    private double? averageGold;
    private int lifetimeGold;
    private int? maxLevel;
    private int totalKills;
    private int totalPlays;
    private string accountName;

    //Parameter for new account: valid account name
    public Account (string accountName)
    {
        //Store the stats of a new account
        maxGold = null;
        averageGold = null;
        lifetimeGold = 0;
        maxLevel = null;
        totalKills = 0;
        totalPlays = 0;
        this.accountName = accountName;
    }

    //Parameters for existing account: valid total plays, valid account name, valid lifetime gold, valid max level, valid total kills, valid max gold, valid average gold
    public Account (int totalPlays, string accountName, int lifetimeGold, int? maxLevel, int totalKills, int? maxGold, double? averageGold)
    {
        //Store the passed existing account stats
        this.maxGold = maxGold;
        this.averageGold = averageGold;
        this.lifetimeGold = lifetimeGold;
        this.maxLevel = maxLevel;
        this.totalKills = totalKills;
        this.totalPlays = totalPlays;
        this.accountName = accountName;
    }

    //Pre: A valid gold level and kills fromthe last finished game
    //Post: None
    //Desc: Update the stats
    public void UpdateStats(int gold, int level, int kills)
    {
        //Increment the total number of plays
        totalPlays++;

        //If this is the first time a new account finished a game, set the values accordingly based on the gold, level, kills. Else update the stats based on the gold, level, kills 
        if (totalPlays == 1)
        {
            //Set the new stats based on the gold, level, kills
            maxGold = gold;
            averageGold = gold;
            lifetimeGold = gold;
            maxLevel = level;
            totalKills = kills;
        }
        else
        {
            //If the gold earned this play is greater then the account's max gold, update the max gold
            if (gold > maxGold)
            {
                //Update the max gold
                maxGold = gold;
            }

            //Increment the lifetime gold by the gold
            lifetimeGold += gold;

            //Calculate the average gold
            averageGold = (double)lifetimeGold / totalPlays;

            //If a new best level was reached, update the max level
            if (level > maxLevel)
            {
                //Update the max level
                maxLevel = level;
            }

            //Increment the total kills by how many kills the user got this play
            totalKills += kills;
        }   
    }

    //Pre: None
    //Post: A boolean representing if this is a new account
    //Desc: Returns a boolean representing if this is a new account by comparing the number of plays to 0
    public bool IsNewAccount()
    {
        //Return if the account is a new account by comparing the number of plays to 0
        return totalPlays == 0;
    }

    //Pre: None
    //Post: The max gold, an integer
    //Desc: Get the max gold of the account
    public int GetMaxGold()
    {
        //Return the max gold, or -1 if this is a new account (when sorting -1 will be smaller then any account that has plays, even if they have a max gold of 0)
        if (maxGold == null)
        {
            return -1;
        }
        return (int)maxGold;
    }

    //Pre: None
    //Post: The average gold, a double
    //Desc: Get the average gold of the account
    public double GetAverageGold()
    {
        //Return the average gold, or -1 if this is a new account (when sorting -1 will be smaller then any account that has plays, even if they have an average gold of 0)
        if (averageGold == null)
        {
            return -1;
        }
        return (double)averageGold;
    }

    //Pre: None
    //Post: The lifetime gold, an integer
    //Desc: Get the lifetime gold of the account
    public int GetLifetimeGold()
    {
        //Return the lifetime gold
        return lifetimeGold;
    }

    //Pre: None
    //Post: The max level, an integer
    //Desc: Get the max level of the account
    public int GetMaxLevel()
    {
        //Return the max level, or -1 if this is a new account (when sorting -1 will be smaller then any account that has plays, even if they have a max level of 1)
        if (maxLevel == null)
        {
            return -1;
        }
        return (int)maxLevel;
    }

    //Pre: None
    //Post: The total kills, an integer
    //Desc: Get the total kills of the account
    public int GetTotalKills()
    {
        //Return the total kills
        return totalKills;
    }

    //Pre: None
    //Post: The total plays, an integer
    //Desc: Get the total plays of the account
    public int GetTotalPlays()
    {
        //Return the total plays
        return totalPlays;
    }

    //Pre: None
    //Post: The account name, a string
    //Desc: Get the name of the account
    public string GetAccountName()
    {
        //Return the account name
        return accountName;
    }

    //Pre: None
    //Post: A string to print to the accounts file
    //Desc: Returns the string to print to the accounts file to save the account
    public string SaveAccount()
    {
        //Return the string to print to the accounts file based on if this is a new account or not
        if (IsNewAccount())
        {
            //Return the new account string
            return $"0,{accountName}";
        }
        else
        {
            //Return the existing account string
            return $"{totalPlays},{accountName},{lifetimeGold},{maxLevel},{totalKills},{maxGold},{averageGold}";
        }
    }

    //Pre: None
    //Post: All the stats as a string in a user friendly fashion
    //Desc: Returns all the stats as a string in a user friendly fashion
    public string GetStats()
    {
        //Return all the stats as a string in a user friendly fashion
        return $"Total plays: {totalPlays} Lifetime Gold: {lifetimeGold} Max Level: {maxLevel} Total Kills: {totalKills} Max Gold: {maxGold} Average Gold: {Convert.ToInt32(averageGold)}";
    }
}

/////////////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: ActionQueue.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: A custom action queue

//Import namespace
using System.Collections.Generic;

class ActionQueue
{
    //Store the collection of Actions
    private List<Action> queue;

    public ActionQueue() 
    {
        //Set the queue to a new, empty list of actions
        queue = new List<Action>();
    }

    //Pre: Valid action
    //Post: None
    //Description: Add the action to the back of the queue
    public void Enqueue(Action newAction)
    {
        //Add the action to the back of the queue
        queue.Add(newAction);
    }

    //Pre: None
    //Post: Returns the front action of the queue
    //Description: Returns and removes the action at the front of the queue, or returns null if there is no action
    public Action Dequeue()
    {
        //Store the result
        Action result = null;

        //Remove an action if possible
        if (queue.Count > 0)
        {
            result = queue[0];
            queue.RemoveAt(0);
        }

        //Return the result
        return result;
    }

    //A peek method which returns the action at the front of the queue in-case it's necessary for an update of the game
    /*
    //Pre: None
    //Post: Returns the front action of the queue
    //Description: Returns the action at the front of the queue, or returns null if there is no action
    public Action Peek()
    {
        Action result = null;

        if (queue.Count > 0)
        {
            result = queue[0];
        }

        return result;
    }
    */

    //A size method which returns the queue's size in-case it's necessary for an update of the game
    /*
    //Pre: None
    //Post: Returns the current size of the queue
    //Description: Returns the current number of actions in the queue
    public int Size()
    {
        //Return the current number of actions in the queue
        return queue.Count;
    }
    */

    //Pre: None
    //Post: Returns true if the size of the queue is 0, false otherwise
    //Description: Compare the size of the queue against 0 to determine if it's empty
    public bool IsEmpty()
    {
        //Compare the size of the queue against 0 to determine if it's empty
        return queue.Count == 0;
    }
}

///////////////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: AOE.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: An A.O.E class

//Import namespaces
using System.Collections.Generic;
using Final_Project_Tower_Defense_Game___Lyam_Katz;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class AOE
{
    //Store the AOE class's constants, variables, and sprite
    public const int OUT_OF_RANGE = 0;
    public const int ENEMY_INDEX = 1;
    public const int DAMAGE = 2;
    public const int TRUE = -1;
    private Sprite aOE;
    private int range;
    private double damage;
    private int speed;

    //Perameters for AOE: valid range, valid location, valid content manager with image "aoe2", valid damage
    public AOE(int range, Vector2 location, ContentManager content, double damage)
    {
        //Subtract the distance already covered from the range and set that as the range
        this.range = range - ((int)location.X - ((int)Game1.WIDTH * ((int)(location.X / Game1.WIDTH))));

        //Set the location, texture, damage and speed (to 10 pixels per update)
        aOE = new Sprite(location, content.Load<Texture2D>("aoe2"));
        this.damage = damage;
        speed = 10;
    }

    //Pre: A valid list of enemy locations
    //Post: A data array of type double
    //Desc: Updates the AOE and returns a data array of type double
    public double[] Update (List<Vector2> enemyLocations)
    {
        //Move the aoe 10 pixels right, and deincrement the range by 10 pixels
        aOE.SetLocation(new Vector2(aOE.GetLocation().X + speed, aOE.GetLocation().Y));
        range -= speed;

        //Store the AOE data
        double[] data = new double[3];

        //Set the enemy index to the inpossible index 1
        data[ENEMY_INDEX] = -1;

        //If the AOE has crossed the game width or has ran out of range, set is out of range in the data array to true
        if (range <= 0 || aOE.GetLocation().X + Game1.WIDTH >= Projectile.GAME_WIDTH)
        {
            //Set is out of range to true
            data[OUT_OF_RANGE] = TRUE;
        }

        //Loop through the enemy locations
        for (int i = 0; i < enemyLocations.Count; i++)
        {
            //If the enemy is intersecting with the AOE, add the enemy index and the damage dealt to the enemy to the data array
            if (enemyLocations[i].Y == aOE.GetLocation().Y && enemyLocations[i].X <= aOE.GetLocation().X + Game1.WIDTH && enemyLocations[i].X + Game1.WIDTH >= aOE.GetLocation().X)
            {
                data[ENEMY_INDEX] = i;
                data[DAMAGE] = damage;

                break;
            }
        }

        //Return the data array
        return data;
    }

    //Pre: A valid spritebatch
    //Post: None
    //Desc: Draws the AOE
    public void Draw(SpriteBatch sprite)
    {
        //Draw the AOE
        sprite.Draw(aOE.GetTexture(), aOE.GetLocation(), Color.White);
    }
}

///////////////////////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: Action.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: An action class

class Action
{
    //Store the action variables
    private int index1;
    private int index2;
    private string actionType;
    private string towerType;

    //Parameters for non build action: valid index1, valid index2, valid action type
    public Action (int index1, int index2, string actionType)
    {
        //Set the action variables to the passed perameters
        this.index1 = index1;
        this.index2 = index2;
        this.actionType = actionType;
    }

    //Parameters for build action: valid index1, valid index2, valid action type, valid tower type
    public Action(int index1, int index2, string actionType, string towerType)
    {
        this.index1 = index1;
        this.index2 = index2;
        this.actionType = actionType;
        this.towerType = towerType;
    }

    //Pre: None
    //Post: The first index of the action tile, an integer
    //Desc: Returns the first index of the action tile
    public int GetIndex1()
    {
        //Return index1
        return index1;
    }

    //Pre: None
    //Post: The second index of the action tile, an integer
    //Desc: Returns the second index of the action tile
    public int GetIndex2()
    {
        //Return index2
        return index2;
    }

    //Pre: None
    //Post: The action type, a string
    //Desc: Returns the action type
    public string GetActionType()
    {
        //Return the action type
        return actionType;
    }

    //Pre: None
    //Post: The tower type, a string
    //Desc: Returns the tower type
    public string GetTowerType()
    {
        //Return the tower type
        return towerType;
    }
}

///////////////////////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: Enemy1.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: An enemy 1 class

//Import namespaces
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class Enemy1 : Enemy
{
    //Perameters for Enemy1: valid content manager with image "enemy1V2", valid row, valid level
    public Enemy1 (ContentManager content, int row, int level)
    {
        //Set the texture and location
        enemy.SetTexture(content.Load<Texture2D>("enemy1V2"));
        enemy.SetLocation(new Vector2(1800 - enemy.GetTexture().Width, (row - 1) * 225));

        //Set the variables
        speed = 2;
        towerDamage = 2;
        this.row = row;
        isEnemy3 = false;
        isEnemy2 = false;
        value = (int)Math.Floor(1 + ((level - 1) * 0.5));
        health = 3;
        cooldown = Math.Max(1000, 1500 - ((level - 1) * 50));
    }
}

///////////////////////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: Enemy2.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: An enemy 2 class

//Import namespaces
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class Enemy2 : Enemy
{
    //Perameters for Enemy2: valid content manager with image "enemy2V2", valid row, valid level
    public Enemy2(ContentManager content, int row, int level)
    {
        //Set the texture and location
        enemy.SetTexture(content.Load<Texture2D>("enemy2V2"));
        enemy.SetLocation(new Vector2(1800 - enemy.GetTexture().Width, (row - 1) * 225));

        //Set the variables
        speed = 3;
        towerDamage = 3;
        this.row = row;
        isEnemy3 = false;
        isEnemy2 = true;
        value = 3 * (int)Math.Floor(1 + ((level - 1) * 0.5));
        health = 8;
        cooldown = Math.Max(900, 1200 - ((level - 1) * 50));
    }
}

///////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: Enemy3.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: An enemy 3 class

//Import namespaces
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class Enemy3 : Enemy
{
    //Perameters for Enemy3: valid content manager with image "enemy3", valid row, valid level
    public Enemy3(ContentManager content, int row, int level)
    {
        //Set the texture and location
        enemy.SetTexture(content.Load<Texture2D>("enemy3"));
        enemy.SetLocation(new Vector2(1800 - enemy.GetTexture().Width, (row - 1) * 225));

        //Set the variables
        speed = 5.75f;
        towerDamage = 1;
        this.row = row;
        isEnemy3 = true;
        isEnemy2 = false;
        value = 2 * (int)Math.Floor(1 + ((level - 1) * 0.5));
        health = 4;
        cooldown = Math.Max(300, 500 - ((level - 1) * 50));
    }
}

/////////////////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: EnemyStack.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: A custom enemy stack

//Import namespace
using System.Collections.Generic;

class EnemyStack
{
    //Store the collection of enemies
    private List<Enemy> stack;

    public EnemyStack()
    {
        //Set the stack to a new, empty list of enemies
        stack = new List<Enemy>();
    }

    //Pre: Valid enemy
    //Post: None
    //Description: Push the enemy to the top of the stack
    public void Push(Enemy enemy)
    {
        //Push the enemy to the top of the stack
        stack.Add(enemy);
    }

    //Pre: None
    //Post: Returns the top enemy of the stack
    //Description: Returns and removes the enemy at the top of the stack, or returns null if there is no enemy
    public Enemy Pop()
    {
        //Store the result
        Enemy result = null;

        //Remove an enemy if possible
        if (!IsEmpty())
        {
            result = stack[stack.Count - 1];
            stack.RemoveAt(stack.Count - 1);
        }

        //Return the result
        return result;
    }

    //A top method which returns the top enemy in the stack in-case it's necessary for an update of the game
    /*
    //Pre: None
    //Post: Returns the top enemy of the stack
    //Description: Returns the enemy at the top of the stack, or returns null if there is no enemy
    public Enemy Top()
    {
        //Store the result
        Enemy result = null;

        //Store the top enemy if possible
        if (!IsEmpty())
        {
            result = stack[stack.Count - 1];
        }

        //Return the result
        return result;
    }
    */

    //Pre: None
    //Post: Returns the current size of the stack
    //Description: Returns the current number of enemies in the stack
    public int Size()
    {
        //Return the current number of enemies in the stack
        return stack.Count;
    }

    //Pre: None
    //Post: Returns true if the size of the stack is 0, false otherwise
    //Description: Compare the size of the stack against 0 to determine if it's empty
    public bool IsEmpty()
    {
        //Compare the size of the stack against 0 to determine if it's empty
        return stack.Count == 0;
    }
}

/////////////////////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: ArcherTower.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: An archer tower class

//Import namespaces
using Final_Project_Tower_Defense_Game___Lyam_Katz;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class ArcherTower : Tower
{
    //Store the archer towers's variables
    private int range;
    private int attackCooldown;
    private int timer;
    private double damage;
    private double health;

    //Perameters for Archer Tower: a valid content manager with the image "archer tower3"
    public ArcherTower(ContentManager content)
    {
        //Set the texture to the archer tower image
        tower.SetTexture(content.Load<Texture2D>("archer tower3"));

        //Set the variables to their default values
        range = (int)(6 * Game1.WIDTH);
        attackCooldown = 500;
        damage = 1;
        timer = 0;
        health = 10;
        value = 10;
        isActiveTower = true;

    }

    //Pre: None
    //Post: A string representing the tower type
    //Desc: Returns the tower type
    public override string GetTowerType()
    {
        //Return the tower type, Archer Tower
        return "Archer Tower";
    }

    //Pre: None
    //Post: None
    //Desc: Upgrades the tower
    public override void Upgrade()
    {
        //Set is upgraded to true
        isUpgraded = true;

        //Upgrade the tower (buff the variables)
        range = (int)(8 * Game1.WIDTH);
        attackCooldown = 300;
        damage = 2.8;
        value = 15;
    }

    //Pre: None
    //Post: The tower's health
    //Desc: Return's the tower's health
    public override double GetHealth()
    {
        //Return the tower's health
        return health;
    }

    //Pre: A valid double representing damage
    //Post: None
    //Desc: Deals damage to the tower
    public override void TakeDamage(double damage)
    {
        //De-increment the health by the damage dealt to the tower
        health -= damage;
    }

    //Pre: None
    //Post: The attack cooldown, an integer
    //Desc: returns the attack cooldown
    public override int GetCooldown()
    {
        //Return the attack cooldown
        return attackCooldown;
    }

    //Pre: None
    //Post: The timer, an int
    //Desc: Returns the timer
    public override int GetTimer()
    {
        //Return the timer
        return timer;
    }

    //Pre: None
    //Post: The tower's damage, a double
    //Desc: Return's the tower's damage
    public override double GetDamage()
    {
        //Return the tower's damage
        return damage;
    }

    //Pre: None
    //Post: The range, an integer
    //Desc: Returns the range
    public override int GetRange()
    {
        //Return the range
        return range;
    }

    //Pre: None
    //Post: None
    //Desc: Resets the timer
    public override void ResetTimer()
    {
        //Reset the timer
        timer = attackCooldown;
    }

    //Pre: The delta time between frames
    //Post: None
    //Desc: Updates the times
    public override void UpdateTimer(GameTime deltaTime)
    {
        //Update the timer
        timer -= (int)deltaTime.ElapsedGameTime.TotalMilliseconds;
    }
}

/////////////////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: Barrier.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: A barrier class

//Import namespaces
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

class Barrier : Tower
{
    //Store the barrier's health
    private double health;

    //Perameters for Barrier: a valid content manager with the image "barrier4"
    public Barrier(ContentManager content)
    {
        //Set the texture to the barrier image
        tower.SetTexture(content.Load<Texture2D>("barrier4"));

        //Set the variables to their default values
        health = 20;
        value = 20;
        isActiveTower = false;
    }

    //Pre: None
    //Post: A string representing the tower type
    //Desc: Returns the tower type
    public override string GetTowerType()
    {
        //Return the tower type, Barrier
        return "Barrier";
    }

    //Pre: A valid double representing damage
    //Post: None
    //Desc: Deals damage to the tower
    public override void TakeDamage(double damage)
    {
        //De-increment the health by the damage dealt to the tower
        health -= damage;
    }

    //Pre: None
    //Post: The tower's health
    //Desc: Return's the tower's health
    public override double GetHealth()
    {
        //Return the tower's health
        return health;
    }

    //Pre: None
    //Post: None
    //Desc: Upgrades the tower
    public override void Upgrade()
    {
        //Set is upgraded to true
        isUpgraded = true;

        //Upgrade the tower (buff the variables)
        health += 10;
        value = 30;
    }
}

///////////////////////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: Bomb.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: A bomb class

//Import namespaces
using System.Collections.Generic;
using Final_Project_Tower_Defense_Game___Lyam_Katz;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class Bomb
{
    //Store the Bomb class's constants, variable, and sprite
    public const int OUT_OF_RANGE = 2;
    public const int TRUE = 1;
    public const int X = 0;
    public const int Y = 1;
    private int speed;
    private Sprite bomb;

    //Perameters for Bomb: valid content manager with image "bomb", valid location
    public Bomb(ContentManager content, Vector2 location)
    {
        //Set the bomb texture, location, and speed (to 10 pixels per update)
        bomb = new Sprite(new Vector2(location.X, location.Y), content.Load<Texture2D>("bomb"));
        speed = 10;
    }

    //Pre: A valid list of enemy locations
    //Post: A data array of type int
    //Desc: Updates the bomb and returns a data array of type int
    public int[] Update(List <Vector2> towerPositions)
    {
        //Move the bomb 10 pixels left
        bomb.SetLocation(new Vector2(bomb.GetLocation().X - speed, bomb.GetLocation().Y));

        //Store the tower location, and if the tower location is out of range
        int[] towerLocation = new int[3];

        //Set the tower location indexes to the impossible index -1
        towerLocation[X] = -1;
        towerLocation[Y] = -1;

        //If enemy 2 shot at something that got killed before the bomb hit it and the bomb got all the way to the home base, set the out of range integer in the tower location array to true
        if (bomb.GetLocation().X <= 0)
        {
            towerLocation[OUT_OF_RANGE] = TRUE;
        }

        //Loop through the tower positions
        for (int i = 0; i < towerPositions.Count; i++)
        {
            //If the bomb is intersecting with a tower (that isn't a mud trap), store the tower that got hit's indexes in the tower location array
            if (towerPositions[i].Y == bomb.GetLocation().Y && bomb.GetLocation().X <= towerPositions[i].X + Game1.WIDTH)
            {
                //Store the tower that got hit's indexes in the tower location array
                towerLocation[X] = (int)(towerPositions[i].X / Game1.WIDTH);
                towerLocation[Y] = (int)(towerPositions[i].Y / Game1.HEIGHT);

                //Break as the bomb can't be intersecting with two towers at the same time
                break;
            }
        }

        //Return the tower location array
        return towerLocation;
    }

    //Pre: A valid spritebatch
    //Post: None
    //Desc: Draws the bomb
    public void Draw(SpriteBatch sprite)
    {
        //Draw the bomb
        sprite.Draw(bomb.GetTexture(), bomb.GetLocation(), Color.White);
    }
}

///////////////////////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: Boss.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: A boss class

//Import namespaces
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class Boss : Enemy
{
    //Perameters for Boss: valid content manager with image "bossV2", valid row, valid level
    public Boss(ContentManager content, int row, int level)
    {
        //Set the texture and location
        enemy.SetTexture(content.Load<Texture2D>("bossV2"));
        enemy.SetLocation(new Vector2(1800 - enemy.GetTexture().Width, (row - 1) * 225));

        //Set the variables
        speed = 1.5f;
        towerDamage = 4;
        this.row = row;
        homeDamage = 5;
        isEnemy3 = false;
        isEnemy2 = false;
        value = 10 * (int)Math.Floor(1 + ((level - 1) * 0.5));
        health = 50;
        cooldown = Math.Max(800, 1200 - ((level - 1) * 50));
    }
}

/////////////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: Cannon.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: A cannon class

//Import namespaces
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Final_Project_Tower_Defense_Game___Lyam_Katz;

class Cannon : Tower
{
    //Store the cannon's variables
    private double health;
    private int visibleRange;
    private int attackCooldown;
    private int timer;
    private double damage;
    private int aoeRange;

    //Perameters for Cannon: a valid content manager with the image "cannon3"
    public Cannon(ContentManager content)
    {
        //Set the texture to the cannon image
        tower.SetTexture(content.Load<Texture2D>("cannon3"));

        //Set the variables to their default values
        health = 20;
        visibleRange = (int)(3 * Game1.WIDTH);
        attackCooldown = 1200;
        damage = 3.6;
        aoeRange = 1;
        timer = 0;
        value = 40;
        isActiveTower = true;
    }

    //Pre: None
    //Post: A string representing the tower type
    //Desc: Returns the tower type
    public override string GetTowerType()
    {
        //Return the tower type, Cannon
        return "Cannon";
    }

    //Pre: A valid double representing damage
    //Post: None
    //Desc: Deals damage to the tower
    public override void TakeDamage(double damage)
    {
        //De-increment the health by the damage dealt to the tower
        health -= damage;
    }


    //Pre: None
    //Post: None
    //Desc: Upgrades the tower
    public override void Upgrade()
    {
        //Set is upgraded to true
        isUpgraded = true;

        //Upgrade the tower (buff the variables)
        health += 10;
        attackCooldown = 900;
        damage = 8.1;
        aoeRange = 2;
        value = 60;
    }

    //Pre: None
    //Post: The tower's health
    //Desc: Return's the tower's health
    public override double GetHealth()
    {
        //Return the tower's health
        return health;
    }

    //Pre: None
    //Post: The attack cooldown, an integer
    //Desc: returns the attack cooldown
    public override int GetCooldown()
    {
        //Return the attack cooldown
        return attackCooldown;
    }

    //Pre: None
    //Post: The timer, an int
    //Desc: Returns the timer
    public override int GetTimer()
    {
        //Return the timer
        return timer;
    }

    //Pre: None
    //Post: The AOE range, an integer
    //Desc: Returns the AOE range
    public override int GetAoeRange()
    {
        //Return the AOE range
        return aoeRange;
    }

    //Pre: None
    //Post: The tower's damage, a double
    //Desc: Return's the tower's damage
    public override double GetDamage()
    {
        //Return the tower's damage
        return damage;
    }

    //Pre: None
    //Post: The range, an integer
    //Desc: Returns the range
    public override int GetRange()
    {
        //Return the visible range
        return visibleRange;
    }

    //Pre: None
    //Post: None
    //Desc: Resets the timer
    public override void ResetTimer()
    {
        //Reset the timer
        timer = attackCooldown;
    }

    //Pre: The delta time between frames
    //Post: None
    //Desc: Updates the times
    public override void UpdateTimer(GameTime deltaTime)
    {
        //Update the timer
        timer -= (int)deltaTime.ElapsedGameTime.TotalMilliseconds;
    }
}

//////////////////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: Enemy.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: An enemy class

//Import namespaces
using System.Collections.Generic;
using Final_Project_Tower_Defense_Game___Lyam_Katz;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Enemy
{
    //Store the enemy sprite
    protected Sprite enemy;

    //Store the enemy constants
    public const int HOME_DAMAGE = 0;
    public const int TOWER_X = 1;
    public const int TOWER_Y = 2;
    public const int TOWER_DAMAGE = 3;
    public const int TOWER_INDEX_1 = 4;
    public const int TOWER_INDEX_2 = 5;
    public const int SHOOT_BOMB = 6;
    public const int VALUE = 7;
    public const int TRUE = 1;

    //Store the enemy variables
    protected int row;
    protected float speed = 0;
    protected bool isEnemy3;
    protected bool isEnemy2;
    protected float speedMultiplier = 1;
    protected int cooldown;
    protected int timer;
    protected int towerDamage;
    protected double health;
    protected float nextX = Projectile.GAME_WIDTH;
    protected int homeDamage = 1;
    protected int value;

    public Enemy()
    {
        //Define the enemy sprite and set the timer to 0
        enemy = new Sprite(new Vector2(), null);
        timer = 0;
    }

    //Pre: A valid list of tower positions, mud trap positions, mud trap slow rates, and the delta time between frames
    //Post: A data array of type int
    //Desc: Updates the enemy and returns a data array of type int
    public int[] Update(List<Vector2> towerPositions, List<Vector2> mudTrapPositions, List<float> mudTrapSlowRates, GameTime deltaTime)
    {
        //Store if the enemy is moving
        bool isMoving = true;

        //Store the array of data
        int[] data = new int[8];

        //Loop through the tower positions
        for (int i = 0; i < towerPositions.Count; i++)
        {
            //If the enemy is collided with a tower, stop them from moving and set their location to right in front of the tower and break
            if (towerPositions[i].Y == enemy.GetLocation().Y && enemy.GetLocation().X <= towerPositions[i].X + Game1.WIDTH && enemy.GetLocation().X >= towerPositions[i].X)
            {
                //Set is moving to false
                isMoving = false;

                //Set the enemy's location to right in front of the tower
                enemy.SetLocation(new Vector2(towerPositions[i].X + Game1.WIDTH, towerPositions[i].Y));

                //Break
                break;
            }
        }

        //Check if the enemy is moving. If they aren't moving, update melee attacks (Enemy 2 acts as a melee attacker when it's touching a tower as shooting a bomb would be pointless as it would hit immidiattely anyway). If they are moving, move them and if they are an enemy 2 check if a bomb should be shot
        if (isMoving)
        {
            //Move the enemy
            enemy.SetLocation(new Vector2(enemy.GetLocation().X - speed * speedMultiplier, enemy.GetLocation().Y));

            //If the enemy is an enemy 2, deal with bomb logic
            if (isEnemy2)
            {
                //If the timer is up, check if there are towers in the enemy 2's visible range. If yes shoot a bomb and reset the timer. If the timer isn't up, update the timer
                if (timer <= 0)
                {
                    //Loop through the tower positions
                    for (int i = 0; i < towerPositions.Count; i++)
                    {
                        //If there is a tower that isnt mudtrap within 3 tiles of the enemy 2 set shoot a bomb to true in the data array and reset the timer and break
                        if (enemy.GetLocation().X <= Projectile.GAME_WIDTH && towerPositions[i].Y == enemy.GetLocation().Y && enemy.GetLocation().X <= 4 * Game1.WIDTH + towerPositions[i].X && enemy.GetLocation().X >= towerPositions[i].X + Game1.WIDTH)
                        {
                            //Set shoot a bomb to true in the data array and reset the timer and break
                            data[SHOOT_BOMB] = TRUE;
                            timer = cooldown;
                            break;
                        }
                    }
                }
                else
                {
                    //Update the timer
                    timer -= (int)deltaTime.ElapsedGameTime.TotalMilliseconds;
                }
            }
        }
        else
        {
            //If the timer is up, deal damage to the tower in front of the enemy and reset the timer. Else update the enemy
            if (timer <= 0)
            {
                //Reset the timer
                timer = cooldown;

                //Store the tower damage and tower index in the data array
                data[TOWER_DAMAGE] = towerDamage;
                data[TOWER_INDEX_1] = (int)((enemy.GetLocation().X - Game1.WIDTH) / Game1.WIDTH);
                data[TOWER_INDEX_2] = (int)(enemy.GetLocation().Y / Game1.HEIGHT);
            }
            else
            {
                //Update the timer
                timer -= (int)deltaTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        //If the enemy reached the home base, set the data array's home base damage to the home damage to deal to the home base. Else set the data's home base damage to 0 for no damage
        if (enemy.GetLocation().X <= 0)
        {
            //Set the data array's home base damage to the home damage to deal to the home base
            data[HOME_DAMAGE] = homeDamage;
        }
        else
        {
            //Set the data's home base damage to 0 for no damage
            data[HOME_DAMAGE] = 0;
        }

        //Set the tower indexes to the impossible index value -1
        data[TOWER_X] = -1;
        data[TOWER_Y] = -1;

        //If the enemy crossed the next tile, update what the next tile is, and if the enemy isn't enemy 3, check if the enemy ran into a mud trap
        if (enemy.GetLocation().X <= nextX && nextX > 0)
        {
            //Update what the next tile is
            nextX -= Game1.WIDTH;

            //If the enemy isn't enemy 3, check if the enemy ran into a mud trap
            if (!isEnemy3)
            {
                //Store the indexes of the tower to the data array
                data[TOWER_X] = (int)nextX;
                data[TOWER_Y] = (int)enemy.GetLocation().Y;

                //Reset the speed multiplier
                speedMultiplier = 1;

                //Loop through the mud trap positions
                for (int i = 0; i < mudTrapPositions.Count; i++)
                {
                    //If there is a mud trap in front of the enemy, update the speed multiplier and break;
                    if (mudTrapPositions[i].Y == enemy.GetLocation().Y && enemy.GetLocation().X <= mudTrapPositions[i].X + Game1.WIDTH && enemy.GetLocation().X > mudTrapPositions[i].X)
                    {
                        //Update the speed multiplier and break;
                        speedMultiplier = mudTrapSlowRates[i];
                        break;
                    }
                }
            }
            
        }

        //If the enemy was killed, store it's gold value to the data array
        if (health <= 0)
        {
            //Store the enemy's gold value to the data array
            data[VALUE] = value;
        }

        //Return the data array
        return data;
    }

    //Pre: A valid spritebatch
    //Post: None
    //Desc: Draws the enemy
    public void Draw(SpriteBatch sprite)
    {
        //Draw the enemy
        sprite.Draw(enemy.GetTexture(), enemy.GetLocation(), Color.White);
    }

    //Pre: None
    //Post: The enemy row, an integer
    //Desc: Returns the enemy row
    public int GetRow()
    {
        //Return the row
        return row;
    }

    //Pre: None
    //Post: The enemy X position, a float
    //Desc: Returns the enemy X position
    public float GetX()
    {
        //Return the X value of the enemy
        return enemy.GetLocation().X;
    }

    //Pre: None
    //Post: The enemy location, a Vector2
    //Desc: Returns the enemy location
    public Vector2 GetLocation()
    {
        //Return the location of the enemy
        return enemy.GetLocation();
    }

    //Pre: An x value
    //Post: None
    //Desc: Sets the x value to the passed x value
    public void SetX(float x)
    {
        //Set the X value of the enemy to the passed x value
        enemy.SetLocation(new Vector2(x, enemy.GetLocation().Y));
    }

    //Pre: A damage amount
    //Post: None
    //Desc: Deal the passed damage to the enemy
    public void TakeDamage(double damage)
    {
        //De-increment health by the passed damage dealt to the enemy
        health -= damage;
    }
}

//////////////////////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: Game1.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: This program is designed to allow the user to play a game of PvZ3

//All artwork by me, instructions based on example project

//Import namespaces
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System;
using System.Collections.Generic;

namespace Final_Project_Tower_Defense_Game___Lyam_Katz
{
    public class Game1 : Game
    {
        //Store the game states
        private const int ACCOUNTS = 0;
        private const int ACCOUNTS_CHOOSE = 1;
        private const int ACCOUNTS_CREATE = 2;
        private const int MENU = 3;
        private const int STATS = 4;
        private const int INSTRUCTIONS = 5;
        private const int GAMEPLAY = 6;
        private const int LEADERBOARDS = 7;
        private const int LEADERBOARD_DISPLAY = 8;
        private const int USER_STATS = 9;
        private const int PAUSE = 10;
        private const int END_GAME = 11;

        //Store the index values of the account details
        private const int NUM_PLAYS = 0;
        private const int AVERAGE_GOLD = 6;
        private const int LIFETIME_GOLD = 2;
        private const int MAX_LEVEL = 3;
        private const int TOTAL_KILLS = 4;
        private const int MAX_GOLD = 5;
        private const int ACCOUNT_NAME = 1;

        //Store the build times of the towers
        private const int BUILD_TIME_ARCHER = 3000;
        private const int BUILD_TIME_BARRIER = 6000;
        private const int BUILD_TIME_MUD = 5000;
        private const int BUILD_TIME_CANNON = 7000;

        //Store the costs of the towers
        private const int COST_ARCHER = 10;
        private const int COST_BARRIER = 20;
        private const int COST_MUD = 30;
        private const int COST_CANNON = 40;

        //Stroe the width and height of 1 tile
        public const float WIDTH = 158;
        public const float HEIGHT = 225;

        //Store the starting health of the home base
        private const int STARTING_HEALTH = 30;

        //Store the height offset when drawing a texture2d to the screen
        private const int HEIGHT_OFFSET = 30;

        //Store the value of the number of plays if there are no plays yet
        private const string NO_PLAYS = "0";

        //Store the amount of damage enemy 2 deals
        private const double ENEMY_2_DAMAGE = 3;

        //Store the max bar width
        private const int MAX_BAR_WIDTH = 325;
        private const int BAR_HEIGHT = 45;

        //Store the last column index in the 2d array
        private const int LAST_COLUMN_INDEX = 9;

        //Store the head start increment
        private const int HEAD_START_INCREMENT = 3000;

        //Store the minimum spawn time
        private const int MIN_SPAWN_TIME = 1000;

        //Store the spawn time increment
        private const int SPAWN_TIME_INCREMENT = 100;

        //Store the number of rows
        private const int NUM_ROWS = 4;

        //Store the number of columns
        private const int NUM_COLUMNS = 10;

        //Store the graphics device manager
        private GraphicsDeviceManager _graphics;

        //Store the sprite batch
        private SpriteBatch _spriteBatch;

        //Store the steam writer and reader
        private StreamReader inFile;
        private StreamWriter outFile;

        //Store the current and second account
        private int? currentAccount = null;
        private int secondAccount = -1;

        //Store the current gamestate
        private int gameState = ACCOUNTS;

        //Store the user input
        private string userInput = "";

        //Store whether or not to ask the user to re-enter
        private bool displayReenter = false;

        //Store the tiles
        private Texture2D tiles;

        //Store the title
        private Texture2D title;

        //Store the instructions, credits to Mr. Lane's PvZ3 Instructions
        private Texture2D instructions;

        //Store the bar texture
        private Texture2D barTexture;

        //Store the towers
        private Towers towers;

        //Store the action queue
        private ActionQueue actions;

        //Store if the action was chosen
        private bool actionChosen = false;

        //Store if the tile was chosen
        private bool tileChosen = false;

        //Store the if the action chosen was a build action or not (if its a build action, a tower type is needed)
        private bool towerTypeNeeded = false;

        //Store the action timer
        private int timer = 0;

        //Store the gold and the gold of the last level
        private int gold = 30;
        private int lastGold = 30;

        //Store the number of kills and the number of kills of the last level
        private int numKills = 0;
        private int lastKills = 0;

        //Store the home base health and the health of the last level
        private int homeBaseHealth = 30;
        private int lastHealth = 30;

        //Store the user entered action
        private string action;

        //Store the user entered tile number
        private int tileNumber;

        //Store if the next action is ready to be dequeued
        private bool dequeue = true;

        //Store the current action
        private Action currentAction = null;

        //Store the head start timer and the increasing head start time
        private int headStart = 30000;
        private int headStartTime = 30000; 

        //Store the spawn timer and decreasing spawn time
        private int spawnTimer = 0;
        private int spawnTime = 3000;

        //Store the current level
        private int level = 1;

        //Store the number of level enemies
        private int numLevelEnemies;

        //Store how much damage the player dealt on the current level
        private double levelDamage = 0;

        //Store the sorted accounts
        private List<Account> sortedAccounts = new List<Account>();

        //Store the active enemies and the stack of next enemies that are yet to be spawned
        private List<Enemy> enemies = new List<Enemy>();
        private EnemyStack nextEnemies = new EnemyStack();

        //Store the active bombs
        private List<Bomb> bombs = new List<Bomb>();

        //Store the active projectiles
        private List<Projectile> projectiles = new List<Projectile>();

        //Store the active aOEs
        private List<AOE> aOEs = new List<AOE>();

        //Store if the current frame is the first update so if the user clicked a key to get to a screen with user input, the key is not added to their input
        private bool firstUpdate = true;

        //Store the keyboard states
        private KeyboardState oldKeyboardState;
        private KeyboardState currentKeyboardState;

        //Store if the user is clicking shift or not
        private bool shift = false;

        //Store the list of accounts
        private List<Account> accounts = new List<Account>();

        //Store the sprite fonts
        private SpriteFont arial40;
        private SpriteFont arial22;

        //Store the user chosen sort type
        private string sortType = "";

        //Store a random object
        private Random rng;

        //Store the progress bar rectangles
        private Rectangle healthBackgroundBar;
        private Rectangle enemiesBackgroundBar;
        private Rectangle headStartBackgroundBar;
        private Rectangle healthBar;
        private Rectangle enemiesBar;
        private Rectangle headStartBar;

        //Pre: None
        //Post: None
        //Desc: Loads content folder, sets mouse visibility, and defines graphics device manager
        public Game1()
        {
            //Define graphics device manager and content manager
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //Set mouse visibility to false
            IsMouseVisible = false;
        }

        //Pre: None
        //Post: None
        //Desc: Sets resolution and initializes game
        protected override void Initialize()
        {
            //Set the resolution
            //80:18 + 6 = 80:24 but character height is double character width and pixels are 1:1 height/width ratio so 80:24*2 = 48 = 80:48 = 5:3 ratio. I settled on the 5:3 ratio resolution 1800 * 1080
            //1080/48 and 1800/80 = 22.5. Therefore 1 character is equal to  22.5*22.5 pixels. 7*5 in chars is the same as 7 * 10 in pixels since chars are twice as tall as they are wide. 22.5 * 7 = 158 pixels. 22.5 * 10 = 225 pixels. Therefore tiles are 158 * 225 pixels.
            _graphics.PreferredBackBufferWidth = 1800;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            //Initialize game
            base.Initialize();
        }


        //Pre: None
        //Post: None
        //Desc: Load the content
        protected override void LoadContent()
        {
            //Define the spitebatch
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Define the random object
            rng = new Random();

            //Define the queue of actions
            actions = new ActionQueue(); 

            //Define the towers
            towers = new Towers();

            //Define the bar rectangles
            enemiesBackgroundBar = new Rectangle(1425, _graphics.PreferredBackBufferHeight - BAR_HEIGHT - HEIGHT_OFFSET, MAX_BAR_WIDTH, BAR_HEIGHT);
            headStartBackgroundBar = new Rectangle(1425, _graphics.PreferredBackBufferHeight - (2 * BAR_HEIGHT) - 5 - HEIGHT_OFFSET, MAX_BAR_WIDTH, BAR_HEIGHT);
            healthBackgroundBar = new Rectangle(1425, _graphics.PreferredBackBufferHeight - (3 * BAR_HEIGHT) - 10 - HEIGHT_OFFSET, MAX_BAR_WIDTH, BAR_HEIGHT);
            headStartBar = new Rectangle(1425, _graphics.PreferredBackBufferHeight - (2 * BAR_HEIGHT) - 5 - HEIGHT_OFFSET, MAX_BAR_WIDTH, BAR_HEIGHT);
            enemiesBar = new Rectangle(1425, _graphics.PreferredBackBufferHeight - BAR_HEIGHT - HEIGHT_OFFSET, MAX_BAR_WIDTH, BAR_HEIGHT);
            healthBar = new Rectangle(1425, _graphics.PreferredBackBufferHeight - (3 * BAR_HEIGHT) - 10 - HEIGHT_OFFSET, MAX_BAR_WIDTH, BAR_HEIGHT);

            //Define the tiles
            tiles = Content.Load<Texture2D>("tilesV4");

            //Define the title
            title = Content.Load<Texture2D>("title2");

            //Define the instructions, credits to Mr. Lane's PvZ3 Instructions
            instructions = Content.Load<Texture2D>("instructions2");

            //Define the bar texture
            barTexture = Content.Load<Texture2D>("bar");

            //Define the spritefonts
            arial40 = Content.Load<SpriteFont>("arial");
            arial22 = Content.Load<SpriteFont>("arial22");

            //Open the file with the existing accounts
            inFile = File.OpenText("Accounts.txt");

            //Loop through the existing accounts
            while (!inFile.EndOfStream)
            {
                //Store the current account information
                string[] data = inFile.ReadLine().Split(",");

                //Add the current account to the list of accounts
                if (data[NUM_PLAYS] == NO_PLAYS)
                {
                    accounts.Add(new Account(data[ACCOUNT_NAME]));
                }
                else
                {
                    accounts.Add(new Account(Convert.ToInt32(data[NUM_PLAYS]), data[ACCOUNT_NAME], Convert.ToInt32(data[LIFETIME_GOLD]), Convert.ToInt32(data[MAX_LEVEL]), Convert.ToInt32(data[TOTAL_KILLS]), Convert.ToInt32(data[MAX_GOLD]), Convert.ToDouble(data[AVERAGE_GOLD])));
                }
                
            }

            //Close the in file
            inFile.Close();

            //Sort the newly stored accounts alphabetically
            SortAccounts();
        }

        //Pre: None
        //Post: None
        //Desc: Update the game
        protected override void Update(GameTime gameTime)
        {
            //Update the current gamestate
            switch (gameState)
            {
                case ACCOUNTS:
                    //Update accounts
                    UpdateAccounts();
                    break;
                case ACCOUNTS_CHOOSE:
                    //Update accounts choose
                    UpdateAccountsChoose();
                    break;
                case ACCOUNTS_CREATE:
                    //Update accounts create
                    UpdateAccountsCreate();
                    break;
                case MENU:
                    //Update menu
                    UpdateMenu();
                    break;
                case STATS:
                    //Update stats
                    UpdateStats();
                    break;
                case INSTRUCTIONS:
                    //Update instructions
                    UpdateInstructions();
                    break;
                case GAMEPLAY:
                    //Update gameplay
                    UpdateGameplay(gameTime);
                    break;
                case LEADERBOARDS:
                    //Update leaderboards
                    UpdateLeaderboard();
                    break;
                case LEADERBOARD_DISPLAY:
                    //Update leaderboard display
                    UpdateLeaderboardDisplay();
                    break;
                case USER_STATS:
                    //Update user stats
                    UpdateUserStats();
                    break;
                case PAUSE:
                    //Update pause
                    UpdatePause();
                    break;
                case END_GAME:
                    //Update end game
                    UpdateEndGame();
                    break;
            }

            base.Update(gameTime);
        }

        //Pre: None
        //Post: None
        //Desc: Draw the game
        protected override void Draw(GameTime gameTime)
        {
            //Clear the screen with the colour to tan
            GraphicsDevice.Clear(Color.Tan);

            //Draw the current frame based on gamestate
            switch (gameState)
            {
                case ACCOUNTS:
                    //Draw accounts
                    DrawAccounts();
                    break;
                case ACCOUNTS_CHOOSE:
                    //Draw accounts choose
                    DrawAccountsChoose();
                    break;
                case ACCOUNTS_CREATE:
                    //Draw accounts create
                    DrawAccountsCreate();
                    break;
                case MENU:
                    //Draw menu
                    DrawMenu();
                    break;
                case STATS:
                    //Draw stats
                    DrawStats();
                    break;
                case INSTRUCTIONS:
                    //Draw instructions
                    DrawInstructions();
                    break;
                case GAMEPLAY:
                    //Draw gameplay
                    DrawGameplay();
                    break;
                case LEADERBOARDS:
                    //Draw leaderboards
                    DrawLeaderboard();
                    break;
                case LEADERBOARD_DISPLAY:
                    //Draw leaderboard display
                    DrawLeaderboardDisplay();
                    break;
                case USER_STATS:
                    //Draw user stats
                    DrawUserStats();
                    break;
                case PAUSE:
                    //Draw pause
                    DrawPause();
                    break;
                case END_GAME:
                    //Draw end game
                    DrawEndGame();
                    break;
            }

            base.Draw(gameTime);
        }

        //Pre: None
        //Post: None
        //Desc: Update the accounts gamestate
        protected void UpdateAccounts()
        {
            //Get the keyboard's state
            Keyboard.GetState();

            //Let the user chose an existing account or create a new one based on their choice
            if (Keyboard.HasBeenPressed(Keys.D1))
            {
                //Set the gamestate to choose an account
                gameState = ACCOUNTS_CHOOSE;
            }
            else if (Keyboard.HasBeenPressed(Keys.D2))
            {
                //Set the gamestate to create an account
                gameState = ACCOUNTS_CREATE;
            }
        }

        //Pre: None
        //Post: None
        //Desc: Update the accounts choose gamestate
        protected void UpdateAccountsChoose()
        {
            //Get the keyboard's state
            Keyboard.GetState();

            //If the user chose an account, set the current account to the user chosen account if the account exists
            if (Keyboard.HasBeenPressed(Keys.D1))
            {
                //If the account exists, set the current account to it. Otherwise, return
                if (accounts.Count > 0)
                {
                    //Set the current account to the account chosen
                    currentAccount = 0;
                }
                else
                {
                    //Return
                    return;
                }
            }
            else if (Keyboard.HasBeenPressed(Keys.D2))
            {
                //If the account exists, set the current account to it. Otherwise, return
                if (accounts.Count > 1)
                {
                    //Set the current account to the account chosen
                    currentAccount = 1;
                }
                else
                {
                    //Return
                    return;
                }
            }
            else if (Keyboard.HasBeenPressed(Keys.D3))
            {
                //If the account exists, set the current account to it. Otherwise, return
                if (accounts.Count > 2)
                {
                    //Set the current account to the account chosen
                    currentAccount = 2;
                }
                else
                {
                    //Return
                    return;
                }
            }
            else if (Keyboard.HasBeenPressed(Keys.D4))
            {
                //If the account exists, set the current account to it. Otherwise, return
                if (accounts.Count > 3)
                {
                    //Set the current account to the account chosen
                    currentAccount = 3;
                }
                else
                {
                    //Return
                    return;
                }
            }
            else if (Keyboard.HasBeenPressed(Keys.D5))
            {
                //If the account exists, set the current account to it. Otherwise, return
                if (accounts.Count > 4)
                {
                    //Set the current account to the account chosen
                    currentAccount = 4;
                }
                else
                {
                    //Return
                    return;
                }
            }
            else if (Keyboard.HasBeenPressed(Keys.D6))
            {
                //If the account exists, set the current account to it. Otherwise, return
                if (accounts.Count > 5)
                {
                    //Set the current account to the account chosen
                    currentAccount = 5;
                }
                else
                {
                    //Return
                    return;
                }
            }
            else if (Keyboard.HasBeenPressed(Keys.D7))
            {
                //If the account exists, set the current account to it. Otherwise, return
                if (accounts.Count > 6)
                {
                    //Set the current account to the account chosen
                    currentAccount = 6;
                }
                else
                {
                    //Return
                    return;
                }
            }
            else if (Keyboard.HasBeenPressed(Keys.D8))
            {
                //If the account exists, set the current account to it. Otherwise, return
                if (accounts.Count > 7)
                {
                    //Set the current account to the account chosen
                    currentAccount = 7;
                }
                else
                {
                    //Return
                    return;
                }
            }
            else if (Keyboard.HasBeenPressed(Keys.D9))
            {
                //If the account exists, set the current account to it. Otherwise, return
                if (accounts.Count > 8)
                {
                    //Set the current account to the account chosen
                    currentAccount = 8;
                }
                else
                {
                    //Return
                    return;
                }
            }
            else
            {
                //If the user chose to go back to the accounts menu, send them back to accounts
                if (Keyboard.HasBeenPressed(Keys.Escape))
                {
                    //Set the gamestate to accounts
                    gameState = ACCOUNTS;
                }

                //Return
                return;
            }
            //If an account was chosen, set the gamestate to accounts
            gameState = MENU;
        }

        //Pre: None
        //Post: None
        //Desc: Update the accounts create gamestate
        protected void UpdateAccountsCreate()
        {
            //Get the keyboard's state
            Keyboard.GetState();

            //If there is an empty account slot let the user make a new account. Else, make them choose an existing account
            if (accounts.Count == 9)
            {
                //If the user clicks enter, take them to choose an existing account
                if (Keyboard.HasBeenPressed(Keys.Enter))
                {
                    //Set the game state to choose account
                    gameState = ACCOUNTS_CHOOSE;
                }
            }
            else
            {
                //If the user has entered an account name, try to make a new account
                if (Keyboard.HasBeenPressed(Keys.Enter))
                {
                    //If an account with the same name already exists, prevent the user from making the account
                    if (accounts.Count != 0 && IndexOfAccount() != -1)
                    {
                        //Tell the user to re-enter the account name
                        displayReenter = true;

                        //Return
                        return;
                    }

                    //Add the account to the list of accounts, re-sort the accounts, store the accounts in the accounts file, reset variables
                    accounts.Add(new Account(userInput));
                    SortAccounts();
                    currentAccount = IndexOfAccount();
                    userInput = "";
                    displayReenter = false;
                    firstUpdate = true;
                    SaveAccounts();

                    //Set the gamestate to menu
                    gameState = MENU;

                    //Return
                    return;
                }

                //Update the input, not only numbers as they can create account names with letters
                UpdateInput(false);

                //If this is the first update, delete the key that was clicked to get this gamestate from the user input
                if(firstUpdate)
                {
                    //Reset the uer input
                    userInput = "";

                    //The next update will no longer be the first update
                    firstUpdate = false;
                }
            }           
        }

        //Pre: None
        //Post: None
        //Desc: Update the menu gamestate
        protected void UpdateMenu()
        {
            //Get the keyboard's state
            Keyboard.GetState();

            //Perform the menu option chosen by the user
            if (Keyboard.HasBeenPressed(Keys.P))
            {
                //Push the enemies for the first level
                PushEnemies();

                //Set the gamestate to gameplay
                gameState = GAMEPLAY;
            }
            else if (Keyboard.HasBeenPressed(Keys.S))
            {
                //Set the gamestate to stats
                gameState = STATS;
            }
            else if (Keyboard.HasBeenPressed(Keys.I))
            {
                //Set the gamestate to instructions
                gameState = INSTRUCTIONS;
            }
            else if (Keyboard.HasBeenPressed(Keys.C))
            {
                //Set the gamestate to accounts
                gameState = ACCOUNTS;
            }
            else if (Keyboard.HasBeenPressed(Keys.Escape))
            {
                //Exit
                Exit();
            }
        }

        //Pre: None
        //Post: None
        //Desc: Update the stats gamestate
        protected void UpdateStats()
        {
            //Get the keyboard's state
            Keyboard.GetState();

            //Perform the menu option chosen by the user
            if (Keyboard.HasBeenPressed(Keys.Escape))
            {
                //Set the gamestate to menu
                gameState = MENU;
            }
            else if (Keyboard.HasBeenPressed(Keys.L))
            {
                //Set the gamestate to leaderboards
                gameState = LEADERBOARDS;
            }
            else if (Keyboard.HasBeenPressed(Keys.S))
            {
                //Set the gamestate to user stats
                gameState = USER_STATS;
            }
        }

        //Pre: None
        //Post: None
        //Desc: Update the instructions gamestate
        protected void UpdateInstructions()
        {
            //Get the keyboard's state
            Keyboard.GetState();

            //If the user clicked escape return to the menu
            if (Keyboard.HasBeenPressed(Keys.Escape))
            {
                //Set the gamestate to menu
                gameState = MENU;
            }
        }

        //Pre: None
        //Post: None
        //Desc: Update the gameplay gamestate
        protected void UpdateGameplay(GameTime deltaTime)
        {
            //Get the keyboard's state
            Keyboard.GetState();

            //If the user clicked escape change the gamestate to pause
            if (Keyboard.HasBeenPressed(Keys.Escape))
            {
                //Set the gamestate to pause
                gameState = PAUSE;

                //Return
                return;
            } 

            //Ask for input depending on if an action has been chosen yet
            if (actionChosen)
            {
                //Ask for input depending on if a tile has been chosen yet. If the boolean tile chosen is true it means a tower type must be necessary or else the action would have been enqueued already and action chosen and this would be set to false
                if (tileChosen)
                {
                    //If the user entered a tower type, try to enqueue a build action if the tower type is valid
                    if (Keyboard.HasBeenPressed(Keys.Enter))
                    {
                        //Store the user chosen tower type
                        string towerType = "";

                        //Store if a valid tower was chosen
                        bool wasValidTowerChosen = true;

                        //Set display reenter to false
                        displayReenter = false;

                        //Set the tower type to the user chosen tower
                        switch (userInput)
                        {
                            case "1":
                                //Set the tower type to archer tower
                                towerType = "Archer Tower";
                                break;

                            case "2":
                                //Set the tower type to barrier
                                towerType = "Barrier";
                                break;

                            case "3":
                                //Set the tower type to mud trap
                                towerType = "Mud Trap";
                                break;

                            case "4":
                                //Set the tower type to cannon
                                towerType = "Cannon";
                                break;
                            default:
                                //If the user's choice was invalid, tell them to reenter a tower type, and return
                                displayReenter = true;

                                //A valid tower was not chosen
                                wasValidTowerChosen = false;
                                break;
                        }

                        //If a valid tower was chosen, enqueue the build action
                        if (wasValidTowerChosen)
                        {
                            //Subtract 1 from the tile number since indexes start at 0, not 1
                            tileNumber -= 1;

                            //Store the first index
                            int index1 = 0;

                            //While the user chosen tile is on a lower row then the current row, keep increasing the row number
                            while (tileNumber > 9)
                            {
                                //For every row down, the first index increases by 1 and the second index decreases by 10;
                                tileNumber -= 10;
                                index1++;
                            }

                            //Enqueue the build action
                            actions.Enqueue(new Action(index1, tileNumber, action, towerType));

                            //Reset the variables
                            actionChosen = false;
                            towerTypeNeeded = false;
                            userInput = "";
                            tileChosen = false;
                        }                       
                    }
                }
                else
                {
                    //If the user entered a tile number, check if it's valid and if a tower type is required
                    if (Keyboard.HasBeenPressed(Keys.Enter))
                    {
                        //Check if the user input is valid and store the user chosen tile number if possible (Try parse is necessary even though they are only entering numbers since they can enter an empty string)
                        if (Int32.TryParse(userInput, out tileNumber) && tileNumber > 0 && tileNumber < 41)
                        {
                            //Since the user just gave valid input, don't display reenter
                            displayReenter = false;

                            //If all the information necessary has been given, enqueue the action. Else, wait for the tower type
                            if (!towerTypeNeeded)
                            {
                                //Subtract 1 from the tile number since indexes start at 0, not 1
                                tileNumber -= 1;

                                //Store the first index
                                int index1 = 0;

                                //While the user chosen tile is on a lower row then the current row, keep increasing the row number
                                while (tileNumber > 9)
                                {
                                    //For every row down, the first index increases by 1 and the second index decreases by 10;
                                    tileNumber -= 10;
                                    index1++;
                                }

                                //Enqueue the user chosen action
                                actions.Enqueue(new Action(index1, tileNumber, action));

                                //Reset the variables
                                actionChosen = false;
                                towerTypeNeeded = false;
                                userInput = "";
                            }
                            else
                            {
                                //A tile has been chosen, but a tower type is still required.
                                tileChosen = true;

                                //Reset the user input
                                userInput = "";
                            }                           
                        }
                        else
                        {
                            //Set display reenter to true as the user has just entered invalid input
                            displayReenter = true;
                        }
                    }
                }
            }
            else
            {
                //If the user has entered
                if (Keyboard.HasBeenPressed(Keys.Enter))
                {
                    //Set display re-enter to false
                    displayReenter = false;

                    //Save the user selected action for after they enter the rest of the information needed to enqueue the action
                    switch (userInput)
                    {
                        case "1":
                            //Set the action to build
                            action = "Build";

                            //Set action chosen to true
                            actionChosen = true;

                            //A tower type is required to build a tower
                            towerTypeNeeded = true;

                            //Reset the user input
                            userInput = "";
                            break;
                        case "2":
                            //Set the action to upgrade
                            action = "Upgrade";

                            //Set action chosen to true
                            actionChosen = true;

                            //Reset the user input
                            userInput = "";
                            break;
                        case "3":
                            //Set the action to destroy
                            action = "Destroy";

                            //Set the action chosen to true
                            actionChosen = true;

                            //Reset the user input
                            userInput = "";
                            break;
                        default:
                            //Set display re-enter to true, as the user just entered invalid input
                            displayReenter = true;
                            break;
                    }
                }
            }

            //If there is an action to be dequeued and there is no current acion being performed, dequeue the next action
            if (!actions.IsEmpty() && dequeue)
            {
                //Dequeue the action in the front of the queue and store it
                currentAction = actions.Dequeue();

                //Set dequeue to false
                dequeue = false;

                //Set the current action in motion
                switch (currentAction.GetActionType())
                {
                    case "Build":
                        //If the tile isn't empty, the action has failed. Otherwise, start building the tower
                        if (!towers.GetTowerType(currentAction.GetIndex1(), currentAction.GetIndex2()).Equals("Empty"))
                        {
                            //Reset the variables
                            dequeue = true;
                            currentAction = null;
                        }
                        else
                        {
                            //Try to start building the user chosen tower
                            switch (currentAction.GetTowerType())
                            {
                                case "Archer Tower":
                                    //Set the timer to the build time of the archer
                                    timer = BUILD_TIME_ARCHER;
                                    break;
                                case "Cannon":
                                    //Start building the cannon if it hasn't been attempted to be built in the last colomn
                                    if (currentAction.GetIndex2() == LAST_COLUMN_INDEX)
                                    {
                                        //Reset the variables
                                        dequeue = true;
                                        currentAction = null;
                                    }
                                    else
                                    {
                                        //Set the timer to the build time of the cannon
                                        timer = BUILD_TIME_CANNON;
                                    }
                                    break;
                                case "Mud Trap":
                                    //Set the timer to the build time of the mud trap
                                    timer = BUILD_TIME_MUD;
                                    break;
                                case "Barrier":
                                    //Set the timer to the build time of the barrier
                                    timer = BUILD_TIME_BARRIER;
                                    break;
                            }
                        }
                        break;
                    case "Upgrade":
                        //Only start upgrading if the tower isn't already upgraded
                        if (towers.IsUpgraded(currentAction.GetIndex1(), currentAction.GetIndex2()))
                        {
                            //Reset the variables
                            dequeue = true;
                            currentAction = null;
                        }
                        else
                        {
                            //Start upgrading the user chosen tower if there is a tower on the chosen tile
                            switch (towers.GetTowerType(currentAction.GetIndex1(), currentAction.GetIndex2()))
                            {
                                case "Archer Tower":
                                    //Set the timer to half the archer build time
                                    timer = BUILD_TIME_ARCHER / 2;
                                    break;

                                case "Cannon":
                                    //Set the timer to half the cannon build time
                                    timer = BUILD_TIME_CANNON / 2;
                                    break;

                                case "Mud Trap":
                                    //Set the timer to half the mud trap build time
                                    timer = BUILD_TIME_MUD / 2;
                                    break;

                                case "Barrier":
                                    //Set the barrier to half the archer build time
                                    timer = BUILD_TIME_BARRIER / 2;
                                    break;

                                default:
                                    //Reset the variables
                                    dequeue = true;
                                    currentAction = null;
                                    break;
                            }
                        }
                        break;
                    case "Destroy":
                        {
                            //Give the user half the value of the tower they are deleting (rounded up)
                            gold += (int)Math.Ceiling((double)towers.GetValue(currentAction.GetIndex1(), currentAction.GetIndex2()) / 2);
                            
                            //Delete the tower, or reset the empty tile if the user chose to delete on an empty tile
                            towers.SetTower(currentAction.GetIndex1(), currentAction.GetIndex2(), new Tower());

                            //Reset the variables
                            dequeue = true;
                            currentAction = null;
                        }
                        break;
                }
            }

            //If there is a current action that hasn't been resolved yet, update the progress of the action
            if (currentAction != null)
            {
                //If the timer is up, try to perform the action. Else, update the timer
                if (timer <= 0)
                {
                    //Try to perform the current action (If it's not build it must be upgrade since destroy actions happen immediately)
                    if (currentAction.GetActionType().Equals("Build"))
                    {
                        //Store if the tower was built or not
                        bool wasBuilt = false;

                        //Try to build the seleted tower
                        switch (currentAction.GetTowerType())
                        {
                            case "Archer Tower":
                                //If the user can afford to build an archer tower, build the archer tower on the user specified tile
                                if (COST_ARCHER <= gold)
                                {
                                    //Deduct the cost of the tower from the user's gold
                                    gold -= COST_ARCHER;

                                    //Build the tower
                                    towers.SetTower(currentAction.GetIndex1(), currentAction.GetIndex2(), new ArcherTower(Content));

                                    //Set was built to true as the tower was built
                                    wasBuilt = true;
                                }
                                break;

                            case "Cannon":
                                //If the user can afford to build an archer tower, build the cannon on the user specified tile
                                if (COST_CANNON <= gold)
                                {
                                    //Deduct the cost of the tower from the user's gold
                                    gold -= COST_CANNON;

                                    //Build the tower
                                    towers.SetTower(currentAction.GetIndex1(), currentAction.GetIndex2(), new Cannon(Content));

                                    //Set was built to true as the tower was built
                                    wasBuilt = true;
                                }
                                break;

                            case "Mud Trap":
                                //If the user can afford to build an archer tower, build the mud trap on the user specified tile
                                if (COST_MUD <= gold)
                                {
                                    //Deduct the cost of the tower from the user's gold
                                    gold -= COST_MUD;

                                    //Build the tower
                                    towers.SetTower(currentAction.GetIndex1(), currentAction.GetIndex2(), new MudTrap(Content));

                                    //Set was built to true as the tower was built
                                    wasBuilt = true;
                                }
                                break;

                            case "Barrier":
                                //If the user can afford to build an archer tower, build the barrier on the user specified tile
                                if (COST_BARRIER <= gold)
                                {
                                    //Deduct the cost of the tower from the user's gold
                                    gold -= COST_BARRIER;

                                    //Build the tower
                                    towers.SetTower(currentAction.GetIndex1(), currentAction.GetIndex2(), new Barrier(Content));

                                    //Set was built to true as the tower was built
                                    wasBuilt = true;
                                }
                                break;
                        }

                        //If the tower was built, check if there is an enemy on the tile, and bump the enemy to the left of the tower
                        if (wasBuilt)
                        {
                            //Loop through the enemies to check if they are on the tile of the newly built tile
                            for (int i = 0; i < enemies.Count; i++)
                            {
                                //If the enemy is on the same row as the tile, check if they are intersecting
                                if (enemies[i].GetRow() == currentAction.GetIndex1() + 1)
                                {
                                    //Store the x positions of the left of the tower and enemy
                                    float leftOfTower = currentAction.GetIndex2() * WIDTH;
                                    float leftOfEnemy = enemies[i].GetX();

                                    //Check if there is a collision between the enemy and the tower
                                    if (leftOfEnemy < (leftOfTower + WIDTH) && (leftOfEnemy + WIDTH) > leftOfTower)
                                    {
                                        //Bump the enemy to the left of the tower
                                        enemies[i].SetX(leftOfTower - WIDTH);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //Attempt to upgrade the tower of the user's choice
                        switch (towers.GetTowerType(currentAction.GetIndex1(), currentAction.GetIndex2()))
                        {
                            case "Archer Tower":
                                //Upgrade the tower if the user can afford to
                                if (COST_ARCHER / 2 <= gold)
                                {
                                    //Deduct half the cost of the tower from the user's gold (Upgrade cost = half Tower cost)
                                    gold -= COST_ARCHER / 2;

                                    //Upgrade the tower
                                    towers.Upgrade(currentAction.GetIndex1(), currentAction.GetIndex2());
                                }
                                break;

                            case "Cannon":
                                //Upgrade the tower if the user can afford to
                                if (COST_CANNON / 2 <= gold)
                                {
                                    //Deduct half the cost of the tower from the user's gold (Upgrade cost = half Tower cost)
                                    gold -= COST_CANNON / 2;

                                    //Upgrade the tower
                                    towers.Upgrade(currentAction.GetIndex1(), currentAction.GetIndex2());
                                }
                                break;

                            case "Mud Trap":
                                //Upgrade the tower if the user can afford to
                                if (COST_MUD / 2 <= gold)
                                {
                                    //Deduct half the cost of the tower from the user's gold (Upgrade cost = half Tower cost)
                                    gold -= COST_MUD / 2;

                                    //Upgrade the tower
                                    towers.Upgrade(currentAction.GetIndex1(), currentAction.GetIndex2());
                                }
                                break;

                            case "Barrier":
                                //Upgrade the tower if the user can afford to
                                if (COST_BARRIER / 2 <= gold)
                                {
                                    //Deduct half the cost of the tower from the user's gold (Upgrade cost = half Tower cost)
                                    gold -= COST_BARRIER / 2;

                                    //Upgrade the tower
                                    towers.Upgrade(currentAction.GetIndex1(), currentAction.GetIndex2());
                                }
                                break;
                        }
                                             
                    }

                    //Reset the variables
                    dequeue = true;
                    currentAction = null;
                }
                else
                {
                    //Update the timer
                    timer -= (int)deltaTime.ElapsedGameTime.TotalMilliseconds;
                }
            }

            //If the head start time hasn't finished, update the head start time and head start bar. Else update the gameplay and the enemies and health bar
            if (headStart != 0)
            {
                //If the user has decided to skip the rest of the head start phase, set the head start timer to 0. Else, update the head start timer
                if (Keyboard.HasBeenPressed(Keys.Space))
                {
                    //Set the head start timer to 0
                    headStart = 0;
                }
                else
                {
                    //Update the head start timer
                    headStart = Math.Max(headStart - (int)deltaTime.ElapsedGameTime.TotalMilliseconds, 0);
                }

                //Set the head start bar width to the ratio of the timer to the starting value of the timer this level multiplied by the maximum bar width
                headStartBar.Width = (int)((double)headStart / headStartTime * MAX_BAR_WIDTH);
            }
            else
            {
                //Set the health bar width to the ratio of the health to the starting health of the tower multiplied by the maximum bar width
                healthBar.Width = Math.Max((int)((double)homeBaseHealth / STARTING_HEALTH * MAX_BAR_WIDTH), 0);

                //Set the enemies bar width to the ratio of the number of enemies left to be spawned to the starting number of enemies to be spawned this level multiplied by the maximum bar width
                enemiesBar.Width = (int)((double)nextEnemies.Size() / numLevelEnemies * MAX_BAR_WIDTH);

                //If the user's home base has been destroyed, end the game
                if (homeBaseHealth <= 0)
                {
                    //Update the current account's stats with the new stats from the last game
                    accounts[(int)currentAccount].UpdateStats(gold, level, numKills);

                    //Save the accounts
                    SaveAccounts();

                    //Set the gamestate to end game
                    gameState = END_GAME;

                    //Return
                    return;
                }

                //If the spawn timer isn't up, update the timer. Else if there is an enemy to be spawned, spawn the enemy. Else if there are no active enemies, setup the next level
                if (spawnTimer > 0)
                {
                    //Update the timer
                    spawnTimer -= (int)deltaTime.ElapsedGameTime.TotalMilliseconds;
                }
                else
                {
                    //If there is an enemy to be spawned, spawn the enemy. Else if there are no active enemies, setup the next level
                    if (nextEnemies.IsEmpty())
                    {
                        //If there are no active enemies, setup the next level
                        if (enemies.Count == 0)
                        {                            
                            //Give the user 75 percent of the gold spent on remaining towers
                            gold += (int)Math.Ceiling(towers.GetValue() * 0.75);

                            //Reset variables and data structures
                            towers = new Towers();
                            actions = new ActionQueue();                            
                            displayReenter = false;
                            dequeue = true;
                            currentAction = null;
                            bombs.Clear();
                            projectiles.Clear();
                            aOEs.Clear();
                            bombs.Clear();
                            spawnTimer = 0;

                            //Increment level
                            level++;

                            //Push the new enemes to the enemy stack
                            PushEnemies();
                            
                            //Increment the head start time by the head start increment, then set the timer to the head start time
                            headStartTime += HEAD_START_INCREMENT;
                            headStart = headStartTime;

                            //Reset the enemies and head start bar widths
                            headStartBar.Width = MAX_BAR_WIDTH;
                            enemiesBar.Width = MAX_BAR_WIDTH;

                            //If the spawn time is still bigger then the minimum spawn time, de-increment it
                            if (spawnTime > MIN_SPAWN_TIME)
                            {
                                //De-increment the spawn time by the spawn time increment
                                spawnTime -= SPAWN_TIME_INCREMENT;
                            }

                            //Set the gamestate to end game
                            gameState = END_GAME;

                            //Return
                            return;                            
                        }                        
                    }
                    else
                    {
                        //Spawn an enemy
                        enemies.Add(nextEnemies.Pop());

                        //If there are more enemies reset the timer. Else set it to 0
                        if (nextEnemies.IsEmpty())
                        {
                            //Set the spawn timer to 0
                            spawnTimer = 0;
                        }
                        else
                        {
                            //Set the spawn timer to the current level's spawn time
                            spawnTimer = spawnTime;
                        }
                    }
                    
                }
            }

            //Store the tower positions
            List<Vector2> towerPositions = new List<Vector2>();

            //Store the mud trap slow rates and locations (parrallel lists, an index would access the same mud trap for both lists)
            List<float> mudTrapSlowRates = new List<float>();
            List<Vector2> mudTrapLocations = new List<Vector2>();

            //Loop through the tower rows
            for (int i = 0; i < NUM_ROWS; i++)
            {
                //Loop through the tower columns
                for (int j = 0; j < NUM_COLUMNS; j++)
                {
                    //There's nothing to do if it's an empty tile
                    if (!towers.GetTowerType(i, j).Equals("Empty"))
                    {
                        //If the tower is a mudtrap, store it's information. Else, save the tower position
                        if (towers.GetTowerType(i, j).Equals("Mud Trap"))
                        {
                            //If the mudtrap has been fully used, update it's 1 second of visibility, or delete it if the time has ran out
                            if (towers.GetUses(i, j) <= 0)
                            {
                                //If the timer has ran out, delete the mudtrap. Else, update the timer
                                if (towers.GetTimer(i, j) <= 0)
                                {
                                    //Delete the mudtrap
                                    towers.SetTower(i, j, new Tower());
                                }
                                else
                                {
                                    //Update the timer
                                    towers.UpdateTimer(i, j, deltaTime);
                                }                              
                            }

                            //Store the mud trap location and slow rate
                            mudTrapLocations.Add(towers.GetLocation(i, j));
                            mudTrapSlowRates.Add(towers.GetSlowRate(i, j));                           
                        }
                        else
                        {
                            //Store the tower's position
                            towerPositions.Add(towers.GetLocation(i, j));
                        }
                        
                    }
                }
            } 

            //Loop through the bombs
            for (int i = 0; i < bombs.Count; i++)
            {
                //Store the tower location
                int[] towerLocation = bombs[i].Update(towerPositions);

                //If the tower location is out of range, delete the bomb. Else if the bomb collided with a tower, deal the enemy 2 damage to the tower and delete the bomb
                if (towerLocation[Bomb.OUT_OF_RANGE] == Bomb.TRUE)
                {
                    //Delete the bomb, de-increment the loop counter so we don't skip the bomb that took the place of the bomb we just deleted (bombs.Count also de-incremented so we won't try to look at an index that doesn't exist in the list)
                    bombs.RemoveAt(i);
                    i--;
                }
                else if (towerLocation[Bomb.X] != -1)
                {
                    //Deal the enemy 2 damage to the tower, delete the bomb, and de-increment the loop counter
                    towers.TakeDamage(towerLocation[Bomb.Y], towerLocation[Bomb.X], ENEMY_2_DAMAGE);
                    bombs.RemoveAt(i);
                    i--;
                }
            }

            //Store the enemy locations
            List<Vector2> enemyLocations = new List<Vector2>();

            //Loop through the enemies and store their locations
            for (int i = 0; i < enemies.Count; i++)
            {
                //Store the current enemy's location
                enemyLocations.Add(enemies[i].GetLocation());
            }

            //Store the list of new projectiles
            List<Projectile> newProjectiles = towers.GetProjectiles(enemyLocations, Content, deltaTime);

            //Add the new projectiles to the list of projectiles
            for (int i = 0; i < newProjectiles.Count; i++)
            {
                projectiles.Add(newProjectiles[i]);
            }

            //Loop through the projectiles
            for (int i = 0; i < projectiles.Count; i++)
            {
                //Store the current projectile's data and update the current projectile
                double[] data = projectiles[i].Update(enemyLocations);

                //If the projectile hit an enemy, remove the projectile and damage the enemy. Increment the level damage by the projectile damage. If the projectile was a cannonball, launch the AOE
                if (data[Projectile.ENEMY_INDEX] != -1)
                {
                    //Increment the level damage by the projectile damage
                    levelDamage += data[Projectile.DAMAGE];

                    //Damage the enemy by the projectile's damage
                    enemies[(int)data[Projectile.ENEMY_INDEX]].TakeDamage(data[Projectile.DAMAGE]);

                    //Delete the projectile and de-increment the loop counter
                    projectiles.RemoveAt(i);
                    i--;

                    //If the projectile was a cannonball, launch the AOE
                    if (data[Projectile.AOE_X] != -1)
                    {
                        //Add a new AOE to the list of AOEs
                        aOEs.Add(new AOE((int)data[Projectile.AOE_RANGE], new Vector2((float)data[Projectile.AOE_X], (float)data[Projectile.AOE_Y]), Content, data[Projectile.DAMAGE]));
                    }
                }
                else if (data[Projectile.EDGE_HIT] == Projectile.TRUE)
                {
                    //Delete the projectile and de-increment the loop counter
                    projectiles.RemoveAt(i);
                    i--;
                }
            }

            //Loop through the AOEs
            for (int i = 0; i < aOEs.Count; i++)
            {
                //Store the current AOE's data and update the current AOE
                double[] data = aOEs[i].Update(enemyLocations);

                //If the AOE hit an enemy, remove the AOE and damage the enemy. Increment the level damage by the AOE damage
                if (data[AOE.ENEMY_INDEX] != -1)
                {
                    //Damage the enemy by the AOE's damage
                    enemies[(int)data[AOE.ENEMY_INDEX]].TakeDamage(data[AOE.DAMAGE]);

                    //Increment the level damage by the projectile damage
                    levelDamage += data[AOE.DAMAGE];

                    //Delete the AOE and de-increment the loop counter
                    aOEs.RemoveAt(i);
                    i--;
                }
                else if(data[AOE.OUT_OF_RANGE] == AOE.TRUE)
                {
                    //Delete the AOE and de-increment the loop counter
                    aOEs.RemoveAt(i);
                    i--;
                }
            }

            //Loop through the enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                //Store the current enemy's data and update the current enemy
                int[] data = enemies[i].Update(towerPositions, mudTrapLocations, mudTrapSlowRates, deltaTime);

                //If the enemy reached the home base, damage the home base and delete the enemy
                if (data[Enemy.HOME_DAMAGE] != 0)
                {
                    //Damage the home base by the enemy's damage
                    homeBaseHealth -= data[Enemy.HOME_DAMAGE];

                    //Delete the enemy and de-increment the loop counter
                    enemies.RemoveAt(i);
                    i--;
                }

                //If the enemy stepped on a mud trap update that mud trap's uses
                if (data[Enemy.TOWER_X] != -1)
                {
                    //Update the mud trap's uses, or do nothing if there is no tower in front of the enemy
                    towers.UpdateUses(data[Enemy.TOWER_Y] / (int)HEIGHT, data[Enemy.TOWER_X] / (int)WIDTH);
                }

                //If the enemy has just dealt damage, deal damage to the tower
                if (data[Enemy.TOWER_DAMAGE] > 0)
                {
                    //Deal damage to the tower
                    towers.TakeDamage(data[Enemy.TOWER_INDEX_2], data[Enemy.TOWER_INDEX_1], data[Enemy.TOWER_DAMAGE]);
                }

                //If the enemy has just shot a bomb, shoot the bomb (add the bomb to the list of bombs)
                if (data[Enemy.SHOOT_BOMB] == Enemy.TRUE)
                {
                    //Add the bomb to the list of bombs
                    bombs.Add(new Bomb(Content, enemies[i].GetLocation()));
                }

                //If the enemy has been killed, delete the enemy, give the user the gold the enemy dropped, and increment the number of kills
                if (data[Enemy.VALUE] != 0)
                {
                    //Delete the enemy and de-increment the loop counter
                    enemies.RemoveAt(i);
                    i--;

                    //Increase the gold by the value of the enemy
                    gold += data[Enemy.VALUE];

                    //Increment the gold
                    numKills++;
                }
            }

            //Delete the destroyed towers
            towers.DeleteDestroyedTowers();

            //Update the input with only numbers
            UpdateInput(true);
        }

        //Pre: None
        //Post: None
        //Desc: Update the leaderboard gamestate
        protected void UpdateLeaderboard()
        {
            //Get the keyboard's state
            Keyboard.GetState();

            //Set the sort type to the user chosen type, or go back to stats if they clicked escape
            if (Keyboard.HasBeenPressed(Keys.D1))
            {
                //Set the sort type to max gold
                sortType = "Max Gold";
            }
            else if (Keyboard.HasBeenPressed(Keys.D2))
            {
                //Set the sort type to average gold
                sortType = "Average Gold";
            }
            else if (Keyboard.HasBeenPressed(Keys.D3))
            {
                //Set the sort type to lifetime gold
                sortType = "Lifetime Gold";
            }
            else if (Keyboard.HasBeenPressed(Keys.D4))
            {
                //Set the sort type to max levels
                sortType = "Max Levels";
            }
            else if (Keyboard.HasBeenPressed(Keys.D5))
            {
                //Set the sort type to total kills
                sortType = "Total Kills";
            }
            else if (Keyboard.HasBeenPressed(Keys.D6))
            {
                //Set the sort type to total plays
                sortType = "Total Plays";
            }
            else
            {
                //If the user clicked escape go back to stats
                if (Keyboard.HasBeenPressed(Keys.Escape))
                {
                    //Set the gamestate to stats
                    gameState = STATS;
                }
                //Return
                return;
            }

            //Sort the accounts based on the user chosen sort type
            sortedAccounts = MergeSort(accounts, 0, accounts.Count - 1, sortType);

            //Set the gamestate to leaderboard display
            gameState = LEADERBOARD_DISPLAY;
        }

        //Pre: None
        //Post: None
        //Desc: Update the leaderboard display gamestate
        protected void UpdateLeaderboardDisplay()
        {
            //Get the keyboard's state
            Keyboard.GetState();

            //If the user clicked escape take them back to the leaderboards
            if (Keyboard.HasBeenPressed(Keys.Escape))
            {
                //Not nessesary since they would get overwritten anyway, but reset the global variable and list to not take up ram space for no reason
                sortedAccounts.Clear();
                sortType = "";

                //Set the gamestate to leaderboards
                gameState = LEADERBOARDS;
            }
        }

        //Pre: None
        //Post: None
        //Desc: Update the user stats gamestate
        protected void UpdateUserStats()
        {
            //Get the keyboard's state
            Keyboard.GetState();

            //If the user clicked escape reset the variables and go back to stats
            if (Keyboard.HasBeenPressed(Keys.Escape))
            {
                //Reset the variables
                secondAccount = -1;
                firstUpdate = true;
                displayReenter = false;
                userInput = "";

                //Set the gamestate to stats
                gameState = STATS;

                //Return
                return;
            }

            //If the user clicked down delete the current account and go back to the accounts gamestate
            if (Keyboard.HasBeenPressed(Keys.Down))
            {
                //Remove the current account and save the updated list of accounts
                accounts.RemoveAt((int)currentAccount);
                SaveAccounts();

                //Reset the variables
                secondAccount = -1;
                currentAccount = null;
                firstUpdate = true;
                displayReenter = false;

                //Set the current gamestate to accounts
                gameState = ACCOUNTS;
                
                //Return
                return;
            }

            //If the user clicked enter, try to update the second account based on the user input
            if (Keyboard.HasBeenPressed(Keys.Enter))
            {
                //Find the index of the account with the name of the user input
                secondAccount = IndexOfAccount();

                //If the second account was not found in the list of accounts, ask the user to re-enter an account name
                if(secondAccount == -1)
                {
                    //Set display re-enter to true as the user has just entered invalid input
                    displayReenter = true;
                }
                else
                {
                    //Reset the variables
                    userInput = "";
                    displayReenter = false;
                }
                
            }

            //If there is a valid second account, check if the user want's to delete it
            if (secondAccount != -1)
            {
                //If the user clicked up, delete the second account
                if (Keyboard.HasBeenPressed(Keys.Up))
                {
                    //Remove the account from the list of accounts
                    accounts.RemoveAt(secondAccount);

                    //If the second account's index is smaller then the current account's index, the index of the current account has just decreased by 1
                    if (secondAccount < currentAccount)
                    {
                        //De-increment the current account
                        currentAccount--;
                    }

                    //Reset the second account
                    secondAccount = -1;

                    //Save the updated list of accounts
                    SaveAccounts();
                }
            }

            //Update the input with letters, not only numbers
            UpdateInput(false);

            //If it's the first update, delete the key the user clicked to get to this gamestate from the user input
            if (firstUpdate)
            {
                //Reset the uer input
                userInput = "";

                //It is no longer the first update
                firstUpdate = false;
            }

        }

        //Pre: None
        //Post: None
        //Desc: Update the pause gamestate
        protected void UpdatePause()
        {
            //Get the keyboard's state
            Keyboard.GetState();

            //If the user clicked r, return to gameplay. If they clicked q, quit
            if (Keyboard.HasBeenPressed(Keys.R))
            {
                //Set the gamestate to gameplay
                gameState = GAMEPLAY;
            }
            else if (Keyboard.HasBeenPressed(Keys.Q))
            {
                //Reset the game
                Reset();

                //Set the gamestate to menu
                gameState = MENU;
            }
        }

        //Pre: None
        //Post: None
        //Desc: Update the end game gamestate
        protected void UpdateEndGame()
        {
            //Get the keyboard's state
            Keyboard.GetState();

            //If the user clicked enter either go back to gameplay or to the menu if the user died
            if(Keyboard.HasBeenPressed(Keys.Enter))
            {
                //If the user died reset the game and take them to the menu. Else take them back to the game
                if (homeBaseHealth <= 0)
                {
                    //Reset the game
                    Reset();

                    //Set the gamestate to menu
                    gameState = MENU;
                }
                else
                {
                    //Reset the "last" variables
                    lastGold = gold;
                    lastHealth = homeBaseHealth;
                    lastKills = numKills;

                    //No damage was done in the new level yet (reset level damage)
                    levelDamage = 0;

                    //Set the gamestate to gameplay
                    gameState = GAMEPLAY;
                }
            }
        }

        //Pre: None
        //Post: None
        //Desc: Draw the end game
        protected void DrawEndGame()
        {
            _spriteBatch.Begin();

            //Draw the endgame based on if the user has died yet or not
            if (homeBaseHealth <= 0)
            {
                //Draw the lose screen
                _spriteBatch.DrawString(arial22, $"Sorry, you lose. Click enter to continue. Level kills: {numKills - lastKills} Total playthrough kills: {numKills} Level gold: {gold - lastGold} Level damage: {Convert.ToInt32(levelDamage)}", new Vector2(0, 0), Color.Black);
                _spriteBatch.DrawString(arial22, $"Total playthrough gold: {gold} Level damage taken: {lastHealth - homeBaseHealth} Total damage taken: {STARTING_HEALTH - homeBaseHealth} Lifetime stats:", new Vector2(0, 50), Color.Black);
                _spriteBatch.DrawString(arial22, $"{accounts[(int)currentAccount].GetStats()}", new Vector2(0, 100), Color.Black);
            }
            else
            {
                //Draw the level win screen
                _spriteBatch.DrawString(arial22, $"Congratulations, you beat the level. Click enter to continue. Level kills: {numKills - lastKills} Total playthrough kills: {numKills} Level gold: {gold - lastGold} Level damage: {Convert.ToInt32(levelDamage)}", new Vector2(0, 0), Color.Black);
                _spriteBatch.DrawString(arial22, $"Total playthrough gold: {gold} Level damage taken: {lastHealth - homeBaseHealth} Total damage taken: {STARTING_HEALTH - homeBaseHealth}", new Vector2(0, 50), Color.Black);
            }

            _spriteBatch.End();
        }

        //Pre: None
        //Post: None
        //Desc: Draw the accounts gamestate
        protected void DrawAccounts()
        {           
            _spriteBatch.Begin();

            //Ask the user if they want to chose or create an account
            _spriteBatch.DrawString(arial40, "Click 1 to choose an existing account or 2 to create a new account", new Vector2(0, _graphics.PreferredBackBufferHeight / 2), Color.Black);

            //If a current account is selected, draw it
            if (currentAccount != null)
            {
                _spriteBatch.DrawString(arial22, $"Current Account: {accounts[(int)currentAccount].GetAccountName()}", new Vector2(_graphics.PreferredBackBufferWidth / 3 * 2, 0), Color.Black);
            }

            _spriteBatch.End();
        }

        //Pre: None
        //Post: None
        //Desc: Draw the accounts choose gamestate
        protected void DrawAccountsChoose()
        {           
            _spriteBatch.Begin();

            //Ask the user to chose an account
            _spriteBatch.DrawString(arial22, "Please enter the number of the account you wish to play with, or click escape to go back to accounts", new Vector2(0, 0), Color.Black);

            //Draw the account names next to numbers the user can pick to select the account
            for (int i = 0; i < accounts.Count; i++)
            {
                _spriteBatch.DrawString(arial22, $"Account {i + 1}) {accounts[i].GetAccountName()}", new Vector2(0, i * 50 + 90), Color.Black);
            } 

            _spriteBatch.End();
        }

        //Pre: None
        //Post: None
        //Desc: Draw the accounts create gamestate
        protected void DrawAccountsCreate()
        {           
            _spriteBatch.Begin();

            //If there are already 9 accounts, tell the user there are too many accounts and prompt them to click enter to pick an existing account. Else let them make a new account
            if (accounts.Count == 9)
            {
                //Tell the user there are too many accounts and prompt them to click enter to pick an existing account
                _spriteBatch.DrawString(arial22, "Sorry, you have too many accounts already. Delete one and try again. Click enter to chose an account", new Vector2(0, _graphics.PreferredBackBufferHeight / 2), Color.Black);
            }
            else
            {
                //Propt the user to enter the new account's name and draw the user string.
                _spriteBatch.DrawString(arial40, "Please enter the new account's name:", new Vector2(0, 0), Color.Black);
                _spriteBatch.DrawString(arial22, userInput, new Vector2(0, 100), Color.Black);

                //If they tried to create an account with a name that already exists, tell them to re-enter a new name
                if (displayReenter)
                {
                    //Tell the user to re-enter a new name
                    _spriteBatch.DrawString(arial22, "Sorry, an account with this name already exists, please enter a new name", new Vector2(0, 200), Color.Black);
                }
            }

            _spriteBatch.End();
        }

        //Pre: None
        //Post: None
        //Desc: Draw the menu gamestate
        protected void DrawMenu()
        {            
            _spriteBatch.Begin();

            //Draw the current account
            _spriteBatch.DrawString(arial22, $"Current Account: {accounts[(int)currentAccount].GetAccountName()}", new Vector2(_graphics.PreferredBackBufferWidth / 3 * 2, 0), Color.Black);

            //Draw the menu options
            _spriteBatch.DrawString(arial22, "Click P to Play", new Vector2(0, 100), Color.Black);
            _spriteBatch.DrawString(arial22, "Click S to see Stats/Delete Users", new Vector2(0, 200), Color.Black);
            _spriteBatch.DrawString(arial22, "Click I to see Instructions", new Vector2(0, 300), Color.Black);
            _spriteBatch.DrawString(arial22, "Click C to Change User", new Vector2(0, 400), Color.Black);
            _spriteBatch.DrawString(arial22, "Click Escape to Exit", new Vector2(0, 500), Color.Black);

            //Draw the title
            _spriteBatch.Draw(title, new Vector2(_graphics.PreferredBackBufferWidth - title.Width, _graphics.PreferredBackBufferHeight - title.Height - HEIGHT_OFFSET), Color.White);

            _spriteBatch.End();
        }

        //Pre: None
        //Post: None
        //Desc: Draw the stats gamestate
        protected void DrawStats()
        {           
            _spriteBatch.Begin();

            //Draw the stats menu options
            _spriteBatch.DrawString(arial22, "Click Escape to go back to the Menu", new Vector2(0, 100), Color.Black);
            _spriteBatch.DrawString(arial22, "Click L to see Leaderboards", new Vector2(0, 200), Color.Black);
            _spriteBatch.DrawString(arial22, "Click S to see User Stats and/or Delete users", new Vector2(0, 300), Color.Black);

            _spriteBatch.End();
        }

        //Pre: None
        //Post: None
        //Desc: Draw the instructions gamestate
        protected void DrawInstructions()
        {           
            _spriteBatch.Begin();

            //Draw the instructions
            _spriteBatch.Draw(instructions, new Vector2(0, 0), Color.White);

            _spriteBatch.End(); 
        }

        //Pre: None
        //Post: None
        //Desc: Draw the gameplay gamestate
        protected void DrawGameplay()
        {           
            _spriteBatch.Begin();

            //Draw the tiles
            _spriteBatch.Draw(tiles, new Vector2(0, 0), Color.White);

            //Draw the towers
            towers.Draw(_spriteBatch);

            //Draw the stats
            _spriteBatch.DrawString(arial22, $"Gold: {gold}", new Vector2(1000, 915), Color.Black);
            _spriteBatch.DrawString(arial22, $"Next Spawn in: {Math.Ceiling(spawnTimer / 1000.0)}", new Vector2(1000, 965), Color.Black);
            _spriteBatch.DrawString(arial22, $"Level: {level}", new Vector2(1000, 1015), Color.Black);

            //Draw prompts
            if (actionChosen)
            {
                if (tileChosen)
                {
                    _spriteBatch.DrawString(arial22, "Please enter a tower type: 1. Archer Tower 2. Barrier 3. Mud Trap 4. Cannon", new Vector2(0, 923), Color.Black);
                }
                else
                {
                    _spriteBatch.DrawString(arial22, "Please enter a tile number to perform the action", new Vector2(0, 923), Color.Black);
                }
            }
            else
            {
                _spriteBatch.DrawString(arial22, "Please enter an action: 1. Build 2. Upgrade 3. Destroy", new Vector2(0, 923), Color.Black);       
            }

            //Draw user input
            _spriteBatch.DrawString(arial22, userInput, new Vector2(0, 963), Color.Black);

            //If the user entered bad input, tell them their input is invalid
            if (displayReenter)
            {
                _spriteBatch.DrawString(arial22, "Sorry, your input is invalid. Please try again.", new Vector2(0, 1003), Color.Black);
            }

            //Draw the enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(_spriteBatch);
            }

            //Draw the bombs
            for (int i = 0; i < bombs.Count; i++)
            {
                bombs[i].Draw(_spriteBatch);
            }

            //Draw the projectiles
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(_spriteBatch);
            }

            //Draw the AOEs
            for (int i = 0; i < aOEs.Count; i++)
            {
                aOEs[i].Draw(_spriteBatch);
            }

            //Draw the progress bars
            _spriteBatch.Draw(barTexture, enemiesBackgroundBar, Color.Green);
            _spriteBatch.Draw(barTexture, headStartBackgroundBar, Color.Green);
            _spriteBatch.Draw(barTexture, healthBackgroundBar, Color.Green);
            _spriteBatch.Draw(barTexture, headStartBar, Color.Red); 
            _spriteBatch.Draw(barTexture, enemiesBar, Color.Red);          
            _spriteBatch.Draw(barTexture, healthBar, Color.Red);

            //Draw the text of the progress bars on the bars
            _spriteBatch.DrawString(arial22, $"Head Start: {Math.Ceiling(headStart / 1000.0)}", new Vector2(headStartBar.X, headStartBar.Y), Color.Black);
            _spriteBatch.DrawString(arial22, $"Home Health: {homeBaseHealth}", new Vector2(healthBar.X, healthBar.Y), Color.Black);
            _spriteBatch.DrawString(arial22, $"Enemies Left: {nextEnemies.Size()}", new Vector2(enemiesBar.X, enemiesBar.Y), Color.Black);

            _spriteBatch.End();
        }

        //Pre: None
        //Post: None
        //Desc: Draw the leaderboard gamestate
        protected void DrawLeaderboard()
        {           
            _spriteBatch.Begin();

            //Draw the leaderboard menu options
            _spriteBatch.DrawString(arial22, "Click Escape to go back to Stats", new Vector2(0, 0), Color.Black);
            _spriteBatch.DrawString(arial22, "Choose a Leaderboard:", new Vector2(0, 200), Color.Black);
            _spriteBatch.DrawString(arial22, "1. Max Gold", new Vector2(0, 300), Color.Black);
            _spriteBatch.DrawString(arial22, "2. Average Gold", new Vector2(0, 400), Color.Black);
            _spriteBatch.DrawString(arial22, "3. Lifetime Gold", new Vector2(0, 500), Color.Black);
            _spriteBatch.DrawString(arial22, "4. Max Levels", new Vector2(0, 600), Color.Black);
            _spriteBatch.DrawString(arial22, "5. Total Kills", new Vector2(0, 700), Color.Black);
            _spriteBatch.DrawString(arial22, "6. Total Plays", new Vector2(0, 800), Color.Black);

            _spriteBatch.End();
        }

        //Pre: None
        //Post: None
        //Desc: Draw the leaderboard display gamestate
        protected void DrawLeaderboardDisplay()
        {            
            _spriteBatch.Begin();

            //Draw the type of leaderboard and tell the user they can click escape to go back to the leaderboard selection
            _spriteBatch.DrawString(arial40, $"{sortType} Leaderboard", new Vector2(0, 0), Color.Black);
            _spriteBatch.DrawString(arial22, "Click Escape to go back to the Leaderboard Selection", new Vector2(800, 0), Color.Black);

            //Loop through up to the 5 highest scores of the chosen leaderboard
            for (int i = 0; i < Math.Min(sortedAccounts.Count, 5); i++)
            {
                //Draw the current account's name, place, and stats
                _spriteBatch.DrawString(arial22, $"{i + 1}) {sortedAccounts[sortedAccounts.Count - i - 1].GetAccountName()}", new Vector2(0, (2 * i - 1) * 50 + 200), Color.Black);
                _spriteBatch.DrawString(arial22, sortedAccounts[sortedAccounts.Count - i - 1].GetStats(), new Vector2(0, 2 * i * 50 + 200), Color.Black);
            }
            _spriteBatch.End();
        }

        //Pre: None
        //Post: None
        //Desc: Draw the user stats gamestate
        protected void DrawUserStats()
        {            
            _spriteBatch.Begin();

            //If the current account isn't null(the user just deleted it), draw it's stats
            if (currentAccount != null)
            {
                _spriteBatch.DrawString(arial22, $"{accounts[(int)currentAccount].GetAccountName()}'s stats:", new Vector2(0, 0), Color.Black);
                _spriteBatch.DrawString(arial22, accounts[(int)currentAccount].GetStats(), new Vector2(0, 100), Color.Black);
            }
            
            //Prompt the user to find an account by name and draw the user input
            _spriteBatch.DrawString(arial22, "Find an account by name:", new Vector2(0, 200), Color.Black);
            _spriteBatch.DrawString(arial22, userInput, new Vector2(0, 300), Color.Black);

            //If a valid second account has been chosen draw it's stats and prompt the user that they can delete it by clicking up if they want
            if (secondAccount != -1)
            {
                _spriteBatch.DrawString(arial22, $"{accounts[secondAccount].GetAccountName()}'s stats:", new Vector2(0, 400), Color.Black);
                _spriteBatch.DrawString(arial22, accounts[secondAccount].GetStats(), new Vector2(0, 500), Color.Black);
                _spriteBatch.DrawString(arial22, $"Press Up to delete {accounts[secondAccount].GetAccountName()}", new Vector2(0, 600), Color.Black);
            }

            //If current account isn't null, prompt the user they can click down to delete the current account
            if (currentAccount != null)
            {
                //Prompt the user they can click down to delete the current account
                _spriteBatch.DrawString(arial22, $"Press Down to delete {accounts[(int)currentAccount].GetAccountName()}", new Vector2(0, 700), Color.Black);
            }

            //Prompt the user they can click escape to go back to stats
            _spriteBatch.DrawString(arial22, "Press Escape to go back to stats", new Vector2(0, 800), Color.Black);

            //If the user entered a non existent account, inform them
            if (displayReenter)
            {
                //Inform the user they picked a non existent account
                _spriteBatch.DrawString(arial22, "Sorry, an account with that name doesn't exist", new Vector2(0, 900), Color.Black);
            }

            _spriteBatch.End();
        }

        //Pre: None
        //Post: None
        //Desc: Draw the pause gamestate
        protected void DrawPause()
        {           
            _spriteBatch.Begin();

            //Prompt the user to click R to resume or Q to quit
            _spriteBatch.DrawString(arial40, "Click R to resume or Q to quit", new Vector2(0, _graphics.PreferredBackBufferHeight / 2), Color.Black);

            _spriteBatch.End();
        }

        //Pre: None
        //Post: None
        //Desc: Recursively merge sort the accounts by their respective account names alphabetically
        protected void SortAccounts()
        {
            //Recursively merge sort the accounts by their respective account names alphabetically
            accounts = MergeSort(accounts, 0, accounts.Count - 1, "Account Name");
        }

        //Pre: The valid accounts list, left index, right index, and sort type
        //Post: A new sorted accounts list
        //Desc: Recursively merge sort the accounts by a passed attribute
        private static List<Account> MergeSort(List<Account> accounts, int left, int right, string sortBy)
        {
            //Base case, if the right index is smaller then the left index, return an empty account list(an account with 0 elements is sorted)
            if (right < left)
            {
                List<Account> emptyList = new List<Account>();
                return emptyList;
            }

            //Base case, if the right index is equal to the left index, return an account list with the account at the index that right and left are equal to(an account with 1 element is sorted)
            if (right == left)
            {
                List<Account> account = new List<Account>();
                account.Add(accounts[right]);
                return account;
            }

            //Recursive case, split the list into 2, and return the merge of the sorted left and right sides of the list
            int mid = (left + right) / 2;
            return Merge(MergeSort(accounts, left, mid, sortBy), MergeSort(accounts, mid + 1, right, sortBy), sortBy);
        }

        //Pre: Two valid sorted lists and a valid sort type
        //Post: The merged list
        //Desc: Merge two sorted lists by a passed attribute
        private static List<Account> Merge(List<Account> accounts1, List<Account> accounts2, string SortBy)
        {
            //Store the result list
            List<Account> result = new List<Account>();

            //Store the indexes
            int idx1 = 0;
            int idx2 = 0;

            //Loop through the accounts of both lists
            for (int i = 0; i < accounts1.Count + accounts2.Count; i++)
            {
                //If all of the accounts in the first list have been added to the results list already, and the boolean loop condition hasn't failed yet, that must mean there are just more elements in the already sorted second list, so keep adding them to the result list until all the elements have been added. Also vice versa. Otherwise if there are accounts in both lists, add the smaller one of the two indexes to the results list and increment the index that has just been used
                if (idx1 == accounts1.Count)
                {
                    //Add the next account in the second list of accounts
                    result.Add(accounts2[idx2]);
                    idx2++;
                }
                else if (idx2 == accounts2.Count)
                {
                    //Add the next account in the first list of accounts
                    result.Add(accounts1[idx1]);
                    idx1++;
                }
                else
                {
                    //Add the "smaller" account from each list based on their respective indexes to the results list. "smaller" depends on the sort type.
                    switch(SortBy)
                    {
                        case "Account Name":
                            //If the account at the respective index of the second account list is lower alphabettically then the account of the respective index of the first account list, add the account to the result list and increment the second account list index. Also vice versa.
                            if (accounts2[idx2].GetAccountName().CompareTo(accounts1[idx1].GetAccountName()) < 0)
                            {
                                //Add the account to the results list and increment the respective list index
                                result.Add(accounts2[idx2]);
                                idx2++;
                            }
                            else
                            {
                                //Add the account to the results list and increment the respective list index
                                result.Add(accounts1[idx1]);
                                idx1++;
                            }

                            break;
                        case "Max Gold":
                            //If the account at the respective index of the second account list is lower max gold wise then the account of the respective index of the first account list, add the account to the result list and increment the second account list index. Also vice versa.
                            if (accounts1[idx1].GetMaxGold() <= accounts2[idx2].GetMaxGold())
                            {
                                //Add the account to the results list and increment the respective list index
                                result.Add(accounts1[idx1]);
                                idx1++;
                            }
                            else
                            {
                                //Add the account to the results list and increment the respective list index
                                result.Add(accounts2[idx2]);
                                idx2++;
                            }

                            break;

                        case "Average Gold":
                            //If the account at the respective index of the second account list is lower average gold wise then the account of the respective index of the first account list, add the account to the result list and increment the second account list index. Also vice versa.
                            if (accounts1[idx1].GetAverageGold() <= accounts2[idx2].GetAverageGold())
                            {
                                //Add the account to the results list and increment the respective list index
                                result.Add(accounts1[idx1]);
                                idx1++;
                            }
                            else
                            {
                                //Add the account to the results list and increment the respective list index
                                result.Add(accounts2[idx2]);
                                idx2++;
                            }

                            break;

                        case "Lifetime Gold":
                            //If the account at the respective index of the second account list is lower lifetime gold wise then the account of the respective index of the first account list, add the account to the result list and increment the second account list index. Also vice versa.
                            if (accounts1[idx1].GetLifetimeGold() <= accounts2[idx2].GetLifetimeGold())
                            {
                                //Add the account to the results list and increment the respective list index
                                result.Add(accounts1[idx1]);
                                idx1++;
                            }
                            else
                            {
                                //Add the account to the results list and increment the respective list index
                                result.Add(accounts2[idx2]);
                                idx2++;
                            }

                            break;

                        case "Max Levels":
                            //If the account at the respective index of the second account list is lower max levels wise then the account of the respective index of the first account list, add the account to the result list and increment the second account list index. Also vice versa.
                            if (accounts1[idx1].GetMaxLevel() <= accounts2[idx2].GetMaxLevel())
                            {
                                //Add the account to the results list and increment the respective list index
                                result.Add(accounts1[idx1]);
                                idx1++;
                            }
                            else
                            {
                                //Add the account to the results list and increment the respective list index
                                result.Add(accounts2[idx2]);
                                idx2++;
                            }

                            break;

                        case "Total Kills":
                            //If the account at the respective index of the second account list is lower total kills wise then the account of the respective index of the first account list, add the account to the result list and increment the second account list index. Also vice versa.
                            if (accounts1[idx1].GetTotalKills() <= accounts2[idx2].GetTotalKills())
                            {
                                //Add the account to the results list and increment the respective list index
                                result.Add(accounts1[idx1]);
                                idx1++;
                            }
                            else
                            {
                                //Add the account to the results list and increment the respective list index
                                result.Add(accounts2[idx2]);
                                idx2++;
                            }

                            break;

                        case "Total Plays":
                            //If the account at the respective index of the second account list is lower total plays wise then the account of the respective index of the first account list, add the account to the result list and increment the second account list index. Also vice versa.
                            if (accounts1[idx1].GetTotalPlays() <= accounts2[idx2].GetTotalPlays())
                            {
                                //Add the account to the results list and increment the respective list index
                                result.Add(accounts1[idx1]);
                                idx1++;
                            }
                            else
                            {
                                //Add the account to the results list and increment the respective list index
                                result.Add(accounts2[idx2]);
                                idx2++;
                            }

                            break;
                    }
                }    
            }
            //Return the result list
            return result;
        }

        //Pre: a valid bool representing if the input is numbers only
        //Post: None
        //Desc: Update the user input
        protected void UpdateInput(bool numsOnly)
        {
            //Store the keyboard states
            oldKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            Keys[] pressedKeys = currentKeyboardState.GetPressedKeys();

            //Loop through the pressed keys for the current frame
            for (int i = 0; i < pressedKeys.Length; i++)
            {
                //If a key has been pressed this frame, try to add it to the end of the user input string
                if (oldKeyboardState.IsKeyUp(pressedKeys[i]))
                {
                    //If the user input has at least one character and the user wants to backspace, remove the last character from the user input. Else update either number or all character input based on the passed boolean numsOnly
                    if (pressedKeys[i] == Keys.Back && userInput.Length > 0)
                    {
                        //Remove the last character from the user input
                        userInput = userInput.Remove(userInput.Length - 1);
                    }
                    else if (numsOnly)
                    { 
                        //If the user input doesn't have the maximum 2 numbers, and the user entered a number, add it to the end of the user input string
                        if (userInput.Length < 2 && pressedKeys[i].ToString()[0] == 'D' && pressedKeys[i].ToString().Length == 2)
                        {
                            //Add the number the user pressed to the end of the user input string
                            userInput += pressedKeys[i].ToString()[pressedKeys[i].ToString().Length - 1];
                        }
                    }
                    else
                    {
                        //Only add a character to the user input if it is smaller then the maximum length of 20
                        if (userInput.Length < 20)
                        {
                            //If the current key is a space, add the space to the end of the string. Else if it's a letter or number, add the letter or number to the end of the string
                            if (pressedKeys[i] == Keys.Space)
                            {
                                //Add the space to the back of the user input string
                                userInput = userInput.Insert(userInput.Length, " ");
                            }
                            else if (pressedKeys[i].ToString().Length == 1 || (pressedKeys[i].ToString()[0] == 'D' && pressedKeys[i].ToString().Length == 2))
                            {
                                //Captitalize the input if the user is holding down shift
                                if (shift)
                                {
                                    //Add the capitalized character to the back of the user input string
                                    userInput += pressedKeys[i].ToString()[pressedKeys[i].ToString().Length - 1];
                                }
                                else
                                {
                                    //Add the lowercase character to the back of the user input string
                                    userInput += char.ToLower(pressedKeys[i].ToString()[pressedKeys[i].ToString().Length - 1]);
                                }
                            }
                        }                     
                    }                    
                }
            }
            //If the user is clicking shift, set shift to true. Else set shift to false
            if (pressedKeys.Length > 0 && (pressedKeys[pressedKeys.Length - 1] == Keys.LeftShift || pressedKeys[pressedKeys.Length - 1] == Keys.RightShift))
            {
                //Set shift to true
                shift = true;
            }
            else
            {
                //Set shift to false
                shift = false;
            }
        }


        //Pre: None
        //Post: None
        //Desc: Save the accounts to the accounts file
        protected void SaveAccounts()
        {
            //Recreate the accounts file 
            outFile = File.CreateText("Accounts.txt");

            //Loop through the accounts in the accounts list
            for (int i = 0; i < accounts.Count; i++)
            {
                //Store the current account to the accounts file
                outFile.WriteLine(accounts[i].SaveAccount());
            }

            //Close the accounts file
            outFile.Close();
        }

        //Pre: None
        //Post: The index of the account
        //Desc: Finds and returns the index of an account with the same name as the user input using binary search
        protected int IndexOfAccount()
        {
            //Recursively find the index of an account with the same name as the user input using binary search
            return RecursiveFindIndexBinarySearch(0, accounts.Count - 1);
        }

        //Pre: The bottom and top of the search range
        //Post: The index of the account
        //Desc: Recursively find the index of an account with the same name as the user input using binary search
        protected int RecursiveFindIndexBinarySearch(int bottom, int top)
        {
            //Base case, if the top is smaller then the bottom, the user input string is not in the accounts list. Return the impossible index -1 to represent "Not Found"
            if (top < bottom)
            {
                //Return the impossible index -1 to represent "Not Found"
                return -1;
            }

            //Calculate and store the middle of the current search range
            int middle = (bottom + top) / 2;

            //Base case, if the account name at the middle of the search range is equal to the user input, return the index (the middle). Recursive case, if the account name at the middle of the search range is bigger alphabettically then the user input, the new top will be the middle minus 1. Else, Recursive case 2, the account name at the middle of the search range must be smaller alphabettically then the user input, so the new bottom becomes the middle plus one.
            if (userInput.Equals(accounts[middle].GetAccountName()))
            {
                //Return the index of the account with the name equal to the user input (the middle)
                return middle;
            }
            else if (userInput.CompareTo(accounts[middle].GetAccountName()) < 0)
            {
                //Return the index of the user input in the account list in the new search range
                return RecursiveFindIndexBinarySearch(bottom, middle - 1);
            }
            else
            {
                //Return the index of the user input in the account list in the new search range
                return RecursiveFindIndexBinarySearch(middle + 1, top);
            }                
        }

        //Pre: None
        //Post: None
        //Desc: Push the enemies of the next level to the enemy stack
        protected void PushEnemies()
        {
            //Push all the bosses to the stack of next enemies
            for (int i = Math.Min(level - 1, 3); i > 0; i--)
            {
                nextEnemies.Push(new Boss(Content, i, level));
            }

            //Store the chance that enemy 1 and 2 will spawn
            double enemy1Chance = 80 - Math.Min(15, (level - 1) * 1.5);
            double enemy2Chance = 20 - Math.Min(5, (level - 1) * 0.5);

            //Store the last row an enemy spawned. On the first spawn, the enemy can go on any row, so make the last row 0, a non existent row
            int lastRow = 0;
            
            //Loop with as many iterations as additional enemies to the boss
            for (int i = 10 + ((level - 1) * 8); i > 0; i--)
            {
                //Store a random double between 1 and 100
                double num = rng.NextDouble() * 99 + 1;

                //Store the randomly generated row
                int row;

                //Randomly generate a row until the row generted is different then the last row
                do
                {
                    row = rng.Next(1, 5);
                }
                while (row == lastRow);

                //Set the last row to the current row
                lastRow = row;

                //If the random number is smaller or equal to the chance of enemy 1, push an enemy 1. Else if the number is smaller then the sum of the chance that enemy1 and enemy2 will spawn, push an enemy 2. Else, push an enemy 3.
                if (num <= enemy1Chance)
                {
                    //Push an enemy 1 to the stack
                    nextEnemies.Push(new Enemy1(Content, row, level));
                }
                else if (num <= enemy1Chance + enemy2Chance)
                {
                    //Push an enemy 2 to the stack
                    nextEnemies.Push(new Enemy2(Content, row, level));
                }
                else
                {
                    //Push an enemy 3 to the stack
                    nextEnemies.Push(new Enemy3(Content, row, level));
                }                   
            }

            //Store the number of enemies of the current level
            numLevelEnemies = nextEnemies.Size();
        }

        //Pre: None
        //Post: None
        //Desc: Reset the game variables and data structures
        protected void Reset()
        {
            //Reset the variables and data structures
            level = 1;
            levelDamage = 0;
            gold = 30;
            lastGold = gold;
            homeBaseHealth = 30;
            lastHealth = homeBaseHealth;
            numKills = 0;
            lastKills = numKills;
            headStartTime = 30000;
            headStart = headStartTime;
            enemies.Clear();
            bombs.Clear();
            projectiles.Clear();
            aOEs.Clear();
            nextEnemies = new EnemyStack();
            actions = new ActionQueue();
            currentAction = null;
            dequeue = true;
            spawnTimer = 0;
            spawnTime = 3000;
            displayReenter = false;
            towers = new Towers();
            headStartBar.Width = MAX_BAR_WIDTH;
            enemiesBar.Width = MAX_BAR_WIDTH;
            healthBar.Width = MAX_BAR_WIDTH;
        }
    }
}

//////////////////////////////////////////////////////////////////////////////

//Citation (Class Copied): https://community.monogame.net/t/one-shot-key-press/11669
using Microsoft.Xna.Framework.Input;

public class Keyboard
{
    static KeyboardState currentKeyState;
    static KeyboardState previousKeyState;

    public static KeyboardState GetState()
    {
        previousKeyState = currentKeyState;
        currentKeyState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        return currentKeyState;
    }

    public static bool IsPressed(Keys key)
    {
        return currentKeyState.IsKeyDown(key);
    }

    public static bool HasBeenPressed(Keys key)
    {
        return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
    }
}

///////////////////////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: MudTrap.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: A mudtrap class

//Import namespaces
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

class MudTrap : Tower
{
    //Store the mud trap's variables
    private float slowRate;
    private int uses;
    private int extraTime = 1000;

    //Perameters for Mud Trap: a valid content manager with the image "mud trap3"
    public MudTrap(ContentManager content)
    {
        //Set the texture to the mud trap image
        tower.SetTexture(content.Load<Texture2D>("mud trap3"));

        //Set the variables to their default values
        slowRate = 0.5f;
        uses = 5;
        value = 30;
        isActiveTower = false;
    }

    //Pre: None
    //Post: A string representing the tower type
    //Desc: Returns the tower type
    public override string GetTowerType()
    {
        //Return the tower type, Mud Trap
        return "Mud Trap";
    }


    //Pre: None
    //Post: None
    //Desc: Upgrades the tower
    public override void Upgrade()
    {
        //Set is upgraded to true
        isUpgraded = true;

        //Upgrade the tower (buff the variables)
        uses += 3;
        slowRate = 0.3f;
        value = 45;
    }

    //Pre: None
    //Post: The tower's remaining uses, an integer
    //Desc: Return's the tower's remaining uses
    public override int GetUses()
    {
        //Return the remaining uses
        return uses;
    }

    //Pre: None
    //Post: None
    //Desc: Updates uses
    public override void UpdateUses()
    {
        //De-increment uses
        uses--;

        //If there are no more uses left, set up the next phase of the mudtrap
        if (uses == 0)
        {
            //If another enemy comes on the trap, don't alter their speed by simply multiplying it by 1
            slowRate = 1;

            //Prevent the user from upgrading a destroyed mud trap
            isUpgraded = true;

            //If the user destroys the mudtrap or wins the level, they don't get any gold from the tower since it's destroyed (Update the tower's value)
            value = 0;
        }
    }

    //Pre: None
    //Post: The tower's slow rate, a float
    //Desc: Return's the tower's slow rate
    public override float GetSlowRate()
    {
        //Return the slow rate
        return slowRate;
    }

    //Pre: None
    //Post: The timer, an int
    //Desc: Returns the timer
    public override int GetTimer()
    {
        //Return the extra time until the mud trap disappears (timer)
        return extraTime;
    }

    //Pre: The delta time between frames
    //Post: None
    //Desc: Updates the times
    public override void UpdateTimer(GameTime deltaTime)
    {
        //Update the extra time until the mud trap disappears
        extraTime -= (int)deltaTime.ElapsedGameTime.TotalMilliseconds;
    }
}

////////////////////////////////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: Projectile.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: A projectile class

//Import namespaces
using System.Collections.Generic;
using Final_Project_Tower_Defense_Game___Lyam_Katz;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class Projectile
{
    //Store the Projectile class's constants
    public const int ENEMY_INDEX = 0;
    public const int AOE_X = 1;
    public const int AOE_Y = 2;
    public const int DAMAGE = 3;
    public const int AOE_RANGE = 4;
    public const int EDGE_HIT = 5;
    public const int TRUE = 1;

    //Store the game width (158 pixels per tile * 10 tiles = 1580)
    public const int GAME_WIDTH = 1580;

    //Store the Projectile's variables and sprite
    private Sprite projectile;
    private double damage;
    private int aoeRange;
    private int speed;
    private bool isArrow;

    //Parameters for Projectile: valid content manager with images "arrow" and "cannonball", valid location, valid damage, valid boolean representing if the projectile is an arrow, valid range, valid AOE Range
    public Projectile(ContentManager content, Vector2 location, double damage, bool isArrow, int range, int aoeRange)
    {
        //Set the texture based on the projectile type
        if (isArrow)
        {
            //Set the texture to an arrow
            projectile = new Sprite(location, content.Load<Texture2D>("arrow"));
        }
        else
        {
            //Set the texture to a cannonball
            projectile = new Sprite(location, content.Load<Texture2D>("cannonball"));
        }
        
        //Set the speed to 10 pixels per update
        speed = 10;

        //Set the variables based on the passed perameters
        this.aoeRange = (int)(aoeRange * Game1.WIDTH);
        this.damage = damage;
        this.isArrow = isArrow;
    }

    //Pre: A valid list of enemy locations
    //Post: A data array of type double
    //Desc: Updates the projectile and returns a data array of type double
    public double [] Update(List<Vector2> enemyLocations)
    {
        //Move the projectile 10 pixels right
        projectile.SetLocation(new Vector2(projectile.GetLocation().X + speed, projectile.GetLocation().Y));

        //Store the data array
        double[] data = new double[6];

        //Set the enemy index to the impossible index 1 and the AOE x coordinate to the out of range coordinate -1 in the data array
        data[ENEMY_INDEX] = -1;
        data[AOE_X] = -1;

        //Store the AOE range in the data array
        data[AOE_RANGE] = aoeRange;

        //If the projectile has passed the last tile, set edge hit to true in the data array
        if (projectile.GetLocation().X + Game1.WIDTH >= GAME_WIDTH)
        {
            //Set edge hit to true in the data array
            data[EDGE_HIT] = TRUE;
        }

        //Loop through the enemy locations
        for (int i = 0; i < enemyLocations.Count; i++)
        {
            //If there is a collision between an enemy and the projectile update the data accordingly
            if (enemyLocations[i].Y == projectile.GetLocation().Y && enemyLocations[i].X <= projectile.GetLocation().X + Game1.WIDTH && enemyLocations[i].X >= projectile.GetLocation().X)
            {
                //Store the enemy index to deal damage to in the array
                data[ENEMY_INDEX] = i;

                //If the projectile isn't an arrow, store the AOE location in the data array
                if (!isArrow)
                {
                    //Store the AOE location in the data array
                    data[AOE_X] = enemyLocations[i].X + Game1.WIDTH;
                    data[AOE_Y] = enemyLocations[i].Y;
                }

                //Store the damage dealt to the enemy in the data array
                data[DAMAGE] = damage;

                //A projectile can't deal damage to 2 enemies, so break
                break;
            }
        }

        //Return the data array
        return data;
    }

    //Pre: A valid spritebatch
    //Post: None
    //Desc: Draws the projectile
    public void Draw(SpriteBatch sprite)
    {
        //Draw the projectile
        sprite.Draw(projectile.GetTexture(), projectile.GetLocation(), Color.White);
    }
}

//////////////////////////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: Sprite.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: A sprite class

//Import namespaces
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Sprite
{
    //Store the location and texture
    private Vector2 location;
    private Texture2D texture;

    //Perameters of Sprite: valid location, valid texture
    public Sprite (Vector2 location, Texture2D texture)
    {
        //Set the location and texture to the passed location and texture
        this.location = location;
        this.texture = texture;
    }

    //Pre: None
    //Post: The sprite's location, a Vector2
    //Desc: Returns the sprite's location
    public Vector2 GetLocation()
    {
        //Return the location
        return location;
    }

    //Pre: None
    //Post: The sprite's texture, a Texture2D
    //Desc: Returns the sprite's texture
    public Texture2D GetTexture()
    {
        //Return the texture
        return texture;
    }

    //Pre: A valid location
    //Post: None
    //Desc: Sets the location of the sprite to the passed location
    public void SetLocation(Vector2 location)
    {
        //Set the location to the passed location
        this.location = location;
    }

    //Pre: A valid texture
    //Post: None
    //Desc: Sets the texture of the sprite to the passed texture
    public void SetTexture (Texture2D texture)
    {
        //Set the texture to the passed texture
        this.texture = texture;
    }
}

///////////////////////////////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: Tower.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: A tower class

//Import namespaces
using System.Collections.Generic;
using Final_Project_Tower_Defense_Game___Lyam_Katz;
using Microsoft.Xna.Framework;

class Tower
{
    //Store the tower sprite and variables
    protected Sprite tower;
    protected bool isUpgraded;
    protected int value;
    protected bool isActiveTower;

    //Parameters of tower with location: valid location (giving locations to empty tiles didn't end up being very useful, but it might be in a future update)
    public Tower (Vector2 location)
    {
        //Store the passed location
        tower = new Sprite(location, null);
        
        //Set the variables to their default values
        isUpgraded = false;
        value = 0;
    }


    public Tower()
    {
        //Define the tower sprite with default variables
        tower = new Sprite(new Vector2(), null);

        //Set the variables to their default values
        isUpgraded = false;
        value = 0;
    }

    //Pre: None
    //Post: The tower sprite
    //Desc: Returns the tower sprite
    public Sprite GetTower()
    {
        //Return the tower sprite
        return tower;
    }

    //Pre: The new location
    //Post: None
    //Desc: Sets the location to the passed location
    public void SetLocation(Vector2 location)
    {
        //Set the tower's location to the specified location
        tower.SetLocation(location);
    }

    //Pre: None
    //Post: The location, a Vector2
    //Desc: Returns the location
    public Vector2 GetLocation()
    {
        //Return the tower's location
        return tower.GetLocation();
    }

    //Pre: None
    //Post: A string representing the tower type
    //Desc: Returns the tower type
    public virtual string GetTowerType()
    {
        //Return the tower type
        return "Empty";
    }

    //Pre: None
    //Post: Is the tower upgraded, a boolean
    //Desc: Returns if the tower is upgraded or not
    public bool IsUpgraded()
    {
        //Return if the tower is upgraded
        return isUpgraded;
    }

    //Pre: None
    //Post: None
    //Desc: Upgrades the tower
    public virtual void Upgrade()
    {
        //(This virtual method is never called)
    }

    //Pre: The enemy locations and delta time between frames
    //Post: If the tower eady to shoot a projectile, a boolean
    //Desc: Returns if the tower eady to shoot a projectile, resets timer if projectile is shot
    public bool ShootProjectile(List<Vector2> enemyLocations, GameTime deltaTime)
    {
        //Only active towers have the potential to shoot a projectile
        if (isActiveTower)
        {
            //If the timer is up, check if there is an enemy within the tower's visible range to shoot. Else, update the timer
            if (GetTimer() <= 0)
            {
                //Loop through the enemy locations
                for (int i = 0; i < enemyLocations.Count; i++)
                {
                    //If there is an enemy within the visible range of the tower, make the tower shoot a projectile
                    if (enemyLocations[i].Y == tower.GetLocation().Y && enemyLocations[i].X <= Projectile.GAME_WIDTH && tower.GetLocation().X < enemyLocations[i].X && enemyLocations[i].X - GetRange() - Game1.WIDTH <= tower.GetLocation().X)
                    {
                        //Reset the timer and return true
                        ResetTimer();
                        return true;
                    }
                }
            }
            else
            {
                //Update the timer
                UpdateTimer(deltaTime);
            }           
        }

        //Return false
        return false;
    }

    //Pre: A valid double representing damage
    //Post: None
    //Desc: Deals damage to the tower
    public virtual void TakeDamage(double damage)
    {
        //(This virtual method is never called)
    }

    //Pre: None
    //Post: The tower's health
    //Desc: Return's the tower's health
    public virtual double GetHealth()
    {
        //Return 1 (This virtual method is never called)
        return 1;
    }

    //Pre: None
    //Post: The tower's remaining uses, an integer
    //Desc: Return's the tower's remaining uses
    public virtual int GetUses()
    {
        //Return 1 (This virtual method is never called)
        return 1;
    }

    //Pre: None
    //Post: The tower's slow rate, a float
    //Desc: Return's the tower's slow rate
    public virtual float GetSlowRate()
    {
        //Return 1 (This virtual method is never called)
        return 1;
    }

    //Pre: None
    //Post: None
    //Desc: Updates uses
    public virtual void UpdateUses()
    {
        //Do nothing when called
    }

    //Pre: None
    //Post: The attack cooldown, an integer
    //Desc: returns the attack cooldown
    public virtual int GetCooldown()
    {
        //Return 0 (This virtual method is never called)
        return 0;
    }


    //Pre: None
    //Post: The timer, an int
    //Desc: Returns the timer
    public virtual int GetTimer()
    {
        //Return 0 (This virtual method is never called)
        return 0;
    }

    //Pre: None
    //Post: The range, an integer
    //Desc: Returns the range
    public virtual int GetRange()
    {
        //Return 0 (This virtual method is never called)
        return 0;
    }

    //Pre: None
    //Post: The AOE range, an integer
    //Desc: Returns the AOE range
    public virtual int GetAoeRange()
    {
        //Return 1 (This virtual method is never called)
        return -1;
    }

    //Pre: None
    //Post: The tower's damage, a double
    //Desc: Return's the tower's damage
    public virtual double GetDamage()
    {
        //Return 0 (This virtual method is never called)
        return 0;
    }

    //Pre: None
    //Post: The tower value, an integer
    //Desc: Returns the tower's value
    public int GetValue()
    {
        //Return the tower's value
        return value;
    }

    //Pre: None
    //Post: A boolean representing if the tower should be deleted
    //Desc: Returns a boolean representing if the tower should be deleted
    public bool DeleteThisTower()
    {
        //Return if the tower should be deleted by comparing the health to 0 (is the health smaller or equal to 0)
        return GetHealth() <= 0;
    }

    //Pre: None
    //Post: None
    //Desc: Resets the timer
    public virtual void ResetTimer()
    {
        //(This virtual method is never called)
    }

    //Pre: The delta time between frames
    //Post: None
    //Desc: Updates the times
    public virtual void UpdateTimer(GameTime deltaTime)
    {
        //(This virtual method is never called)
    }
}

///////////////////////////////////////////////////////////////////

//Author: Lyam Katz
//File Name: Towers.cs
//Project Name: Final Project Tower Defense Game - Lyam Katz
//Creation Date: 2021-06-05
//Modified Date: 2021-06-16
//Description: A class of towers

//Import namespaces
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class Towers
{
    //Store the 2d array of towers
    private Tower[,] towers;

    public Towers ()
    {
        //Define the array of towers
        towers = new Tower[4, 10];

        //Set the towers to empty tiles (Loop through all the rows)
        for (int i = 0; i < towers.GetLength(0); i++)
        {
            //Loop through the columns
            for(int j = 0; j < towers.GetLength(1); j++)
            {
                //Set the current tower to an empty tile
                towers[i, j] = new Tower(new Vector2(j * 158, i * 225));
            }
        }
    }

    //Pre: A valid new tower, the indexes of the tower
    //Post: None
    //Desc: Sets the tower with the passed indexes to the passed tower
    public void SetTower (int index1, int index2, Tower tower)
    {
        //Set a tower in the array to a new tower that was passed, to the passed indexes of the array
        towers[index1, index2] = tower;
        towers[index1, index2].SetLocation(new Vector2(index2 * 158, index1 * 225));
    }

    //Pre: The valid indexes of the tower
    //Post: The tower type, a string
    //Desc: Returns the tower type of the tower with the passed indexes
    public string GetTowerType(int index1, int index2)
    {
        //Return the tower type of the tower with the passed indexes
        return towers[index1, index2].GetTowerType();
    }

    //Pre: The valid indexes of the tower, valid damage
    //Post: None
    //Desc: Deals the passed damage to the tower with the passed indexes
    public void TakeDamage(int index1, int index2, double damage)
    {
        //Deal damage to the tower with the passed indexes
        towers[index1, index2].TakeDamage(damage);
    }

    //Pre: None
    //Post: None
    //Desc: Delete the destroyed towers
    public void DeleteDestroyedTowers()
    {
        //Loop through the rows
        for (int i = 0; i < towers.GetLength(0); i++)
        {
            //Loop through the columns
            for (int j = 0; j < towers.GetLength(1); j++)
            {
                //Delete the current tower if it is destroyed
                if (towers[i, j].DeleteThisTower())
                {
                    //Set the current tower to an empty tile
                    towers[i, j] = new Tower(new Vector2(j * 158, i * 225));
                }
            }
        }
    }

    //Pre: The valid enemy locations, content manager, and delta time between frames
    //Post: A list of projectiles shot this update from the towers
    //Desc: Returns a list of projectiles shot this update from the towers
    public List<Projectile> GetProjectiles(List<Vector2> enemyLocations, ContentManager content, GameTime deltaTime)
    {
        //Store the list of projectiles shot this update
        List<Projectile> projectiles = new List<Projectile>();

        //Loop through the rows
        for (int i = 0; i < towers.GetLength(0); i++)
        {
            //Loop through the columns
            for (int j = 0; j < towers.GetLength(1); j++)
            {
                //If the current tower shot a projectile, add the projectile to the list of projectiles to shoot this update
                if (towers[i, j].ShootProjectile(enemyLocations, deltaTime))
                {
                    //Add the projectile to the list of projectiles to shoot this update
                    projectiles.Add(new Projectile(content, towers[i, j].GetLocation(), towers[i, j].GetDamage(), towers[i, j].GetAoeRange() == -1, towers[i, j].GetRange(), towers[i, j].GetAoeRange()));
                }
            }
        }

        //Return the list of projectiles
        return projectiles;
    }

    //Pre: The valid indexes of the tower
    //Post: The tower location at the passed indexes, a Vector2
    //Desc: Returns the tower location at the passed indexes, a Vector2
    public Vector2 GetLocation(int index1, int index2)
    {
        //Return the location of the tower with the passed indexes
        return towers[index1, index2].GetTower().GetLocation();
    }

    //Pre: The valid indexes of the tower
    //Post: The slow rate at the passed indexes, a float
    //Desc: Returns the tower slow rate at the passed indexes, a float
    public float GetSlowRate(int index1, int index2)
    {
        //Return the slow rate of the tower with the passed indexes
        return towers[index1, index2].GetSlowRate();
    }

    //Pre: The valid indexes of the tower
    //Post: A boolean representing if the tower at the passed indexes was upgraded
    //Desc: Returns a boolean representing if the tower at the passed indexes was upgraded
    public bool IsUpgraded(int index1, int index2)
    {
        //Return if the tower with the passed indexes is upgraded
        return towers[index1, index2].IsUpgraded();
    }

    //Pre: The valid indexes of the tower
    //Post: None
    //Desc: Upgrades the tower at the passed indexes
    public void Upgrade(int index1, int index2)
    {
        //Upgrade the tower with the passed indexes
        towers[index1, index2].Upgrade();
    }

    //Pre: The valid indexes of the tower
    //Post: The timer of the tower at the passed indexes
    //Desc: Returns the timer of the tower at the passed indexes
    public int GetTimer(int index1, int index2)
    {
        //Return the timer of the tower with the passed indexes
        return towers[index1, index2].GetTimer();
    }

    //Pre: The valid indexes of the tower, the valid delta time between frames
    //Post: None
    //Desc: Updates the timer at the tower with the passed indexes
    public void UpdateTimer(int index1, int index2, GameTime deltaTime)
    {
        //Update the timer of the tower with the passed indexes
        towers[index1, index2].UpdateTimer(deltaTime);
    }

    //Pre: The valid indexes of the tower
    //Post: The uses remaining of the tower with the passed indexes, an int
    //Desc: Returns the uses remaining of the tower with the passed indexes
    public int GetUses(int index1, int index2)
    {
        //Return the remaining uses of the tower with the passed indexes
        return towers[index1, index2].GetUses();
    }

    //Pre: The valid indexes of the tower
    //Post: None
    //Desc: Updates the uses of the tower with the passed indexes
    public void UpdateUses(int index1, int index2)
    {
        //Update the uses of the tower with the passed indexes
        towers[index1, index2].UpdateUses();
    }

    //Pre: None
    //Post: The total gold value of all the towers
    //Desc: Returns the total gold value of all the towers
    public int GetValue()
    {
        //Store the total gold value of all the towers
        int value = 0;

        //Loop through the rows
        for (int i = 0; i < towers.GetLength(0); i++)
        {
            //Loop through the columns
            for (int j = 0; j < towers.GetLength(1); j++)
            {
                //Increment the total value by the value of the current tower (empty tiles and destroyed mud traps that haven't disappered yet return a value of 0)
                value += towers[i, j].GetValue();
            }
        }

        //Return the total value
        return value;
    }

    //Pre: The valid indexes of the tower
    //Post: The value of the tower with the passed indexes, an int
    //Desc: Returns the value of the tower with the passed indexes
    public int GetValue(int index1, int index2)
    {
        //Return the gold value of the tower with the passed indexes
        return towers[index1, index2].GetValue();
    }

    //Pre: A valid spritebatch
    //Post: None
    //Desc: Draws all the towers
    public void Draw(SpriteBatch sprite)
    {
        //Loop through the rows
        for (int i = 0; i < towers.GetLength(0); i++)
        {
            //Loop through the columns
            for (int j = 0; j < towers.GetLength(1); j++)
            {
                //If the current tower isn't an empty tile, draw it
                if (towers[i, j].GetTower().GetTexture() != null)
                {
                    //Draw the current tower
                    sprite.Draw(towers[i, j].GetTower().GetTexture(), towers[i, j].GetTower().GetLocation(), Color.White);
                }
            }
        }
    }
}
