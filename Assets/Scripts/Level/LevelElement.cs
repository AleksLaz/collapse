// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using UnityEngine;


namespace LaserGames.Collapse.Level
{
	class LevelElement : MonoBehaviour
	{
		[SerializeField]
		SpriteRenderer singleSR;

		[SerializeField]
		SpriteRenderer bombSR;

		[SerializeField]
		GameObject singleGO;

		[SerializeField]
		GameObject bombGO;


		public void SetColor(Color color)
		{
			singleSR.color = color;
			bombSR.color = color;
		}

		public void SetSingle()
		{
			singleGO.SetActive(true);
			bombGO.SetActive(false);
		}

		public void SetBomb()
		{
			singleGO.SetActive(false);
			bombGO.SetActive(true);
		}
	}
}