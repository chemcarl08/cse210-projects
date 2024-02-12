using System;
using System.Threading;

// Base class for all activities
public abstract class Activity
{
    private string name;
    private string description;
    public int duration;

    protected Activity(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public void StartActivity()
    {
        Console.WriteLine($"Welcome to the {name} activity:");
        Console.WriteLine(description);

        // Set duration
        SetDuration();

        // Perform the specific activity
        PerformActivity();

        // Display finishing message
        PauseWithSpinner($"Well done! You have completed another {duration} seconds of the {name} activity.");
    }

     public void ShowSpinner(int duration)
    {
        for (int i = 0; i < duration; i++)
        {
            Console.Write(".", Console.ForegroundColor = ConsoleColor.Gray);
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    public void SetDuration()
    {
        Console.Write("How long, in seconds, would you like for your session(in seconds)? ");
        duration = int.Parse(Console.ReadLine());
    }

    public void PauseWithSpinner(string message)
    {
        Console.WriteLine(message);
        Console.Write("Please wait");
        for (int i = 0; i < 3; i++)
        {
            Thread.Sleep(1000);
            Console.Write(".");
        }
        Console.WriteLine();
    }

    protected abstract void PerformActivity();
}

// BREATHING ACTIVITY
public class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    protected override void PerformActivity()
    {
        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            Console.WriteLine("Breathe in...");
            ShowSpinner(2);
            Console.WriteLine("Breathe out...");
            ShowSpinner(2);
        }
    }
}

// REFLECTION ACTIVITY
public class ReflectionActivity : Activity
{
    private string[] prompts = { "Think of a time when you stood up for someone else.",
                                 "Think of a time when you did something really difficult.",
                                 "Think of a time when you helped someone in need.",
                                 "Think of a time when you did something truly selfless." };

    private string[] reflectionQuestions = { "Why was this experience meaningful to you?",
                                             "Have you ever done anything like this before?",
                                             "How did you get started?",
                                             "How did you feel when it was complete?",
                                             "What made this time different than other times when you were not as successful?",
                                             "What is your favorite thing about this experience?",
                                             "What could you learn from this experience that applies to other situations?",
                                             "What did you learn about yourself through this experience?",
                                             "How can you keep this experience in mind in the future?" };

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
    }

    protected override void PerformActivity()
    {
        Random random = new Random();

        DateTime endTime = DateTime.Now.AddSeconds(duration);
        
            string prompt = prompts[random.Next(prompts.Length)];
            Console.WriteLine(prompt);
            ShowSpinner(2);

        while (DateTime.Now < endTime)
        {
            var question = reflectionQuestions[random.Next(reflectionQuestions.Length)];
            Console.WriteLine(question);
            ShowSpinner(2);
        }
    }
}

// LISTING ACTIVITY
public class ListingActivity : Activity
{
    private string[] listPrompts = { "Who are people that you appreciate?",
                                     "What are personal strengths of yours?",
                                     "Who are people that you have helped this week?",
                                     "When have you felt the Holy Ghost this month?",
                                     "Who are some of your personal heroes?" };

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    protected override void PerformActivity()
    {
        Random random = new Random();
        string prompt = listPrompts[random.Next(listPrompts.Length)];

        Console.WriteLine(prompt);
        ShowSpinner(2);

        Console.WriteLine("Start listing items:");

        // Countdown for user to think about the prompt
        for (int i = 3; i > 0; i--)
        {
            Console.WriteLine($"{i}...");
            Thread.Sleep(1000);
        }

        Console.WriteLine("Go!");

        // Capture user input for the specified duration
        DateTime endTime = DateTime.Now.AddSeconds(duration);
        int itemCount = 0;

        while (DateTime.Now < endTime)
        {
            string item = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(item))
            {
                itemCount++;
            }
        }

        Console.WriteLine($"You listed {itemCount} items.");
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Start Breathing Activity");
            Console.WriteLine("2. Start Reflection Activity");
            Console.WriteLine("3. Start Listing Activity");
            Console.WriteLine("4. Exit");

            Console.Write("Select a choice from the menu: ");
            int choice = int.Parse(Console.ReadLine());

            Activity activity = null;

            switch (choice)
            {
                case 1:
                    activity = new BreathingActivity();
                    break;
                case 2:
                    activity = new ReflectionActivity();
                    break;
                case 3:
                    activity = new ListingActivity();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            if (activity != null)
            {
                activity.StartActivity();
            }
        }
    }
}