using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private char[,] arrayBoard = new char[columnsLength, lineLength];


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
                            arrayBoard[x, y] = 'O';
                            Drow();
                            break;
                        }
                    case ConsoleKey.Spacebar:
                        {
                            //Start
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
            for (int i = 0; i < columnsLength; i++)
            {
                for (int j = 0; j < lineLength; j++)
                {
                    if (i == x && j == y)
                    {
                        Console.Write('*');
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
            for (int i = 0; i < columnsLength; i++)
            {
                for (int j = 0; j < lineLength; j++)
                {
                    arrayBoard[i, j] = ' ';
                }
            }
        }
    }
}
