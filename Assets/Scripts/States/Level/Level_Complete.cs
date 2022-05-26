// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.DB;
using LaserGames.Collapse.UI;
using LaserGames.Framework;
using LaserGames.Framework.TimeManagement;
using UnityEngine;


namespace LaserGames.Collapse.Level
{
	class Level_Complete : ALevelSubstate, ILevelSubstate
	{


		void Subscribe()
		{
			bus.AddListener<S_UI_LevelRestart>(H_UI_LevelRestart);
			bus.AddListener<S_UI_LevelExit>(H_UI_LevelExit);
		}

		void Unsubscribe()
		{
			bus.RemoveListener<S_UI_LevelRestart>(H_UI_LevelRestart);
			bus.RemoveListener<S_UI_LevelExit>(H_UI_LevelExit);
		}

		void H_UI_LevelRestart()
		{
			Unsubscribe();
			bus.Invoke<S_CloseLevelCompleteWindow>();
			context.SwitchState<Level_Reset, Level_Complete>();
		}

		void H_UI_LevelExit()
		{
			Unsubscribe();
			bus.Invoke<S_CloseLevelCompleteWindow>();
			context.SwitchState<Level_Exit, Level_Complete>();
		}

		#region AState
		public override void Activate<T>()
		{
			Subscribe();
			bus.Invoke<S_OpenLevelCompleteWindow>();
		}
		#endregion AState
	}
}
