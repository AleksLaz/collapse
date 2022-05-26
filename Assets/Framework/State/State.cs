// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Framework.TimeManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LaserGames.Framework
{
	public abstract class AState<G> : IState where G : IState
	{
		protected IContext<G> context = null;
		protected IBus bus = null;
		protected ITimeController timeController = null;


		#region IState
		public virtual void Initialize<T>(IContext<T> context, IBus bus, ITimeController timeController) where T : IState
		{
			this.context = (IContext<G>)context;
			this.bus = bus;
			this.timeController = timeController;
		}

		//public void SwitchState<T1, T2>()
		//{
		//	context.SwitchState<T1, T2>();
		//}

		public abstract void Activate<T>() where T : IState;
		#endregion IState
	}

	public interface IState
	{
		void Initialize<T>(IContext<T> context, IBus bus, ITimeController timeController) where T : IState;
		void Activate<T>() where T : IState;    // T - предыдущее состояние
	}

	public interface INullState : IState
	{ }
}