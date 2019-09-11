using Data;
using System.Collections;
using System.Collections.Generic;
using Model.AI;
using Model.Units;
using Utils;
using System.Linq;
using UnityEngine;
using Services;
using Services.StatChange;
using Services.Buffs;
using Services.Commands;
using Zenject;

namespace Model.Abilities
{
	public class CommanderAbility2 : AbilityCommand  //must get the ability name exactly to the calss
	{
		private readonly UnitModel _unit;
		private readonly WorldModel _world;
		private readonly BuffAbilityParams _data;
		private readonly CommandProcessor _command;
		private readonly AbilityRepository _abilityRepository;

		private readonly TickService _tickService;
		private readonly IFactory<IBuff, BuffData, BuffTargetCommand> _buffFactory;
		private readonly IFactory<StatChangeData, HPChangeCommand> _HPChangeFactory;

		private int _finalCooldownTick;
		private int _finalCastTick;

		private readonly float _castTime;
		private readonly float _cooldownTime;

		private float _tickTime;



		private UnitModel[] recruits = new UnitModel[3];
		private WorldPosition[] formationTargets = new WorldPosition[3]; 
		//these are associated by element number, not directly linked. They should be directly linked.

		bool cooldown;
		bool casting;


		public CommanderAbility2(UnitModel unit, AbilityData data, WorldModel world, TickService tickservice, CommandProcessor command,
			IFactory<IBuff, BuffData, BuffTargetCommand> buffFactory, AbilityRepository abilityRepository,

			IFactory<StatChangeData, HPChangeCommand> HPChangeFactory)
		{
			_unit = unit;
			_world = world;
			_command = command;
			_data = new BuffAbilityParams(data);
			_tickService = tickservice;
			_buffFactory = buffFactory;
			_HPChangeFactory = HPChangeFactory;
			_abilityRepository = abilityRepository;

			_castTime = data.castTime;
			_cooldownTime = data.cooldownTime;


		}

		protected override void DoTheLogic()
		{

			if (_unit.isAlive) {
				formationTargets [0] = new WorldPosition (_unit.Position.X - 10, _unit.Position.Y);
				formationTargets [1] = new WorldPosition (_unit.Position.X, _unit.Position.Y + 5);
				formationTargets [2] = new WorldPosition (_unit.Position.X + 5, _unit.Position.Y);
			} else {
				for (int i = 0; i < formationTargets.Length; i ++){
					formationTargets[i] = new WorldPosition (-5000, -5000);
				}
			}

			//note that this will continue to find recruits for the entire game

			var allyUnits = _world.GetAllyUnitsTo(_unit.Alliance);
			for (int i = 0; i < recruits.Length; i++) {
				if (recruits [i] == null) {
					recruits[i] = GetClosestAlly (allyUnits, formationTargets[i]); //closest ally that isn't already chosen

					//in the case that it does find a unit to add,
					if (recruits[i] != null){ //should have a few more checks here including a range check
						var abilityToAdd = _abilityRepository.CreateAbilityFromId ("ability.formationTargetAbility2", _unit, //make this from the factory
							new AbilityData {worldPos1 = formationTargets[i], int1 = _data.TargetPriority, unit1 = _unit, int2 = i, commanderAbility2 = this}); 
						_command.AddCommand (abilityToAdd);
						recruits[i].Abilities.Add(abilityToAdd);

					}

				} else {
					if (!formationTargets [i].IsInRange (recruits [i].GetPosition(), 100)) {
						recruits [i] = null;
						//makes recruit null if no longer meets criteira
					}
				}
			}
		}


		public override void Init()
		{
		}

		public override bool Check()
		{
			return true;
		}



		public UnitModel GetClosestAlly(IEnumerable<UnitModel> targets, WorldPosition recruitTarget){
			return targets
				.Where(unit => unit != _unit)
				.Where (unit => !AlreadyRecruit(unit))
				.OrderBy(target => WorldPosition.DistanceSq(recruitTarget, target.Position))
				.FirstOrDefault();
		}

		public bool AlreadyRecruit(UnitModel newRecruit){  //did this work???!
			foreach (UnitModel currentRecruit in recruits) {
				if (newRecruit == currentRecruit)
					return true;
			}
			return false;
		}

		public WorldPosition[] GetFormationTargetsArray{
			get {return formationTargets;}
		}


		public class BuffAbilityParams
		{
			public int TargetPriority
			{
				get { return _data.targetPriority; }
			}
			public float castTime
			{
				get { return _data.castTime; }
			}
			public float cooldownTime
			{
				get { return _data.cooldownTime; }
			}
			public float range
			{
				get { return _data.range; }
			}


			private readonly AbilityData _data;

			public BuffAbilityParams (AbilityData data)
			{
				_data = data;
			}

		}

	}
}