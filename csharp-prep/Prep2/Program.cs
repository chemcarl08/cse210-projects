using System;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage? ");
        string percentage = Console.ReadLine();
        int percent = int.Parse(percentage);

        string lettergrade = "";

        if (percent >= 90)
        {
            lettergrade = "A";
        }
        else if (percent >= 80)
        {
            lettergrade = "B";
        }
        else if (percent >= 70)
        {
            lettergrade = "C";
        }
        else if (percent >= 60)
        {
            lettergrade = "D";
        }
        else
        {
            lettergrade = "F";
        }

        Console.WriteLine($"Your grade is: {lettergrade}.");

        if (percent >=70)
        {
            Console.WriteLine("You passed!");
        }
        else
        {
            Console.WriteLine("Failed for now but you can do it next time!");
        }

    }
}