// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.Level;
using LaserGames.Collapse.Models;
using LaserGames.Framework;
using LaserGames.Framework.TimeManagement;
using UnityEngine;


namespace LaserGames.Collapse
{
	class LevelState : AState<IGlobalState>, IGlobalState
	{
		IContext<ILevelSubstate> localContext = null;


		void Subscribe()
		{
			bus.AddListener<S_LevelStateExit>(LevelStateExitHandler);
		}

		void Unsubscribe()
		{
			bus.RemoveListener<S_LevelStateExit>(LevelStateExitHandler);
		}

		void LevelStateExitHandler()
		{
			Unsubscribe();
			context.SwitchState<MainMenuState, LevelState>();
		}

		#region AState
		public override void Activate<T>()
		{
			Subscribe();

			localContext.SwitchState<Level_Start, INullState>();
		}
		#endregion AState

		#region IState
		public override void Initialize<IGlobalState>(IContext<IGlobalState> context, IBus bus, ITimeController timeController, ModelPlayer modelPlayer)
		{
			base.Initialize(context, bus, timeController, modelPlayer);

			if (localContext == null)
			{
				localContext = new LevelContext(bus, timeController, modelPlayer);
			}
		}
		#endregion IState
	}
}