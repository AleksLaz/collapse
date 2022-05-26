// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.DB;
using LaserGames.Framework;


namespace LaserGames.Collapse.Level
{
	class LevelGoalValidator : ILevelGoalValidator
	{
		IBus bus = null;
		LevelConfig config = null;
		int value = 0;


		void Unsubscribe()
		{
			bus.RemoveListener<S_LevelRowSpawned>(H_LevelRowSpawned);
			bus.RemoveListener<S_LeveltimeIsUp>(H_LeveltimeIsUp);
		}

		void H_LevelRowSpawned()
		{
			value++;

			bus.Invoke<S_LevelGoalProgressChanged, int>(value);

			if (value >= config.GoalValue)
			{
				bus.Invoke<S_LevelGoal>();
				Unsubscribe();
			}
		}

		void H_LeveltimeIsUp()
		{
			bus.Invoke<S_LevelGoal>();
			Unsubscribe();
		}

		void InitializeGoal()
		{
			value = 0;

			switch (config.goal)
			{
				case GoalType.Time:
					bus.AddListener<S_LeveltimeIsUp>(H_LeveltimeIsUp);
					break;

				default:
					bus.AddListener<S_LevelRowSpawned>(H_LevelRowSpawned);
					break;
			}
		}

		public LevelGoalValidator(IBus bus, LevelConfig config)
		{
			this.bus = bus;
			this.config = config;

			InitializeGoal();
		}

		void ILevelGoalValidator.Reset()
		{
			Unsubscribe();
			InitializeGoal();
		}

		void ILevelGoalValidator.Clear()
		{
			Unsubscribe();

			config = null;
			bus = null;
		}
	}

	interface ILevelGoalValidator
	{
		void Reset();
		void Clear();
	}
}
