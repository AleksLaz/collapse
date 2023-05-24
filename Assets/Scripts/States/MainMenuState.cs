// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.DB;
using LaserGames.Collapse.UI;
using LaserGames.Framework;
using UnityEngine;


namespace LaserGames.Collapse
{
	class MainMenuState : AState<IGlobalState>, IGlobalState
	{

		void Subscribe()
		{
			bus.AddListener<S_UI_MainMenuExit>(H_UI_MainMenuExit);
			bus.AddListener<S_UI_MainMenuLevelSelect>(H_UI_MainMenuLevelSelect);
		}

		void Unsubscribe()
		{
			bus.RemoveListener<S_UI_MainMenuExit>(H_UI_MainMenuExit);
			bus.RemoveListener<S_UI_MainMenuLevelSelect>(H_UI_MainMenuLevelSelect);
		}

		void H_UI_MainMenuExit()
		{
			Unsubscribe();

#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}

		void H_UI_MainMenuLevelSelect()
		{
			Unsubscribe();

			bus.Invoke<S_CloseMainMenuWindow>();
			//bus.Invoke<S_OpenLoadingWindow>();

			context.SwitchState<LevelState, MainMenuState>();
		}

		#region AState
		public override void Activate<T>()
		{
			Subscribe();
			bus.Invoke<S_OpenMainMenuWindow, int>(DBLevels.I.GetLevelsCount());
		}
		#endregion AState
	}
}