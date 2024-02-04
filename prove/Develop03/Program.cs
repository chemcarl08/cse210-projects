using System;
using System.Collections.Generic;
using System.Linq;

class ScriptureWord
{
    public string Word { get; set; }
    public bool IsHidden { get; set; }

    public ScriptureWord(string word)
    {
        Word = word;
        IsHidden = false;
    }

    public override string ToString()
    {
        return IsHidden ? new string('_', Word.Length) : Word;
    }
}

class ScriptureReference
{
    public string Reference { get; }

    public ScriptureReference(string reference)
    {
        Reference = reference;
    }

    public override string ToString()
    {
        return Reference;
    }
}

class Scripture
{
    public ScriptureReference Reference { get; }
    private List<ScriptureWord> Words { get; }

    public Scripture(string reference, string text)
    {
        Reference = new ScriptureReference(reference);
        Words = text.Split(' ').Select(word => new ScriptureWord(word)).ToList();
    }

    public bool HideRandomWord()
    {
        List<ScriptureWord> wordsToHide = Words.Where(word => !word.IsHidden).ToList();

        if (wordsToHide.Count == 0)
            return false;

        Random random = new Random();
        int randomIndex = random.Next(wordsToHide.Count);
        wordsToHide[randomIndex].IsHidden = true;

        return true;
    }

    public override string ToString()
    {
        return $"{Reference}: {string.Join(" ", Words)}";
    }
}

class Program
{
    static void Main()
    {
        Scripture scripture = new Scripture("Alma 32:21", "And now as I said concerning faith-faith is not to have a perfect knowledge of things; therefore if ye have faith ye hope for things which are not seen, which are true.");

        HideScripture(scripture);

        Console.ReadLine();
    }

    static void HideScripture(Scripture scripture)
    {
        Console.Clear();
        Console.WriteLine(scripture);

        while (scripture.HideRandomWord())
        {
            Console.WriteLine("\nPress Enter to continue or type 'quit' to end: ");
            string userInput = Console.ReadLine();

            if (userInput.ToLower() == "quit")
                break;

            Console.Clear();
            Console.WriteLine(scripture);
        }
    }
}