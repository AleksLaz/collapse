// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.DB;
using LaserGames.Framework;
using System;
using System.Collections;


namespace LaserGames.Collapse
{
	class StartState : AState<IGlobalState>, IGlobalState
	{


		IEnumerator Load()
		{
			int loading = 2;
			Action<bool> cb = (res) => loading--;

			DBLevels.LoadAddressables(DBRoot.I.DBLevels, cb);
			DBScreenIds.LoadAddressables(DBRoot.I.DBScreenIds, cb);

			while (loading != 0)
			{
				yield return null;
			}

			context.SwitchState<MainMenuState, StartState>();
		}


		#region AState
		public override void Activate<T>()
		{
			timeController.StartCoroutine(Load());
		}
		#endregion AState
	}
}