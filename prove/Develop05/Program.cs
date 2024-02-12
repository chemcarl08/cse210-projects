using System;
using System.Collections.Generic;
using System.IO;

// Base class for goals
class Goal
{
    public string Name { get; set; }
    public int Value { get; set; }
    public bool IsComplete { get; set; }

    public virtual void RecordEvent()
    {
        Console.WriteLine($"Event recorded for goal: {Name}");
        IsComplete = true;
    }

    public virtual void DisplayStatus()
    {
        string status = IsComplete ? "[X]" : "[ ]";
        Console.WriteLine($"{status} {Name}");
    }
}

// Simple goal class
class SimpleGoal : Goal
{
    public SimpleGoal(string name, int value)
    {
        Name = name;
        Value = value;
        IsComplete = false;
    }

    public override void RecordEvent()
    {
        base.RecordEvent();
        Console.WriteLine($"You gained {Value} points for completing the goal: {Name}");
    }
}

// Eternal goal class
class EternalGoal : Goal
{
    public EternalGoal(string name, int value)
    {
        Name = name;
        Value = value;
        IsComplete = false;
    }

    public override void RecordEvent()
    {
        base.RecordEvent();
        Console.WriteLine($"You gained {Value} points for recording the eternal goal: {Name}");
    }
}

// Checklist goal class
class ChecklistGoal : Goal
{
    private int completionCount;
    private int requiredCount;

    public ChecklistGoal(string name, int value, int requiredCount)
    {
        Name = name;
        Value = value;
        IsComplete = false;
        this.requiredCount = requiredCount;
    }

    public override void RecordEvent()
    {
        base.RecordEvent();
        completionCount++;

        Console.WriteLine($"You gained {Value} points for completing the checklist goal: {Name}");

        if (completionCount == requiredCount)
        {
            Console.WriteLine($"Bonus! You gained an additional {Value * requiredCount} points for completing {requiredCount} times.");
        }
    }

    public override void DisplayStatus()
    {
        string status = IsComplete ? "[X]" : $"Completed {completionCount}/{requiredCount} times";
        Console.WriteLine($"{status} {Name}");
    }
}

class Program
{
    static void Main()
    {
        List<Goal> goals = LoadGoals();
        int userScore = LoadScore();

        while (true)
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. Record Event");
            Console.WriteLine("2. Display Goals");
            Console.WriteLine("3. Display Score");
            Console.WriteLine("4. Create New Goal");
            Console.WriteLine("5. Save Data");
            Console.WriteLine("6. Exit");

            Console.Write("Choose an option (1-6): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RecordEvent(goals, ref userScore);
                    break;
                case "2":
                    DisplayGoals(goals);
                    break;
                case "3":
                    DisplayScore(userScore);
                    break;
                case "4":
                    CreateNewGoal(goals);
                    break;
                case "5":
                    SaveData(goals, userScore);
                    break;
                case "6":
                    Console.WriteLine("Exiting program. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                    break;
            }
        }
    }

    static void RecordEvent(List<Goal> goals, ref int userScore)
    {
        Console.WriteLine("\nSelect a goal to record an event:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name}");
        }
        Console.Write("Enter the goal number: ");
        int goalNumber;
        if (int.TryParse(Console.ReadLine(), out goalNumber) && goalNumber >= 1 && goalNumber <= goals.Count)
        {
            goals[goalNumber - 1].RecordEvent();
            userScore += goals[goalNumber - 1].Value;
        }
        else
        {
            Console.WriteLine("Invalid goal number.");
        }
    }

    static void DisplayGoals(List<Goal> goals)
    {
        Console.WriteLine("\nCurrent Goals:");
        foreach (var goal in goals)
        {
            goal.DisplayStatus();
        }
    }

    static void DisplayScore(int userScore)
    {
        Console.WriteLine($"\nYour current score: {userScore} points");
    }

    static void CreateNewGoal(List<Goal> goals)
    {
        Console.WriteLine("\nCreate a New Goal:");

        Console.Write("Enter the goal name: ");
        string goalName = Console.ReadLine();

        Console.Write("Enter the goal type (Simple/Eternal/Checklist): ");
        string goalType = Console.ReadLine().ToLower();

        Console.Write("Enter the goal value: ");
        int goalValue;
        if (!int.TryParse(Console.ReadLine(), out goalValue))
        {
            Console.WriteLine("Invalid goal value. Please enter a valid integer.");
            return;
        }

        switch (goalType)
        {
            case "simple":
                goals.Add(new SimpleGoal(goalName, goalValue));
                break;
            case "eternal":
                goals.Add(new EternalGoal(goalName, goalValue));
                break;
            case "checklist":
                Console.Write("Enter the required count: ");
                int requiredCount;
                if (!int.TryParse(Console.ReadLine(), out requiredCount) || requiredCount <= 0)
                {
                    Console.WriteLine("Invalid required count. Please enter a positive integer.");
                    return;
                }
                goals.Add(new ChecklistGoal(goalName, goalValue, requiredCount));
                break;
            default:
                Console.WriteLine("Invalid goal type. Please enter Simple, Eternal, or Checklist.");
                break;
        }

        Console.WriteLine($"New goal '{goalName}' created.");
    }

    static void SaveData(List<Goal> goals, int userScore)
    {
        Console.Write("Enter the filename to save (e.g., data.txt): ");
        string filename = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var goal in goals)
            {
                writer.WriteLine($"{goal.GetType().Name},{goal.Name},{goal.Value},{goal.IsComplete}");
            }
        }

        using (StreamWriter writer = new StreamWriter("score.txt"))
        {
            writer.WriteLine(userScore);
        }

        Console.WriteLine($"Data saved to {filename}.");
    }

    static List<Goal> LoadGoals()
    {
        List<Goal> goals = new List<Goal>();

        if (File.Exists("goals.txt"))
        {
            string[] goalLines = File.ReadAllLines("goals.txt");

            foreach (var line in goalLines)
            {
                string[] parts = line.Split(',');

                switch (parts[0])
                {
                    case "SimpleGoal":
                        goals.Add(new SimpleGoal(parts[1], int.Parse(parts[2]))
                        {
                            IsComplete = bool.Parse(parts[3].ToLower())
                        });
                        break;
                    case "EternalGoal":
                        goals.Add(new EternalGoal(parts[1], int.Parse(parts[2]))
                        {
                            IsComplete = bool.Parse(parts[3].ToLower())
                        });
                        break;
                    case "ChecklistGoal":
                        goals.Add(new ChecklistGoal(parts[1], int.Parse(parts[2]), int.Parse(parts[3]))
                        {
                            IsComplete = bool.Parse(parts[4].ToLower())
                        });
                        break;
                    default:
                        Console.WriteLine("Invalid goal type in the data file.");
                        break;
                }
            }
        }

        return goals;
    }

    static int LoadScore()
    {
        if (File.Exists("score.txt"))
        {
            return int.Parse(File.ReadAllText("score.txt"));
        }
        return 0;
    }
}