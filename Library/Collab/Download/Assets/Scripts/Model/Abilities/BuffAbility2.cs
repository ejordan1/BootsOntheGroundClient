using Data;
using Model.AI;
using Model.Units;
using Utils;
using UnityEngine;
using Services;
using Services.Buffs;
using Services.CC;
using Services.Commands;
using Services.StatChange;
using Zenject;
using Model.Abilities.AbilityStuff;


namespace Model.Abilities
{
	public class BuffAbility2 : AbilityCommand, ITargetAbility, ICastingAbility  //must get the ability name exactly to the calss
	{
		private readonly UnitModel _unit;
		private readonly WorldModel _world;
		private readonly BuffAbilityParams _data;
		private readonly CommandProcessor _command;

		private readonly TickService _tickService;
		private readonly IFactory<IBuff, BuffData, BuffTargetCommand> _buffFactory;
		private readonly IFactory<CCData, RootCommand> _rootFactory;
		private readonly IFactory<CCData, SilenceCommand> _silenceFactory;
		private readonly IFactory<CCData, FreezeCommand> _freezeFactory;
		private readonly IFactory<CCData, KnockUpCommand> _knockUpFactory;
		private readonly IFactory<CCData, LaunchCommand> _launchFactory;
		private readonly IFactory<CCData, HookCommand> _hookFactory;
		private readonly IFactory<CCData, PetrifyCommand> _petrifyFactory;
		private readonly IFactory<IStatChange, StatChangeData, StatChangeCommand> _statChangeFactory;
		private readonly IFactory<StatChangeData, HPChangeCommand> _hPChangeFactory;
		private readonly IFactory<StatChangeData, ResurrectCommand> _resurrectFactory;


		private bool casting;
		private bool cooldown;

		private int castFinalTick;
		private int cooldownFinalTick;



		public BuffAbility2(UnitModel unit, AbilityData data, WorldModel world, TickService tickservice, CommandProcessor command,
			IFactory<IBuff, BuffData, BuffTargetCommand> buffFactory,
			IFactory<CCData, RootCommand> cCFactory,
			IFactory<CCData, SilenceCommand> silenceFactory,
			IFactory<CCData, FreezeCommand> freezeFactory,
			IFactory<CCData, KnockUpCommand> knockUpFactory,
			IFactory<CCData, LaunchCommand> launchFactory,
			IFactory<CCData, HookCommand> hookFactory,
			IFactory<CCData, PetrifyCommand> petrifyFactory,
			IFactory<IStatChange, StatChangeData, StatChangeCommand> statChangeFactory,
			IFactory<StatChangeData, HPChangeCommand> hPChangeFactory,
			IFactory<StatChangeData, ResurrectCommand> resurrectFactory)
		
		{
			_unit = unit;
			_world = world;
			_command = command;
			_data = new BuffAbilityParams(data);
			_tickService = tickservice;
			_buffFactory = buffFactory;
			_rootFactory = cCFactory;
			_silenceFactory = silenceFactory;
			_freezeFactory = freezeFactory;
			_knockUpFactory = knockUpFactory;
			_launchFactory = launchFactory;
			_hookFactory = hookFactory;
			_petrifyFactory = petrifyFactory;
			_statChangeFactory = statChangeFactory;
			_hPChangeFactory = hPChangeFactory;
			_resurrectFactory = resurrectFactory;
		}

		protected override void DoTheLogic()
		{
			

			var targets = _world.GetAllyUnitsTo(_unit.Alliance);
			_target = new UnitTarget(targets.GetClosestUnit1(_unit));

			if (_target != null && !_target.UnitModel.IsAlive && !_target.UnitModel.IsReviving) {
				
				instaResurrect ();

			}

			//here the logic for if to check for ability cast
			//first checks if no other abilities are being cast
			if (!_unit.AnyAbilitiesCasting() && !cooldown && _unit.IsAlive && _unit.Position.IsInRange (_target.GetPosition(), _data.abilityRange)) {
				
				Cast ();
				Debug.Log ("this should only be called once");
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
					cooldownFinalTick = _tickService.currentTick + (int)(_data.cooldownTime/_tickService.tickTime);
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

			casting = true;
			castFinalTick = _tickService.currentTick + (int)(_data.castTime/_tickService.tickTime);
		}
		public void Execute(){
		if (_unit.Position.IsInRange (_target.GetPosition (), _data.abilityRange)) {
			var command = _buffFactory.Create(Buffs.MaxHpBuff, new BuffData {strength = 10, duration = 50, receiver = _target.UnitModel, sender = _unit});
			_command.AddCommand (command);
			var root = _rootFactory.Create(new CCData {duration = 10, receiver = _target.UnitModel, sender = _unit});
			_command.AddCommand (root);
				var silence = _silenceFactory.Create(new CCData {duration = 10, receiver = _target.UnitModel, sender = _unit});
				_command.AddCommand (silence);

				//var petrify = _petrifyFactory.Create(new CCData {duration = 10, receiver = Target.GetUnitModel(), sender = _unit});
				//_command.AddCommand (petrify);

				var launch = _launchFactory.Create(new CCData {duration = 10, receiver = _target.UnitModel, sender = _unit});
				_command.AddCommand (launch);

				var knockup = _knockUpFactory.Create(new CCData {duration = 10, receiver = _target.UnitModel, sender = _unit});
				_command.AddCommand (knockup);

				var freeze = _freezeFactory.Create(new CCData {duration = 10, receiver = _target.UnitModel, sender = _unit});
				_command.AddCommand (freeze);

				//subscribe to the event: the enemy is being frozen

				var hook = _hookFactory.Create(new CCData {duration = 10, receiver = _target.UnitModel, sender = _unit});
				_command.AddCommand (hook);

				//var addArmor = _statChangeFactory.Create(StatChanges.AddedArmor, new StatChangeData {value = 30, receiver = Target.GetUnitModel(), sender = _unit});
				//_command.AddCommand (addArmor);

				var hpAdd = _hPChangeFactory.Create(new StatChangeData {value = 30, receiver = _target.UnitModel, sender = _unit});
				_command.AddCommand (hpAdd);

				var hpMinus = _hPChangeFactory.Create(new StatChangeData {value = -90, receiver = _target.UnitModel, sender = _unit});
				_command.AddCommand (hpMinus);

				var giveShield = _statChangeFactory.Create (StatChanges.AddedShield, new StatChangeData { value = 1000, receiver = _target.UnitModel, sender = _unit});
				_command.AddCommand (giveShield);
			}
		}

		public void instaResurrect(){
			Debug.Log ("insta resurrectin");
			var resurrect = _resurrectFactory.Create(new StatChangeData {value = 50, receiver = _target.UnitModel, sender = _unit});
			_command.AddCommand (resurrect);
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
			public float castTime
			{
				get { return _data.castTime; }
			}
			public float cooldownTime
			{
				get { return _data.cooldownTime; }
			}

			private readonly AbilityData _data;

			public BuffAbilityParams (AbilityData data)
			{
				_data = data;
			}
		}
	}
}