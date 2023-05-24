// Created by Lazarevich Aleksei in year 2022.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using System;


namespace LaserGames.Framework.Model
{
	[Serializable]
	public class Field<T>
	{
		protected T v;
		

		public Field(T v)
		{
			this.v = v;
		}

		public virtual T Value
		{ 
			get { return v; } 
			set
			{
				if (v == null)
				{
					if (value == null)
					{
						return;
					}
				}
				else if (v.Equals(value))
				{
					return;
				}

				T oldValue = v;
				v = value;
				BeforeFiringValueChanged();
				OnValueChanged?.Invoke(oldValue, value);
			}
		}

		protected virtual void BeforeFiringValueChanged() {	}

		public Action<T, T> OnValueChanged;
	}

	public class Int : Field<int>
	{
		public Int(int v) : base(v)
		{
		}
	}
}