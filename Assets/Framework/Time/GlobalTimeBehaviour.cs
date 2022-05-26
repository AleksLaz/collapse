// Created by Lazarevich Aleksei in year 2019.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using System;
using UnityEngine;


namespace LaserGames.Framework.TimeManagement
{
	class GlobalTimeBehaviour : MonoBehaviour, ITimeSource, IUpdateSource
	{
		Action<float> update = null;
		Action<float> fixedUpdate = null;
		Action<float> lateUpdate = null;


		void Update()
		{
			update?.Invoke(Time.unscaledDeltaTime);
		}

		void FixedUpdate()
		{
			fixedUpdate?.Invoke(Time.fixedUnscaledDeltaTime);
		}

		void LateUpdate()
		{
			lateUpdate?.Invoke(Time.unscaledDeltaTime);
		}

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

		float ITimeSource.deltaTime => Time.deltaTime;
		float ITimeSource.fixedDeltaTime => Time.fixedDeltaTime;
		//float ITimeSource.time => state == TimeSourceState.Run ? Time.time : 0f;
	}
}