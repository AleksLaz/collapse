// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using System;
using System.Collections;
using UnityEngine;


namespace LaserGames.Collapse.Level
{
	/// <summary>
	/// Creates and initializes instances of Board class.
	/// </summary>
	static class BoardBuilder
	{
		/// <summary>
		/// Creates parts of board and puts them into Board class instance.
		/// </summary>
		/// <param name="width">Width of board aka number of columns.</param>
		/// <param name="height">Height of board aka number of rows.</param>
		/// /// <param name="callback">Since it is used like coroutine a created instance is returned with callback.</param>
		/// <returns>Enumerator to start coroutine.</returns>
		public static IEnumerator Create(int width, int height, int cellSize, Action<Board> callback)
		{
			if (callback == null)
			{
				Debug.LogError("BoardFactory.Create(): callback is null.");
				yield break;
			}

			int cellsCount = width * height;
			int i;

			// create all cells
			Cell[] cells = new Cell[cellsCount];
			for (i = 0; i < cellsCount; i++)
			{
				cells[i] = new Cell();
				cells[i].Index = i;
			}

			float[] yPos = new float[height];
			float yOridgin = -height / 2f + 0.5f;
			for (i = 0; i < height; i++)
			{
				yPos[i] = yOridgin + i;
			}

			// create all columns and put created cells there
			Line[] columns = new Line[width];
			float xOridgin = -width / 2f + 0.5f;
			float[] xPos = new float[width];

			for (i = 0; i < width; i++)
			{
				columns[i] = new Line(height);
				xPos[i] = xOridgin + i;

				int shift = i * height;
				int shiftIndex;
				for (int c = 0; c < height; c++)
				{
					shiftIndex = c + shift;
					columns[i].Cells[c] = cells[shiftIndex];
					cells[shiftIndex].ColumnIndex = i;
					cells[shiftIndex].position = new Vector2(xPos[i], yPos[c]);
				}
			}

			yield return null;

			// create all rows and put created cells there
			Line[] rows = new Line[height];
			for (i = 0; i < height; i++)
			{
				rows[i] = new Line(width);

				for (int c = 0; c < width; c++)
				{
					rows[i].Cells[c] = cells[i + c * height];
					rows[i].Cells[c].RowIndex = i;
				}
			}

			Line rowBottom = new Line(width);
			float y = yPos[0] - 1;
			for (i = 0; i < width; i++)
			{
				rowBottom.Cells[i] = new Cell();
				rowBottom.Cells[i].position = new Vector2(xPos[i], y);
			}

			Line rowTop = new Line(width);
			y = yPos[yPos.Length-1] + 1;
			for (i = 0; i < width; i++)
			{
				rowTop.Cells[i] = new Cell();
				rowTop.Cells[i].position = new Vector2(xPos[i], y);
			}

			Vector2 bottomDelimeterPosition = new Vector2(0f, yPos[0] - 0.5f);
			Vector2 topDelimeterPosition = new Vector2(0f, yPos[yPos.Length-1] + 0.5f);
			Rect rect = new Rect(xPos[0] - 0.5f, bottomDelimeterPosition.y, width, height);

			yield return null;

			// find and set neighbour cells for each cell
			int left, right, top, bottom;
			for (i = 0; i < cellsCount; i++)
			{
				left = i - height;
				right = i + height;
				top = i + 1;
				bottom = i - 1;

				if (left >= 0 && left < cellsCount)
				{
					cells[i].Left = cells[left];
				}

				if (right >= 0 && right < cellsCount)
				{
					cells[i].Right = cells[right];
				}

				if (top >= 0 && top < cellsCount)
				{
					cells[i].Top = cells[top];
				}

				if (bottom >= 0 && bottom < cellsCount)
				{
					cells[i].Bottom = cells[bottom];
				}
			}

			yield return null;

			callback.Invoke(new Board(width, height, 
				cells, 
				columns, rows, rowBottom, rowTop,
				topDelimeterPosition, bottomDelimeterPosition,
				rect));
		}
	}
}