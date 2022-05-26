// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.DB;
using LaserGames.Framework;
using LaserGames.Framework.TimeManagement;
using System;
using System.Collections;


namespace LaserGames.Collapse.Level
{
	class IntroProcessor : IIntroProcessor
	{
		IEnumerator IIntroProcessor.Process(Board board, LevelConfig config, ILevelElementsRandomizer randomizer,
			ITimeSource time, ObjectPool<IElement> objectPool, Action callback)
		{
			var wfs = new WaitForSeconds(time, config.IntroDelay);
			while (wfs.MoveNext())
			{
				yield return null;
			}

			var interval = new WaitForSeconds(time, config.IntroRowsInterval);

			for (int i = 0; i < config.IntroRowsCount; i++)
			{
				(ElementType, ElementColor)[] rndRow = randomizer.RndSet(board.Width);

				for (int c = 0; c < rndRow.Length; c++)
				{
					board.RowBottom.Cells[c].Element = objectPool.GetObject();
					board.RowBottom.Cells[c].Element.SetType(rndRow[c].Item1);
					board.RowBottom.Cells[c].Element.SetColor(rndRow[c].Item2);
					board.RowBottom.Cells[c].Element.SetActive(true);
				}

				ShiftRows(i, board);

				while (interval.MoveNext())
				{
					yield return null;
				}
			}

			callback?.Invoke();
		}

		void ShiftRows(int iteration, Board board)
		{
			for (int i = iteration; i > 0; i--)
			{
				Line.MoveLine(board.Rows[i - 1], board.Rows[i]);
			}

			Line.MoveLine(board.RowBottom, board.Rows[0]);
		}
	}

	interface IIntroProcessor
	{
		IEnumerator Process(Board board, LevelConfig levelConfig, ILevelElementsRandomizer randomizer,
			ITimeSource time, ObjectPool<IElement> objectPool, Action callback);
	}
}