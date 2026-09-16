namespace BowlingKata
{
	public class Game
	{
		public void Roll( int pins )
		{
			// Record the number of pins knocked down in the current frame (creating a new frame if necessary)
			var currentFrame = GetCurrentOrAddNew();
			currentFrame.AddRoll( pins );
		}

		public int Score()
		{
			// Calculate the total score of the game
			return frames.Take(10).Sum(ScoreFrame);
		}

		int ScoreFrame( Frame frame )
		{
			var frameIndex = frames.IndexOf( frame );
			if( frameIndex < 0 || frameIndex >= frames.Count )
			{
				throw new ArgumentOutOfRangeException( nameof( frameIndex ), "Frame index is out of range." );
			}
			var score = ( frame.FirstRoll ?? 0 ) + ( frame.SecondRoll ?? 0 );
			if (score == 10 ) // Spare or Strike
			{
				if (frameIndex < frames.Count - 1 )
				{
					// Add the score of the next roll for a spare or the next two rolls for a strike
					var nextFrame = frames[frameIndex + 1];
					score += nextFrame.FirstRoll ?? 0;
					if (frame.IsStrike)
					{
						// If the next frame is also a strike, we need to add the first roll of the frame after that
						if( nextFrame.IsStrike && frameIndex + 2 < frames.Count )
						{
							score += frames[frameIndex + 2].FirstRoll ?? 0;
						}
						else
						{
							score += nextFrame.SecondRoll ?? 0;
						}
					}
				}
			}
			return score;
		}

		Frame GetCurrentOrAddNew()
		{
			if( frames.Count == 0 || CurrentFrame?.IsComplete == true )
			{
				frames.Add( new Frame() );
			}
			return frames[frames.Count - 1];
		}

		Frame? CurrentFrame => frames.Count > 0 ? frames[frames.Count - 1] : (Frame?)null;

		List<Frame> frames = new List<Frame>();

		public class Frame
		{
			public int? FirstRoll { get; private set; }
			public int? SecondRoll { get; private set; }
			public bool IsStrike => FirstRoll == 10;
			public bool IsSpare => FirstRoll.HasValue && SecondRoll.HasValue && ( FirstRoll + SecondRoll == 10 );
			public bool IsComplete => SecondRoll.HasValue || IsStrike; // A frame is complete if it has two rolls or if the first roll is a strike
			public void AddRoll( int pins )
			{
				if ( pins < 0 || pins > 10 )
				{
					throw new ArgumentOutOfRangeException( nameof( pins ), "Pins must be between 0 and 10." );
				}
				if( !FirstRoll.HasValue )
				{
					FirstRoll = pins;
				}
				else if( !SecondRoll.HasValue )
				{
					if( FirstRoll + pins > 10 )
					{
						throw new InvalidOperationException( "Total pins in a frame cannot exceed 10." );
					}
					SecondRoll = pins;
				}
				else
				{
					throw new InvalidOperationException( "Cannot add more rolls to this frame." );
				}
			}
		}
	}
}
