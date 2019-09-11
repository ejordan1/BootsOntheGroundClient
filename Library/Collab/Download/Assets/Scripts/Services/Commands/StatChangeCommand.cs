using Model.Units;
using UnityEngine;
using Services.StatChange;

namespace Services.Commands

{

	public class StatChangeCommand : GameCommand
	{
		private readonly StatChangeData _statChangeData;
		private readonly IStatChange _iStatChange;



		public StatChangeCommand(IStatChange iStatChange, StatChangeData statChangeData)
		{ //takes in this unit

			_statChangeData = statChangeData;
			_iStatChange = iStatChange;    //this is actually bringing in the instance of the wrapper buff, not the "class" iBuff.


		}
			
		public override GameCommandStatus FixedStep()
		{
								
			if (_iStatChange.Validate(_statChangeData)) {  //if it is valid then the effect will be applied
				_iStatChange.Apply (_statChangeData);
				//Debug.Log ("applied stat change");
			}

				return GameCommandStatus.Complete; 
		} 
	}
}