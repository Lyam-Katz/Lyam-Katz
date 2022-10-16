//Author:
//FileName:
//Project Name:
//Creation Date:
//Modified Date:
//Description:

using System;
using System.Collections.Generic;

class MainClass : AbstractGame
{
  //Game States - Add/Remove/Modify as needed
  //These are the most common game states, but modify as needed
  //You will ALSO need to modify the two switch statements in Update and Draw
  private const int MENU = 0;
  private const int SETTINGS = 1;
  private const int INSTRUCTIONS = 2;
  private const int GAMEPLAY = 3;
  private const int PAUSE = 4;
  private const int ENDGAME = 5;
  private const int NUM_BOTS = 50;

    private const int UP = 1;
private const int LEFT = 2;
private const int DOWN = 3;
private const int RIGHT = 4;

  //Choose between UI_RIGHT, UI_LEFT, UI_TOP, UI_BOTTOM, UI_NONEs
  private static int uiLocation = Helper.UI_BOTTOM;

  ////////////////////////////////////////////
  //Set the game and user interface dimensions
  ////////////////////////////////////////////

  //Min: 5 top/bottom, 10 left/right, Max: 30
  private static int uiSize = 5;

  //On VS: Max: 120 - uiSize, UI_NONE gives full width up to 120
  //On Repl: Max: 80 - uiSize, UI_NONE can give full width up to 80
  private static int gameWidth = 80;

  //On VS: Max: 50 - uiSize, UI_NONE gives full height up to 50
  //On Repl: Max: 24 - uiSize, UI_NONE can give full height up to 24
  private static int gameHeight = 24;

  //Store and set the initial game state, typically MENU to start
  int gameState = GAMEPLAY;

  ////////////////////////////////////////////
  //Define your Global variables here (They do NOT need to be static)
  ////////////////////////////////////////////
    int numFinished;
    Image botImg;
    Image targetImg;
    Image bestBotImg;
    int moveNumber;
    List<String> parentPaths = new List<String>();
    List<int> indexOfFinished = new List<int>();
    List<GameObject> bots = new List<GameObject>();
    List<String> botPaths = new List<String>();
    bool useBest;
    List<bool> isFollowingParent = new List <bool>();
    List<int> moveToDeviate = new List <int>();
    GameObject bestBot;
    UITextObject currLengthBest;
    String bestBotPath;
    GameObject target;
  static void Main(string[] args)
  {
    /***************************************************************
                DO NOT TOUCH THIS SECTION
    ***************************************************************/
    GameContainer gameContainer = new GameContainer(new MainClass(), uiLocation, uiSize, gameWidth, gameHeight);
    gameContainer.Start();
  }

  public override void LoadContent(GameContainer gc)
  {
    botImg = Helper.LoadImage("Images/bot.txt");
    bestBotImg = Helper.LoadImage("Images/bestbot.txt");
    targetImg = Helper.LoadImage("Images/target.txt");
    numFinished = 0;
    moveNumber = 0;
useBest = false;
bestBotPath = "";
    currLengthBest = new UITextObject(gc, 5, 2, Helper.YELLOW, true, $"Current Length of best path: {bestBotPath.Length} Best possible length: 26");
    target = new GameObject(gc, targetImg, 13, 13, true);
    bestBot = new GameObject(gc, bestBotImg, 0, 0, false);
    for (int i = 0; i < NUM_BOTS; i++)
{
    bots.Add(new GameObject(gc, botImg, 0, 0, true));
    parentPaths.Add("");
    botPaths.Add("");
    moveToDeviate.Add(0);
    isFollowingParent.Add(false);
}
    
    //Load all of your "Images", GameObjects and UIObjects here.
    //This is also the place to setup any other aspects of your program before the game loop starts

  }

  public override void Update(GameContainer gc, float deltaTime)
  {

    //This will exit your program with the x key.  You may remove this if you want       
    if (Input.IsKeyDown(ConsoleKey.X)) gc.Stop();

    switch (gameState)
    {
      case MENU:
        //Get and implement menu interactions
        break;
      case SETTINGS:
        //Get and apply changes to game settings
        break;
      case INSTRUCTIONS:
        //Get user input to return to MENU
        break;
      case GAMEPLAY:
        
        UpdateGamePlay(gc, deltaTime);

        
        //Implement standared game logic (input, update game objects, apply physics, collision detection)
        break;
      case PAUSE:
        //Get user input to resume the game
        break;
      case ENDGAME:
        //Wait for final input based on end of game options (end, restart, etc.)
        break;
    }
  }

  public override void Draw(GameContainer gc)
  {
    //NOTE: The only logic in this section should be draw commands and loops.
    //There may be some minor selection, but choosing what to draw 
    //should be handled in the Update and the visibility property 
    //of GameObject's

    switch (gameState)
    {
      case MENU:
        //Draw the possible menu options
        break;
      case SETTINGS:
        //Draw the settings with prompts
        break;
      case INSTRUCTIONS:
        //Draw the game instructions including prompt to return to MENU
        break;
      case GAMEPLAY:
        gc.DrawToUserInterface(currLengthBest);
        gc.DrawToMidground(target);
        gc.DrawToForeground(bestBot);
        for (int i = 0; i < NUM_BOTS; i++)
{
gc.DrawToMidground(bots[i]);
}
        //Draw all game objects on each layers (background, middleground, foreground and user interface)
        break;
      case PAUSE:
        //Draw the pause screen, this may include the full game drawing behind
        break;
      case ENDGAME:
        //Draw the final feedback and prompt for available options (exit,restart, etc.)
        break;
    }
  }
    private void UpdateGamePlay(GameContainer gc, float deltaTime)
    {
    currLengthBest.UpdateText($"Current Length of best path: {bestBotPath.Length} Best possible length: 26");
    if (numFinished >= 10){
bestBot.SetVisibility(true);
bestBot.SetPosition(0,0);
moveNumber = 0;
numFinished = 0;
String path10 = "";
if (bestBotPath.Length == 0){
bestBotPath = botPaths[indexOfFinished[0]];
}
else if(useBest){
if (bestBotPath.Length >= botPaths[indexOfFinished[0]].Length)
{
bestBotPath = botPaths[indexOfFinished[0]];
}
path10 = bestBotPath;

}
else{
bestBotPath = botPaths[indexOfFinished[0]];
  path10 = botPaths[indexOfFinished[9]];
}
  String path1 = botPaths[indexOfFinished[0]];
  String path2 = botPaths[indexOfFinished[1]];
  String path3 = botPaths[indexOfFinished[2]];
  String path4 = botPaths[indexOfFinished[3]];
  String path5 = botPaths[indexOfFinished[4]];
  String path6 = botPaths[indexOfFinished[5]];
  String path7 = botPaths[indexOfFinished[6]];
  String path8 = botPaths[indexOfFinished[7]];
  String path9 = botPaths[indexOfFinished[8]];
    

useBest = false;
    indexOfFinished.Clear();
    for (int i = 0; i < NUM_BOTS/10; i++)
{
    bots[i] = new GameObject(gc, botImg, 0, 0, true);
    botPaths[i] = "";
    parentPaths[i] = path1;
    
}

for (int i = 5; i < NUM_BOTS/10 + 5; i++)
{
    bots[i] = new GameObject(gc, botImg, 0, 0, true);
    botPaths[i] = "";
    parentPaths[i] = path2;
    
}
for (int i = 10; i < NUM_BOTS/10 + 10; i++)
{
    bots[i] = new GameObject(gc, botImg, 0, 0, true);
    botPaths[i] = "";
    parentPaths[i] = path3;
    
}
for (int i = 15; i < NUM_BOTS/10 + 15; i++)
{
    bots[i] = new GameObject(gc, botImg, 0, 0, true);
    botPaths[i] = "";
    parentPaths[i] = path4;
    
}
for (int i = 20; i < NUM_BOTS/10 + 20; i++)
{
    bots[i] = new GameObject(gc, botImg, 0, 0, true);
    botPaths[i] = "";
    parentPaths[i] = path5;
    
}
for (int i = 25; i < NUM_BOTS/10 + 25; i++)
{
    bots[i] = new GameObject(gc, botImg, 0, 0, true);
    botPaths[i] = "";
    parentPaths[i] = path6;
    
}
for (int i = 30; i < NUM_BOTS/10 + 30; i++)
{
    bots[i] = new GameObject(gc, botImg, 0, 0, true);
    botPaths[i] = "";
    parentPaths[i] = path7;
    
}
for (int i = 35; i < NUM_BOTS/10 + 35; i++)
{
    bots[i] = new GameObject(gc, botImg, 0, 0, true);
    botPaths[i] = "";
    parentPaths[i] = path8;
    
}
for (int i = 40; i < NUM_BOTS/10 + 40; i++)
{
    bots[i] = new GameObject(gc, botImg, 0, 0, true);
    botPaths[i] = "";
    parentPaths[i] = path9;
    
}
for (int i = 45; i < NUM_BOTS/10 + 45; i++)
{
    bots[i] = new GameObject(gc, botImg, 0, 0, true);
    botPaths[i] = "";
    parentPaths[i] = path10;
    
}

    
    }

        var rng = new Random();
        for (int i = 0; i < NUM_BOTS; i++)

{
if (moveNumber >= moveToDeviate[i]){
isFollowingParent[i] = false;
}

if(isFollowingParent[i]){
switch(parentPaths[i][moveNumber])
{
case '1':

     bots[i].SetPosition(bots[i].GetPos().x, bots[i].GetPos().y - 1);
        botPaths[i] = botPaths[i] + "1";
if (bots[i].GetPos().x == 13 && bots[i].GetPos().y == 13){
numFinished ++;
indexOfFinished.Add(i);
bots[i].SetVisibility(false);

}
         
    break;
  case '2':
    
     bots[i].SetPosition(bots[i].GetPos().x - 1, bots[i].GetPos().y);
botPaths[i] = botPaths[i] + "2";
if (bots[i].GetPos().x == 13 && bots[i].GetPos().y == 13){
numFinished ++;
indexOfFinished.Add(i);
bots[i].SetVisibility(false);
}
          
    break;
  case '3':
    
     bots[i].SetPosition(bots[i].GetPos().x, bots[i].GetPos().y + 1);
botPaths[i] = botPaths[i] + "3";
if (bots[i].GetPos().x == 13 && bots[i].GetPos().y == 13){
numFinished ++;
indexOfFinished.Add(i);
bots[i].SetVisibility(false);
}
         
    break;
  case '4':
     
     bots[i].SetPosition(bots[i].GetPos().x + 1, bots[i].GetPos().y);
botPaths[i] = botPaths[i] + "4";
if (bots[i].GetPos().x == 13 && bots[i].GetPos().y == 13){
numFinished ++;
indexOfFinished.Add(i);
bots[i].SetVisibility(false);
}
    
    break;

}
}
if (!isFollowingParent[i] && (bots[i].GetPos().x != 13 || bots[i].GetPos().y != 13))
{
jump:
        switch(rng.Next(1,5))
  {
  case UP:
      if (bots[i].GetPos().y >= 1)
    {
     bots[i].SetPosition(bots[i].GetPos().x, bots[i].GetPos().y - 1);
        botPaths[i] = botPaths[i] + "1";
if (bots[i].GetPos().x == 13 && bots[i].GetPos().y == 13){
numFinished ++;
indexOfFinished.Add(i);
bots[i].SetVisibility(false);

}
    }      
    break;
  case LEFT:
      if (bots[i].GetPos().x >= 1)
    {
     bots[i].SetPosition(bots[i].GetPos().x - 1, bots[i].GetPos().y);
botPaths[i] = botPaths[i] + "2";
if (bots[i].GetPos().x == 13 && bots[i].GetPos().y == 13){
numFinished ++;
indexOfFinished.Add(i);
bots[i].SetVisibility(false);
}
    }      
    break;
  case DOWN:
      if (bots[i].GetPos().y <= 19)
    {
     bots[i].SetPosition(bots[i].GetPos().x, bots[i].GetPos().y + 1);
botPaths[i] = botPaths[i] + "3";
if (bots[i].GetPos().x == 13 && bots[i].GetPos().y == 13){
numFinished ++;
indexOfFinished.Add(i);
bots[i].SetVisibility(false);
}
    }       
    break;
  case RIGHT:
      if (bots[i].GetPos().x <= 79)
    {
     bots[i].SetPosition(bots[i].GetPos().x + 1, bots[i].GetPos().y);
botPaths[i] = botPaths[i] + "4";
if (bots[i].GetPos().x == 13 && bots[i].GetPos().y == 13){
numFinished ++;
indexOfFinished.Add(i);
bots[i].SetVisibility(false);
}
    }    
    break;
default:
    goto jump;
break;

  }
}
       // bots[i].SetPosition()
}
if(bestBot.GetVisibility()){
switch(bestBotPath[moveNumber])
{
case '1':

     bestBot.SetPosition(bestBot.GetPos().x, bestBot.GetPos().y - 1);

if (bestBot.GetPos().x == 13 && bestBot.GetPos().y == 13){
numFinished ++;
useBest = true;
bestBot.SetVisibility(false);

}
         
    break;
  case '2':
    
     bestBot.SetPosition(bestBot.GetPos().x - 1, bestBot.GetPos().y);
if (bestBot.GetPos().x == 13 && bestBot.GetPos().y == 13){
numFinished ++;
useBest = true;
bestBot.SetVisibility(false);
}
          
    break;
  case '3':
    
     bestBot.SetPosition(bestBot.GetPos().x, bestBot.GetPos().y + 1);
if (bestBot.GetPos().x == 13 && bestBot.GetPos().y == 13){
numFinished ++;
useBest = true;
bestBot.SetVisibility(false);
}
         
    break;
  case '4':
     
    bestBot.SetPosition(bestBot.GetPos().x + 1, bestBot.GetPos().y);
if (bestBot.GetPos().x == 13 && bestBot.GetPos().y == 13){
numFinished ++;
useBest = true;
bestBot.SetVisibility(false);
}
    
    break;
}

}
moveNumber ++;
    
}
}
