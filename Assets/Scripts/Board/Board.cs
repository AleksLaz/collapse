// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using UnityEngine;


namespace LaserGames.Collapse.Level
{
	class Board
	{
		public readonly int Width;
		public readonly int Height;
		public Cell[] Cells;
		public Line[] Columns;
		public Line[] Rows;
		public Line RowBottom;
		public Line RowTop;
		public readonly int columnMiddleLeft;
		public readonly int columnMiddleRight;
		public readonly Vector2 TopDelimeterPosition;
		public readonly Vector2 BottomDelimeterPosition;
		Rect rect;


		public Board(int width, int height,
			Cell[] cells, Line[] columns,
			Line[] rows, Line rowBottom, Line rowTop, 
			Vector2 topDelimeterPosition, Vector2 bottomDelimeterPosition,
			Rect rect)
		{
			Width = width;
			Height = height;
			Cells = cells;
			Columns = columns;
			Rows = rows;
			RowBottom = rowBottom;
			RowTop = rowTop;
			TopDelimeterPosition = topDelimeterPosition;
			BottomDelimeterPosition = bottomDelimeterPosition;
			this.rect = rect;
			columnMiddleRight = width / 2;
			columnMiddleLeft = columnMiddleRight - 1;
		}

		public Cell GetCellByPoint(Vector2 point)
		{
			if (!rect.Contains(point))
			{
				return null;
			}

			Vector2 normPosition = point - rect.position;
			int x = Mathf.FloorToInt(normPosition.x);
			int y = Mathf.FloorToInt(normPosition.y);
			int index = x * Height + y;

			return Cells[index];
		}
	}
}