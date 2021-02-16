using System;
using System.Linq;
using System.Text;
using System.Threading;
using ConsoleTextRPG.Characters;
using ConsoleTextRPG.Skills;
using ConsoleTextRPG.ConsoleRendering;
using ConsoleTextRPG.GameEvents;

namespace ConsoleTextRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            //Rendering.BeginRenderLoop();

            Player player = new Player(10, 10, 10, 10, 10, 10);
            RandomBattle battle = RandomBattle.Generate(8);


        }
    }
}
