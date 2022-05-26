// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.DB;
using LaserGames.Framework;
using System;
using System.Collections;
using System.Collections.Generic;


namespace LaserGames.Collapse.Level
{
	class MatchProcessor : IMatchProcessor
	{
		ObjectPool<IElement> objectPool = null;
		LevelConfig levelConfig = null;
		HashSet<int> columnsAffected = new HashSet<int>();
		Queue<Cell> fallQueue = new Queue<Cell>();
		Queue<Line> columnsQueue = new Queue<Line>();


		public MatchProcessor(ObjectPool<IElement> objectPool, LevelConfig levelConfig)
		{
			this.objectPool = objectPool;
			this.levelConfig = levelConfig;
		}

		IEnumerator IMatchProcessor.Process(Cell origin, Cell[] match, Board board, List<IElement> elementsToRelease, 
			Action callback)
		{
			if (match == null)
			{
				callback?.Invoke();
				yield break;
			}

			var color = origin.Element.Color();
			var type = origin.Element.Type();

			columnsAffected.Clear();

			for (int i = 0; i < match.Length; i++)
			{
				columnsAffected.Add(match[i].ColumnIndex);
				elementsToRelease.Add(match[i].Element);
				match[i].Element.SetActive(false);
				match[i].Element = null;
			}

			yield return null;

			FindFeature(origin, type, color, match);

			foreach (int c in columnsAffected)
			{
				FallColumnElements(board.Columns[c]);
			}

			yield return null;

			ShiftColumnsFromLeft(board);
			ShiftColumnsFromRight(board);

			yield return null;

			callback?.Invoke();
		}

		void FallColumnElements(Line column)
		{
			fallQueue.Clear();

			for (int i = 0; i < column.Cells.Length; i++)
			{
				Cell cell = column.Cells[i];
				if (cell.Element == null)
				{
					fallQueue.Enqueue(cell);
				}
				else if (fallQueue.Count > 0)
				{
					Cell target = fallQueue.Dequeue();
					target.Element = cell.Element;
					target.Element.SetPosition(target.position);
					cell.Element = null;
					fallQueue.Enqueue(cell);
				}
			}
		}

		void ShiftColumnsFromLeft(Board board)
		{
			columnsQueue.Clear();

			for (int i = board.columnMiddleLeft; i >= 0; i--)
			{
				if (columnsQueue.Count == 0 &&
					!columnsAffected.Contains(i))
				{
					continue;
				}

				Line column = board.Columns[i];
				if (column.IsEmpty())
				{
					columnsQueue.Enqueue(column);
					continue;
				}

				if (columnsQueue.Count == 0)
				{
					continue;
				}

				Line.MoveLine(column, columnsQueue.Dequeue());
				columnsQueue.Enqueue(column);
			}
		}

		void ShiftColumnsFromRight(Board board)
		{
			columnsQueue.Clear();

			for (int i = board.columnMiddleRight; i < board.Width; i++)
			{
				if (columnsQueue.Count == 0 &&
					!columnsAffected.Contains(i))
				{
					continue;
				}

				Line column = board.Columns[i];
				if (column.IsEmpty())
				{
					columnsQueue.Enqueue(column);
					continue;
				}

				if (columnsQueue.Count == 0)
				{
					continue;
				}

				Line.MoveLine(column, columnsQueue.Dequeue());
				columnsQueue.Enqueue(column);
			}
		}

		void FindFeature(Cell origin, ElementType type, ElementColor color, Cell[] match)
		{
			if (type != ElementType.Single)
			{
				return;
			}

			if (match.Length >= levelConfig.BombMatchCount)
			{
				IElement element = objectPool.GetObject();
				element.SetType(ElementType.Bomb);
				element.SetColor(color);
				element.SetPosition(origin.position);
				element.SetActive(true);
				origin.Element = element;
			}
		}

		void IMatchProcessor.Clear()
		{
			columnsAffected.Clear();
			columnsAffected = null;

			fallQueue.Clear();
			fallQueue = null;

			columnsQueue.Clear();
			columnsQueue = null;

			levelConfig = null;
			objectPool = null;
		}
	}

	interface IMatchProcessor
	{
		IEnumerator Process(Cell origin, Cell[] match, Board board, List<IElement> elementsToRelease, Action callback);
		void Clear();
	}
}