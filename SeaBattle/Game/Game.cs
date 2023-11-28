using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Game
{
    internal class Game
    {
        public char[,] playerBoard;
        public char[,] computerBoard;
        Random random;
        public void StartNewGame()
        {
            Console.Clear();

            playerBoard = new char[11, 11];
            computerBoard = new char[11, 11];
            random = new Random();

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    playerBoard[i, j] = '□';
                    computerBoard[i, j] = '□';
                }
            }

            PlaceComputerShips();
        }

        public void PlaceComputerShips()
        {
            int[] shipSizes = { 1, 1, 1, 1, 2, 2, 2, 3, 3 };

            foreach (int shipSize in shipSizes)
            {
                bool shipPlaced = false;

                while (!shipPlaced)
                {
                    int row = random.Next(1, 11);
                    int col = random.Next(1, 11);
                    bool position = random.Next(2) == 0;

                    if (IsValidShipPosition(row, col, shipSize, position, computerBoard))
                    {
                        PlaceShip(row, col, shipSize, position, computerBoard);
                    }

                    if (IsValidShipPosition(row, col, shipSize, position, playerBoard))
                    {
                        PlaceShip(row, col, shipSize, position, playerBoard);
                        shipPlaced = true;
                    }
                }
            }
        }

        public bool IsValidShipPosition(int row, int col, int size, bool position, char[,] board)
        {
            if (position && row + size > 11)
            {
                return false;
            }
            else if (!position && col + size > 11)
            {
                return false;
            }

            for (int i = -1; i <= size; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (position && (row + i < 1 || row + i > 10 || col + j < 1 || col + j > 10 || board[row + i, col + j] != '□'))
                    {
                        return false;
                    }
                    else if (!position && (col + i < 1 || col + i > 10 || row + j < 1 || row + j > 10 || board[row + j, col + i] != '□'))
                    {
                        return false;
                    }
                }
            }

            for (int i = 0; i < size; i++)
            {
                if (position && computerBoard[row + i, col] != '□')
                {
                    return false;
                }
                else if (!position && computerBoard[row, col + i] != '□')
                {
                    return false;
                }
            }

            return true;
        }

        public void PlaceShip(int row, int col, int size, bool position, char[,] board)
        {
            for (int i = 0; i < size; i++)
            {
                if (position)
                {
                    board[row + i, col] = '■';
                }
                else
                {
                    board[row, col + i] = '■';
                }
            }
        }

        public void PlayGame()
        {
            bool gameOver = false;

            while (!gameOver)
            {
                Console.Clear();
                PrintBoards();

                Console.WriteLine("\nВведите координаты выстрела (например, А1):");
                string input = Console.ReadLine();

                if (input.ToLower() == "к1")
                {
                    input = "й1";
                }
                else if (input.ToLower() == "к2")
                {
                    input = "й2";
                }
                else if (input.ToLower() == "к3")
                {
                    input = "й3";
                }
                else if (input.ToLower() == "к4")
                {
                    input = "й4";
                }
                else if (input.ToLower() == "к5")
                {
                    input = "й5";
                }
                else if (input.ToLower() == "к6")
                {
                    input = "й6";
                }
                else if (input.ToLower() == "к7")
                {
                    input = "й7";
                }
                else if (input.ToLower() == "к8")
                {
                    input = "й8";
                }
                else if (input.ToLower() == "к9")
                {
                    input = "й9";
                }
                else if (input.ToLower() == "к10")
                {
                    input = "й10";
                }

                if (input.Length == 2 && char.IsLetter(input[0]) && char.IsDigit(input[1]))
                {
                    int row = input[1] - '0';
                    int col = char.ToUpper(input[0]) - 'А' + 1;

                    if (row >= 1 && row <= 10 && col >= 1 && col <= 10)
                    {
                        if (computerBoard[row, col] == '■')
                        {
                            computerBoard[row, col] = '⛝';
                            Console.WriteLine("Попадание! Ваш следующий ход.");
                            CheckShipDestroyed(row, col);

                            Console.WriteLine("\nНажмите Enter");
                            Console.ReadLine();
                        }
                        else if (computerBoard[row, col] == '□')
                        {
                            computerBoard[row, col] = '•';
                            Console.WriteLine("Промах! Ход компьютера.");

                            Console.WriteLine("\nНажмите Enter");
                            Console.ReadLine();

                            ComputerTurn();
                        }
                        else
                        {
                            Console.WriteLine("Вы уже стреляли в эту ячейку. Попробуйте еще раз.");

                            Console.WriteLine("\nНажмите Enter");
                            Console.ReadLine();
                        }

                        if (CheckWin())
                        {
                            gameOver = true;
                            Console.Clear();
                            PrintBoards();
                            Console.WriteLine("Вы победили!");
                        }
                    }
                }

                if (input.Length == 3 && char.IsLetter(input[0]) && char.IsDigit(input[1]) && char.IsDigit(input[2]))
                {
                    int row = Convert.ToInt32(input.Substring(1));
                    int col = char.ToUpper(input[0]) - 'А' + 1;

                    if (row >= 1 && row <= 10 && col >= 1 && col <= 10)
                    {
                        if (computerBoard[row, col] == '■')
                        {
                            computerBoard[row, col] = '⛝';
                            Console.WriteLine("Попадание! Ваш следующий ход.");
                            CheckShipDestroyed(row, col);

                            Console.WriteLine("\nНажмите Enter");
                            Console.ReadLine();
                        }
                        else if (computerBoard[row, col] == '□')
                        {
                            computerBoard[row, col] = '•';
                            Console.WriteLine("Промах! Ход компьютера.");

                            Console.WriteLine("\nНажмите Enter");
                            Console.ReadLine();

                            ComputerTurn();
                        }
                        else
                        {
                            Console.WriteLine("Вы уже стреляли в эту ячейку. Попробуйте еще раз.");

                            Console.WriteLine("\nНажмите Enter");
                            Console.ReadLine();
                        }

                        if (CheckWin())
                        {
                            gameOver = true;
                            Console.Clear();
                            PrintBoards();
                            Console.WriteLine("Вы победили!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверные координаты. Попробуйте еще раз.");

                        Console.WriteLine("\nНажмите Enter");
                        Console.ReadLine();
                    }
                }
            }
        }

        public void ComputerTurn()
        {
            bool hitTarget = false;

            while (!hitTarget)
            {
                int row = random.Next(1, 11);
                int col = random.Next(1, 11);

                if (playerBoard[row, col] == '■')
                {
                    playerBoard[row, col] = '⛝';
                    Console.WriteLine("Компьютер попал! Ваш следующий ход.");
                    CheckShipDestroyed(row, col);

                    Console.WriteLine("\nНажмите Enter");
                    Console.ReadLine();
                    hitTarget = true;

                }
                else if (playerBoard[row, col] == '□')
                {
                    playerBoard[row, col] = '•';
                    Console.WriteLine("Компьютер промахнулся. Ваш ход.");

                    Console.WriteLine("\nНажмите Enter");
                    Console.ReadLine();

                    return;
                }
            }

            if (CheckWin())
            {
                Console.Clear();
                PrintBoards();
                Console.WriteLine("Компьютер победил!");
            }
        }

        public void CheckShipDestroyed(int row, int col)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (row + i >= 0 && row + i < 11 && col + j >= 0 && col + j < 11)
                    {
                        if (computerBoard[row + i, col + j] == '■')
                        {
                            return;
                        }
                    }
                }
            }

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (row + i >= 0 && row + i < 11 && col + j >= 0 && col + j < 11)
                    {
                        if (computerBoard[row + i, col + j] == '□')
                        {
                            computerBoard[row + i, col + j] = '•';
                        }
                    }
                }
            }
        }

        public bool CheckWin()
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    if (computerBoard[i, j] == '■')
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void PrintBoards()
        {
            Console.WriteLine("         ВАШЕ ПОЛЕ");
            Console.WriteLine("     A Б В Г Д Е Ж З И К");

            for (int i = 1; i <= 10; i++)
            {
                if (i < 10)
                {
                    Console.Write($"ㅤ{i}  ");
                }
                else
                {
                    Console.Write($" {i}  ");
                }

                for (int j = 1; j <= 10; j++)
                {
                    Console.Write($"{playerBoard[i, j]} ");
                }
                Console.WriteLine("");
            }

            Console.WriteLine("\n      ПОЛЕ КАПМУКТЕРА");
            Console.WriteLine("     A Б В Г Д Е Ж З И К");

            for (int i = 1; i <= 10; i++)
            {
                if (i < 10)
                {
                    Console.Write($"ㅤ{i}  ");
                }
                else
                {
                    Console.Write($" {i}  ");
                }

                for (int j = 1; j <= 10; j++)
                {
                    Console.Write($"{computerBoard[i, j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}