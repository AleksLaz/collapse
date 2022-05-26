// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Framework;
using UnityEngine;


namespace LaserGames.Collapse.Level
{
	class LevelElementsInstantiator : MonoBehaviour, IObjectPoolInstantiator<IElement>
	{
		[SerializeField]
		LevelElement elementPrefab;


		IElement IObjectPoolInstantiator<IElement>.Instantiate()
		{
			LevelElement element = Instantiate(elementPrefab, Vector3.zero, Quaternion.identity);
			IElement result = new Element(element);
			result.SetActive(false);
			return result;
		}
	}
}