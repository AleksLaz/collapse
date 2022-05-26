// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Framework.UI;


namespace LaserGames.Collapse.UI
{
	class WLevelMenu : ABusWindowController<BusWindowProperties>
	{


		public void ButtonResumeClick()
		{
			Properties.Bus.Invoke<S_UI_LevelResume>();
		}

		public void ButtonRestartClick()
		{
			Properties.Bus.Invoke<S_UI_LevelRestart>();
		}

		public void ButtonMeainMenuClick()
		{
			Properties.Bus.Invoke<S_UI_LevelExit>();
		}
	}
}