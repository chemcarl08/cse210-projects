using System;

class Address
{
    private string street;
    private string city;
    private string state;
    private string country;

    public string Street
    {
        get { return street; }
        set { street = value; }
    }

    public string City
    {
        get { return city; }
        set { city = value; }
    }

    public string State
    {
        get { return state; }
        set { state = value; }
    }

    public string Country
    {
        get { return country; }
        set { country = value; }
    }

    public override string ToString()
    {
        return $"{Street}, {City}, {State}, {Country}";
    }
}

class Event
{
    private string title;
    private string description;
    private DateTime date;
    private TimeSpan time;
    private Address address;

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public DateTime Date
    {
        get { return date; }
        set { date = value; }
    }

    public TimeSpan Time
    {
        get { return time; }
        set { time = value; }
    }

    public Address Address
    {
        get { return address; }
        set { address = value; }
    }

    public override string ToString()
    {
        return $"{Title}, {Description}, {Date}, {Address}";
    }

    public string GenerateStandardMessage()
    {
        return $"Standard Details: {ToString()}";
    }

    public virtual string GenerateFullDetailsMessage()
    {
        return $"Full Details: {ToString()}";
    }

    public string GenerateShortDescription()
    {
        return $"Short Description: {GetType().Name}, {Title}, {Date}";
    }
}

class LectureEvent : Event
{
    private string speakerName;
    private int capacity;

    public string SpeakerName
    {
        get { return speakerName; }
        set { speakerName = value; }
    }

    public int Capacity
    {
        get { return capacity; }
        set { capacity = value; }
    }

    public override string GenerateFullDetailsMessage()
    {
        return base.GenerateFullDetailsMessage() + $", Speaker: {SpeakerName}, Capacity: {Capacity}";
    }
}

class ReceptionEvent : Event
{
    private string rsvpEmail;

    public string RSVPEmail
    {
        get { return rsvpEmail; }
        set { rsvpEmail = value; }
    }

    public override string GenerateFullDetailsMessage()
    {
        return base.GenerateFullDetailsMessage() + $", RSVP Email: {RSVPEmail}";
    }
}

class OutdoorGatheringEvent : Event
{
    private string weatherStatement;

    public string WeatherStatement
    {
        get { return weatherStatement; }
        set { weatherStatement = value; }
    }

    public override string GenerateFullDetailsMessage()
    {
        return base.GenerateFullDetailsMessage() + $", Weather: {WeatherStatement}";
    }
}

class Program
{
    static void Main()
    {
        Address address = new Address { Street = "409 Ludlow St", City = "Saskatoon", State = "SK", Country = "CANADA" };

        LectureEvent lectureEvent = new LectureEvent
        {
            Title = "Ted Talk",
            Description = "Inspirational ted talk from various speaker",
            Date = DateTime.Now.Date.AddDays(7),
            Time = TimeSpan.FromHours(19),
            Address = address,
            SpeakerName = "Edison Garcia",
            Capacity = 50
        };

        ReceptionEvent receptionEvent = new ReceptionEvent
        {
            Title = "Business Networking",
            Description = "Casual networking with friends",
            Date = DateTime.Now.Date.AddMonths(1),
            Time = TimeSpan.FromHours(20),
            Address = address,
            RSVPEmail = "Edison.Garcia@yahoo.com"
        };

        OutdoorGatheringEvent outdoorEvent = new OutdoorGatheringEvent
        {
            Title = "Lunch in the Park",
            Description = "Enjoy the outdoors",
            Date = DateTime.Now.Date.AddMonths(2),
            Time = TimeSpan.FromHours(12),
            Address = address,
            WeatherStatement = "Sunny with a chance of clouds"
        };

        // Display messages of each event
         Console.WriteLine("Lectures:");
        DisplayEventMessages(lectureEvent);
         Console.WriteLine("Receptions:");
        DisplayEventMessages(receptionEvent);
         Console.WriteLine("Outdoor Gatherings:");
        DisplayEventMessages(outdoorEvent);
    }

    static void DisplayEventMessages(Event eventObj)
    {
        Console.WriteLine("------");
        Console.WriteLine(eventObj.GenerateStandardMessage());
        Console.WriteLine(eventObj.GenerateFullDetailsMessage());
        Console.WriteLine(eventObj.GenerateShortDescription());
        Console.WriteLine("------\n");
    }
}