// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using deVoid.UIFramework;
using UnityEngine;
using LaserGames.Framework.TimeManagement;
using DG.Tweening;
using LaserGames.Framework;
using LaserGames.Collapse.UI;
using System.Collections;
using LaserGames.Collapse.Models;
using System.Runtime.Remoting.Contexts;
using LaserGames.Collapse.DB;

namespace LaserGames.Collapse
{
	class GlobalController : MonoBehaviour
	{
		[SerializeField] GlobalTimeBehaviour globalTimeController = null;
		[SerializeField] UISettings uiSettings = null;
		[SerializeField] string LoadingWindowId = string.Empty;

		IContext<IGlobalState> globalContext = null;
		UIFrame uiFrame = null;
		ITimeController timeController = null;
		IBus bus = null;
		InputProcessor inputProcessor = null;


		void Awake()
		{
			uiFrame = uiSettings.CreateUIInstance();
			uiFrame.OpenWindow(LoadingWindowId);
		}

		void Start()
		{
			StartCoroutine(Initialize());
		}

		IEnumerator Initialize()
		{
			DOTween.defaultTimeScaleIndependent = true;

			bus = BusFabric.CreateBus();
			UIHelper.Initialize(bus, uiFrame);

			yield return null;

			DBPlayer.Load();
			ModelPlayer modelPlayer = ModelBuilder.CreatePlayer();
			yield return modelPlayer.Load();
			yield return modelPlayer.Save();

			timeController = new TimeController(globalTimeController, globalTimeController);

			globalContext = new GlobalContext(bus, timeController, modelPlayer);

			timeController.Play();

			inputProcessor = new InputProcessor(timeController, bus, Camera.main);

			Subscribe();

			yield return null;

			globalContext.SwitchState<StartState, INullState>();
		}

		void Subscribe()
		{
		}
	}
}