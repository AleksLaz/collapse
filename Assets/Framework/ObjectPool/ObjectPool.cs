//
// ObjectPool.cs
//
// Created by Lazarevich Aleksey in 26.10.2015.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace LaserGames.Framework
{
	/// <summary>
	/// Базовый класс объектных пулов для типов объектов производных от ObjectPoolObject.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ObjectPool<T> : AObjectPool<T> where T : IObjectPoolObject
	{
		public ObjectPool(int capacity, IObjectPoolInstantiator<T> instantiator) : base(capacity, instantiator)
		{ }

		protected override void InitializeObject(T obj)
		{
			obj.SetPool(this);
		}
	}

	public abstract class AObjectPool<T> : IObjectPool where T : IObjectPoolObject
	{
		/// <summary>
		/// Начальная ёмкость пула объектов.
		/// </summary>
		int capacity = 1;

		/// <summary>
		/// Коллекция свободных объектов пула.
		/// </summary>
		Queue<IObjectPoolObject> availableObjects = null;
		HashSet<IObjectPoolObject> duplicatesCheck = null;
		HashSet<IObjectPoolObject> occupied = null;

		IObjectPoolInstantiator<T> instantiator = null;


		public AObjectPool(int capacity, IObjectPoolInstantiator<T> instantiator)
		{
			this.capacity = capacity;
			this.instantiator = instantiator;
			availableObjects = new Queue<IObjectPoolObject>(capacity);
			duplicatesCheck = new HashSet<IObjectPoolObject>();
			occupied = new HashSet<IObjectPoolObject>();
		}

		public IEnumerator Initialize(int interval, Action callback)
		{
			yield return null;

			for (int i = 0, y=0; i < capacity; i++, y++)
			{ 
				if (y == interval)
				{
					y = 0;
					yield return null;
				}

				var obj = InstantiateObject();
				availableObjects.Enqueue(obj);
				duplicatesCheck.Add(obj);
			}

			callback?.Invoke();
		}

		/// <summary>
		/// Помещает объекст в коллекцию свободных объектов пула.
		/// </summary>
		/// <param name="poolObject">Ссылка на объект, который нужно поместить в коллекцию.</param>
		public void ReleaseObject(IObjectPoolObject poolObject)
		{
			if (poolObject != null)
			{
				if (duplicatesCheck.Add(poolObject))
				{
					availableObjects.Enqueue(poolObject);
				}

				occupied.Remove(poolObject);
			}
			else
			{
				Debug.LogWarning("AObjectPool.ReleaseObject(): poolObject is null.");
			}
		}

		/// <summary>
		/// Выбирает объект из пула свободных объектов или инстанцирует новый.
		/// Инициализирует объект в обоих случаях.
		/// </summary>
		/// <returns>Ссылка на найденный или созданный объект</returns>
		public T GetObject()
		{
			T result = default;
			if (availableObjects.Count > 0)
			{
				result = (T)availableObjects.Dequeue();
				duplicatesCheck.Remove(result);
			}
			else
			{
				result = InstantiateObject();
			}

			occupied.Add(result);
			return result;
		}

		T InstantiateObject()
		{
			T result = instantiator.Instantiate();
			InitializeObject(result);

			return result;
		}

		public IEnumerator Clear(Action callback)
		{
			availableObjects.Clear();

			foreach (var obj in duplicatesCheck)
			{
				obj.Clear();
			}
			duplicatesCheck.Clear();

			yield return null;

			foreach (var obj in occupied)
			{
				obj.Clear();
			}
			occupied.Clear();

			callback?.Invoke();
		}

		protected abstract void InitializeObject(T obj);
	}

	public interface IObjectPoolInstantiator<T> where T : IObjectPoolObject
	{
		T Instantiate();
	}

	public interface IObjectPool
	{
		void ReleaseObject(IObjectPoolObject poolObject);
		IEnumerator Clear(Action callback);
	}
}