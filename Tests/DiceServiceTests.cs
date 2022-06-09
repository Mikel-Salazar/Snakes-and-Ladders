using Moq;
using Services.Services;

namespace Tests
{
    public class DiceServiceTests
    {
        const string PLAYERS_NAME = "Mikel";
        const int MAX_SQUARES = 100;

        [Fact]
        public void RollResultShouldBe1_6()
        {
            /*
            Given the game is started
            When the player rolls a die
            Then the result should be between 1-6 inclusive
            */
            var results = new List<int>();
            var dice = new DiceService();

            for (int i = 0; i < 100; i++)
            {
                results.Add(dice.Roll());
            }

            var invalidRolls = results.Any(n => n > 6 || n < 1);

            Assert.False(invalidRolls);
        }

        [Fact]
        public void PlayerMovesAsManySquaresAsHasRolled()
        {
            /*
            Given the player rolls a 4
            When they move their token
            Then the token should move 4 spaces
            */

            const int TARGET_SQUARES = 4;
            const int INITIAL_POSITION = 10;

            var mockDiceService = new Mock<IDiceService>();
            mockDiceService.Setup(d => d.Roll(6)).Returns(TARGET_SQUARES);

            var playerService = new PlayerService();
            var player = playerService.CreatePlayer(PLAYERS_NAME);
            player.Position = INITIAL_POSITION;

            var roll = mockDiceService.Object.Roll();

            playerService.MovePlayer(player, roll, MAX_SQUARES);

            Assert.True(player.Position - INITIAL_POSITION == TARGET_SQUARES);
        }
    }
}