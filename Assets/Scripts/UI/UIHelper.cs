// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using deVoid.UIFramework;
using LaserGames.Collapse.DB;
using LaserGames.Framework;
using LaserGames.Framework.UI;


namespace LaserGames.Collapse.UI
{
	public static class UIHelper
	{
		static IBus bus = null;
		static UIFrame frame = null;
		static BusPanelProperties busPanelProperties = null;
		static BusWindowProperties busWindowProperties = null;

		public static void Initialize(IBus bus, UIFrame frame)
		{
			UIHelper.bus = bus;
			UIHelper.frame = frame;
			busPanelProperties = new BusPanelProperties(bus);
			busWindowProperties = new BusWindowProperties(bus);

			Subscribe();
		}

		static void Subscribe()
		{
			// windows
			bus.AddListener<S_OpenLoadingWindow>(H_OpenLoadingWindow);
			bus.AddListener<S_CloseLoadingWindow>(H_CloseLoadingWindow);

			bus.AddListener<S_OpenLevelDefeatWindow>(H_OpenLevelDefeatWindow);
			bus.AddListener<S_CloseLevelDefeatWindow>(H_CloseLevelDefeatWindow);

			bus.AddListener<S_OpenLevelVictoryWindow>(H_OpenLevelVictoryWindow);
			bus.AddListener<S_CloseLevelVictoryWindow>(H_CloseLevelVictoryWindow);

			bus.AddListener<S_OpenLevelMenuWindow>(H_OpenLevelMenuWindow);
			bus.AddListener<S_CloseLevelMenuWindow>(H_CloseLevelMenuWindow);

			bus.AddListener<S_OpenMainMenuWindow, int>(H_OpenMainMenuWindow);
			bus.AddListener<S_CloseMainMenuWindow>(H_CloseMainMenuWindow);

			bus.AddListener<S_OpenLevelCompleteWindow>(H_OpenLevelCompleteWindow);
			bus.AddListener<S_CloseLevelCompleteWindow>(H_CloseLevelCompleteWindow);


			// panels
			bus.AddListener<S_ShowLevelTopPanel, (GoalType, int)>(H_ShowLevelTopPanel);
			bus.AddListener<S_HideLevelTopPanel>(H_HideLevelTopPanel);

			bus.AddListener<S_ShowLevelBottomPanel>(H_ShowLevelBottomPanel);
			bus.AddListener<S_HideLevelBottomPanel, bool>(H_HideLevelBottomPanel);
		}

		static void H_ShowLevelBottomPanel()
		{
			frame.ShowPanel(DBScreenIds.I.LevelBottomPanel, busPanelProperties);
		}

		static void H_HideLevelBottomPanel(bool animate)
		{
			frame.HidePanel(DBScreenIds.I.LevelBottomPanel, animate);
		}

		static void H_OpenLoadingWindow()
		{
			frame.OpenWindow(DBScreenIds.I.LoadingWindow);
		}

		static void H_CloseLoadingWindow()
		{
			frame.CloseWindow(DBScreenIds.I.LoadingWindow);
		}

		static void H_ShowLevelTopPanel((GoalType, int) parameters)
		{
			frame.ShowPanel(DBScreenIds.I.LevelTopPanel, new PLevelTopProperties(bus, parameters.Item1, parameters.Item2));
		}

		static void H_HideLevelTopPanel()
		{
			frame.HidePanel(DBScreenIds.I.LevelTopPanel);
		}

		static void H_OpenLevelDefeatWindow()
		{
			frame.OpenWindow(DBScreenIds.I.LevelDefeatWindow);
		}

		static void H_CloseLevelDefeatWindow()
		{
			frame.CloseWindow(DBScreenIds.I.LevelDefeatWindow);
		}

		static void H_OpenLevelVictoryWindow()
		{
			frame.OpenWindow(DBScreenIds.I.LevelVictoryWindow);
		}

		static void H_CloseLevelVictoryWindow()
		{
			frame.CloseWindow(DBScreenIds.I.LevelVictoryWindow);
		}

		static void H_OpenLevelMenuWindow()
		{
			frame.OpenWindow(DBScreenIds.I.LevelMenuWindow, busWindowProperties);
		}

		static void H_CloseLevelMenuWindow()
		{
			frame.CloseWindow(DBScreenIds.I.LevelMenuWindow);
		}

		static void H_OpenMainMenuWindow(int levelsCount)
		{
			frame.OpenWindow(DBScreenIds.I.MainMenuWindow, new WMainMenuProperties(bus, levelsCount));
		}

		static void H_CloseMainMenuWindow()
		{
			frame.CloseWindow(DBScreenIds.I.MainMenuWindow);
		}

		static void H_OpenLevelCompleteWindow()
		{
			frame.OpenWindow(DBScreenIds.I.LevelComplete, busWindowProperties);
		}

		static void H_CloseLevelCompleteWindow()
		{
			frame.CloseWindow(DBScreenIds.I.LevelComplete);
		}
	}

	#region Signals
	// windows
	public class S_OpenLoadingWindow : ASignalNoParam { }
	public class S_CloseLoadingWindow : ASignalNoParam { }

	public class S_OpenLevelDefeatWindow : ASignalNoParam { }
	public class S_CloseLevelDefeatWindow : ASignalNoParam { }

	public class S_OpenLevelVictoryWindow : ASignalNoParam { }
	public class S_CloseLevelVictoryWindow : ASignalNoParam { }

	public class S_OpenLevelMenuWindow : ASignalNoParam { }
	public class S_CloseLevelMenuWindow : ASignalNoParam { }

	public class S_OpenMainMenuWindow : ASignalParam<int> { }
	public class S_CloseMainMenuWindow : ASignalNoParam { }

	public class S_OpenLevelCompleteWindow : ASignalNoParam { }
	public class S_CloseLevelCompleteWindow : ASignalNoParam { }

	// panels
	public class S_ShowLevelTopPanel : ASignalParam<(GoalType, int)> { }
	public class S_HideLevelTopPanel : ASignalNoParam { }

	public class S_ShowLevelBottomPanel : ASignalNoParam { }
	public class S_HideLevelBottomPanel : ASignalParam<bool> { }
	#endregion
}