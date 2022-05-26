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
	class Level_Load : ALevelSubstate, ILevelSubstate
	{
		

		void LoadScene()
		{
			Extensions.LoadSceneAsync(timeController, levelConfig.SceneName, LoadSceneCallback);
		}

		void LoadSceneCallback()
		{
			levelController = UnityEngine.Object.FindObjectOfType<LevelController>();

			if (levelController == null)
			{
				context.SwitchState<Level_Exit, Level_Load>();
				return;
			}

			timeController.StartCoroutine(BoardBuilder.Create(levelConfig.Width, levelConfig.Height, levelConfig.CellSize,
				BoardBuilderCallback));
		}

		void BoardBuilderCallback(Board board)
		{
			ALevelSubstate.board = board;

			levelController.BoardView.localScale = new Vector3(board.Width, board.Height + 2, 1f);

			levelController.BoardTopDelimeter.localScale = new Vector3(board.Width, 0.1f, 1f);
			levelController.BoardTopDelimeter.position = board.TopDelimeterPosition;
			
			levelController.BoardBottomDelimeter.localScale = new Vector3(board.Width, 0.1f, 1f);
			levelController.BoardBottomDelimeter.position = board.BottomDelimeterPosition;

			BuiltObjectPools();
		}

		void BuiltObjectPools()
		{
			elementsObjectPool = new ObjectPool<IElement>(board.Cells.Length, levelController.ElementsInstantiator);
			timeController.StartCoroutine(elementsObjectPool.Initialize(25, InitObjectPoolCallback));
		}

		void InitObjectPoolCallback()
		{
			timeController.StartCoroutine(BuildBoardController(BuildBoardControllerCallback));
		}

		IEnumerator BuildBoardController(Action callback)
		{
			introProcessor = new IntroProcessor();
			levelElementsRandomizer = new LevelElementsRandomizer(levelConfig.ElementTypes, levelConfig.ElementColors);
			featureApplier = new FeatureApplier(bus);
			matchFinder = new MatchFinder();
			matchApprover = new MatchApprover(levelConfig.MinMatchLength);
			matchProcessor = new MatchProcessor(elementsObjectPool, levelConfig);
			rowsShifter = new RowsShifter();

			yield return null;

			boardController = new BoardController(board, bus, timeController, featureApplier, matchFinder, matchApprover, matchProcessor,
				elementsObjectPool, rowsShifter);

			levelScoreCounter = new LevelScoreCounter(bus, levelConfig);
			levelGoalValidator = new LevelGoalValidator(bus, levelConfig);

			if (levelConfig.goal == GoalType.Time)
			{
				levelTimer = new LevelTimer(bus, timeController, levelConfig.GoalValue);
			}

			callback?.Invoke();
		}

		void BuildBoardControllerCallback()
		{
			camera = Camera.main;

			context.SwitchState<Level_Reset, Level_Load>();
		}

		#region AState
		public override void Activate<T>()
		{
			if (!DBLevels.I.GetLevel(PlayerPrefs.GetInt(PrefsKeys.SelectedLevelId), out levelConfig))
			{
				context.SwitchState<Level_Exit, Level_Load>();
				return;
			}

			LoadScene();
		}
		#endregion AState
	}
}
