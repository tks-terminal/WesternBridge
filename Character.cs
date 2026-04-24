using System;

public class Character
{
    //Private Variables, private so code outside this class cannot directly change 
    private string _name;
    private int _health;
    private bool _isAlive;

    //This gives access to _name
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    //This gives access to _health
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    //This gives access to _isAlive
    public bool IsAlive
    {
        get { return _isAlive; }
        set { _isAlive = value; }
    }

    //Constructor to initialize name & health
    public Character(string name, int health)
    {
        _name = name;
        _health = health;
        _isAlive = true;
    }

    //Damage method
    public void TakeDamage(int amount)
    {
        _health -= amount;//subtract health

        if (_health <= 0)
        {
            _health = 0; //set to 0 to avoid negative health
            _isAlive = false;
        }
    }

    //Heal method
    public void Heal(int amount)
    {
        _health += amount;//adds health
    }


    public virtual void DisplayStats()
    {
        //Show the characters name
        Console.WriteLine($"Name: {_name}");

        //Show the characters health
        Console.WriteLine($"Health: {_health}");

        //Show whether the character is alive
        Console.WriteLine($"Alive: {_isAlive}");
    }
}
