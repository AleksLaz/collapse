// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//


namespace LaserGames.Collapse.Level
{
	class Line
	{
		public Cell[] Cells;

		public Line(int cellsCount)
		{
			Cells = new Cell[cellsCount];
		}

		public bool IsEmpty()
		{
			for (int i = 0; i < Cells.Length; i++)
			{
				if (Cells[i].Element != null)
				{
					return false;
				}
			}

			return true;
		}

		public static void MoveLine(Line source, Line target)
		{
			for (int i = 0; i < source.Cells.Length; i++)
			{
				target.Cells[i].Element = source.Cells[i].Element;

				if (source.Cells[i].Element != null)
				{
					target.Cells[i].Element.SetPosition(target.Cells[i].position);
					source.Cells[i].Element = null;
				}
			}
		}

		public static void MoveLine(IElement[] source, Line target)
		{
			for (int i = 0; i < source.Length; i++)
			{
				target.Cells[i].Element = source[i];

				if (source[i] != null)
				{
					target.Cells[i].Element.SetPosition(target.Cells[i].position);
					source[i] = null;
				}
			}
		}

		public void Clear()
		{
			for (int i = 0; i < Cells.Length; i++)
			{
				Cells[i].Clear();
				Cells[i] = null;
			}

			Cells = null;
		}
	}
}