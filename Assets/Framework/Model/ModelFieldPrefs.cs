// Created by Lazarevich Aleksei in year 2022.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using System;
using UnityEngine;


namespace LaserGames.Framework.Model
{
	[Serializable]
	public abstract class FieldPrefs<T> : Field<T>
	{
		protected string keyS = string.Empty;
		protected int keyI = -1;


		public FieldPrefs(T v, string key) : base(v)
		{
			keyS = key;
			Load += LoadS;
			Save += SaveS;
		}

		protected override void BeforeFiringValueChanged() 
		{
			Save();
		}

		protected abstract void LoadS();
		protected abstract void SaveS();

		public Action Load;
		public Action Save;
	}


	public class IntP : FieldPrefs<int>
	{
		public IntP(int v, string key) : base(v, key) { }

		protected override void LoadS()
		{
			v = PlayerPrefs.GetInt(keyS, v);
		}

		protected override void SaveS()
		{
			PlayerPrefs.SetInt(keyS, v);
		}
	}
}