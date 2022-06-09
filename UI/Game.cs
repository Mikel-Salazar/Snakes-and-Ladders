using Microsoft.Extensions.Configuration;
using Services;
using Services.Services;

namespace UI
{
    public class Game
    {
        private readonly IPlayerService _playerService;
        private readonly IDiceService _diceService;
        private readonly IConfiguration _configuration;

        private int PlayerTurn { get; set; } = 0;
        Stages Stage { get; set; } = Stages.Start;
        List<Player> Players { get; set; }
        readonly int maxSquares;

        public Game(IPlayerService playerService, IDiceService diceService, IConfiguration configuration)
        {
            _playerService = playerService;
            _diceService = diceService;
            _configuration = configuration;
            Players = new List<Player>();
            maxSquares = int.Parse(_configuration["BoardSquares"]);
        }

        public void Run()
        {
            GetPlayers();
            Play();
        }

        private void GetPlayers()
        {
            var playersQty = int.Parse(_configuration["PlayersQty"]);
            for (int i = 0; i < playersQty; i++)
            {
                Players.Add(_playerService.CreatePlayer($"Player {i + 1}"));
            }
        }

        private void Play()
        {
            Stage = Stages.Playing;
            Console.WriteLine("The game starts.");

            while (Stage != Stages.End)
            {
                var playerPlaying = Players[PlayerTurn];
                Console.WriteLine($"Now is {playerPlaying.Name}'s turn who is in square {playerPlaying.Position}.");

                var roll = _diceService.Roll();
                Console.WriteLine($"{playerPlaying.Name} rolls a {roll}.");

                MovePlayer(playerPlaying, roll);
                
                PlayerWinsOrContinuePlaying(playerPlaying);
            }

            Console.WriteLine("The game has ended.");
            Console.ReadKey();
        }

        private void MovePlayer(Player playerPlaying, int roll)
        {
            var movement = _playerService.MovePlayer(playerPlaying, roll, maxSquares);

            if (movement)
            {
                Console.WriteLine($"{playerPlaying.Name} moved to square {playerPlaying.Position}.");
            }
            else
            {
                Console.WriteLine($"{playerPlaying.Name} cannot move {roll} squares because s/he is in square {playerPlaying.Position}.");
            }

            Console.WriteLine("--------------------------------");
        }

        private void PlayerWinsOrContinuePlaying(Player playerPlaying)
        {
            if (playerPlaying.DidIWin())
            {
                Stage = Stages.End;
                Console.WriteLine($"The winner is {playerPlaying.Name}.");
            }
            else
            {
                PlayerTurn++;
                if (PlayerTurn >= Players.Count)
                {
                    PlayerTurn = 0;
                    Console.WriteLine();
                    Console.WriteLine(" -----------------");
                    Console.WriteLine("| New round starts |");
                    Console.WriteLine(" -----------------");
                    Console.WriteLine();
                }
            }
        }
    }
}
