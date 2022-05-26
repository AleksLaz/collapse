// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using System;


namespace LaserGames.Framework
{
	public static class BusFabric
	{
		public static IBus CreateBus()
		{
			return new Bus();
		}
	}

	public interface IBus
	{
		void AddListener<T>(Action handler) where T : ASignalNoParam;
		void AddListener<T1, T2>(Action<T2> handler) where T1 : ASignalParam<T2>;

		void RemoveListener<T>(Action handler) where T : ASignalNoParam;
		void RemoveListener<T1, T2>(Action<T2> handler) where T1 : ASignalParam<T2>;

		void Invoke<T>() where T : ASignalNoParam;
		void Invoke<T1, T2>(T2 param) where T1 : ASignalParam<T2>;
	}
}