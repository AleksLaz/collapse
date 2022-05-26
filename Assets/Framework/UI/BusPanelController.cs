// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using deVoid.UIFramework;


namespace LaserGames.Framework.UI
{
	public abstract class ABusPanelController<T> : APanelController<T> where T : BusPanelProperties
	{
	}

	public class BusPanelProperties : PanelProperties
	{
		public IBus Bus;

		public BusPanelProperties(IBus bus)
		{
			Bus = bus;
		}
	}
}