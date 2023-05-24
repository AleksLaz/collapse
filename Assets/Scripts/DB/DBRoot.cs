// Created by Lazarevich Aleksei in year 2023.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace LaserGames.Collapse.DB
{
	[CreateAssetMenu(fileName = "DBRoot", menuName = "Collapse/DB/Create DBRoot", order = 100)]
	public class DBRoot : ScriptableObjectSingleton<DBRoot>
	{
		public AssetReference DBPlayer;
		public AssetReference DBLevels;
		public AssetReference DBScreenIds;
	}
}