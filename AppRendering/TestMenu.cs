using System;
using SharpLabProject.Characters;
using SharpLabProject.Skills;

namespace SharpLabProject.ConsoleRendering
{
    public class TestMenu : Menu
    {
        private int _state;

        private string _toRender = "";

        public TestMenu()
        {
            Player player = new Player(10, 10, 10, 10, 10, 10);

            Actions[ConsoleKey.Spacebar] = () =>
            {
                AbilityInfo info = AbilityInfo.Generate(10);

                _toRender = info.Name + "\n" + player.GetDescription(info);

                _state = 1;
            };
        }

        public override string Render()
        {
            switch (_state)
            {
                case 0:
                {
                    return "Press [Space] to generate a random ability and see it's description.";
                }
                case 1:
                {
                    return _toRender;
                }
            }

            return null;
        }
    }
}