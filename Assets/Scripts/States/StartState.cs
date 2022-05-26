// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.DB;
using LaserGames.Framework;


namespace LaserGames.Collapse
{
	class StartState : AState<IGlobalState>, IGlobalState
	{


		void Load()
		{
			DBLevels.Load();
			DBScreenIds.Load();

			LoadComplete();
		}

		void LoadComplete()
		{
			context.SwitchState<MainMenuState, StartState>();
		}


		#region AState
		public override void Activate<T>()
		{
			Load();
		}
		#endregion AState
	}
}