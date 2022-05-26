// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using deVoid.UIFramework;


namespace LaserGames.Framework.UI
{
	public abstract class ABusWindowController<T> : AWindowController<T> where T : BusWindowProperties
	{
	}

	[System.Serializable]
	public class BusWindowProperties : WindowProperties
	{
		public IBus Bus;

		public BusWindowProperties(IBus bus)
		{
			Bus = bus;
		}
	}
}