// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using System;
using System.Collections;
using UnityEngine;


namespace LaserGames.Collapse.Level
{
	class RowsShifter : IRowsShifter
	{
		IEnumerator IRowsShifter.Shift(Board board, IElement[] row, Action callback)
		{
			if (!board.RowTop.IsEmpty())
			{
				Debug.LogError("RowsShifter.Shift(): RowTop is not empty.");
			}

			int maxIndex = board.Rows.Length - 1;

			if (!board.Rows[maxIndex].IsEmpty())
			{
				Line.MoveLine(board.Rows[maxIndex], board.RowTop);
			}


			for (int i = maxIndex - 1; i >= 0; i--)
			{
				if (!board.Rows[i].IsEmpty())
				{
					Line.MoveLine(board.Rows[i], board.Rows[i+1]);
				}

				yield return null;
			}

			Line.MoveLine(row, board.Rows[0]);

			callback?.Invoke();
		}
	}


	interface IRowsShifter
	{
		IEnumerator Shift(Board board, IElement[] row, Action callback);
	} 
}