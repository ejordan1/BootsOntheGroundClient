using System;
using Data;
using FixMath.NET;
using Model.Abilities;
using Model.Units;
using Services.Commands;
using Zenject;
using Services;
using Services.StatChange;
using UnityEngine;
using Model.Abilities.AbilityStuff;
using Model.AI;


namespace Model.Abilities
{
	public class UnitUpdateAbility : AbilityCommand, IAbility
	{
		private readonly UnitModel _unit;

		private readonly CommandProcessor _command;
		private readonly TickService _tick;
		private readonly IFactory<IStatChange, StatChangeData, StatChangeCommand> _statChangeFactory;

		private bool shieldReducing = false;

		private int _finalShieldReduceTick = -1;


		public UnitUpdateAbility(UnitModel unit, AbilityData data, CommandProcessor command, TickService tick,
			IFactory<IStatChange, StatChangeData, StatChangeCommand> statChangeFactory)
		{
			_unit = unit;
			_tick = tick;
			_command = command;
			_statChangeFactory = statChangeFactory;
		}

		protected override void DoTheLogic()
		{ 			
			ShieldReduce (); 
		}

		public void ShieldReduce(){ 
			//			Debug.Log ("shield = " + _unit.shield.getAddedValue()); 
			//			Debug.Log ("finalShieldReduceTick = " + _finalShieldReduceTick);

			if (_finalShieldReduceTick == -1 && !shieldReducing && _unit.shield.value > Fix64.Zero) {
				_finalShieldReduceTick = _tick.currentTick + 10;   //sets the tick counter
			}

			if (_finalShieldReduceTick != -1 &&_tick.currentTick > _finalShieldReduceTick) {
				var shieldReduce = _statChangeFactory.Create (StatChanges.AddedShield, new StatChangeData{ value = (Fix64) (- 5), receiver = _unit });
				_command.AddCommand (shieldReduce);

				_finalShieldReduceTick = -1;  

			}
		} 

		public override void Init()
		{

		}

		public override string ToString()
		{
			return string.Format("[MovementAbility, target={0}]", _unit.GetAbilityTarget());
		}
	}
}