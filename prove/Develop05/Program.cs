using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// Base class for all goals
[Serializable]
public abstract class Goal
{
    protected string name;
    protected int points;

    public Goal(string name, int points)
    {
        this.name = name;
        this.points = points;
    }

    public virtual void MarkComplete()
    {
        Console.WriteLine($"Goal '{name}' completed! You earned {points} points.");
    }

    public virtual void RecordEvent()
    {
        
    }

    public int Points => points;  // Make the points property accessible

    public abstract void DisplayStatus();
}

// SimpleGoal class for goals with completion
[Serializable]
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points)
    {
    }

    public override void DisplayStatus()
    {
        Console.WriteLine($"[{(IsComplete ? 'X' : ' ')}] {name}");
    }

    public bool IsComplete { get; private set; } = false;

    public override void RecordEvent()
    {
        IsComplete = true;
        MarkComplete();
    }
}

// EternalGoal class for goals that can be recorded multiple times
[Serializable]
public class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points)
    {
    }

    public override void DisplayStatus()
    {
        Console.WriteLine($"{name}: {points} points each time recorded");
    }

    public override void RecordEvent()
    {
        MarkComplete();
    }
}

// ChecklistGoal class for goals that require a certain number of completions
[Serializable]
public class ChecklistGoal : Goal
{
    private int completionTarget;
    private int completions = 0;

    public ChecklistGoal(string name, int points, int completionTarget) : base(name, points)
    {
        this.completionTarget = completionTarget;
    }

    public override void DisplayStatus()
    {
        Console.WriteLine($"{name}: {points} points each time, Bonus {points * completionTarget} on {completionTarget} completions");
        Console.WriteLine($"Completed {completions}/{completionTarget} times");
    }

    public override void RecordEvent()
    {
        completions++;
        if (completions < completionTarget)
        {
            MarkComplete();
        }
        else
        {
            Console.WriteLine($"Goal '{name}' completed! You earned {points} points and a bonus of {points * completionTarget} points.");
        }
    }
}

// User class to manage goals and scores
[Serializable]
public class User
{
    private List<Goal> goals = new List<Goal>();
    private int score = 0;

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void DisplayGoals()
    {
        Console.WriteLine("Your Goals:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            goals[i].DisplayStatus();
        }
    }

    public void RecordEvent(int goalIndex)
    {
        if (goalIndex >= 0 && goalIndex < goals.Count)
        {
            goals[goalIndex].RecordEvent();
            score += goals[goalIndex].Points;
        }
        else
        {
            Console.WriteLine("Invalid goal index.");
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void SaveToFile(string fileName)
    {
        try
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                JsonSerializer.Serialize(fs, this);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data: {ex.Message}");
        }
    }

    public static User LoadFromFile(string fileName)
    {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    return (User)JsonSerializer.Deserialize(fs, typeof(User));
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {fileName} not found. You have 0 points.");
                return new User();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
                return new User();
            }
    }
}
class Program
{
    static void Main()
    {
        User user = User.LoadFromFile("userdata.json");

        while (true)
        {
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create new goal");
            Console.WriteLine("2. List goals");
            Console.WriteLine("3. Save goals");
            Console.WriteLine("4. Load goals");
            Console.WriteLine("5. Record event");
            Console.WriteLine("6. Quit");

            Console.Write("Select a choice from the menu: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddNewGoal(user);
                    break;
                case 2:
                    user.DisplayGoals();
                    break;
                case 3:
                    Console.WriteLine("Enter the file name to save: ");
                    string fileName = Console.ReadLine();
                    user.SaveToFile(fileName);
                    break;
                case 4:
                    Console.WriteLine("Enter the file name to load:");
                    string loadFileName = Console.ReadLine();
                    user = User.LoadFromFile(loadFileName);
                    Console.WriteLine("Goals loaded successfully!");
                    break;
                case 5:
                    Console.WriteLine("Select a goal to record (enter the index):");
                    user.DisplayGoals();
                    int goalIndex = int.Parse(Console.ReadLine()) - 1;
                    user.RecordEvent(goalIndex);
                    break;
                case 6:
                    user.SaveToFile("userdata.json");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void AddNewGoal(User user)
    {
        Console.WriteLine("Enter the name of the goal:");
        string goalName = Console.ReadLine();

        Console.WriteLine("Enter the number of points for the goal:");
        int points = int.Parse(Console.ReadLine());

        Console.WriteLine("Select the type of goal:");
        Console.WriteLine("1. Simple Goal (Completable)");
        Console.WriteLine("2. Eternal Goal (Repeatable)");
        Console.WriteLine("3. Checklist Goal (Requires a certain number of completions)");

        int goalType = int.Parse(Console.ReadLine());

        switch (goalType)
        {
            case 1:
                user.AddGoal(new SimpleGoal(goalName, points));
                break;
            case 2:
                user.AddGoal(new EternalGoal(goalName, points));
                break;
            case 3:
                Console.WriteLine("Enter the number of times this goal must be completed for a bonus:");
                int completionTarget = int.Parse(Console.ReadLine());
                user.AddGoal(new ChecklistGoal(goalName, points, completionTarget));
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                break;
        }

        Console.WriteLine("Goal added successfully!");
    }
}

