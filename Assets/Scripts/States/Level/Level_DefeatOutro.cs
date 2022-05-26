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
	class Level_DefeatOutro : ALevelSubstate, ILevelSubstate
	{



		void DelayCallback()
		{
			bus.Invoke<S_CloseLevelDefeatWindow>();
			context.SwitchState<Level_Complete, Level_DefeatOutro>();
		}

		#region AState
		public override void Activate<T>()
		{
			bus.Invoke<S_OpenLevelDefeatWindow>();
			timeController.StartCoroutine(Extensions.InvokeDelay(timeController, levelConfig.DefeatWindowDuration, 
				DelayCallback));
		}
		#endregion AState
	}
}
