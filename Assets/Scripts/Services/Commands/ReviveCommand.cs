using Model.Units;
using UnityEngine;
using Services.CC;


namespace Services.Commands

{

	public class ReviveCommand : GameCommand
	{

		private readonly TickService _tick;
		private int finalTick = -1;
		private readonly UnitModel _unit;



		public ReviveCommand(UnitModel unit, TickService tick)
		{ //takes in this unit
			
			_tick = tick;
			_unit = unit;
		}


		public override GameCommandStatus FixedStep()
		{
			//	Debug.Log ("tryna silence");

			if (finalTick == -1) {									//what is the effect of the cc?
				if (true) { //some condition here
					//			Debug.Log ("silencing");
				    _unit.AliveState.Value = UnitModel.AliveStateFlag.Reviving;
				}
				finalTick = _tick.currentTick + _unit.reviveTime;
			} else {												//will the cc continue?

				/*if (false) {    //revive command is interruprted (is set to false right now)
					_unit.reviving = false;
					return GameCommandStatus.Complete;
				}*/

				if (_tick.currentTick > finalTick) {				//when it is over?
					_unit.AliveState.Value = UnitModel.AliveStateFlag.Alive;
					return GameCommandStatus.Complete; 
				}
			} 
			return GameCommandStatus.InProgress; //if it has not passed the final tick then keep the buff going.


		}

	}
}