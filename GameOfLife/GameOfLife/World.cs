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
        // Массив для разчетов.
        private bool[,] _arrayBoard;
        // Массив для вывода.
        private bool[,] _nextGeneration;

        // Конструктор.
        public World(int sizeX, int sizeY)
        {
            ColumnsLength = sizeX;
            LineLength = sizeY;

            _arrayBoard = new bool[ColumnsLength, LineLength];
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
                            if (_arrayBoard[x, y] == false)
                                _arrayBoard[x, y] = true;
                            else
                                _arrayBoard[x, y] = false;

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

        // Вывод поля и звездочки.
        public void Drow()
        {          
            for (var i = 0; i < ColumnsLength; i++)
            {
                for (var j = 0; j < LineLength; j++)
                {
                    if (i == x && j == y)
                        Console.Write("*");
                    else
                        Console.Write(_arrayBoard[i, j] ? "X" : "_");
                }
                Console.WriteLine();
            }
        }

        // Вывод поля после начала игры.
        public void NextDrow()
        {
            for (var i = 0; i < ColumnsLength; i++)
            {
                for (var j = 0; j < LineLength; j++)
                {
                    Console.Write(_nextGeneration[i, j] ? "X" : "_");
                }
                Console.WriteLine();
            }
        }

        // Соседние слоты и их подсчет.
        public int Neighbors(int numX, int numY)
        {
            int callsCount = 0;

            for (var i = numX - 1; i < numX + 2; i++)
            {
                for (var j = numY - 1; j < numY + 2; j++)
                {
                    if (i == numX && j == numY)
                        continue;

                    if (!((numX < 0 || numY < 0) || (numX >= ColumnsLength || numY >= LineLength)))
                    {
                        if (_arrayBoard[i, j] == true) //ошибка
                        {
                            callsCount++;
                        }
                    }
                }
            }
            return callsCount;
        }

        // Игровой процесс.
        public void StartGame()
        {
            Console.Clear();
            int count = 1;
            Console.WriteLine("Поколение {0}", count);
            Drow();
            CallBehavior();
            Thread.Sleep(2000);           

            do
            {
                Console.Clear();
                count++;
                Console.WriteLine("Поколение {0}", count);

                //не готово!
                for (var i = 0; i < ColumnsLength; i++)
                {
                    for (var j = 0; j < LineLength; j++)
                    {
                        _nextGeneration[i, j] = _arrayBoard[i, j];
                    }
                }

                NextDrow();
                CallBehavior();
                Thread.Sleep(2500);

            } while (true);
        }

        // Поведение клеток.
        private void CallBehavior()
        {
            for (var i = 0; i < ColumnsLength; i++)
            {
                for (var j = 0; j < LineLength; j++)
                {
                    if (_arrayBoard[i, j] == true)
                    {
                        int neighbors = Neighbors(i, j);

                        if (neighbors < 2 || neighbors > 3)
                            _arrayBoard[i, j] = false;
                        else
                            _arrayBoard[i, j] = true;
                    }
                    else
                    {
                        int neighbors = Neighbors(i, j);

                        if (neighbors == 3)
                        {
                            _arrayBoard[i, j] = true;
                        }
                    }
                }
            }
        }
    }
}
