// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.DB;
using LaserGames.Framework;
using UnityEngine;


namespace LaserGames.Collapse.Level
{
	class LevelScoreCounter : ILevelScoreCounter
	{
		IBus bus = null;
		LevelConfig levelConfig = null;
		int scores = 0;


		public LevelScoreCounter(IBus bus, LevelConfig levelConfig)
		{
			this.bus = bus;
			this.levelConfig = levelConfig;

			Subscribe();
		}

		void Subscribe()
		{
			bus.AddListener<S_LevelElementsMatched, (ElementType, ElementColor)[]>(LevelElementsMatchedHandler);
		}

		void Unsubscribe()
		{
			bus.RemoveListener<S_LevelElementsMatched, (ElementType, ElementColor)[]>(LevelElementsMatchedHandler);
		}

		void LevelElementsMatchedHandler((ElementType, ElementColor)[] match)
		{
			if (match == null ||
				match.Length == 0)
			{
				Debug.LogError("Level_Gameplay.LevelElementsMatchedHandler(): match is null or empty.");
			}

			int scoresBefore = scores;

			for (int i = 0; i < match.Length; i++)
			{
				switch (match[i].Item1)
				{
					case ElementType.Single:
						scores += levelConfig.SingleScore;
						break;

					default:
						break;
				}
			}

			if (scoresBefore != scores)
			{ 
				bus.Invoke<S_LevelScoresChanged, int> (scores);
			}
		}

		void ILevelScoreCounter.Clear()
		{
			Unsubscribe();

			bus = null;
			levelConfig = null;
		}
	}

	interface ILevelScoreCounter
	{
		void Clear();
	}
}