using System;
using FixMath.NET;
using Zenject;
using Model.Units;

namespace Signals
{
	public static class GameSignals
	{

		public class DamageReceiveSignal : Signal<DamageReceiveSignal.Data, DamageReceiveSignal>{ //extends this with generic types the data and the signal
			public struct Data {
				
				public UnitModel sender;
				public UnitModel receiver;
				public Fix64 damage;
				public int tick;

			}
		}


	}
}

