// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Framework;
using LaserGames.Framework.TimeManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LaserGames.Collapse.Level
{
	class BoardController
	{
		Board board = null;
		IBus bus = null;
		ICoroutineController coroutiner = null;
		IFeatureApplier featureApplier = null;
		IMatchFinder matchFinder = null;
		IMatchApprover matchApprover = null;
		IMatchProcessor matchProcessor = null;
		ObjectPool<IElement> objectPool = null;
		IRowsShifter rowsShifter = null;

		bool processing = false;
		List<IElement> elementsToRelease = null;
		IElement[] spawnedElements = null;
		int spawnedElementsIndex = 0;
		bool rowsShiftNeeded = false;


		public BoardController(Board board, IBus bus, ICoroutineController coroutiner, IFeatureApplier featureApplier, IMatchFinder matchFinder,
			IMatchApprover matchApprover, IMatchProcessor matchProcessor, ObjectPool<IElement> objectPool, IRowsShifter rowsShifter)
		{
			this.board = board;
			this.bus = bus;
			this.coroutiner = coroutiner;
			this.featureApplier = featureApplier;
			this.matchFinder = matchFinder;
			this.matchApprover = matchApprover;
			this.matchProcessor = matchProcessor;
			this.objectPool = objectPool;
			this.rowsShifter = rowsShifter;
			this.rowsShifter = rowsShifter;
			elementsToRelease = new List<IElement>(board.Cells.Length / 4);
			spawnedElements = new IElement[board.Width];
			spawnedElementsIndex = 0;
		}

		public void Clear()
		{
			board = null;
			bus = null;
			coroutiner = null;
			matchFinder = null;
			matchApprover = null;
			matchProcessor = null;
			objectPool = null;
			rowsShifter = null;

			elementsToRelease.Clear();
			elementsToRelease = null;

			for (int i = 0; i < spawnedElements.Length; i++)
			{
				spawnedElements[i] = null;
			}
			spawnedElements = null;
		}

		public void Reset()
		{
			elementsToRelease.Clear();
			for (int i = 0; i < spawnedElements.Length; i++)
			{
				spawnedElements[i] = null;
			}
			spawnedElementsIndex = 0;
			processing = false;
		}

		public void LeftButtonDown(Vector2 point)
		{
			if (processing)
			{
				return;
			}

			Cell cell = board.GetCellByPoint(point);
			if (cell == null || cell.Element == null)
			{
				return;
			}

			processing = true;

			if (featureApplier.HasFeature(cell))
			{
				coroutiner.StartCoroutine(featureApplier.Apply(cell, board, FeatureApplierCallback));
				return;
			}
				
			coroutiner.StartCoroutine(matchFinder.Find(cell, MatchFinderCallback));
		}

		public void ElementSpawned((ElementType, ElementColor) description)
		{
			if (spawnedElementsIndex >= board.Width)
			{
				TryShiftRows();
				spawnedElementsIndex = 0;
				return;
			}

			Cell target = board.RowBottom.Cells[spawnedElementsIndex];
			if (target.Element != null)
			{
				Debug.LogError("BoardController.ElementSpawned(): element already exists;");
				return;
			}

			IElement element = objectPool.GetObject();
			element.SetType(description.Item1);
			element.SetColor(description.Item2);
			element.SetPosition(target.position);
			element.SetActive(true);
			target.Element = element;
			spawnedElements[spawnedElementsIndex++] = element;
		}

		void MatchFinderCallback(Cell origin, Cell[] match)
		{
			if (match == null || match.Length == 0)
			{
				ReleaseElementsToRelease();
				processing = false;
				return;
			}

			if (!matchApprover.Approve(match))
			{
				ReleaseElementsToRelease();
				processing = false;
				return;
			}

			bus.Invoke<S_LevelElementsMatched, (ElementType, ElementColor)[]>(BoardUtils.ArchiveMatch(match));

			coroutiner.StartCoroutine(matchProcessor.Process(origin, match, board, elementsToRelease, MatchProcessorCallback));
		}

		void MatchProcessorCallback()
		{
			ReleaseElementsToRelease();

			if (rowsShiftNeeded)
			{
				ShiftRows();
				return;
			}

			processing = false;
		}

		void ReleaseElementsToRelease()
		{
			for (int i = 0, iMax = elementsToRelease.Count; i < iMax; i++)
			{
				elementsToRelease[i].Release();
			}
			elementsToRelease.Clear();
		}

		void TryShiftRows()
		{
			for (int i = 0; i < board.RowBottom.Cells.Length; i++)
			{
				board.RowBottom.Cells[i].Element = null;
			}

			if (processing)
			{
				rowsShiftNeeded = true;
				return;
			}

			ShiftRows();
		}

		void ShiftRows()
		{
			processing = true;
			coroutiner.StartCoroutine(rowsShifter.Shift(board, spawnedElements, RowsShiftCallback));
		}

		void RowsShiftCallback()
		{
			for (int i = 0; i < spawnedElements.Length; i++)
			{
				spawnedElements[i] = null;
			}
			rowsShiftNeeded = false;
			processing = false;

			if (!board.RowTop.IsEmpty())
			{
				bus.Invoke<S_LevelTopRowReached>();
			}
			else
			{
				bus.Invoke<S_LevelRowSpawned>();
			}
		}

		void FeatureApplierCallback(Cell origin, Cell[] match)
		{
			if (match == null ||
				match.Length == 0)
			{
				processing = false;
				return;
			}

			coroutiner.StartCoroutine(matchProcessor.Process(origin, match, board, elementsToRelease,
				MatchProcessorCallback));
		}
	}
}