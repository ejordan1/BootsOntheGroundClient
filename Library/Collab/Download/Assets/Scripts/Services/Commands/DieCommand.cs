using Model.Units;
using UnityEngine;
using Services.CC;


namespace Services.Commands

{

	public class DieCommand : GameCommand
	{

		private readonly TickService _tick;
		private int finalTick = -1;
		private readonly UnitModel _unit;



		public DieCommand(UnitModel unit, TickService tick)
		{ //takes in this unit

			_tick = tick;
			_unit = unit;
		}


		public override GameCommandStatus FixedStep()
		{ Debug.Log ("Dying" + _unit.IsAlive);
			//	Debug.Log ("tryna silence");

			if (finalTick == -1) {									//what is the effect of the cc?
				if (true) { //some condition here
					//			Debug.Log ("silencing");
				    _unit.AliveState.Value = UnitModel.AliveStateFlag.Dying;
				}
				finalTick = _tick.currentTick + _unit.dieTime;
			} else {												//will the cc continue?

				//if (_unit.petrified > 0) {    //revive command is interruprted (is set to false right now)
				//	_unit.dying = false;
				//	_unit.alive = false;
				//	return GameCommandStatus.Complete;
				//}

				if (_tick.currentTick > finalTick) {				//when it is over?
				    _unit.AliveState.Value = UnitModel.AliveStateFlag.Dead;
					return GameCommandStatus.Complete; 
				}
			} 
//			Debug.Log ("returning true for death + time till dead is: + " + (finalTick - _tick.currentTick));
			return GameCommandStatus.InProgress; //if it has not passed the final tick then keep the buff going.


		}

	}
}