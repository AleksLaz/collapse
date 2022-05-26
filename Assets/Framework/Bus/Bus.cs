// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using System;
using System.Collections.Generic;
using UnityEngine;


namespace LaserGames.Framework
{
	class Bus : IBus
	{
		Dictionary<Type, ASignal> signals = new Dictionary<Type, ASignal>();

		T GetSignal<T>() where T : ASignal
		{
			Type type = typeof(T);

			if (signals.TryGetValue(type, out ASignal signal))
			{
				return (T)signal;
			}

			T newSignal = (T)Activator.CreateInstance(type);
			signals.Add(type, newSignal);
			return newSignal;
		}


		#region IBus
		void IBus.AddListener<T>(Action handler)
		{
			if (handler == null)
			{
				Debug.LogError("Bus.AddListener<T>(): handler is null.");
				return;
			}

			var signal = GetSignal<T>();

			signal.Callback += handler;
		}

		void IBus.AddListener<T1, T2>(Action<T2> handler)
		{
			if (handler == null)
			{
				Debug.LogError("Bus.AddListener<T1, T2>(): handler is null.");
				return;
			}

			var signal = GetSignal<T1>();

			signal.Callback += handler;
		}

		void IBus.RemoveListener<T>(Action handler)
		{
			if (handler == null)
			{
				Debug.LogError("Bus.RemoveListener<T>(): handler is null.");
				return;
			}

			var signal = GetSignal<T>();

			signal.Callback -= handler;
		}

		void IBus.RemoveListener<T1, T2>(Action<T2> handler)
		{
			if (handler == null)
			{
				Debug.LogError("Bus.RemoveListener<T1, T2>(): handler is null.");
				return;
			}

			var signal = GetSignal<T1>();

			signal.Callback -= handler;
		}

		void IBus.Invoke<T>()
		{
			GetSignal<T>().callback?.Invoke();
		}

		void IBus.Invoke<T1, T2>(T2 param)
		{
			GetSignal<T1>().callback?.Invoke(param);
		}
		#endregion IBus
	}
}