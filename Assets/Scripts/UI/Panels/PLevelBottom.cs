// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Framework.UI;


namespace LaserGames.Collapse.UI
{
	class PLevelBottom : ABusPanelController<BusPanelProperties>
	{



		public void ButtonPauseClick()
		{
			Properties.Bus.Invoke<S_UI_LevelPause>();
		}
	}
}