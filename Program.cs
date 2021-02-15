using System;
using System.Linq;
using System.Text;
using System.Threading;
using SharpLabProject.Characters;
using SharpLabProject.Skills;
using SharpLabProject.AppRendering;

namespace SharpLabProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Rendering.BeginRenderLoop();

            Player player = new Player(10, 10, 10, 10, 10, 10);
            player.Skills.Add(AbilityInfo.Generate(10));
            player.Skills.Add(AbilityInfo.Generate(10));
            player.Skills.Add(AbilityInfo.Generate(10));

            SkillSelection menu = new SkillSelection(player);
            Rendering.SetMenu(menu);
        }
    }
}
