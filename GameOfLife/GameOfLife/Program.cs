using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            World world = new World(10,20);

            // Вывод поля и звездочки.
            world.Drow();
            // Управление.
            world.CellArrangement();
        }
    }
}
