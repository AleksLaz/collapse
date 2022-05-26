// Created by Lazarevich Aleksei in year 2019.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using System;
using System.Collections;
using UnityEngine;

namespace LaserGames.Framework.TimeManagement
{
	public interface ITimeSource
	{
		float deltaTime { get; }
		float fixedDeltaTime { get; }
		//float time { get; }
	}

	enum TimeSourceState
	{
		Play,
		Pause
	}

	public interface ICoroutineController : ITimeSource
	{
		IEnumerator StartCoroutine(IEnumerator routine);
		void StopCoroutine(IEnumerator routine);
		void StopAllCoroutines();
	}

	public interface IUpdateSource
	{
		event Action<float> update;
		event Action<float> fixedUpdate;
		event Action<float> lateUpdate;
	}

	public interface ITimeController : ICoroutineController, IUpdateSource
	{
		void Play();
		void Pause();
	}
}