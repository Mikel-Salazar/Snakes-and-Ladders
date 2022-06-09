namespace Services.Services
{
    public class PlayerService : IPlayerService
    {        
        public Player CreatePlayer(string name)
        {
            return new Player() { Name = name };
        }

        public bool MovePlayer(Player player, int roll, int maxSquares)
        {
            if (player.Position + roll <= maxSquares)
            {
                player.Position += roll;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
