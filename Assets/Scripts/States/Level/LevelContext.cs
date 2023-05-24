// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.Models;
using LaserGames.Framework;
using LaserGames.Framework.TimeManagement;


namespace LaserGames.Collapse.Level
{
	public class LevelContext : AContext<ILevelSubstate>
	{
		IBus bus = null;
		ITimeController timeController = null;
		ModelPlayer modelPlayer = null;


		public LevelContext(IBus bus, ITimeController timeController, ModelPlayer modelPlayer)
		{
			this.bus = bus;
			this.timeController = timeController;
			this.modelPlayer = modelPlayer;
		}

		#region AContext
		protected override void InitializeState(ILevelSubstate state)
		{
			state.Initialize(this, bus, timeController, modelPlayer);
		}
		#endregion AContext
	}

	public interface ILevelSubstate : IState
	{ }
}