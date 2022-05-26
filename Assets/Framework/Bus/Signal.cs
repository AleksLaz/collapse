// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using System;
using System.Collections;

namespace LaserGames.Framework
{
	public abstract class ASignal
	{
		static int hash = 0;
		int hashCode = 0;


		public ASignal()
		{
			hashCode = hash++;
		}

		public override int GetHashCode()
		{
			return hashCode;
		}
	}

	public abstract class ASignalNoParam : ASignal
	{
		public Action callback;

		public event Action Callback
		{
			add
			{
				callback += value;
			}
			remove
			{
				callback -= value;
			}
		}
	}

	public abstract class ASignalParam<T> : ASignal
	{
		public Action<T> callback;

		public event Action<T> Callback
		{
			add
			{
				callback += value;
			}
			remove
			{
				callback -= value;
			}
		}
	}
}
