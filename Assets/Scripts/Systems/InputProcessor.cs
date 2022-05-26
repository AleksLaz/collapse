// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Framework;
using LaserGames.Framework.TimeManagement;
using UnityEngine;
using UnityEngine.EventSystems;


namespace LaserGames.Collapse
{
	/// <summary>
	/// Обрабатывает Unity input.
	/// </summary>
	class InputProcessor
	{
		IUpdateSource updateSource = null;
		IBus bus = null;
		Camera camera = null;


		/// <param name="UpdateSource">Источник Update.</param>
		/// <param name="camera">Активная камера.</param>
		public InputProcessor(IUpdateSource updateSource, IBus bus, Camera camera)
		{
			this.updateSource = updateSource;
			this.bus = bus;
			this.camera = camera;

			Subcribe();
		}


		void Subcribe()
		{
			updateSource.update += Update;
		}

		void Update(float deltaTime)
		{
			if (Input.GetMouseButtonDown(0))
			{
				ProcesLeftDown(camera.ScreenToWorldPoint(Input.mousePosition));
			}
		}

		void ProcesLeftDown(Vector3 position)
		{
			if (!EventSystem.current.IsPointerOverGameObject())
			{
				bus.Invoke<S_MouseLeftButtonDown, Vector3>(position);
			}
		}
	}
}
