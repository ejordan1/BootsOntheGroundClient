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
	public class Ability2 : GameCommand, ITargetAbility
	{
		
		private readonly CommandProcessor _commandProcessor;
		private readonly TickService _tickService;
		private readonly UnitModel _unit;
		private readonly WorldModel _world;
		private readonly WorldPosition _worldPos;
		private readonly AbilityVariablesRegistry _abilityVariablesRegistry;
		private readonly IFactory<IBuff, BuffData, BuffTargetCommand> _buffTargetCommandFactory;

		public UnitModel target { get; private set;}
		public int tick;
		public int castTick;
		public int coolDownTick;
		public bool casting;
		public bool coolDown;
		public AbilityData _abilityData;



		private IGameCommand _currentCommand;



		public Ability2(UnitModel unit, WorldModel world, TickService tickService,  
			CommandProcessor commandProcessor, AbilityVariablesRegistry abilityVariablesRegistry, IFactory<IBuff, BuffData, BuffTargetCommand> buffTargetCommandFactory){
			_unit = unit;
			_buffTargetCommandFactory = buffTargetCommandFactory;
			_world = world;
			_tickService = tickService;
			_commandProcessor = commandProcessor;
			_abilityVariablesRegistry = abilityVariablesRegistry;
			_abilityData = _abilityVariablesRegistry [2];   //this is bad
		
		}

		public override GameCommandStatus FixedStep()
		{
			
			 
			if (target == null) {						//if target is null		
				if (_unit.hP.value() > 0) {						//if targetFind criteria is met
					FindTarget ();						//find target
				}
			}
				
			if (target != null && casting == false && coolDown == false ){						//if target is not null
				Debug.Log ("checking dist abil 2");
				if (WorldPosition.DistanceSq (_unit.Position, target.Position) < _unit.range.value()) { //if the current target meets criteria
					Debug.Log ("checking cast and cooldown");
					if (casting == false && coolDown == false){
						Debug.Log ("casting ability");
						CastAbility ();
					}
				}

			}

			if (coolDown == true && _tickService.currentTick > coolDownTick) {
				coolDown = false;
				coolDownTick = 0;
			}

			if (casting == true && _tickService.currentTick > castTick) {                  //execute ability
				
				if (target != null && _worldPos.IsInRange (target.Position, _abilityData.range)) {		//final check
					Debug.Log ("executing ability");
						ExecuteAbility ();
				}
			}


			_unit.targetPrioritiesHack [2] = target;   //THIS IS PART OF THE hack
			return GameCommandStatus.InProgress; 
		}
			



		private void FindTarget(){ 
			var targets = _world.GetAllyUnitsTo(_unit.Alliance);
			var closestTarget = targets
				.Where (target => target != _unit)
				.OrderBy(target => WorldPosition.DistanceSq(_unit.Position, target.Position))
				.FirstOrDefault();


			if (closestTarget != null && _worldPos.IsInRange (closestTarget.Position, _abilityData.range)) { 
				//cannot do this: must call factory to do this
				target = closestTarget;
			Debug.Log (closestTarget);
			}
		}

		private void CastAbility(){
			casting = true;
			castTick = _tickService.currentTick + _abilityData.castTime;

			//try a root self method here
				
		}
		private void ExecuteAbility(){
			casting = false;
			castTick = 0;
			var command = _buffTargetCommandFactory.Create(Buffs.MaxHpBuff, new BuffData {strength = 10, duration = 100, receiver = _unit.Target, sender = _unit});
			Debug.Log ("executed ability");
			//do damage here
			coolDown = true;
			coolDownTick = _tickService.currentTick + _abilityData.coolDownTime;
		}
	}
}