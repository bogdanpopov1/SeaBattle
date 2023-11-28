using SeaBattle.Game;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using SeaBattle.Game;

Console.OutputEncoding = Encoding.UTF8;

Game game = new Game();

bool trigger = true;

while (trigger)
{
    game.StartNewGame();
    game.PlayGame();

    Console.WriteLine("\nХотите начать новую игру? (Да/Нет)");
    string input = Console.ReadLine();

    if (input.ToLower() == "нет")
    {
        Console.Clear();
        Console.WriteLine("Не заплывайте за буйки!");
        trigger = false;
    }
}