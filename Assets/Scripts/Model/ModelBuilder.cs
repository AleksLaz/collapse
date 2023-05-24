// Created by Lazarevich Aleksei in year 2022.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.DB;


namespace LaserGames.Collapse.Models
{
	public static class ModelBuilder
	{
		public static readonly string PlayerFormat = "{0}Player";


		public static ModelPlayer CreatePlayer(string keyPrefix = "")
		{
			string keyPlayer = string.Format(PlayerFormat, keyPrefix);

			return new ModelPlayer(keyPlayer, DBPlayer.I);
		}
	}
}
