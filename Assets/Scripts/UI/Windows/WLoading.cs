// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using deVoid.UIFramework;
using UnityEngine;


namespace LaserGames.Collapse.UI
{
	class WLoading : AWindowController
	{
		[SerializeField] Transform indicator = null;

		private void Update()
		{
			indicator.Rotate(0, 0, -200 * Time.deltaTime, Space.Self);
		}
	}
}