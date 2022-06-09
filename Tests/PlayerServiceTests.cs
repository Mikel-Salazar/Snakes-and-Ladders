using Moq;
using Services.Services;

namespace Tests
{
    public class PlayerServiceTests
    {
        const string PLAYERS_NAME = "Mikel";
        const int MAX_SQUARES = 100;

        [Fact]
        public void CreatePlayerAtSquare1()
        {
            /*
            Given the game is started
            When the token is placed on the board
            Then the token is on square 1
            */

            var playerService = new PlayerService();
            var player = playerService.CreatePlayer(PLAYERS_NAME);

            Assert.True(player.Name == PLAYERS_NAME);
            Assert.True(player.Position == 1);
        }

        [Fact]
        public void MovePlayer3Squares()
        {
            /*
            Given the token is on square 1
            When the token is moved 3 spaces
            Then the token is on square 4
            */

            var playerService = new PlayerService();
            var player = playerService.CreatePlayer(PLAYERS_NAME);

            var fakeRoll = 3;

            playerService.MovePlayer(player, fakeRoll, MAX_SQUARES);

            Assert.True(player.Position == 4);
        }

        [Fact]
        public void MovePlayer3SquaresAndThen4More()
        {
            /*
            Given the token is on square 1
            When the token is moved 3 spaces
            And then it is moved 4 spaces
            Then the token is on square 8
            */

            var playerService = new PlayerService();
            var player = playerService.CreatePlayer(PLAYERS_NAME);

            var fakeRoll1 = 3;

            playerService.MovePlayer(player, fakeRoll1, MAX_SQUARES);

            var fakeRoll2 = 4;

            playerService.MovePlayer(player, fakeRoll2, MAX_SQUARES);

            Assert.True(player.Position == 8);
        }

        [Fact]
        public void PlayerWinsMovingExactlyToLastSquare()
        {
            /*
            Given the token is on square 97
            When the token is moved 3 spaces
            Then the token is on square 100
            And the player has won the game
            */
            const int STARTING_POSITION = 97;

            var playerService = new PlayerService();
            var player = playerService.CreatePlayer(PLAYERS_NAME);
            player.Position = STARTING_POSITION;

            var fakeRoll = 3;

            playerService.MovePlayer(player, fakeRoll, MAX_SQUARES);

            Assert.True(player.Position == 100);
            Assert.True(player.DidIWin());
        }

        [Fact]
        public void PlayerDoesntMoveIfRollsBeyondLastSquare()
        {
            /*
            Given the token is on square 97
            When the token is moved 4 spaces
            Then the token is on square 97
            And the player has not won the game
            */

            const int STARTING_POSITION = 97;
            
            var playerService = new PlayerService();
            var player = playerService.CreatePlayer(PLAYERS_NAME);
            player.Position = STARTING_POSITION;

            var fakeRoll = 4;

            playerService.MovePlayer(player, fakeRoll, MAX_SQUARES);

            Assert.True(player.Position == STARTING_POSITION);
            Assert.False(player.DidIWin());
        }
    }
}