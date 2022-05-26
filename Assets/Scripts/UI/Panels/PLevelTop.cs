// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.DB;
using LaserGames.Framework;
using LaserGames.Framework.UI;
using System;
using TMPro;
using UnityEngine;


namespace LaserGames.Collapse.UI
{
	class PLevelTop : ABusPanelController<PLevelTopProperties>
	{
		[SerializeField]
		TextMeshProUGUI textScoresValue;

		[SerializeField]
		GameObject goalContainer;

		[SerializeField]
		TextMeshProUGUI textGoalMax;

		[SerializeField]
		TextMeshProUGUI textGoalCurrent;

		[SerializeField]
		GameObject timerContainer;

		[SerializeField]
		TextMeshProUGUI textTimeMinutes;

		[SerializeField]
		TextMeshProUGUI textTimeSeconds;

		static readonly string SecondsFormat = "{0:00}";

		int goalCurrent = 0;
		int lastMinutes = 0;
		int lastSeconds = 0;


		protected override void AddListeners()
		{
			var properties = Properties;
			properties.Bus.AddListener<S_LevelScoresChanged, int>(LevelScoresChangedHandler);
		}

		protected override void RemoveListeners()
		{
			if (Properties == null ||
				Properties.Bus == null)
			{
				return;
			}

			var properties = Properties;
			properties.Bus.RemoveListener<S_LevelScoresChanged, int>(LevelScoresChangedHandler);
			properties.Bus.RemoveListener<S_LevelTimeTick, int>(H_LeveltimeTick);
			properties.Bus.RemoveListener<S_LevelGoalProgressChanged, int>(H_LevelGoalProgressChanged);
		}

		protected override void OnPropertiesSet()
		{
			base.OnPropertiesSet();

			LevelScoresChangedHandler(0);

			InitializeGoal();
		}

		void LevelScoresChangedHandler(int scores)
		{
			textScoresValue.text = scores.ToString();
		}

		void InitializeGoal()
		{
			var properties = Properties;

			switch (properties.goal)
			{
				case GoalType.Time:
					goalContainer.SetActive(false);
					timerContainer.SetActive(true);

					UpdateTimer(properties.goalValue);

					properties.Bus.AddListener<S_LevelTimeTick, int>(H_LeveltimeTick);

					break;

				default:
					goalContainer.SetActive(true);
					timerContainer.SetActive(false);

					goalCurrent = 0;
					UpdateGoalCurrent();
					textGoalMax.text = properties.goalValue.ToString();

					properties.Bus.AddListener<S_LevelGoalProgressChanged, int>(H_LevelGoalProgressChanged);
					break;
			}
		}

		void H_LeveltimeTick(int secondsLeft)
		{
			UpdateTimer(secondsLeft);
		}

		void H_LevelGoalProgressChanged(int goalCurrent)
		{
			this.goalCurrent = goalCurrent;
			UpdateGoalCurrent();
		}

		void UpdateTimer(int secondsLeft)
		{
			int minutes = Math.DivRem(secondsLeft, 60, out int seconds);

			if (minutes != lastMinutes)
			{
				lastMinutes = minutes;
				textTimeMinutes.text = lastMinutes.ToString();
			}

			if (seconds != lastSeconds)
			{
				lastSeconds = seconds;
				textTimeSeconds.text = string.Format(SecondsFormat, lastSeconds);
			}
		}

		void UpdateGoalCurrent()
		{
			textGoalCurrent.text = goalCurrent.ToString();
		}
	}

	[Serializable]
	class PLevelTopProperties : BusPanelProperties
	{
		public GoalType goal;
		public int goalValue;

		public PLevelTopProperties(IBus bus, GoalType goal, int goalValue) : base(bus)
		{
			this.goal = goal;
			this.goalValue = goalValue;
		}
	}
}