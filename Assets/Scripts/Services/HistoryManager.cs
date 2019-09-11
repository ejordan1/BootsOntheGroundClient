using System;
using System.Collections.Generic;
using FixMath.NET;
using Model.Units;
using UnityEngine;
using Signals;


namespace Services
{
	public class HistoryManager
	{
		private Dictionary<UnitModel, Fix64> damageLog = new Dictionary<UnitModel, Fix64> ();
		private readonly GameSignals.DamageReceiveSignal _damageReceiveSignal;

		public HistoryManager (GameSignals.DamageReceiveSignal damageReceiveSignal)  
		{
			_damageReceiveSignal = damageReceiveSignal;
			

			_damageReceiveSignal.Listen(data => {
				if (damageLog.ContainsKey(data.receiver)){
					damageLog[data.receiver] += data.damage;
//					Debug.Log ("Received Signal History Manager: damaged for " + data.damage + "by " + data.sender);
	//				Debug.Log ((damageLog[data.receiver]) + " =  total damage done by unit");
				} //going to the key, += automatircally sets the key value to the new one

				else {
					damageLog[data.receiver] = data.damage; //this creates a new entry for the unit data receiver, and then sets it to the damage
//					Debug.Log ("added new entry");
				}
			});


		}
		public void deleteHistory(){
			damageLog.Clear ();
		}
	}
}

//could get the signal directly, or can get it through the history manager. 

// we set in the main staller the signal, we made the signal class, where the signal happens we get a ref to the signal and .fire, then we listen to it

//declare reference to the history manager, create methods, getter methods, etc. get the data by unit. When I need it, add an extra function.