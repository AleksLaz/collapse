// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Framework.TimeManagement;
using System;
using System.Collections;
using UnityEngine.SceneManagement;


namespace LaserGames.Framework
{
	public static class Extensions
	{
		public static void LoadSceneAsync(ICoroutineController coroutineController, string sceneName, Action callback,
			LoadSceneMode mode = LoadSceneMode.Additive)
		{
			coroutineController.StartCoroutine(LoadScene(sceneName, callback, mode));
		}

		public static void UnloadSceneAsync(ICoroutineController coroutineController, string sceneName, Action callback, float delay = 0.5f)
		{
			coroutineController.StartCoroutine(UnloadScene(sceneName, callback, coroutineController, delay));
		}

		static IEnumerator LoadScene(string sceneName, Action callback, LoadSceneMode mode = LoadSceneMode.Additive)
		{
			var op = SceneManager.LoadSceneAsync(sceneName, mode);

			while (!op.isDone)
			{
				yield return null;
			}

			callback?.Invoke();
		}

		static IEnumerator UnloadScene(string sceneName, Action callback, ITimeSource timeSource, float delay)
		{
			TimeManagement.WaitForSeconds wfs = new TimeManagement.WaitForSeconds(timeSource, delay);
			while (wfs.MoveNext())
			{
				yield return null;
			}

			var op = SceneManager.UnloadSceneAsync(sceneName);
			while (!op.isDone)
			{
				yield return null;
			}

			callback?.Invoke();
		}

		public static bool HasElements<T>(this T[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					return false;
				}
			}

			return true;
		}

		public static IEnumerator InvokeDelay(ITimeSource timeSource, float delay, Action callback)
		{
			var wfs = new TimeManagement.WaitForSeconds(timeSource, delay);
			while (wfs.MoveNext())
			{
				yield return null;
			}

			callback?.Invoke();
		}
	}
}