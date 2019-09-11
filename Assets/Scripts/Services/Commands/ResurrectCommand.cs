using FixMath.NET;
using Model.Units;
using UnityEngine;
using Services.CC;
using Services.StatChange;


namespace Services.Commands

{

	public class ResurrectCommand : GameCommand
	{

		private readonly UnitModel _unit;
		private readonly StatChangeData _statChangeData;

		public ResurrectCommand(StatChangeData statChangeData)
		{ //takes in this unit

			_statChangeData = statChangeData;
			_unit = _statChangeData.receiver.GetUnitModel();
			Debug.Log (_unit);
		}


		public override GameCommandStatus FixedStep()
		{
			if (!_unit.IsAlive && !_unit.IsReviving && !_unit.IsDying && _unit.hP.value < Fix64.One) {   //not all these checks should need to exist
                
                var heal = Fix64.Clamp (_statChangeData.value * (_unit.heal.getMultiplier ()), Fix64.Zero, _unit.maxHp.value);

				_unit.hP.addedValueChange (heal);

				Debug.Log (heal + ": revived with health");

			} else {
				Debug.Log ("Unit not dead so didn't revive");
			}
			return GameCommandStatus.Complete;
		}
		public override void Dispose(){

		}
	}
}