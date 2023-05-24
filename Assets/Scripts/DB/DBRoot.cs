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