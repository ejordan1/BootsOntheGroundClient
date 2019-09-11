using Model.Units;
using UnityEngine;
using Services.Buffs;

namespace Services.Commands

{

	public class BuffTargetCommand : GameCommand
	{
		private readonly BuffData _buffData;
		private readonly IBuff _iBuff;
		private readonly int _hpAddedValue;
		private readonly TickService _tick;
		private int finalTick = -1;



		public BuffTargetCommand(IBuff iBuff, BuffData buffData, TickService tick)
		{ //takes in this unit

			_buffData = buffData;
			_iBuff = iBuff;    //this is actually bringing in the instance of the wrapper buff, not the "class" iBuff.
			//you can call it IBuff because it extends it, and use the IBuff method because it extends it. 
			_tick = tick;

		}


		public override GameCommandStatus FixedStep()
		{
			
			if (finalTick == -1) {					
				if (_iBuff.Validate(_buffData)) {  //if it is valid then the effect will be applied
					_iBuff.Apply (_buffData);
				}
				finalTick = _tick.currentTick + _buffData.duration;
			} else {
				if (_tick.currentTick > finalTick) {
					_iBuff.Clear (_buffData);
					return GameCommandStatus.Complete; 
				}
			} 
			return GameCommandStatus.InProgress; //if it has not passed the final tick then keep the buff going.


			//_unitReceiver.UnitData.hpAdded = _unitReceiver.UnitData.hpAdded + _hpAddedValue;
			//Debug.Log("received buff");

			//Logs this buff in the event list. Maybe this could be done in the command processor itself?
			//EventObj newEvent = new EventObj(Time.time, 2, currentBuffObj.buffType, currentBuffObj.buffSender, currentBuffObj.buffDuration, currentBuffObj.buffStrength);
			//_receivingUnit.UnitData.eventList.Add(newEvent);

			//first time it is called, when it is first called take the current tick and figure out the final tick: wait and send in progress, check the current tick. After 
			//the current tick == more than the finaltick call clear and return complete. 

			//multiple conditions for the buff clearing: one of the conditions is the tick = final tick. Is this unit still alive: clear.


		}

	}
}