using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace LaserGames.Framework
{
	public abstract class ScriptableObjectSingletonAddressable<T> : ScriptableObject where T : ScriptableObjectSingletonAddressable<T>
	{
		public static T I;
		static AsyncOperationHandle<T> handle;


		public static void LoadAddressables(AssetReference reference, Action<bool> callback)
		{
			Addressables.LoadAssetAsync<T>(reference).Completed += (handle) => LoadComplete(handle, callback);
		}

		static void LoadComplete(AsyncOperationHandle<T> handle, Action<bool> callback)
		{
			if (handle.Status != AsyncOperationStatus.Succeeded)
			{
				Debug.LogError($"ScriptableObjectSingletonAddressable.LoadComplete(): handle {handle.Status}.");
				callback?.Invoke(false);
				return;
			}

			I = handle.Result;
			ScriptableObjectSingletonAddressable<T>.handle = handle;

			callback?.Invoke(true);
		}

		public static void Unload()
		{
			I = null;
			Addressables.Release(handle);
		}
	}
}