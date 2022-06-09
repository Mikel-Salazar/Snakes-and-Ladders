namespace Services.Services
{
    public class DiceService : IDiceService
    {        
        private int value;
        private readonly Random random = new();

        public int Roll(int sides = 6)
        {
            value = random.Next(1, sides + 1);
            return value;
        }
    }
}
