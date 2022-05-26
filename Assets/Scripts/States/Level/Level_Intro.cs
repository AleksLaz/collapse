// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.DB;
using LaserGames.Collapse.UI;


namespace LaserGames.Collapse.Level
{
	class Level_Intro : ALevelSubstate, ILevelSubstate
	{




		void IntroCallback()
		{
			context.SwitchState<Level_Gameplay, Level_Intro>();
		}

		#region AState
		public override void Activate<T>()
		{
			bus.Invoke<S_CloseLoadingWindow>();
			bus.Invoke<S_ShowLevelTopPanel, (GoalType, int)>((levelConfig.goal, levelConfig.GoalValue));


			timeController.StartCoroutine(introProcessor.Process(board, levelConfig, levelElementsRandomizer,
				timeController, elementsObjectPool, IntroCallback));
		}
		#endregion AState
	}
}
