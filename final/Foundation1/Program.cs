using System;
using System.Collections.Generic;

class Comment
{
    public string CommenterName { get; set; }
    public string CommentText { get; set; }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>();

    public void AddComment(string commenterName, string commentText)
    {
        Comment comment = new Comment { CommenterName = commenterName, CommentText = commentText };
        Comments.Add(comment);
    }

    public int GetNumComments()
    {
        return Comments.Count;
    }

    public void DisplayInfoWithComments()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Number of Comments: {GetNumComments()}");
        Console.WriteLine("Comments:");

        // Shuffle the comments before displaying
        List<Comment> shuffledComments = new List<Comment>(Comments);
        FisherYatesShuffle(shuffledComments);

        foreach (var comment in shuffledComments)
        {
            Console.WriteLine($"  - {comment.CommenterName}: {comment.CommentText}");
        }

        Console.WriteLine("\n");
    }

    private static void FisherYatesShuffle<T>(List<T> list)
    {
        Random random = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

class Program
{
    static void Main()
    {
        List<Video> videos = new List<Video>
        {
            new Video { Title = "Exploring the massive sea fortress", Author = "SEF TV", Length = 120 },
            new Video { Title = "The ghost city of the Pacific", Author = "National Geographic", Length = 180 },
            new Video { Title = "Touring the Alcatraz Island", Author = "California Through my Lens", Length = 150 },
        };

        videos[0].AddComment("@Magellan&Greyhound", "Great video!");
        videos[0].AddComment("@kellywall3465", "Interesting content.");

        videos[1].AddComment("@charles1487", "Nice work!");
        videos[1].AddComment("@inspiredtowander", "I learned a lot.");

        videos[2].AddComment("@blackfilmguild", "This is amazing.");
        videos[2].AddComment("@chrisabout4555", "Keep it up!");

        FisherYatesShuffle(videos);

        foreach (var video in videos)
        {
            video.DisplayInfoWithComments();
        }
    }

    private static void FisherYatesShuffle<T>(List<T> list)
    {
        Random random = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}