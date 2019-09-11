using Data;
using Model.AI;
using Model.Units;
using Utils;
using UnityEngine;
using Services;
using Services.StatChange;
using Services.Buffs;
using Services.Commands;
using Zenject;
using Model.Abilities.AbilityStuff;


namespace Model.Abilities
{
	public class AbilityMakerAbility : AbilityCommand, ICastingAbility  //must get the ability name exactly to the calss
	{
		private readonly UnitModel _unit;
		private readonly WorldModel _world;
		private readonly BuffAbilityParams _data;
		private readonly CommandProcessor _command;
		private readonly AbilityRepository _abilityRepository;
		private readonly TickService _tickService;
		private readonly UnitRegistry _unitRegistry;

		private bool casting;
		private bool cooldown;
		private int castFinalTick;
		private int cooldownFinalTick;



		public AbilityMakerAbility(UnitModel unit, AbilityData data, WorldModel world, TickService tickservice,
			CommandProcessor command, UnitRegistry unitRegistry, AbilityRepository abilityRepository)
		{
			_unit = unit;
			_world = world;
			_command = command;
			_data = new BuffAbilityParams(data);
			_tickService = tickservice;
			_unitRegistry = unitRegistry;
			_abilityRepository = abilityRepository;

		}

		protected override void DoTheLogic()
		{
			//here the logic for if to check for ability cast
			//first checks if no other abilities are being cast
			if (!_unit.AnyAbilitiesCasting() && !cooldown && _unit.IsAlive ) {

				Cast ();
			}

			//here the logic for the ability cast

			//here the logic for if it is casting, does it continue to cast
			if (casting == true) {
				if (!_unit.IsAlive){
					castFinalTick = -1;
					casting = false;

				}
				if (_tickService.currentTick > castFinalTick){ 
					casting = false;
					castFinalTick = -1;
					Execute ();											///i put all the logic in execute, This is best I think.
					cooldownFinalTick = _tickService.currentTick + _data.cooldownTick;
					cooldown = true;
				} 

			}
			//careful with the is eqaul to and equals
			if (cooldown == true && _tickService.currentTick > cooldownFinalTick) {
				cooldownFinalTick = -1;
				cooldown = false;
			}

		}

		public void Cast(){
			// casting doesn't do anything right now: make this root self.
			Debug.Log ("make abilities Cast");
			casting = true;
			castFinalTick = _tickService.currentTick + _data.castTick;
		}
		public void Execute(){
			if (_unit.IsAlive) {

				var abilityToAdd = _abilityRepository.CreateAbilityFromId ("ability.spawnUnit", _unit);
				_unit.Abilities.Add(abilityToAdd); //adds the ability to the unit ability array. 
				_command.AddCommand (abilityToAdd);

				//very easy to add abilities: just do this

				Debug.Log ("tried to make ability spawn units on self");
			

			}

			//Debug.Log ("make abilities execute");
		}

		public override void Init()
		{
		}

		public override bool Check()
		{
			return true;
		}

		public bool Casting { get { return casting;} }


		public bool Cooldown {
			get {
				return 
					cooldown;
			}
		}



		public class BuffAbilityParams
		{

			public float abilityRange
			{
				get { return _data.float1; }
			}
			public int castTick
			{
				get { return _data.int3; }
			}
			public int cooldownTick
			{
				get { return _data.int4; }
			}

			private readonly AbilityData _data;

			public BuffAbilityParams (AbilityData data)
			{
				_data = data;
			}
		}
	}
}