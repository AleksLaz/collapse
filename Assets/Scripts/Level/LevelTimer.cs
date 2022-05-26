// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Framework;
using LaserGames.Framework.TimeManagement;
using UnityEngine;


namespace LaserGames.Collapse.Level
{
	class LevelTimer : ILevelTimer
	{
		IBus bus = null;
		ITimeController timeController = null;
		int secondsMax;
		int secondsLeft;
		float tickCounter;


		public LevelTimer(IBus bus, ITimeController timeController, int seconds)
		{
			this.bus = bus;
			this.timeController = timeController;
			secondsMax = seconds;
			secondsLeft = secondsMax;
			tickCounter = 0;
		}

		void Subscribe()
		{
			bus.AddListener<S_LevelGameplayStart>(H_LevelGameplayStart);
		}

		void Unsubscribe()
		{
			bus.RemoveListener<S_LevelGameplayStart>(H_LevelGameplayStart);
			timeController.update -= Update;
		}

		void H_LevelGameplayStart()
		{
			timeController.update += Update;
		}

		void Update(float deltaTime)
		{
			tickCounter += deltaTime;

			if (tickCounter >= 1.0f)
			{
				tickCounter -= 1.0f;
				secondsLeft--;
				bus.Invoke<S_LevelTimeTick, int>(secondsLeft);
			}

			if (secondsLeft <= 0)
			{
				bus.Invoke<S_LeveltimeIsUp>();
				Unsubscribe();
			}
		}

		void ILevelTimer.Reset()
		{
			Unsubscribe();
			Subscribe();

			secondsLeft = secondsMax;
			tickCounter = 0;
		}

		void ILevelTimer.Clear()
		{
			Unsubscribe();

			bus = null;
			timeController = null;
		}
	}

	interface ILevelTimer
	{
		void Reset();
		void Clear();
	}
}