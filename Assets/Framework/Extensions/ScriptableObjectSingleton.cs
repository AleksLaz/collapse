// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using UnityEngine;


namespace LaserGames.Framework
{
	public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObjectSingleton<T>
	{
		public static T I;
		static readonly string prefix = "DB/";


		public static void Load()
		{
			string fileName = prefix + typeof(T).Name;

			I = Resources.Load(fileName) as T;
			I.Initialize();
		}

		protected virtual void Initialize() { }
	}
}