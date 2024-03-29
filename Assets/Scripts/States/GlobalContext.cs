﻿// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.Models;
using LaserGames.Framework;
using LaserGames.Framework.TimeManagement;


namespace LaserGames.Collapse
{
	class GlobalContext : AContext<IGlobalState>
	{
		IBus bus = null;
		ITimeController timeController = null;
		ModelPlayer modelPlayer = null;


		public GlobalContext(IBus bus, ITimeController timeController, ModelPlayer modelPlayer)
		{
			this.bus = bus;
			this.timeController = timeController;
			this.modelPlayer = modelPlayer;
		}

		protected override void InitializeState(IGlobalState state)
		{
			state.Initialize(this, bus, timeController, modelPlayer);
		}
	}
}