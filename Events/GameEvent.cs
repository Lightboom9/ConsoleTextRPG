namespace ConsoleTextRPG.GameEvents
{
    public abstract class GameEvent
    {
        public GameEventType Type { get; protected set; }
    }
}