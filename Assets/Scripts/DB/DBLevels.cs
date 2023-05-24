// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Framework;
using UnityEngine;


namespace LaserGames.Collapse.DB
{
	[CreateAssetMenu(fileName = "DbLevels", menuName = "Collapse/DB/Creare Levels", order = 1000)]
	public class DBLevels : ScriptableObjectSingletonAddressable<DBLevels>
	{
		[SerializeField]
		LevelConfig[] levels = null;


		public bool GetLevel(int id, out LevelConfig result)
		{
			if (id < 0 || id >= levels.Length)
			{
				result = null;
				return false;
			}

			result = levels[id];
			return true;
		}

		public int GetLevelsCount()
		{
			return levels.Length;
		}
	}
}