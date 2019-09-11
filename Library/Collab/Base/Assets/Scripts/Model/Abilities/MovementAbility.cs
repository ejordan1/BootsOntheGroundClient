using System;
using Data;
using Model.Abilities;
using Model.Units;
using Services.Commands;
using Zenject;
using Services;
using Services.Buffs;
using Services.StatChange;
using UnityEngine;

namespace Model.Abilities
{
	public class MovementAbility : AbilityCommand, IAbility
	{
		private readonly UnitModel _unit;
	    private readonly AbilityData _data;
		private readonly IFactory<UnitModel, ReviveCommand> _reviveFactory;
		private readonly IFactory<UnitModel, DieCommand> _dieFactory;
		private readonly CommandProcessor _command;
		private readonly TickService _tick;
		private readonly IFactory<IStatChange, StatChangeData, StatChangeCommand> _statChangeFactory;

		private bool shieldReducing = false;
		private bool knockUpReducing = false;
		private int _finalShieldReduceTick = -1;
		private int _finalKnockUpReduceTick = -1;

		public MovementAbility(UnitModel unit, AbilityData data, CommandProcessor command, TickService tick,
			IFactory<UnitModel, ReviveCommand> reviveFactory,
			IFactory<UnitModel, DieCommand> dieFactory,
			IFactory<IStatChange, StatChangeData, StatChangeCommand> statChangeFactory)
	    {
	        _unit = unit;
	        _data = data;
			_tick = tick;
			_command = command;
			_dieFactory = dieFactory;
			_reviveFactory = reviveFactory;
			_statChangeFactory = statChangeFactory;
	    }

	    protected override void DoTheLogic()
		{ //Debug.Log (_unit.hP.value); 
			//Debug.Log (_unit.isAlive);
			//Debug.Log (_unit.isAlive + "unit alive or daed at health :" + _unit.hP.value);



			if (_unit.isAlive && _unit.hP.value <= 0 && !_unit.dying && !_unit.reviving) { //kills unit its hp goes below 0 // greater or equal to 0
				//Debug.Log (" die command ");
				Debug.Log ("going to die now");
				var die = _dieFactory.Create(_unit);
				_command.AddCommand (die);
			}
			if (!_unit.isAlive && _unit.hP.value > 0 && !_unit.reviving && !_unit.dying) { //&& unit not already reviving //greater than 0
				var revive = _reviveFactory.Create(_unit);
				_command.AddCommand (revive);
			}

			ShieldReduce (); //ongoing shield losing
			KnockUpReduce(); //ongoing falling


			if (_unit.rooted > 0) {
			//	Debug.Log ("rooted");
			}
			if (_unit.silenced > 0) {
			//	Debug.Log ("silenced");
			}
			if (_unit.frozen > 0) {
			//	Debug.Log ("frozen");
			}
			if (_unit.knockedUp > 0) {
			//	Debug.Log ("knockedup");
			}
			if (_unit.hooked > 0) {
			//	Debug.Log ("hooked");
			}
			if (_unit.launched > 0) {
			//	Debug.Log ("launched");
			}
			if (_unit.petrified > 0) {
			//	Debug.Log ("petrified");
			}

            var target = _unit.GetAbilityTarget();


			if (_unit.isAlive && target != null)
            {
                var targPos = target.GetPosition();
				if (targPos.IsInRange(_unit.Position, _unit.moveRange.value)) return;
                var forwardVect = _unit.Position.GetFowardVect(targPos);
                _unit.SetPosition(_unit.Position + (forwardVect * _unit.moveSpeed.value * Time.fixedDeltaTime));
            }
        }

	    public override void Init()
	    {
	        
	    }


	    public override bool Check()
	    {
			return true;
	    }

		public void ShieldReduce(){ 
//			Debug.Log ("shield = " + _unit.shield.getAddedValue()); 
//			Debug.Log ("finalShieldReduceTick = " + _finalShieldReduceTick);

			if (_finalShieldReduceTick == -1 && !shieldReducing && _unit.shield.value > 0) {
				_finalShieldReduceTick = _tick.currentTick + 10;   //sets the tick counter
			
			}


			if (_finalShieldReduceTick != -1 &&_tick.currentTick > _finalShieldReduceTick) {
				var shieldReduce = _statChangeFactory.Create (StatChanges.AddedShield, new StatChangeData{ value = -5, receiver = _unit });
				_command.AddCommand (shieldReduce);
			
				_finalShieldReduceTick = -1;  
			
			}
		} 

		public void KnockUpReduce(){ 
			//			
			if (_unit.knockedUp > 0 && _finalKnockUpReduceTick == -1 && !knockUpReducing) {
				_finalKnockUpReduceTick = _tick.currentTick + 20;   //sets the tick counter

			}


			if (_finalKnockUpReduceTick != -1 &&_tick.currentTick > _finalKnockUpReduceTick) {
				_unit.knockedUp = _unit.knockedUp - (int)_unit.weight.value;

				_finalKnockUpReduceTick = -1;  

			}
		} 
	}
}