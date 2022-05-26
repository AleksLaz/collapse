// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//


namespace LaserGames.Collapse.Level
{
	class Level_Start : ALevelSubstate, ILevelSubstate
	{




		#region AState
		public override void Activate<T>()
		{
			// TODO: detect loaded scene

			context.SwitchState<Level_Load, Level_Start>();
		}
		#endregion AState
	}
}
