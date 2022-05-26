// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace LaserGames.Collapse.Level
{
	/// <summary>
	/// Implements specific logic of matching.
	/// It analyses left, right, top and bottom neighbours of cell for match and repeat that 
	/// analyses for each matched neighbour.
	/// </summary>
	class MatchFinder : IMatchFinder
	{
		HashSet<Cell> match = new HashSet<Cell>();
		Queue<Cell> queue = new Queue<Cell>(6);


		/// <summary>
		/// Coroutine lookeng for match.
		/// </summary>
		/// <param name="origin">Initial cell to start search process.</param>
		/// <param name="callback">Function to call after search completed. Receives origin cell and 
		/// matched cells array (inclusing origin).</param>
		/// <returns>This coroutine instanse reference to be run as coroutine.</returns>
		IEnumerator IMatchFinder.Find(Cell origin, Action<Cell, Cell[]> callback)
		{
			if (origin == null || origin.Element == null)
			{
				callback?.Invoke(origin, null);
				yield break;
			}

			match.Clear();
			match.Add(origin);
			queue.Clear();
			queue.Enqueue(origin);

			var element = origin.Element;
			var color = origin.Element.Color();

			while (queue.Count > 0)
			{
				Cell source = queue.Dequeue();

				if (source.Bottom != null &&
					source.Bottom.Element != null &&
					source.Bottom.Element.Color() == color)
				{
					if (match.Add(source.Bottom))
					{
						queue.Enqueue(source.Bottom);
					}
				}

				if (source.Left != null &&
					source.Left.Element != null &&
					source.Left.Element.Color() == color)
				{
					if (match.Add(source.Left))
					{
						queue.Enqueue(source.Left);
					}
				}

				if (source.Top != null &&
					source.Top.Element != null &&
					source.Top.Element.Color() == color)
				{
					if (match.Add(source.Top))
					{
						queue.Enqueue(source.Top);
					}
				}

				if (source.Right != null &&
					source.Right.Element != null &&
					source.Right.Element.Color() == color)
				{
					if (match.Add(source.Right))
					{
						queue.Enqueue(source.Right);
					}
				}
			}

			callback?.Invoke(origin, match.ToArray());
		}

		/// <summary>
		/// Clears instance fields.
		/// </summary>
		void IMatchFinder.Clear()
		{
			match.Clear();
			match = null;
			queue.Clear();
			queue = null;
		}
	}

	/// <summary>
	/// Interface for match finders.
	/// </summary>
	interface IMatchFinder
	{
		IEnumerator Find(Cell cell, Action<Cell, Cell[]> callback);
		void Clear();
	}
}