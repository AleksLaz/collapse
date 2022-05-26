// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.UI;
using LaserGames.Framework;


namespace LaserGames.Collapse.Level
{
	class Level_Exit : ALevelSubstate, ILevelSubstate
	{


		void Unload()
		{
			boardController.Clear();
			boardController = null;
			levelController = null;
			levelElementsRandomizer.Clear();
			levelElementsRandomizer = null;
			introProcessor = null;
			featureApplier.Clear();
			featureApplier = null;
			matchFinder.Clear();
			matchFinder = null;
			matchApprover = null;
			matchProcessor.Clear();
			matchProcessor = null;
			rowsShifter = null;
			levelScoreCounter.Clear();
			levelScoreCounter = null;
			levelGoalValidator.Clear();
			levelGoalValidator = null;
			levelTimer?.Clear();
			levelTimer = null;
			camera = null;

			timeController.StartCoroutine(ClearBoard(ClearBoardCallback));
		}

		void ClearBoardCallback()
		{
			board = null;

			timeController.StartCoroutine(elementsObjectPool.Clear(ObjectPoolClearCallback));
		}

		void ObjectPoolClearCallback()
		{
			elementsObjectPool = null;

			Extensions.UnloadSceneAsync(timeController, levelConfig.SceneName, UnloadSceneCallback);
		}

		void UnloadSceneCallback()
		{
			levelConfig = null;

			bus.Invoke<S_LevelStateExit>();
		}


		#region AState
		public override void Activate<T>()
		{
			timeController.StopAllCoroutines();
			bus.Invoke<S_HideLevelTopPanel>();
			bus.Invoke<S_OpenLoadingWindow>();
			Unload();
		}
		#endregion AState
	}
}
