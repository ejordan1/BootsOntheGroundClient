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

namespace Model.Abilities
{
	public class BuffAbility : AbilityCommand, ITargetAbility, ICastingAbility  //must get the ability name exactly to the calss
	{
		private readonly UnitModel _unit;
		private readonly WorldModel _world;
		private readonly BuffAbilityParams _data;
		private readonly CommandProcessor _command;

		private readonly TickService _tickService;
		private readonly IFactory<IBuff, BuffData, BuffTargetCommand> _buffFactory;
		private readonly IFactory<StatChangeData, HPChangeCommand> _HPChangeFactory;

		private bool casting;
		private bool cooldown;

		private int castFinalTick;
		private int cooldownFinalTick;



		public BuffAbility(UnitModel unit, AbilityData data, WorldModel world, TickService tickservice, CommandProcessor command,
			IFactory<IBuff, BuffData, BuffTargetCommand> buffFactory,
			IFactory<StatChangeData, HPChangeCommand> HPChangeFactory)
		{
			_unit = unit;
			_world = world;
			_command = command;
			_data = new BuffAbilityParams(data);
			_tickService = tickservice;
			_buffFactory = buffFactory;
			_HPChangeFactory = HPChangeFactory;
		}

		protected override void DoTheLogic()
		{



			var targets = _world.GetAllyUnitsTo(_unit.Alliance);
			Target = new UnitTarget (targets.GetClosestUnit(_unit));

			//here the logic for if to check for ability cast
			//first checks if no other abilities are being cast
			if (!_unit.AnyAbilitiesCasting() && !cooldown && _unit.isAlive && Target != null && _unit.Position.IsInRange (Target.GetPosition(), _data.abilityRange)) {

				Cast ();
			}

			//here the logic for the ability cast

			//here the logic for if it is casting, does it continue to cast
			if (casting == true) {
				if (!_unit.isAlive){
					castFinalTick = -1;
					casting = false;              	}
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
			Debug.Log ("buff Cast");
			casting = true;
			castFinalTick = _tickService.currentTick + (int)(_data.castTime/_tickService.tickTime);
		}
		public void Execute(){
			if (_unit.Position.IsInRange (Target.GetPosition (), _data.abilityRange)) {
				var command = _buffFactory.Create(Buffs.MaxHpBuff, new BuffData {strength = 10, duration = 50, receiver = Target.UnitModel, sender = _unit});
				_command.AddCommand (command);

				var dmg = _HPChangeFactory.Create(new StatChangeData { value = -50, receiver = Target.UnitModel, sender = _unit});
				_command.AddCommand (dmg);


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

		public UnitTarget Target { get; private set; }

		public int TargetPriority
		{
			get { return _data.TargetPriority; }
		}

		public class BuffAbilityParams
		{
			public int TargetPriority
			{
				get { return _data.targetPriority; }
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