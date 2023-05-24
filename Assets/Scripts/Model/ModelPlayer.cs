// Created by Lazarevich Aleksei in year 2022.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//

using LaserGames.Collapse.DB;
using LaserGames.Framework.Model;
using System;
using System.Collections;


namespace LaserGames.Collapse.Models
{
	public class ModelPlayer
	{
		[NonSerialized]
		string key;

		public IntP BestScore;


		public ModelPlayer(string key, DBPlayer playerData) 
		{
			this.key = key;
			BestScore = new IntP(playerData.BestScore, key + "BestScore");
		}

		public IEnumerator Load()
		{
			yield return null;
			BestScore.Load();
		}

		public IEnumerator Save()
		{
			BestScore.Save();

			yield return null;
		}
	}
}