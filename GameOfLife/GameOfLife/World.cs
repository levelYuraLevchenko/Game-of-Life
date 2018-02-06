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
        private int ColumnsLength { get; set; }
        // Длинна строки массива.
        private int LineLength { get; set; }
        // Массив для разчетов.
        private bool[,] _arrayBoard;

        // Конструктор.
        public World(int sizeX, int sizeY)
        {
            ColumnsLength = sizeX;
            LineLength = sizeY;

            _arrayBoard = new bool[ColumnsLength, LineLength];
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
                    // Запуск игры.
                    case ConsoleKey.Enter:
                        {
                            StartGame();                            
                            break;
                        }
                    // Создает или удаляет живую клетку.
                    case ConsoleKey.Spacebar:
                        {
                            if (_arrayBoard[x, y] == false)
                                _arrayBoard[x, y] = true;
                            else
                                _arrayBoard[x, y] = false;

                            Drow();
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
                    default:
                        {
                            Console.WriteLine("Что-то не так...");
                            break;
                        }
                }
            } while (info.Key != ConsoleKey.Escape);
        }

        // Вывод поля и звездочки.
        public void Drow()
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (var i = 0; i < ColumnsLength; i++)
            {
                for (var j = 0; j < LineLength; j++)
                {
                    if (i == x && j == y)
                        Console.Write("*");
                    else
                        Console.Write(_arrayBoard[i, j] ? "O" : "_");
                }
                Console.WriteLine();
            }
        }

        // Вывод поля после начала игры.
        private void NextDrow()
        {
            for (var i = 0; i < ColumnsLength; i++)
            {
                for (var j = 0; j < LineLength; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(_arrayBoard[i, j] ? "O" : " ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }

        #region
        // Соседние слоты и их подсчет.V.1
        //private int Neighbors(int numX, int numY)
        //{
        //    int neighborsCount = 0;

        //    for (var i = numX - 1; i < numX + 2; i++)
        //    {
        //        for (var j = numY - 1; j < numY + 2; j++)
        //        {
        //            if (i == numX && j == numY)
        //                continue;

        //            if (((numX - 1 >= 0 || numY - 1 >= 0) || (numX + 1 < ColumnsLength || numY + 1 < LineLength)))
        //            {
        //                if (_arrayBoard[numX - 1, numY - 1] == true) 
        //                    neighborsCount++;
        //                if (_arrayBoard[numX - 1, numY] == true)
        //                    neighborsCount++;
        //                if (_arrayBoard[numX - 1, numY + 1] == true)
        //                    neighborsCount++;
        //                if (_arrayBoard[numX, numY - 1] == true)
        //                    neighborsCount++;
        //                if (_arrayBoard[numX, numY + 1] == true)
        //                    neighborsCount++;
        //                if (_arrayBoard[numX + 1, numY - 1] == true)
        //                    neighborsCount++;
        //                if (_arrayBoard[numX + 1, numY] == true)
        //                    neighborsCount++;
        //                if (_arrayBoard[numX + 1, numY + 1] == true)
        //                    neighborsCount++;
        //            }
        //        }
        //    }
        //    return neighborsCount;
        //}
        #endregion

        // Соседние слоты и их подсчет.V.2
        private int Neighbors(int numX, int numY)
        {
            int neighborsCount = 0;

            if (numX - 1 >= 0)
                if (_arrayBoard[numX - 1, numY] == true)
                    neighborsCount++;

            if (numX - 1 >= 0 && numY - 1 >= 0)
                if (_arrayBoard[numX - 1, numY - 1] == true)
                    neighborsCount++;

            if (numY - 1 >= 0)
                if (_arrayBoard[numX, numY - 1] == true)
                    neighborsCount++;

            if (numX + 1 < ColumnsLength)
                if (_arrayBoard[numX + 1, numY] == true)
                    neighborsCount++;

            if (numY + 1 < LineLength)
                if (_arrayBoard[numX, numY + 1] == true)
                    neighborsCount++;

            if (numX + 1 < ColumnsLength && numY + 1 < LineLength)
                if (_arrayBoard[numX + 1, numY + 1] == true)
                    neighborsCount++;

            if (numX + 1 < ColumnsLength && numY - 1 >= 0)
                if (_arrayBoard[numX + 1, numY - 1] == true)
                    neighborsCount++;

            if (numX - 1 >= 0 && numY + 1 < LineLength)
                if (_arrayBoard[numX - 1, numY + 1]  == true)
                    neighborsCount++;

            return neighborsCount;
        }

        // Игровой процесс.
        private void StartGame()
        {
            Console.Clear();
            int count = 1;
            Console.WriteLine("Поколение {0}", count);
            Drow();

            do
            {
                Thread.Sleep(500);
                Console.Clear();
                count++;
                Console.WriteLine("Поколение {0}", count);
                CallBehavior();
                NextDrow();

            } while (IsWorking() == true);
        }

        // Проверка на наличие живих клеток для выхода из цикла в StartGame.
        private bool IsWorking()
        {
            bool isWorking = false;

            for (var i = 0; i < ColumnsLength; i++)
                for (var j = 0; j < LineLength; j++)
                    if(_arrayBoard[i,j] == true)
                        isWorking = true;
            return isWorking;
        }

        // Поведение клеток.
        private void CallBehavior()
        {
            bool[,] _nextGeneration = new bool[ColumnsLength, LineLength];

            for (var i = 0; i < ColumnsLength; i++)
            {
                for (var j = 0; j < LineLength; j++)
                {
                    if (_arrayBoard[i, j] == true)
                    {
                        if (Neighbors(i, j) < 2 || Neighbors(i, j) > 3)
                            _nextGeneration[i, j] = false;
                        else
                            _nextGeneration[i, j] = true;
                    }
                    else
                        if (Neighbors(i, j) == 3)
                            _nextGeneration[i, j] = true;
                }
            }
            _arrayBoard = _nextGeneration;
        }
    }
}
