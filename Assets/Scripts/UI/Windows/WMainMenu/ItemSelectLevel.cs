// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace LaserGames.Collapse.UI
{
	class ItemSelectLevel : MonoBehaviour
	{
		[SerializeField]
		Button button;

		[SerializeField]
		TextMeshProUGUI text;

		int id;


		public event Action<int> onClick;


		private void Awake()
		{
			button.onClick.AddListener(ButtonClick);
		}

		public void SetData(int id, string text)
		{
			this.text.text = text;
			this.id = id;
		}

		void ButtonClick()
		{
			onClick?.Invoke(id);
		}
	}
}