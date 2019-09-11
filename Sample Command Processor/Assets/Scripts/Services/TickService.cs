using System;
using UnityEngine;
using Zenject;

namespace Services
{
	public class TickService :  IFixedTickable
	{
		public int currentTick { get; private set;}
		public float tickTime { get; private set;}    //ability duration / tickTime = number of current Ticks

		public TickService ()
		{
			tickTime = Time.fixedDeltaTime;
		}
		public void FixedTick()
		{
			currentTick++;

		}
	}
}

