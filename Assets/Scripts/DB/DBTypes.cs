// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.Level;
using System;
using UnityEngine;


namespace LaserGames.Collapse.DB
{
	[Serializable]
	public class LevelConfig
	{
		public int Width;
		public int Height;
		public int CellSize;
		public string SceneName;
		public ElementType[] ElementTypes;
		public ElementColor[] ElementColors;
		public int MinMatchLength;
		public int IntroRowsCount;
		public float IntroDelay;
		public float IntroRowsInterval;
		public float ElementsSpawnInterval;
		public Vector3 CameraPosition;
		public float CameraSize;
		public int SingleScore;
		public float DefeatWindowDuration;
		public float VictoryWindowDuration;
		public string Name;
		public GoalType goal;
		public int GoalValue;
		public int BombMatchCount;
	}

	public enum GoalType
	{ 
		Rows,
		Time
	}
}