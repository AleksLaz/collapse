// Created by Lazarevich Aleksei in year 2022.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Framework;
using UnityEngine;


namespace LaserGames.Collapse.DB
{
	[CreateAssetMenu(fileName = "DBPlayer", menuName = "Collapse/DB/Create DBPlayer", order = 103)]
	public class DBPlayer : ScriptableObjectSingletonAddressable<DBPlayer>
	{
		public int BestScore;  // значение рекорда игрока по-умолчанию
	}
}