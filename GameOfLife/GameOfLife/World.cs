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
        public int ColumnsLength { get; private set; }
        // Длинна строки массива.
        public int LineLength { get; private set; }

        private int[,] _arrayBoard;
        private bool[,] _nextGeneration;


        public World(int sizeX, int sizeY)
        {
            ColumnsLength = sizeX;
            LineLength = sizeY;

            _arrayBoard = new int[ColumnsLength, LineLength];
            _nextGeneration = new bool[ColumnsLength, LineLength];
        }

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
                    // Создает или удаляет живую клетку.
                    case ConsoleKey.Enter:
                        {
                            if(_arrayBoard[x,y] == 0)
                                _arrayBoard[x, y] = 1;
                            else
                                _arrayBoard[x, y] = 0;

                            Drow();
                            break;
                        }
                    // Запуск игры.
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
                            if (x < (ColumnsLength - 1))                            
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
                            if (y < (LineLength - 1))                            
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
            for (var i = 0; i < ColumnsLength; i++)
            {
                for (var j = 0; j < LineLength; j++)
                {
                    if (i == x && j == y)
                        Console.Write("*");
                    else
                        Console.Write(_arrayBoard[i, j]);
                }
                Console.WriteLine();
            }
        }

        // Вывод пустого поля.
        public void Empty()
        {
            for (var i = 0; i < ColumnsLength; i++)
                for (var j = 0; j < LineLength; j++)
                    _arrayBoard[i, j] = 0;
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

                for (var i = 0; i < ColumnsLength; i++)
                {
                    for (var j = 0; j < LineLength; j++)
                    {
                        if (_arrayBoard[i, j] == 1)
                        {
                            //int[] lifeCall = new int[] { _arrayBoard[i - 1, j - 1], _arrayBoard[i - 1, j], _arrayBoard[i - 1, j + 1], _arrayBoard[i, j - 1], _arrayBoard[i, j + 1], _arrayBoard[i + 1, j - 1], _arrayBoard[i + 1, j], _arrayBoard[i + 1, j + 1]};        

                            int callsCount = 0;

                            try
                            {
                                int[] lifeCall = new int[] { _arrayBoard[i - 1, j - 1], _arrayBoard[i - 1, j], _arrayBoard[i - 1, j + 1], _arrayBoard[i, j - 1], _arrayBoard[i, j + 1], _arrayBoard[i + 1, j - 1], _arrayBoard[i + 1, j], _arrayBoard[i + 1, j + 1] };

                                for (var k = 0; k < lifeCall.Length; k++)
                                    if (lifeCall[k] == 1)
                                        callsCount++;
                            }
                            catch (IndexOutOfRangeException)
                            {
                                continue;
                            }

                            //for (var k = 0; k < lifeCall.Length; k++)
                            //{
                            //    if (lifeCall[k] == 1)
                            //    {
                            //        callsCount++;
                            //    }
                            //}

                            if (callsCount < 2 || callsCount > 3)
                                _arrayBoard[i, j] = 0;
                            else
                                _arrayBoard[i, j] = 1;
                        }
                    }
                }
            } while (true);
        }
    }
}
