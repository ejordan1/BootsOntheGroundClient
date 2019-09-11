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
	public class TemplateAbility : AbstractTickAbility, ITargetAbility, ICastingAbility  //must get the ability name exactly to the calss
	{
		private readonly UnitModel _unit;
		private readonly WorldModel _world;
		private readonly BuffAbilityParams _data;
		private readonly CommandProcessor _command;

		private readonly TickService _tickService;
		private readonly IFactory<IBuff, BuffData, BuffTargetCommand> _buffFactory;
		private readonly IFactory<StatChangeData, HPChangeCommand> _HPChangeFactory;

		private int _finalCooldownTick;
		private int _finalCastTick;

		private float _tickTime;


		bool cooldown;
		bool casting;


		public TemplateAbility(UnitModel unit, AbilityData data, WorldModel world, TickService tickservice, CommandProcessor command,
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
//			Debug.Log (casting + "casting is ");
//			Debug.Log (cooldown + "casting is ");
			var targets = _world.GetEnemyUnitsTo(_unit.Alliance);
			Target = new UnitTarget(targets.GetClosestUnit1(_unit)); //make this the correct target finder


			if (CanCast (_unit) && _unit.IsAlive && Target != null && _unit.Position.IsInRange (Target.GetPosition (), _data.AbilityRange)) {
				SetCastTick ();
				Cast ();
			}

			if (casting == true) {
				if (!_unit.IsAlive) { //conditions to stop cast
					CastCancel ();
				}
				if (CastFinalTick ()) {
					CastCancel ();
					Execute ();	
					SetCooldownTick ();
				}
			}

			if (CooldownTickCheck ()) {
				CoolDownCancel ();
			}
		}

		public override void Cast(){
			// casting doesn't do anything right now: make this root self.
//			Debug.Log ("DERPDERPDERPDERP");
		}


		public override void Execute(){
			//SINGLE TARGET IN RANGE
			//if (_unit.Position.IsInRange (Target.GetPosition (), _data.AbilityRange)) {
			//}

			//AOE EFFECT
			//var targets = _world.GetAllyUnitsTo (_unit); //(or enemy units) 
			//List<UnitModel> targs = targets.GetAllDist1 (this, _data.AbilityRange);
			//foreach (UnitModel targ in targs) {
			//}
		}
			

		public override void Init()
		{
		}

		public override bool Check()
		{
			return true;
		}


		public UnitTarget Target { get; private set; }

	    ITarget ITargetAbility.Target
	    {
			get { return Target; }
	    }

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
			public float AbilityRange
			{
				get { return _data.range; }
			}
			public float CastTime
			{
				get { return _data.castTime; }
			}
			public float CooldownTime
			{
				get { return _data.cooldownTime; }
			}

			private readonly AbilityData _data;

			public BuffAbilityParams (AbilityData data)
			{
				_data = data;
			}
		}
		public override bool Casting {
			get { return casting; }

			set { casting = value; }
		}
		public override bool Cooldown {
			get { return cooldown; }

			set {
				cooldown = value;
			}
		}

		public override float CastTime {
			get { return _data.CastTime; }
		}
		public override float CooldownTime {
			get { return _data.CooldownTime; }
		}
		public override int CurrentTick {
			get { return _tickService.currentTick; }
		}
		public override int FinalCastTick {
			get { return _finalCastTick; }
			set { _finalCastTick = value; }
		}
		public override int FinalCooldownTick {
			get { return _finalCooldownTick; }

			set { _finalCooldownTick = value; }
		}
		public override float TickTime {
			get { return _tickService.tickTime; }
		}

	}
}