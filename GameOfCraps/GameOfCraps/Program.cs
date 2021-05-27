using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfCraps
{
    class Program
    {
        static void Main(string[] args)
        {
            CrapsGame craps = new CrapsGame();
            craps.Play();
        }
    }
}