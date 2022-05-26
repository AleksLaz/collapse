// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Framework.UI;


namespace LaserGames.Collapse.UI
{
	class WLevelComplete : ABusWindowController<BusWindowProperties>
	{


		public void ButtonRestartClick()
		{
			Properties.Bus.Invoke<S_UI_LevelRestart>();
		}

		public void ButtonExitClick()
		{
			Properties.Bus.Invoke<S_UI_LevelExit>();
		}
	}
}