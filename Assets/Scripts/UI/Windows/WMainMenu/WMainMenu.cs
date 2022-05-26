// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.DB;
using LaserGames.Framework;
using LaserGames.Framework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace LaserGames.Collapse.UI
{
	class WMainMenu : ABusWindowController<WMainMenuProperties>
	{
		[SerializeField]
		GameObject buttonPlay;

		[SerializeField]
		GameObject levelsView;

		[SerializeField]
		ItemSelectLevel itemTemplate;


		List<ItemSelectLevel> items = new List<ItemSelectLevel>();


		public void ButtonPlayClick()
		{
			buttonPlay.SetActive(false);
			levelsView.SetActive(true);
		}

		public void ButtonExitClick()
		{
			Properties.Bus.Invoke<S_UI_MainMenuExit>();
		}

		protected override void OnPropertiesSet()
		{
			UpdateItems();
		}

		void UpdateItems()
		{
			if (items.Count > 0)
			{
				return;
			}

			for (int i = 0; i < Properties.levelsCount; i++)
			{
				if (DBLevels.I.GetLevel(i, out LevelConfig level))
				{
					var item = Instantiate(itemTemplate,
								itemTemplate.transform.parent,
								false); // Never forget to pass worldPositionStays as false for UI!

					item.gameObject.SetActive(true);
					items.Add(item);
					item.SetData(i, level.Name);
					item.onClick += ItemClick;
				}
			}
		}

		void ItemClick(int id)
		{
			if (id < 0 && id >= items.Count)
			{
				return;
			}

			PlayerPrefs.SetInt(PrefsKeys.SelectedLevelId, id);
			Properties.Bus.Invoke<S_UI_MainMenuLevelSelect>();
		}
	}

	[Serializable]
	class WMainMenuProperties : BusWindowProperties
	{
		public int levelsCount;

		public WMainMenuProperties(IBus bus, int levelsCount) : base(bus)
		{
			this.levelsCount = levelsCount;
		}
	}
}