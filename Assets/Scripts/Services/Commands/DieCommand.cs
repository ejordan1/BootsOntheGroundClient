using Model.Units;
using UnityEngine;
using Services.CC;
using Services.StatChange;

namespace Services.Commands

{

	public class DieCommand : GameCommand
	{

		private readonly TickService _tick;
		private int finalTick = -1;
		private StatChangeData _damageData;



		public DieCommand(StatChangeData data, TickService tick)
		{ 
			_tick = tick;  
			_damageData = data;
		}


		public override GameCommandStatus FixedStep()
		{
			if (finalTick == -1) {									
				if (true) { //condition here for unit not to die: CC, buff, etc.
//					Debug.Log ("dying now");
					_damageData.receiver.AliveState.Value = UnitModel.AliveStateFlag.Dying;
				}
				finalTick = _tick.currentTick + _damageData.receiver.dieTime;
			} else {												//will the cc continue?

				//if (_unit.petrified > 0) {    //revive command is interruprted (is set to false right now)
				//	_unit.dying = false;
				//	_unit.alive = false;
				//	return GameCommandStatus.Complete;
				//}

				if (_tick.currentTick > finalTick) {				//when it is over?
					_damageData.receiver.AliveState.Value = UnitModel.AliveStateFlag.Dead;   
					//SEND A SIGNAL THAT SAYS WHO KILLED THE UNIT: THIS INFO IS IN THE _DATA
					return GameCommandStatus.Complete; 
				}
			} 
//			Debug.Log ("returning true for death + time till dead is: + " + (finalTick - _tick.currentTick));
			return GameCommandStatus.InProgress; //if it has not passed the final tick then keep the buff going.


		}

	}
}