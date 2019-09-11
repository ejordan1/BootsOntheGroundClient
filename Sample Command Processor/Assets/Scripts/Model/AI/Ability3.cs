using System;
using Model.Units;
using Services.Commands;
using Zenject;
using Services;
using Services.Buffs;
using UnityEngine;
using System.Linq;
using Data;
using Model;



namespace Model.AI
{
	public class Ability3 : GameCommand
	{

		private readonly CommandProcessor _commandProcessor;
		private readonly TickService _tickService;
		private readonly UnitModel _unit;
		private readonly WorldModel _world;
		private readonly WorldPosition _worldPos;
		private readonly AbilityVariablesRegistry _abilityVariablesRegistry;

		public UnitModel target;
		public int tick;
		public int castTick;
		public int coolDownTick;
		public bool casting;
		public bool coolDown;
		public AbilityData _abilityData;



		private IGameCommand _currentCommand;


		public Ability3(UnitModel unit, WorldModel world, TickService tickService,  
			CommandProcessor commandProcessor, AbilityVariablesRegistry abilityVariablesRegistry){
			_unit = unit;
			_world = world;
			_tickService = tickService;
			_commandProcessor = commandProcessor;
			_abilityVariablesRegistry = abilityVariablesRegistry;
			_abilityData = _abilityVariablesRegistry [3];   //this is fine here but could be set in the unit as well

		}

		public override GameCommandStatus FixedStep()
		{
			
			if (target == null) {						//if target is null		
				if (_unit.isAlive) {						//if targetFind criteria is met
					FindTarget ();						//find target
				}
			}

			if (target != null){						//if target is not null
				if (_worldPos.IsInRange (target.Position, _abilityData.range)) { //if the current target meets criteria
					if (casting == false && coolDown == false){
						CastAbility ();
					}
				}

			}

			if (_tickService.currentTick > castTick) {                  //execute ability
				if (target != null && _worldPos.IsInRange (target.Position, _abilityData.int1)) {		//final check
					ExecuteAbility ();
				}
			}

			// dvscode : _unit.targetPrioritiesHack [1] = target;   //THIS IS PART OF THE hack
			return GameCommandStatus.InProgress; 
		}



		//for some reason getAllyUnits made it the not work
		private void FindTarget(){ 
			var targets = _world.GetEnemyUnitsTo(_unit.Alliance);
			var closestTarget = targets
				.OrderBy(target => WorldPosition.DistanceSq(_unit.Position, target.Position))
				.FirstOrDefault();

			if (WorldPosition.DistanceSq (_unit.Position, closestTarget.Position) < 20) { 
				//cannot do this: must call factory to do this
				target = closestTarget;
			}
		}

		private void CastAbility(){
			casting = true;
			castTick = _tickService.currentTick + _abilityData.castTime;
			//try a root self method here

		}
		private void ExecuteAbility(){
			casting = false;
			//do damage here
			coolDownTick = _tickService.currentTick + _abilityData.coolDownTime;
		}
	}
}