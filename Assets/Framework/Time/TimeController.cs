// Created by Lazarevich Aleksei in year 2019.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LaserGames.Framework.TimeManagement
{
	public class TimeController : ITimeController
	{
		TimeSourceState state = TimeSourceState.Pause;

		ITimeSource timeSource;
		IUpdateSource updateSource;
		HashSet<IEnumerator> coroutines = new HashSet<IEnumerator>();
		HashSet<IEnumerator> coroutinesToStart = new HashSet<IEnumerator>();
		HashSet<IEnumerator> coroutinesToRemove = new HashSet<IEnumerator>();

		Action<float> update = null;
		Action<float> fixedUpdate = null;
		Action<float> lateUpdate = null;

		public TimeController(ITimeSource timeSource, IUpdateSource updateSource)
		{
			this.timeSource = timeSource;
			this.updateSource = updateSource;
		}

		void Subscribe()
		{
			updateSource.update += Update;
			updateSource.fixedUpdate += FixedUpdate;
			updateSource.lateUpdate += LateUpdate;
		}

		void Unsubscribe()
		{
			updateSource.update -= Update;
			updateSource.fixedUpdate -= FixedUpdate;
			updateSource.lateUpdate -= LateUpdate;
		}

		void Update(float deltaTime)
		{
			if (state == TimeSourceState.Pause)
			{
				return;
			}

			update?.Invoke(deltaTime);

			foreach (var coroutine in coroutinesToRemove)
			{
				coroutines.Remove(coroutine);
			}
			coroutinesToRemove.Clear();

			coroutines.UnionWith(coroutinesToStart);
			coroutinesToStart.Clear();

			foreach (var coroutine in coroutines)
			{
				if (!coroutine.MoveNext())
				{
					coroutinesToRemove.Add(coroutine);
				}
			}
		}

		void FixedUpdate(float deltaTime)
		{
			if (state == TimeSourceState.Pause)
			{
				return;
			}

			fixedUpdate?.Invoke(deltaTime);
		}

		void LateUpdate(float deltaTime)
		{
			if (state == TimeSourceState.Pause)
			{
				return;
			}

			lateUpdate?.Invoke(deltaTime);
		}

		float ITimeSource.deltaTime => state == TimeSourceState.Play ? timeSource.deltaTime : 0f;
		float ITimeSource.fixedDeltaTime => state == TimeSourceState.Play ? timeSource.fixedDeltaTime : 0f;


		event Action<float> IUpdateSource.update
		{
			add { update += value; }
			remove { update -= value; }
		}

		event Action<float> IUpdateSource.fixedUpdate
		{
			add { fixedUpdate += value; }
			remove { fixedUpdate -= value; }
		}

		event Action<float> IUpdateSource.lateUpdate
		{
			add { lateUpdate += value; }
			remove { lateUpdate -= value; }
		}

		void ITimeController.Play()
		{
			Subscribe();
			state = TimeSourceState.Play;
		}

		void ITimeController.Pause()
		{
			Unsubscribe();
			state = TimeSourceState.Pause;
		}


		IEnumerator ICoroutineController.StartCoroutine(IEnumerator coroutine)
		{
			coroutinesToStart.Add(coroutine);
			return coroutine;
		}

		void ICoroutineController.StopCoroutine(IEnumerator coroutine)
		{
			coroutinesToRemove.Add(coroutine);
		}

		void ICoroutineController.StopAllCoroutines()
		{
			coroutinesToRemove.UnionWith(coroutines);
			coroutinesToRemove.UnionWith(coroutinesToStart);

			coroutinesToStart.Clear();
		}
	}
}
