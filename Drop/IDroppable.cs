using ConsoleTextRPG.Characters;

namespace ConsoleTextRPG.Drop
{
    public interface IDroppable
    {
        public int DropWeight { get; }

        public string GetDropDescription(Player player);

        public void ApplyToPlayer(Player player);
    }
}