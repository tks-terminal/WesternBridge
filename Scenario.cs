using System;

public class Scenario
{
    //this private field stores the text description of the scenario
    private string _description;

    //This private field stores whether the scenario is good or bad
    private bool _isGood; //true = good, false = bad

    //This private field stores the name of the stat that the scenario changes
    private string _effectType; //ex "Health" "FoodAmount" "MedicineAmount" "WagonHealth" "DistanceAway"

    //this private field stores how much the chosen stat changes
    private int _effectAmount; // +/- number that changes the stat by


    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public bool IsGood
    {
        get { return _isGood; }
        set { _isGood = value; }
    }

    public string EffectType
    {
        get { return _effectType; }
        set { _effectType = value; }
    }

    public int EffectAmount
    {
        get { return _effectAmount; }
        set { _effectAmount = value; }
    }

    //constructor
    public Scenario(string description, bool isGood, string effectType, int effectAmount)
    {
        _description = description;
        _isGood = isGood;
        _effectType = effectType;
        _effectAmount = effectAmount;
    }

    //This method displays the scenario description
    public void DisplayScenario()
    {
        Console.WriteLine(_description);
    }

    
    public void ApplyToPlayer(Player player)
    {
        //if the effect type is Health change health
        if (_effectType == "Health")
        {
            player.Health += _effectAmount;
        }
        //food amount
        else if (_effectType == "FoodAmount")
        {
            player.FoodAmount += _effectAmount;
        }
        //medicine amount
        else if (_effectType == "MedicineAmount")
        {
            player.MedicineAmount += _effectAmount;
        }
        //wagon health
        else if (_effectType == "WagonHealth")
        {
            player.WagonHealth += _effectAmount;
        }
        //distance away
        else if (_effectType == "DistanceAway")
        {
            player.DistanceAway += _effectAmount;
        }
    }
}
