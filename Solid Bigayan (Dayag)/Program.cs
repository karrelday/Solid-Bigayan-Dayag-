using System;
using System.Collections.Generic;

// ISP: YAN LANG YAN
interface IGameMove
{
    string Name { get; }
    bool Beats(IGameMove other);
}

// OCP: LAHAT SILA FINOFOLLOW YUNG ISANG RULE 
class Rock : IGameMove
{
    public string Name => "Rock";
    public bool Beats(IGameMove other) => other.Name == "Scissors";
}

class Paper : IGameMove
{
    public string Name => "Paper";
    public bool Beats(IGameMove other) => other.Name == "Rock";
}

class Scissors : IGameMove
{
    public string Name => "Scissors";
    public bool Beats(IGameMove other) => other.Name == "Paper";
}

// SRP: KANYA KANYANG USE 
class MoveFactory
{
    public static IGameMove GetMove(string move)
    {
        switch (move.Trim().ToLower())
        {
            case "rock": return new Rock();
            case "paper": return new Paper();
            case "scissors": return new Scissors();
            default: throw new ArgumentException("Invalid move. Please enter Rock, Paper, or Scissors.");
        }
    }
}

class GameUI
{
    public static void DisplayResult(string result)
    {
        Console.WriteLine(result);
    }
}

class GameLogic
{
    public static string PlayRound(IGameMove playerMove, IGameMove computerMove, ref int winCount)
    {
        if (playerMove.Beats(computerMove))
        {
            winCount++;
            return $"You Win! {playerMove.Name} beats {computerMove.Name}";
        }
        if (computerMove.Beats(playerMove))
            return $"You Lose! {computerMove.Name} beats {playerMove.Name}";
        return "It's a tie!";
    }
}

class Program
{
    static void Main()
    {
        var random = new Random();
        var moves = new List<string> { "Rock", "Paper", "Scissors" };
        int winCount = 0;
        bool playAgain = true;

        Console.WriteLine("Welcome to Rock Paper Scissors!");

        while (playAgain)
        {
            Console.Write("Enter your move (Rock, Paper, Scissors): ");
            string playerInput = Console.ReadLine();

            try
            {
                IGameMove playerMove = MoveFactory.GetMove(playerInput);
                IGameMove computerMove = MoveFactory.GetMove(moves[random.Next(moves.Count)]);

                Console.WriteLine($"Computer chose: {computerMove.Name}");
                GameUI.DisplayResult(GameLogic.PlayRound(playerMove, computerMove, ref winCount));
                Console.WriteLine($"Total Wins: {winCount}");

                Console.Write("Do you want to play again? (yes/no): ");
                string response = Console.ReadLine().Trim().ToLower();
                playAgain = response == "yes";
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        Console.WriteLine("Thanks for playing! Your total wins: " + winCount);
    }
}