// Created by Lazarevich Aleksei in year 2019.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using System.Collections;


namespace LaserGames.Framework.TimeManagement
{
	class WaitForSeconds : IEnumerator
	{
		float secondsPassed = 0;
		ITimeSource timeSource;
		float seconds = 0;


		public WaitForSeconds(ITimeSource timeSource, float seconds)
		{
			this.timeSource = timeSource;
			this.seconds = seconds;
		}

		public bool MoveNext()
		{
			if (secondsPassed >= seconds)
			{
				Reset();
				return false;
			}

			secondsPassed += timeSource.deltaTime;

			return true;
		}

		public object Current { get { return null; } }

		public void Reset() { secondsPassed = 0; }
	}
}