using Model.Units;
using UnityEngine;
using Services.CC;


namespace Services.Commands

{

	public class LaunchCommand : GameCommand
	{
		
		private readonly TickService _tick;
		private int finalTick = -1;
		private CCData _cCData;


		public LaunchCommand(CCData cCData, TickService tick)
		{ //takes in this unit
			_cCData = cCData;
			_tick = tick;

		}


		public override GameCommandStatus FixedStep()
		{
			//Debug.Log (finalTick + "launch final tick");
	//		Debug.Log ("tryna launch");

			if (finalTick == -1) {									//what is the effect of the cc?
				if (_cCData.receiver.IsAlive) { 
		//			Debug.Log ("launchon");
					_cCData.receiver.launched+=1;
				}
				finalTick = _tick.currentTick + _cCData.duration;
			} else {												//will the cc continue?

				if (!_cCData.receiver.IsAlive) {
					_cCData.receiver.launched-=1;
					return GameCommandStatus.Complete;
				}

				if (_tick.currentTick > finalTick) {				//when it is over?
					_cCData.receiver.launched -=1;
					return GameCommandStatus.Complete; 
				}
			} 
			return GameCommandStatus.InProgress; //if it has not passed the final tick then keep the buff going.

		}

	}
}

//NOT SURE IF THE INT THING WORKS FOR THIS