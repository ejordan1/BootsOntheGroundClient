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
using Model.Abilities.AbilityStuff;

namespace Model.Abilities
{
	public class CommanderAbility : AbilityCommand  //must get the ability name exactly to the calss
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


		public CommanderAbility(UnitModel unit, AbilityData data, WorldModel world, TickService tickservice, CommandProcessor command,
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
			
			
			formationTargets [0] = new WorldPosition (_unit.Position.X - 5, _unit.Position.Y);
			formationTargets [1] = new WorldPosition (_unit.Position.X, _unit.Position.Y + 5);
			formationTargets [2] = new WorldPosition (_unit.Position.X + 5, _unit.Position.Y);
			
			//note that this will continue to find recruits for the entire game
			var allyUnits = _world.GetEnemyUnitsTo(_unit.Alliance);
			for (int i = 0; i < recruits.Length; i++) {
				if (recruits [i] == null) {
					recruits[i] = GetClosestAlly (allyUnits, formationTargets[i]); //closest ally that isn't already chosen

				} else {
					if (formationTargets [i].IsInRange (recruits [i].GetPosition(), 100)) {


						var abilityToAdd = _abilityRepository.CreateAbilityFromId ("ability.formationTargetAbility", _unit, 
							new AbilityData {worldPos1 = formationTargets[i], int1 = _data.TargetPriority, unit1 = _unit}); 
						_command.AddCommand (abilityToAdd);
						_unit.Abilities.Add(abilityToAdd);
						//Debug.Log ("ability added");
					} else {
						recruits [i] = null;
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