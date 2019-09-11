using FixMath.NET;
using Model.Units;
using Zenject;
using UnityEngine;
using Services.CC;
using Services.StatChange;
using Model.Units;


namespace Services.Commands

{

	public class HPChangeCommand : GameCommand
	{

		private readonly UnitModel _unit;
		private readonly StatChangeData _statChangeData;
		private readonly Signals.GameSignals.DamageReceiveSignal _damageReceiveSignal;
		private Fix64 rawValue;
		private readonly IFactory<StatChangeData, DieCommand> _dieCommandFactory;
		private readonly CommandProcessor _commandProcessor;

		public HPChangeCommand(StatChangeData statChangeData, Signals.GameSignals.DamageReceiveSignal damageReceiveSignal, 
			CommandProcessor commandProcessor,
			IFactory<StatChangeData, DieCommand> dieCommandFactory)
		{ //takes in this unit

			_statChangeData = statChangeData;
			_damageReceiveSignal = damageReceiveSignal;
			_unit = _statChangeData.receiver.GetUnitModel();
			_dieCommandFactory = dieCommandFactory;
			_commandProcessor = commandProcessor;

//			Debug.Log (_unit);
		}


		public override GameCommandStatus FixedStep()
		{
			if (_unit != null && _unit.IsAlive) {

				//DETERMINES WHETHER UNIT IS ACTUALLY BEING DAMAGED OR HEALED (for reverse effects)
				if (_statChangeData.value < Fix64.Zero) { 
					rawValue = _statChangeData.receiver.damageModifier.returnModifiedValue(_statChangeData.value); 
				}
				if (_statChangeData.value > Fix64.Zero) { 
					rawValue = (Fix64)_statChangeData.value * _statChangeData.receiver.healModifier.returnModifiedValue (_statChangeData.value); 
				}


				//UNIT  DAMAGED
				if (rawValue < Fix64.Zero){ //still have to include max value, make it subtract the correct amount, etc.

                    Fix64 afterArmorDamage = _statChangeData.value * ((Fix64)100 / ((Fix64)100 + _unit.armor.value));
					//damage is negative, shield is positive so it reduces abs. value
					Fix64 finalDamage = afterArmorDamage + _unit.shield.value;
					if (finalDamage <= -_statChangeData.receiver.hP.value){
						_unit.hP.addedValueChange (-_statChangeData.receiver.hP.value);  //weird way to do it, just substracts the amount of health the unit has
//						Debug.Log("dying now");
						var dieCommand = _dieCommandFactory.Create(_statChangeData);
						_commandProcessor.AddCommand (dieCommand);

					} else {
						if (finalDamage < Fix64.Zero) {
							_unit.hP.addedValueChange (finalDamage);  //if unit is not going to die, reduce health by damage amount
						} else {
							Debug.Log ("shield absorbed the damage");
						}
					}
						
					_unit.shield.addedValueChange (afterArmorDamage); //substracts shield after so it can be used in the health calculation
//					Debug.Log ("Damage for " + finalDamage);

					//fires the signal here
					_damageReceiveSignal.Fire (new Signals.GameSignals.DamageReceiveSignal.Data {
						sender = _statChangeData.sender,
						receiver = _statChangeData.receiver,
						damage = finalDamage
					});

				} else { //IF THE UNIT IS BEING HEALED: STILL NEED TO DO THIS PART
				
					/*
					int heal = (int)Mathf.Clamp ((float)_statChangeData.value * (_unit.heal.getMultiplier ()), 0, _unit.maxHp.value - _unit.hP.value);
					_unit.hP.addedValueChange (heal);
					Debug.Log (heal + "Healed for");
					*/
					Debug.Log ("Tried to heal, healing not implented yet in HPCOMMAND");
				}
			}
			return GameCommandStatus.Complete;
		}
	}
}


//revive and die get moved to here