using Data;
using Model.AI;
using Model.Units;
using Utils;
using UnityEngine;
using Services;
using Services.Buffs;
using Services.Commands;
using Zenject;
using Services.StatChange;
using Model.Abilities.AbilityStuff;


namespace Model.Abilities
{
	public class ResurrectAbility : AbilityCommand, ITargetAbility, ICastingAbility  //must get the ability name exactly to the calss
	{
		private readonly UnitModel _unit;
		private readonly WorldModel _world;
		private readonly BuffAbilityParams _data;
		private readonly CommandProcessor _command;
		private readonly IFactory<StatChangeData, ResurrectCommand> _resurrectFactory;

		private readonly TickService _tickService;
		private readonly IFactory<IBuff, BuffData, BuffTargetCommand> _buffFactory;


		private bool casting;
		private bool cooldown;

		private int castFinalTick;
		private int cooldownFinalTick;



		public ResurrectAbility(UnitModel unit, AbilityData data, WorldModel world, TickService tickservice, CommandProcessor command,
			IFactory<IBuff, BuffData, BuffTargetCommand> buffFactory, 
			IFactory<StatChangeData, ResurrectCommand> resurrectFactory)
		{
			_unit = unit;
			_world = world;
			_command = command;
			_data = new BuffAbilityParams(data);
			_tickService = tickservice;
			_buffFactory = buffFactory;
			_resurrectFactory = resurrectFactory;

		}

		protected override void DoTheLogic()
		{

			var targets = _world.GetAllyUnitsTo(_unit.Alliance);
			_target = new UnitTarget(targets.GetClosestUnit1(_unit));

			//here the logic for if to check for ability cast
			//first checks if no other abilities are being cast
			if (!_unit.AnyAbilitiesCasting() && !cooldown && !_unit.IsAlive && _unit.Position.IsInRange (_target.GetPosition(), _data.abilityRange)) {

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
			Debug.Log ("buff Cast");
			casting = true;
			castFinalTick = _tickService.currentTick + _data.castTick;
		}
		public void Execute(){
			if (_unit.Position.IsInRange (_target.GetPosition (), _data.abilityRange)) {
			//	var command = _buffFactory.Create(Buffs.MaxHpBuff, new BuffData {strength = 10, duration = 50, receiver = Target.GetUnitModel(), sender = _unit});
			//	_command.AddCommand (command);
				Debug.Log ("executing ressurect ability putting in command");
				var resurrectCommand = _resurrectFactory.Create (new StatChangeData {value = 50, receiver = _target.UnitModel, sender = _unit});
				_command.AddCommand (resurrectCommand);
			}

			Debug.Log ("buff execute");
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

	    private UnitTarget _target;

	    ITarget ITargetAbility.Target
	    {
	        get { return _target; }
	    }

        public int TargetPriority
		{
			get { return _data.TargetPriority; }
		}

		public class BuffAbilityParams
		{
			public int TargetPriority
			{
				get { return _data.int1; }
			}
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