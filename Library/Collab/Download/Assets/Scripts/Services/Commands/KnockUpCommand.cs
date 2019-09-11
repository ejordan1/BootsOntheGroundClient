using UnityEngine;
using Services.CC;


namespace Services.Commands

{

	public class KnockUpCommand : GameCommand
	{
		private readonly TickService _tick;
		private CCData _cCData;


		public KnockUpCommand(CCData cCData, TickService tick)
		{ 
			_cCData = cCData;
			_tick = tick;
		}


		public override GameCommandStatus FixedStep()
		{
			_cCData.receiver.knockedUp +=  _cCData.strength;
			return GameCommandStatus.Complete; //the real logic for this is the in the movement script
		}
	}
}