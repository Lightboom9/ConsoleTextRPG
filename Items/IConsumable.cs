using ConsoleTextRPG.Characters;

namespace ConsoleTextRPG.Items
{
    public interface IConsumable
    {
        public void Consume(Player player);
    }
}