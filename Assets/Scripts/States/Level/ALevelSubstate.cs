// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.DB;
using LaserGames.Framework;
using System;
using System.Collections;
using UnityEngine;


namespace LaserGames.Collapse.Level
{
	abstract class ALevelSubstate : AState<ILevelSubstate>
	{
		protected static LevelConfig levelConfig;
		protected static LevelController levelController;
		protected static Board board;
		protected static ObjectPool<IElement> elementsObjectPool;
		protected static ILevelElementsRandomizer levelElementsRandomizer;
		protected static IIntroProcessor introProcessor;
		protected static IFeatureApplier featureApplier;
		protected static IMatchFinder matchFinder;
		protected static IMatchApprover matchApprover;
		protected static IMatchProcessor matchProcessor;
		protected static IRowsShifter rowsShifter;
		protected static BoardController boardController;
		protected static ILevelScoreCounter levelScoreCounter;
		protected static ILevelGoalValidator levelGoalValidator;
		protected static ILevelTimer levelTimer;
		protected static Camera camera;


		protected IEnumerator ClearBoardElements(Action callback)
		{
			yield return null;

			for (int i = 0; i < board.Cells.Length; i++)
			{
				if (board.Cells[i].Element != null)
				{
					board.Cells[i].Element.Release();
					board.Cells[i].Element = null;
				}
			}

			yield return null;

			for (int i = 0; i < board.RowBottom.Cells.Length; i++)
			{
				if (board.RowBottom.Cells[i].Element != null)
				{
					board.RowBottom.Cells[i].Element.Release();
					board.RowBottom.Cells[i].Element = null;
				}
			}

			for (int i = 0; i < board.RowTop.Cells.Length; i++)
			{
				if (board.RowTop.Cells[i].Element != null)
				{
					board.RowTop.Cells[i].Element.Release();
					board.RowTop.Cells[i].Element = null;
				}
			}

			callback?.Invoke();
		}

		protected IEnumerator ClearBoard(Action callback)
		{
			yield return null;

			for (int i = 0; i < board.Cells.Length; i++)
			{
				board.Cells[i].Clear();
				board.Cells[i] = null;
			}
			board.Cells = null;

			yield return null;

			for (int i = 0; i < board.Columns.Length; i++)
			{
				board.Columns[i].Clear();
				board.Columns[i] = null;
			}
			board.Columns = null;

			for (int i = 0; i < board.Rows.Length; i++)
			{
				board.Rows[i].Clear();
				board.Rows[i] = null;
			}
			board.Rows = null;

			board.RowBottom.Clear();
			board.RowBottom = null;

			board.RowTop.Clear();
			board.RowTop = null;

			callback?.Invoke();
		}
	}
}