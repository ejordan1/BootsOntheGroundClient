using System.Linq;
using Data;
using Model;
using Model.Units;
using UnityEngine;

namespace Services.Commands
{
	public class SelectTargetCommand : GameCommand {

		private readonly UnitModel _thisUnit;
		private readonly UnitModel _targetUnit;
		private readonly int _abilityInt;

		public SelectTargetCommand(UnitModel thisUnit, UnitModel targetUnit, int abilityInt)
		{
			_thisUnit = thisUnit;
			_targetUnit = targetUnit;
			_abilityInt = abilityInt;

		}

		public override GameCommandStatus FixedStep() 
		{
			
			// dvscode: _thisUnit.UnitData.abilityTargets [_abilityInt] = _targetUnit; //target unit can be null, which is the way to deselect a target


			return GameCommandStatus.Complete;


		}
	}
}
	