// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using UnityEngine;


namespace LaserGames.Collapse.Level
{
	class LevelElementsRandomizer : ILevelElementsRandomizer
	{
		ILevelElementsRandomizer self;
		ElementType[] elementTypes;
		ElementColor[] elementColors;
		int elementTypesLength;
		int elementColorsLength;


		public LevelElementsRandomizer()
		{
			self = this;
		}

		public LevelElementsRandomizer(ElementType[] elementTypes, ElementColor[] elementColors)
		{
			self = this;
			self.Reinitialize(elementTypes, elementColors);
		}


		#region ILevelElementsRandomizer
		void ILevelElementsRandomizer.Reinitialize(ElementType[] elementTypes, ElementColor[] elementColors)
		{
			this.elementTypes = elementTypes;
			this.elementColors = elementColors;
			elementTypesLength = elementTypes.Length;
			elementColorsLength = elementColors.Length;
		}

		(ElementType, ElementColor)[] ILevelElementsRandomizer.RndSet(int size)
		{
			if (size <= 0)
			{
				Debug.LogError("LevelElementsRandomizer.RndSet(): wrong size.");
				return null;
			}

			(ElementType, ElementColor)[] result = new (ElementType, ElementColor)[size];
			for (int i = 0; i < size; i++)
			{
				result[i] = self.RndoneSingle();
			}

			return result;
		}

		(ElementType, ElementColor) ILevelElementsRandomizer.RndoneSingle()
		{
			return (ElementType.Single, elementColors[Random.Range(0, elementColorsLength)]);
		}

		void ILevelElementsRandomizer.Clear()
		{
			self = null;
			elementTypes = null;
			elementColors = null;
		}
		#endregion ILevelElementsRandomizer
	}

	interface ILevelElementsRandomizer
	{
		void Reinitialize(ElementType[] elementTypes, ElementColor[] elementColors);
		(ElementType, ElementColor) RndoneSingle();
		(ElementType, ElementColor)[] RndSet(int size);
		void Clear();
	}
}