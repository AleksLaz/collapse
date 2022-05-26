// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LaserGames.Framework
{
	public abstract class AContext<T> : IContext<T> where T : IState
	{
		Dictionary<Type, T> states = new Dictionary<Type, T>();
		protected T currentState = default;


		void IContext<T>.SwitchState<V1, V2>()
		{
			Type type = typeof(V1);

			if (states.TryGetValue(type, out T state))
			{
				currentState = state;
			}
			else
			{
				var newState = (V1)Activator.CreateInstance(type);
				InitializeState(newState);

				states.Add(type, newState);
				currentState = newState;
			}

			currentState.Activate<V2>();
		}

		protected abstract void InitializeState(T state);
	}

	public interface IContext<T> where T : IState
	{
		void SwitchState<V1, V2>() where V1 : T where V2 : IState;
	}
}