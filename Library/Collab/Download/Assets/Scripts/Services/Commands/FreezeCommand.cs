using Model.Units;
using UnityEngine;
using Services.CC;


namespace Services.Commands

{

	public class FreezeCommand : GameCommand
	{
		
		private readonly TickService _tick;
		private int finalTick = -1;
		private CCData _cCData;


		public FreezeCommand(CCData cCData, TickService tick)
		{ //takes in this unit
			_cCData = cCData;
			_tick = tick;

		}


		public override GameCommandStatus FixedStep() //only need this once: only how to apply the effect and how to move it. 
		{
		//	Debug.Log ("tryna freeze");

			if (finalTick == -1) {									//what is the effect of the cc?
				if (_cCData.receiver.IsAlive) { 
		//			Debug.Log ("freezing");
					_cCData.receiver.frozen+=1;
				}
				finalTick = _tick.currentTick + Mathf.RoundToInt(_cCData.duration/_tick.tickTime); //THIS IS HOW
			} else {												//will the cc continue?

				if (!_cCData.receiver.IsAlive) {
					_cCData.receiver.frozen-=1;
					//potential logic: if the receiver would be frozen from this, ... this is hard though because of how it works in ticks
					//and other effects could go first that wouldn't be accounted for in that tick. 
					return GameCommandStatus.Complete;
				}

				if (_tick.currentTick > finalTick) {				//when it is over?
					_cCData.receiver.frozen -=1;
					return GameCommandStatus.Complete; 
				}
			} 
			return GameCommandStatus.InProgress; //if it has not passed the final tick then keep the buff going.


		}

	}
}

//create class for timed commmand: periodic command. apply effect, clear effect. 