// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.UI;


namespace LaserGames.Collapse.Level
{
	class Level_Reset : ALevelSubstate, ILevelSubstate
	{


		void ClearBoardCallback()
		{
			camera.transform.position = levelConfig.CameraPosition;
			camera.orthographicSize = levelConfig.CameraSize;

			bus.Invoke<S_LevelScoresChanged, int>(0);
			boardController.Reset();
			levelGoalValidator.Reset();
			levelTimer?.Reset();

			context.SwitchState<Level_Intro, Level_Reset>();
		}

		#region AState
		public override void Activate<T>()
		{
			timeController.StopAllCoroutines();

			bus.Invoke<S_HideLevelBottomPanel, bool>(false);
			timeController.StartCoroutine(ClearBoardElements(ClearBoardCallback));
		}
		#endregion AState
	}
}
