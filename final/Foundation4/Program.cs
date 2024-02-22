using System;

class Activity
{
    private DateTime date;
    private int lengthInMinutes;

    public DateTime Date
    {
        get { return date; }
        set { date = value; }
    }

    public int LengthInMinutes
    {
        get { return lengthInMinutes; }
        set { lengthInMinutes = value; }
    }

    public virtual double GetDistance()
    {
        return 0;
    }

    public virtual double GetSpeed()
    {
        return 0; 
    }

    public virtual double GetPace()
    {
        return 0;
    }

    // GetSummary method to produce a string with all the summary information
    public string GetSummary()
    {
        return $"{Date:dd MMM yyyy} - {GetType().Name} ({LengthInMinutes} min) - Distance: {GetDistance()}, Speed: {GetSpeed()}, Pace: {GetPace()}";
    }
}

class Running : Activity
{
    private double distance;

    public double Distance
    {
        get { return distance; }
        set { distance = value; }
    }

    public override double GetDistance()
    {
        return Distance;
    }

    public override double GetSpeed()
    {
        return Distance / LengthInMinutes * 60;
    }

    public override double GetPace()
    {
        return LengthInMinutes / Distance;
    }
}

class Cycling : Activity
{
    private double speed;

    public double Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public override double GetDistance()
    {
        return Speed * LengthInMinutes / 60;
    }

    public override double GetSpeed()
    {
        return Speed;
    }

    public override double GetPace()
    {
        return 60 / Speed;
    }
}

class Swimming : Activity
{
    private int laps;

    public int Laps
    {
        get { return laps; }
        set { laps = value; }
    }

    public override double GetDistance()
    {
        return Laps * 50 / 1000;
    }

    public override double GetSpeed()
    {
        return GetDistance() / LengthInMinutes * 60;
    }

    public override double GetPace()
    {
        return LengthInMinutes / GetDistance();
    }
}

class Program
{
    static void Main()
    {
        Running runningActivity = new Running
        {
            Date = DateTime.Now,
            LengthInMinutes = 30,
            Distance = 3.0
        };

        Cycling cyclingActivity = new Cycling
        {
            Date = DateTime.Now.AddDays(1),
            LengthInMinutes = 45,
            Speed = 20.0
        };

        Swimming swimmingActivity = new Swimming
        {
            Date = DateTime.Now.AddDays(2),
            LengthInMinutes = 60,
            Laps = 40
        };

        // Summary
        Console.WriteLine(runningActivity.GetSummary());
        Console.WriteLine(cyclingActivity.GetSummary());
        Console.WriteLine(swimmingActivity.GetSummary());
    }
}