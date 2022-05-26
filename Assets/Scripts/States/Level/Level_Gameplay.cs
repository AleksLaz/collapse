using LaserGames.Collapse.UI;
using System.Collections;
using UnityEngine;
using WaitForSeconds = LaserGames.Framework.TimeManagement.WaitForSeconds;


namespace LaserGames.Collapse.Level
{
	class Level_Gameplay : ALevelSubstate, ILevelSubstate
	{
		IEnumerator elementsSpawn = null;


		void Subscribe()
		{
			bus.AddListener<S_MouseLeftButtonDown, Vector3>(H_MouseLeftButtonDown);
			bus.AddListener<S_LevelTopRowReached>(H_LevelTopRowReached);
			bus.AddListener<S_LevelGoal>(H_LevelGoal);

			bus.AddListener<S_UI_LevelResume>(H_UI_LevelResume);
			bus.AddListener<S_UI_LevelRestart>(H_UI_LevelRestart);
			bus.AddListener<S_UI_LevelExit>(H_UI_LevelExit);
			bus.AddListener<S_UI_LevelPause>(H_UI_LevelPause);
		}

		void Unsubscribe()
		{
			bus.RemoveListener<S_MouseLeftButtonDown, Vector3>(H_MouseLeftButtonDown);
			bus.RemoveListener<S_LevelTopRowReached>(H_LevelTopRowReached);
			bus.RemoveListener<S_LevelGoal>(H_LevelGoal);

			bus.RemoveListener<S_UI_LevelResume>(H_UI_LevelResume);
			bus.RemoveListener<S_UI_LevelRestart>(H_UI_LevelRestart);
			bus.RemoveListener<S_UI_LevelExit>(H_UI_LevelExit);
			bus.RemoveListener<S_UI_LevelPause>(H_UI_LevelPause);
		}

		void H_MouseLeftButtonDown(Vector3 point)
		{
			boardController.LeftButtonDown(point);
		}

		void H_LevelTopRowReached()
		{
			Unsubscribe();
			timeController.StopCoroutine(elementsSpawn);
			bus.Invoke<S_HideLevelBottomPanel, bool>(false);
			context.SwitchState<Level_DefeatOutro, Level_Gameplay>();
		}

		void H_LevelGoal()
		{
			Unsubscribe();
			timeController.StopCoroutine(elementsSpawn);
			bus.Invoke<S_HideLevelBottomPanel, bool>(false);
			context.SwitchState<Level_VictoryOutro, Level_Gameplay>();
		}

		IEnumerator ElementsSpawn()
		{
			var wfs = new WaitForSeconds(timeController, levelConfig.ElementsSpawnInterval);

			while (true)
			{
				while (wfs.MoveNext())
				{
					yield return null;
				}

				boardController.ElementSpawned(levelElementsRandomizer.RndoneSingle());
				wfs.Reset();
			}
		}

		void H_UI_LevelResume()
		{
			bus.Invoke<S_CloseLevelMenuWindow>();
			timeController.Play();
		}

		void H_UI_LevelRestart()
		{
			timeController.Play();

			Unsubscribe();
			bus.Invoke<S_CloseLevelMenuWindow>();
			context.SwitchState<Level_Reset, Level_Gameplay>();
		}

		void H_UI_LevelExit()
		{
			timeController.Play();

			Unsubscribe();
			bus.Invoke<S_CloseLevelMenuWindow>();
			context.SwitchState<Level_Exit, Level_Gameplay>();
		}

		void H_UI_LevelPause()
		{
			timeController.Pause();
			bus.Invoke<S_OpenLevelMenuWindow>();
		}

		#region AState
		public override void Activate<T>()
		{
			Subscribe();

			bus.Invoke<S_ShowLevelBottomPanel>();
			bus.Invoke<S_LevelGameplayStart>();
			elementsSpawn = timeController.StartCoroutine(ElementsSpawn());
		}
		#endregion AState
	}
}
