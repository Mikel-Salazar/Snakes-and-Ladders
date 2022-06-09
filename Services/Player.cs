namespace Services
{
    public class Player
    {
        public string Name { get; set; }
        public int Position { get; set; } = 1;

        public bool DidIWin()
        {
            return Position == 100;
        }
    }
}
