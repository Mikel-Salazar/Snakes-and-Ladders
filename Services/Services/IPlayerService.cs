namespace Services.Services
{
    public interface IPlayerService
    {
        Player CreatePlayer(string name);
        bool MovePlayer(Player player, int roll, int maxSquares);
    }
}
