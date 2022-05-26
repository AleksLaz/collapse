// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.UI;
using LaserGames.Framework;


namespace LaserGames.Collapse.Level
{
	class Level_VictoryOutro : ALevelSubstate, ILevelSubstate
	{


		void DelayCallback()
		{
			bus.Invoke<S_CloseLevelVictoryWindow>();
			context.SwitchState<Level_Complete, Level_VictoryOutro>();
		}


		#region AState
		public override void Activate<T>()
		{
			bus.Invoke<S_OpenLevelVictoryWindow>();
			timeController.StartCoroutine(Extensions.InvokeDelay(timeController, levelConfig.VictoryWindowDuration, 
				DelayCallback));
		}
		#endregion AState
	}
}
