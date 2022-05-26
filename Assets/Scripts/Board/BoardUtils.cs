// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using System.Collections.Generic;


namespace LaserGames.Collapse.Level
{
	static class BoardUtils
	{
		public static (ElementType, ElementColor)[] ArchiveMatch(Cell[] match)
		{
			(ElementType, ElementColor)[] result = new (ElementType, ElementColor)[match.Length];

			for (int i = 0; i < match.Length; i++)
			{
				result[i] = (match[i].Element.Type(), match[i].Element.Color());
			}

			return result;
		}

		public static (ElementType, ElementColor)[] ArchiveMatch(List<Cell> match)
		{
			(ElementType, ElementColor)[] result = new (ElementType, ElementColor)[match.Count];

			for (int i = 0, iMax = match.Count; i < iMax; i++)
			{
				result[i] = (match[i].Element.Type(), match[i].Element.Color());
			}

			return result;
		}

		public static (ElementType, ElementColor)[] ArchiveMatch(HashSet<Cell> match)
		{
			(ElementType, ElementColor)[] result = new (ElementType, ElementColor)[match.Count];

			int i = 0;
			foreach (var cell in match)
			{
				result[i++] = (cell.Element.Type(), cell.Element.Color());
			}

			return result;
		}
	}
}
