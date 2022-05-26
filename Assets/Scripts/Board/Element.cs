// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Framework;
using UnityEngine;


namespace LaserGames.Collapse.Level
{
	public enum ElementType
	{
		Single,
		Bomb
	}

	public enum ElementColor
	{ 
		Red,
		Green,
		Blue,
		Yellow,
		Magenta
	}

	class Element : AObjectPoolObject<IElement>, IElement
	{
		ElementType type;
		ElementColor color;
		LevelElement element = null;
		Transform transform = null;


		public Element(LevelElement element) : base(element.gameObject)
		{
			this.element = element;
			transform = element.transform;
		}

		#region IElement
		public ElementColor Color()
		{
			return color;
		}

		public void SetColor(ElementColor color)
		{
			this.color = color;

			Color rgba;
			switch (color)
			{
				case ElementColor.Red:
					rgba = UnityEngine.Color.red;
					break;

				case ElementColor.Green:
					rgba = UnityEngine.Color.green;
					break;

				case ElementColor.Blue:
					rgba = UnityEngine.Color.blue;
					break;

				case ElementColor.Yellow:
					rgba = UnityEngine.Color.yellow;
					break;

				case ElementColor.Magenta:
					rgba = UnityEngine.Color.magenta;
					break;

				default:
					rgba = UnityEngine.Color.white;
					break;
			}

			element.SetColor(rgba);
		}

		public void SetPosition(Vector2 position)
		{
			transform.position = position;
		}

		public ElementType Type()
		{
			return type;
		}

		public void SetType(ElementType type)
		{
			this.type = type;

			if (type == ElementType.Single)
			{
				element.SetSingle();
			}
			else
			{
				element.SetBomb();
			}
		}

		public override void Clear()
		{
			transform = null;
			element = null;

			base.Clear();
		}
		#endregion IElement
	}

	interface IElement : IObjectPoolObject
	{
		ElementType Type();
		void SetType(ElementType type);
		ElementColor Color();
		void SetPosition(Vector2 position);
		void SetColor(ElementColor color);
	}
}