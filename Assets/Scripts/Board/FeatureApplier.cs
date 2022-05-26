// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace LaserGames.Collapse.Level
{
	class FeatureApplier : IFeatureApplier
	{
		IBus bus = null;
		HashSet<Cell> match = new HashSet<Cell>();


		public FeatureApplier(IBus bus)
		{
			this.bus = bus;
		}

		IEnumerator IFeatureApplier.Apply(Cell origin, Board board, Action<Cell, Cell[]> callback)
		{
			match.Clear();

			if (origin.Element.Type() == ElementType.Bomb)
			{
				ElementColor color = origin.Element.Color();

				for (int i = 0; i < board.Cells.Length; i++)
				{
					if (board.Cells[i].Element?.Color() != color)
					{
						continue;
					}

					match.Add(board.Cells[i]);
				}

				yield return null;

				bus.Invoke<S_LevelBombApplied, (ElementType, ElementColor)[]>(BoardUtils.ArchiveMatch(match));
			}

			callback?.Invoke(origin, match.ToArray());
		}

		bool IFeatureApplier.HasFeature(Cell cell)
		{
			if (cell == null ||
				cell.Element == null ||
				cell.Element.Type() == ElementType.Single)
			{
				return false;
			}

			return true;
		}

		void IFeatureApplier.Clear()
		{
			match.Clear();
			match = null;

			bus = null;
		}
	}

	interface IFeatureApplier
	{
		bool HasFeature(Cell cell);
		IEnumerator Apply(Cell origin, Board board, Action<Cell, Cell[]> callback);
		void Clear();
	}
}