namespace ConsoleTextRPG.Items
{
    public interface IEquippable
    {
        public bool Equipped { get; set; }

        public int StrengthBonus { get; }
        public int IntelligenceBonus { get; }
        public int AgilityBonus { get; }
        public int EnduranceBonus { get; }
        public int WisdomBonus { get; }
        public int WitsBonus { get; }
    }
}