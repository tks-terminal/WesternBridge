using System;

public class Player : Character //subclass of Character - Player inherits from Character
{
    private int _distanceAway;
    private int _foodAmount;
    private int _medicineAmount;
    private int _wagonHealth;
    //private int _goldAmount;

    public int DistanceAway
    {
        get { return _distanceAway; }//return distance away
        set { _distanceAway = value; }//set distance away + or - when traveling
    }

    public int FoodAmount
    {
        get { return _foodAmount; } //return food amount
        set { _foodAmount = value; } //set food amount when consuming or finding food
    }

    public int MedicineAmount
    {
        get { return _medicineAmount; } //return medicine amount
        set { _medicineAmount = value; } //set medicine amount when using or finding medicine
    }

    public int WagonHealth
    {
        get { return _wagonHealth; }//return wagon health
        set { _wagonHealth = value; }//set wagon health when resting or taking damage
    }


    //constructor
    public Player(string name, int health, int distanceAway, int foodAmount, int medicineAmount, int wagonHealth) : base(name, health) //inheriting name and health
    {
        _distanceAway = distanceAway;
        _foodAmount = foodAmount;
        _medicineAmount = medicineAmount;
        _wagonHealth = wagonHealth;
    }

    //This method handles the Travel action It uses food
    public void Travel()
    {
        _distanceAway -= 2;
        _foodAmount -= 2;
    }

    //This method handles the Resting action
    public void Rest()
    {
        Heal(2); //Heal(2) is inherited from the Character class.
        _foodAmount -= 1;
        _wagonHealth += 1;
    }

    //This method handles the Hunt action
    public void Hunt(Random random)
    {
        int result = random.Next(1, 101); //Generate a random number from 1 to 100

        //If the random number is 85 or less the hunt succeeds
        if (result <= 85)
        {
            _foodAmount += 4;
        }
        else //if number is greater than 85 the hunt fails and the player takes damage
        {
            TakeDamage(1);
        }
    }

    //This method handles the Scavenge action
    public void Scavenge(Random random)
    {
        int result = random.Next(1, 4); //Generate a random number from 1 to 3

        //If result is 1 the player finds food.
        if (result == 1)
        {
            _foodAmount += 1;
        }
        //If result is 2 the player finds medicine.
        else if (result == 2)
        {
            _medicineAmount += 1;
        }
        //If result is 3 the player finds nothing
        else
        {
            //Console.WriteLine("You searched everywhere but found nothing, someone scavenged this area.");
        }
    }



    //This method lets the player use medicine
    public void UseMedicine()
    {
        if (_medicineAmount > 0)
        {
            _medicineAmount -= 1;
            Heal(3);
        }
    }

    //This method overrides the DisplayStats method from the Character class
    public override void DisplayStats()
    {
        //Stats from the Character base class
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Health: {Health}");
        //Console.WriteLine($"Alive: {IsAlive}");

        //These are the player class stats
        Console.WriteLine($"Miles from Western Bridge: {_distanceAway}");
        Console.WriteLine($"Food Amount: {_foodAmount}");
        Console.WriteLine($"Medicine Amount: {_medicineAmount}");
        Console.WriteLine($"Wagon Health: {_wagonHealth}");
    }
}
