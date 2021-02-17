using System;
using ConsoleTextRPG.Characters;

namespace ConsoleTextRPG.ConsoleRendering
{
    public class ExplorationMenu : Menu
    {
        public ExplorationMenu()
        {
            Actions[ConsoleKey.Spacebar] = () =>
            {
                // Advance event. Or just start a fight, for now...
            };
        }

        public override string Render()
        {
            string str = "";

            return str;
        }
    }
}