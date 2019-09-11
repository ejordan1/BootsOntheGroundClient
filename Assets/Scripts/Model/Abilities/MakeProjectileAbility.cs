using Data;
using FixMath.NET;
using Model.AI;
using Model.Units;
using Utils;
using UnityEngine;
using Services;
using Services.StatChange;
using Services.Buffs;
using Services.Commands;
using Model.Projectiles;
using Zenject;
using Model.Abilities.AbilityStuff;


namespace Model.Abilities
{
	public class MakeProjectileAbility : AbstractTickAbility, ITargetAbility, ICastingAbility  //must get the ability name exactly to the calss
	{
		private readonly UnitModel _unit;
		private readonly WorldModel _world;
		private readonly MakeArrowAbilityParams _data;
		private readonly CommandProcessor _command;

		private readonly TickService _tickService;
	
		private readonly IFactory<StatChangeData, HPChangeCommand> _HPChangeFactory;
		private readonly ProjectileManager _projectileManager;

		private bool casting;
		private bool cooldown;

		private int _finalCastTick;
		private int _finalCooldownTick;



		public MakeProjectileAbility(UnitModel unit, AbilityData data, WorldModel world, TickService tickservice, CommandProcessor command,
			ProjectileManager projectileManager

			)
		{
			_unit = unit;
			_world = world;
			_command = command;
			_data = new MakeArrowAbilityParams(data);
			_tickService = tickservice;
			_projectileManager = projectileManager;



		}

		protected override void DoTheLogic()
		{



			var targets = _world.GetEnemyUnitsTo(_unit.Alliance);
			Target = new UnitTarget(targets.GetClosestUnit1(_unit));

			if (Target.UnitModel != null) {
				if (CanCast (_unit) && _unit.IsAlive && Target != null && _unit.Position.IsInRange (Target.GetPosition (), _data.AbilityRange)) {
					SetCastTick ();
					Cast ();
				}
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
	//		Debug.Log ("make arrow Cast");

		}
		public override void Execute(){
			if (_unit.Position.IsInRange (Target.GetPosition (), _data.AbilityRange)) {
//				Debug.Log ("executing make arrow");
				_projectileManager.SpawnProjectile (_data.ProjectileId, _unit, Target, _unit.GetPosition()); //magic numbers approach: take the projectile ID from the configuration:

			}
		}

		public override void Init()
		{
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


		public class MakeArrowAbilityParams
		{
			public string ProjectileId
			{
				get { return _data.string1; }
			}
			public Fix64 AbilityRange
			{
				get { return _data.range.GetValue(); }
			}
			public Fix64 CastTime
			{
				get { return _data.castTime.GetValue(); }
			}
			public Fix64 CooldownTime
			{
				get { return _data.cooldownTime.GetValue(); }
			}

			public int TargetPriority
			{
				get { return _data.targetPriority; }
			}

			private readonly AbilityData _data;

			public MakeArrowAbilityParams (AbilityData data)
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
			

		public override Fix64 CastTime {
			get { return _data.CastTime; }
		}
		public override Fix64 CooldownTime {
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
		public override Fix64 TickTime {
			get { return _tickService.tickTime; }
		}
	}
}