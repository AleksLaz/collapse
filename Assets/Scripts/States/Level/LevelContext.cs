// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Framework;
using LaserGames.Framework.TimeManagement;


namespace LaserGames.Collapse.Level
{
	public class LevelContext : AContext<ILevelSubstate>
	{
		IBus bus = null;
		ITimeController timeController = null;


		public LevelContext(IBus bus, ITimeController timeController)
		{
			this.bus = bus;
			this.timeController = timeController;
		}

		#region AContext
		protected override void InitializeState(ILevelSubstate state)
		{
			state.Initialize(this, bus, timeController);
		}
		#endregion AContext
	}

	public interface ILevelSubstate : IState
	{ }
}