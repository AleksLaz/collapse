// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Framework;
using UnityEngine;


namespace LaserGames.Collapse.DB
{
	[CreateAssetMenu(fileName = "DBScreenIds", menuName = "Collapse/DB/Creare ScreenIds", order = 1100)]
	public class DBScreenIds : ScriptableObjectSingletonAddressable<DBScreenIds>
	{
		public string LoadingWindow;
		public string LevelDefeatWindow;
		public string LevelVictoryWindow;
		public string LevelMenuWindow;
		public string MainMenuWindow;
		public string LevelComplete;

		public string LevelTopPanel;
		public string LevelBottomPanel;
	}
}
