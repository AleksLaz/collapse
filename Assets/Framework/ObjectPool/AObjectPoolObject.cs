// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using UnityEngine;


namespace LaserGames.Framework
{
	/// <summary>
	/// Базовый класс для объектов, которые погут использоваться с объуктным пулом ObjectPool.
	/// </summary>
	public abstract class AObjectPoolObject<T> : IObjectPoolObject where T : IObjectPoolObject
	{
		GameObject gameObject = null;
		IObjectPool objectPool = null;


		public AObjectPoolObject(GameObject gameObject)
		{
			this.gameObject = gameObject;
		}

		public void SetPool(IObjectPool objectPool)
		{
			this.objectPool = objectPool;
		}

		public void Release()
		{
			SetActive(false);

			if (objectPool != null)
			{
				objectPool.ReleaseObject(this);
			}
		}

		public void SetActive(bool value)
		{
			gameObject.SetActive(value);
		}

		public virtual void Clear()
		{
			Object.Destroy(gameObject);
			gameObject = null;
			objectPool = null;
		}
	}

	public interface IObjectPoolObject
	{
		void SetPool(IObjectPool objectPool);
		void Release();
		void SetActive(bool value);
		void Clear();
	}
}