using System;
using FixMath.NET;
using UnityEngine;
using Zenject;

namespace Services
{
	public class TickService :  IFixedTickable
	{
		public int currentTick { get; private set;}
		public Fix64 tickTime { get; private set;}    //ability duration / tickTime = number of current Ticks

		public TickService ()
		{
			tickTime = (Fix64) Time.fixedDeltaTime;
		}
		public void FixedTick()
		{
			currentTick++;

		}
	}

	//convert from seconds to ticks convertToTick(5) 
}