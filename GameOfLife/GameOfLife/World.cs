using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class World
    {
        // Координатная точка Х.
        private int x = 0;
        // Координатная точка Y.
        private int y = 0;
        // Количество столбцов массива.
        private static int columnsLength = 20;
        // Длинна строки массива.
        private static int lineLength = 40;
        // Двумерный массив символов.
        private int[,] arrayBoard = new int[columnsLength, lineLength];


        // Метод отвечающий за расстоновку живих клеток.
        public void CellArrangement()
        {
            ConsoleKeyInfo info;

            do
            {
                info = Console.ReadKey(true);
                Console.Clear();

                switch (info.Key)
                {
                    case ConsoleKey.Enter:
                        {
                            if(arrayBoard[x,y] == 0)
                                arrayBoard[x, y] = 1;
                            else
                                arrayBoard[x, y] = 0;

                            Drow();
                            break;
                        }
                    case ConsoleKey.Spacebar:
                        {
                            StartGame();
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (x > 0)                            
                                x--;
                            
                            Drow();
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (x < (columnsLength - 1))                            
                                x++;
                            
                            Drow();
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        {
                            if (y > 0)                           
                                y--;
                            
                            Drow();
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            if (y < (lineLength -1))                            
                                y++;
                            
                            Drow();
                            break;
                        }
                }
            } while (info.Key != ConsoleKey.Escape);
        }

        // Вывод поля с внутренними изменениями.
        public void Drow()
        {          
            for (var i = 0; i < columnsLength; i++)
            {
                for (var j = 0; j < lineLength; j++)
                {
                    if (i == x && j == y)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(arrayBoard[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }

        // Вывод пустого поля.
        public void Empty()
        {
            for (var i = 0; i < columnsLength; i++)
            {
                for (var j = 0; j < lineLength; j++)
                {
                    arrayBoard[i, j] = 0;
                }
            }
        }

        public void StartGame()
        {
            int count = 0;
            do
            {
                Console.Clear();
                count++;
                Console.WriteLine("Поколение {0}", count);
                Drow();
                Thread.Sleep(2500);

                for (var i = 0; i < columnsLength; i++)
                {
                    for (var j = 0; j < lineLength; j++)
                    {
                        if (arrayBoard[i, j] == 1)
                        {
                            int[] lifeCall = new int[] { arrayBoard[i - 1, j - 1], arrayBoard[i - 1, j], arrayBoard[i - 1, j + 1], arrayBoard[i, j - 1],arrayBoard[i, j + 1],arrayBoard[i + 1, j - 1], arrayBoard[i + 1, j], arrayBoard[i + 1, j + 1]};

                            int callsCount = 0;

                            for (var k = 0; k < lifeCall.Length; k++)
                            {
                                if (lifeCall[k] == 1)
                                {
                                    callsCount++;
                                }
                            }
                            if (callsCount < 2 || callsCount > 3)
                            {
                                arrayBoard[i, j] = 0;
                            }
                            else
                            {
                                arrayBoard[i, j] = 1;
                            }
                        }
                    }
                }
            } while (true);
        }
    }
}
