using System;
using System.Threading;
//20 = fast
//30 = nice
//40 = slower
//60 = dramatic
//threading text "typing out" delay source cited in Sources.txt
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
    TypeText("Enter your travler's name: ", 30);
    playerName = Console.ReadLine();

    if (playerName == "")
    {
        TypeText("Please enter a valid name.", 20);
        TypeText("Press Enter to try again...", 20);
        Console.ReadLine();
        Console.Clear();
    }
}

Console.Clear();

Console.WriteLine("==================================");
Console.WriteLine("        WESTERN BRIDGE");
Console.WriteLine("==================================");
Console.WriteLine();

TypeText($"Welcome, {playerName}.", 30);
Console.WriteLine();
TypeText("The frontier is expanding westward, and new land waits ahead.", 15);
TypeText("You will travel through the Wild West in search of a better future.", 15);
TypeText("Your choices will decide if you survive the journey.", 15);
Console.WriteLine();
TypeText("Press Enter to continue...", 30);
Console.ReadLine();

Console.Clear();

static void TypeText(string text, int speed)
{
    foreach (char letter in text)
    {
        Console.Write(letter);
        Thread.Sleep(speed);
    }

    Console.WriteLine();
}
