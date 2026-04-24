using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

public class Game
{
    private Player _currentPlayer;
    private List<Scenario> _scenarios; //scenario list
    private bool _isRunning; //game running state
    private Random _random; //random number gen

    public Player CurrentPlayer
    {
        get { return _currentPlayer; }
        set { _currentPlayer = value; }
    }


    public List<Scenario> Scenarios
    {
        get { return _scenarios; }
        set { _scenarios = value; }
    }


    public bool IsRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }

    //Constructor
    public Game()
    {
        _currentPlayer = null;//null meaning not created yet
        _scenarios = new List<Scenario>();//emptyh list of scenarios to be filled from text file
        _isRunning = true;//game starts in running state
        _random = new Random();
    }

    //Starts the game
    public void StartGame()
    {
        Console.Clear();

        Console.WriteLine("==================================");
        Console.WriteLine("        WESTERN BRIDGE");
        Console.WriteLine("==================================");
        Console.WriteLine();

        TypeText("Press Enter to start...", 40);
        Console.ReadLine();

        Console.Clear();

        string playerName = "";

        while (playerName == "")
        {
            TypeText("Enter your Traveler's name: ", 30);
            playerName = Console.ReadLine();

            if (playerName == "")
            {
                TypeText("Please enter a valid name.", 30);
                TypeText("Press Enter to try again...", 30);
                Console.ReadLine();
                Console.Clear();
            }
        }

        //Create the player
        _currentPlayer = new Player(playerName, 10, 20, 10, 2, 10);//parameters: name, health, distance, food, medicine, wagon health

        //Load scenarios from the text file
        LoadScenariosFromFile();

        Console.Clear();

        Console.WriteLine("==================================");
        Console.WriteLine("        WESTERN BRIDGE");
        Console.WriteLine("==================================");
        Console.WriteLine();

        TypeText($"Welcome, {playerName}.", 30);
        Console.WriteLine();
        TypeText("The frontier is expanding westward, and new land waits ahead.", 15);
        TypeText("You will travel through the Wild West in search of a better future.", 15);
        TypeText("Your choices will decide whether you survive the journey.", 15);
        Console.WriteLine();
        TypeText("Press Enter to continue...", 30);
        Console.ReadLine();

        //Main game loop
        while (_isRunning)
        {
            ShowMenu();
            CheckGameOver();
        }
    }

    //Loads scenarios from scenarios.txt
    public void LoadScenariosFromFile()
    {
        string filePath = "Scenario.txt";

        foreach (string line in File.ReadAllLines(filePath))
        {
            //Skip comment lines
            if (!line.StartsWith("#"))
            {
                string[] parts = line.Split('|');//split the line into parts using | and pout them into array

                if (parts.Length == 4)//lenght of array should be 4 
                {
                    string description = parts[0];
                    bool isGood = bool.Parse(parts[1]);//convert string to bool
                    string effectType = parts[2];
                    int effectAmount = int.Parse(parts[3]);//convert string to int

                    Scenario newScenario = new Scenario(description, isGood, effectType, effectAmount);
                    _scenarios.Add(newScenario);
                }
            }
        }
    }

    //Shows player stats and action menu
    public void ShowMenu()
    {
        Console.Clear();

        Console.WriteLine("==================================");
        Console.WriteLine("        WESTERN BRIDGE");
        Console.WriteLine("==================================");
        Console.WriteLine();

        _currentPlayer.DisplayStats();

        Console.WriteLine();
        Console.WriteLine("Choose an action for the day:");
        Console.WriteLine("1. Travel");
        Console.WriteLine("2. Rest");
        Console.WriteLine("3. Hunt");
        Console.WriteLine("4. Scavenge");
        Console.WriteLine("5. Use Medicine");
        Console.WriteLine("6. Exit");
        Console.WriteLine();
        Console.Write("Choice: ");

        string choice = Console.ReadLine();

        Console.Clear();

        if (choice == "1")
        {
            _currentPlayer.Travel();//travel method is in player class
            TypeText("You travel farther down the trail.", 30);
            TypeText("You've traveled 2 miles closer to the Western Bridge.", 30);
            if (_currentPlayer.FoodAmount > 0)
            {
                TypeText("Food Amount decreased by 2.", 30);
            }
            else
            {
                TypeText("You have no food to eat during your travel.", 30);
                TypeText("Health decreased by 3 due to starvation.", 30);
            }
            TriggerRandomScenario();
        }
        else if (choice == "2")
        {
            _currentPlayer.Rest();//rest method is in player class
            TypeText("You stop to rest and recover.", 30);
            
            if (_currentPlayer.FoodAmount > 0)
            {
                TypeText("Food Amount decreased by 1.", 30);
                TypeText("Health increased by 2.", 30);
            }
            else
            {
                TypeText("You have no food to eat while resting.", 30);
                TypeText("Health decreased by 3 due to starvation.", 30);
            }
            TypeText("Wagon Health increased by 1.", 30);
        }
        else if (choice == "3")
        {
            int foodBefore = _currentPlayer.FoodAmount;
            int healthBefore = _currentPlayer.Health;

            _currentPlayer.Hunt(_random);//hunt method is in player class and takes random as parameter bc random is in game constructor
            TypeText("You spend the day hunting.", 30);

            if (_currentPlayer.FoodAmount > foodBefore)//stops the problem of saying food increased by 0 if hunt fails and also only displays if food actually increases
            {
                TypeText($"Hunt Successful! Food Amount increased by {_currentPlayer.FoodAmount - foodBefore}.", 30);
            }
            else if (_currentPlayer.Health < healthBefore)//if health decreased from hunting then display that
            {
                TypeText($"You were injured during the hunt. Health decreased by {healthBefore - _currentPlayer.Health}.", 30);
            }
        }
        else if (choice == "4")
        {
            int foodBefore = _currentPlayer.FoodAmount;
            int medicineBefore = _currentPlayer.MedicineAmount;

            _currentPlayer.Scavenge(_random);//scavenge method is in player class and takes random
            TypeText("You search the area for supplies.", 30);

            if (_currentPlayer.FoodAmount > foodBefore)
            {
                TypeText($"Food Amount increased by {_currentPlayer.FoodAmount - foodBefore}.", 30);
            }
            else if (_currentPlayer.MedicineAmount > medicineBefore)
            {
                TypeText($"Medicine Amount increased by {_currentPlayer.MedicineAmount - medicineBefore}.", 30);
            }
            else
            {
                TypeText("You found nothing while scavenging, area was already looted.", 30);
            }

        }
        else if (choice == "5")
        {
            int medicineBefore = _currentPlayer.MedicineAmount;
            int healthBefore = _currentPlayer.Health;
            _currentPlayer.UseMedicine();//use medicine method is in player class
            if (_currentPlayer.MedicineAmount < medicineBefore)
            {
                TypeText("You used medicine.", 30);
                TypeText($"Medicine Amount decreased by {medicineBefore - _currentPlayer.MedicineAmount}.", 30);
                TypeText($"Health increased by {_currentPlayer.Health - healthBefore}.", 30);
            }
            else
            {
                TypeText("You have no medicine to use.", 30);
            }

        }
        else if (choice == "6")
        {
            _isRunning = false;//stops game loop
            TypeText("Thanks for playing Western Bridge.", 30);
        }
        else
        {
            TypeText("Invalid choice.", 30);
        }

        //Keep stats from dropping below 0
        FixStats();//method 

        if (_isRunning)
        {
            Console.WriteLine();
            TypeText("Press Enter to continue...", 30);
            Console.ReadLine();
        }
    }


    //Picks and applies a random scenario
    public void TriggerRandomScenario()
    {
        if (_scenarios.Count == 0)//if no scenarios loaded then leave method
        {
            return;
        }

        int randomIndex = _random.Next(_scenarios.Count);//picks random scenario
        Scenario chosenScenario = _scenarios[randomIndex];//takes random index and saves it in chosenScenario variable

        Console.WriteLine();
        TypeText("!!!A random event occurs!!!", 30);
        Console.WriteLine();

        chosenScenario.DisplayScenario();//from the scenario class
        chosenScenario.ApplyToPlayer(_currentPlayer);//from the scenario class
        
        Console.WriteLine();

        if (chosenScenario.EffectAmount > 0)
        {
        TypeText($"{chosenScenario.EffectType} increased by {chosenScenario.EffectAmount}.", 30);
        }
        else
        {
        TypeText($"{chosenScenario.EffectType} decreased by {Math.Abs(chosenScenario.EffectAmount)}.", 30);//math abs makes positive for display purposes
        }
    }

    //Checks win/lose conditions
    public void CheckGameOver()
    {
        if (_currentPlayer.DistanceAway <= 0)
        {
            Console.Clear();
            TypeText("You have reached Western Bridge.", 30);
            TypeText("You survived the journey west.", 30);
            _isRunning = false;
        }
        else if (_currentPlayer.Health <= 0 || !_currentPlayer.IsAlive)//if health is 0 or player dead
        {
            Console.Clear();
            TypeText("You died trying to reach the Western Bridge.", 30);
            _isRunning = false;
        }
        else if (_currentPlayer.WagonHealth <= 0)
        {
            Console.Clear();
            TypeText("Your wagon broke down and you couldn't continue.", 30);
            TypeText("You failed trying to reach the Western Bridge.", 30);
            _isRunning = false;
        }
    }

    //Prevents stats from going below 0 having a problem where if player takes damage and health goes
    //negative it causes issues with game over conditions so this method makes sure stats dont go below 0
    public void FixStats()
    {
        if (_currentPlayer.DistanceAway < 0)
        {
            _currentPlayer.DistanceAway = 0;
        }

        if (_currentPlayer.FoodAmount < 0)
        {
            _currentPlayer.FoodAmount = 0;
        }

        if (_currentPlayer.MedicineAmount < 0)
        {
            _currentPlayer.MedicineAmount = 0;
        }

        if (_currentPlayer.WagonHealth < 0)
        {
            _currentPlayer.WagonHealth = 0;
        }

        if (_currentPlayer.Health < 0)
        {
            _currentPlayer.Health = 0;
        }
    }

    //Typewriter effect
    public void TypeText(string text, int speed)
    {
        foreach (char letter in text)
        {
            Console.Write(letter);
            Thread.Sleep(speed);
        }

        Console.WriteLine();
    }
}
