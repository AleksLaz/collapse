// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using UnityEngine;


namespace LaserGames.Collapse.Level
{
	class Cell
	{
		public IElement Element = null;
		public Cell Left = null;
		public Cell Right = null;
		public Cell Top = null;
		public Cell Bottom = null;
		public int Index;
		public int ColumnIndex;
		public int RowIndex;
		public Vector2 position;

		public void Clear()
		{
			if (Element != null)
			{
				Element.Release();
			}
			Element = null;
			Left = null;
			Right = null;
			Top = null;
			Bottom = null;
		}
	}
}