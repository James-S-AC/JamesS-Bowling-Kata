namespace BowlingKataTests
{
	public class GameTests
	{
		[Fact]
		public void Game_RollAllZeros_ScoresZero()
		{
			var game = new BowlingKata.Game();
			for ( int i = 0; i < 20; i++ )
			{
				game.Roll( 0 );
			}
			Assert.Equal( 0, game.Score() );
		}

		[Fact]
		public void Game_RollAllFours_ScoresEighty()
		{
			var game = new BowlingKata.Game();
			for( int i = 0; i < 20; i++ )
			{
				game.Roll( 4 );
			}
			Assert.Equal( 80, game.Score() );
		}

		[Fact]
		public void Game_RollSpareFollowedByThree_ScoresSixteen()
		{
			var game = new BowlingKata.Game();
			game.Roll( 5 );
			game.Roll( 5 ); // Spare
			game.Roll( 3 );
			for( int i = 0; i < 17; i++ )
			{
				game.Roll( 0 );
			}
			Assert.Equal( 16, game.Score() );
		}

		[Fact]
		public void PerfectGame_ScoresThreeHundred()
		{
			var game = new BowlingKata.Game();
			for( int i = 0; i < 12; i++ )
			{
				game.Roll( 10 ); // Strike
			}
			Assert.Equal( 300, game.Score() );
		}
	}
}
