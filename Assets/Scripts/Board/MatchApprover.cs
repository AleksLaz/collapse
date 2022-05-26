// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//


namespace LaserGames.Collapse.Level
{
	class MatchApprover : IMatchApprover
	{
		int minLength;

		public MatchApprover(int minLength)
		{
			this.minLength = minLength;
		}

		bool IMatchApprover.Approve(Cell[] match)
		{
			return minLength <= match.Length;
		}
	}

	interface IMatchApprover
	{
		bool Approve(Cell[] match);
	}
}