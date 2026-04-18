using System;
using System.IO;
using System.Collections.Generic;

string filePath = "scenarios.txt";


List<string> scenarios = new List<string>();
foreach (string line in File.ReadAllLines(filePath))
{
    if (!line.StartsWith("#"))//if line dosnt start with # add to list of scenarios
    {
        scenarios.Add(line);
    }
}

Random random = new Random();
int randomScenario = random.Next(scenarios.Count);

Console.WriteLine("Random Scenario:");
Console.WriteLine(scenarios[randomScenario]);
