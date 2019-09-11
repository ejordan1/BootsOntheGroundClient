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
using System.Collections.Generic;
using FixMath.NET;


namespace Model.Abilities
{
	public class ShieldSplashAbility : AbstractTickAbility, ITargetAbility, ICastingAbility  //must get the ability name exactly to the calss
	{
		private readonly UnitModel _unit;
		private readonly WorldModel _world;
		private readonly BuffAbilityParams _data;
		private readonly CommandProcessor _command;

		private readonly TickService _tickService;

		private readonly IFactory<IStatChange, StatChangeData, StatChangeCommand> _statChangeFactory;

		private int _finalCooldownTick;
		private int _finalCastTick;

		private Fix64 _tickTime;


		bool cooldown;
		bool casting;


		public ShieldSplashAbility(UnitModel unit, AbilityData data, WorldModel world, TickService tickservice, CommandProcessor command,
			IFactory<IStatChange, StatChangeData, StatChangeCommand> statChangeFactory
			)
		{
			_unit = unit;
			_world = world;
			_command = command;
			_data = new BuffAbilityParams(data);
			_tickService = tickservice;
			_statChangeFactory = statChangeFactory;



		}

		protected override void DoTheLogic()
		{
			//			Debug.Log (casting + "casting is ");
			//			Debug.Log (cooldown + "casting is ");
			var targets = _world.GetEnemyUnitsTo(_unit.Alliance);
			Target = new UnitTarget(targets.GetClosestUnit1(_unit)); //make this the correct target finder


			if (CanCast (_unit) && _unit.IsAlive) {  //doesn't check for target being in range here
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
			
			var targets = _world.GetAllyUnitsTo (_unit.Alliance);
			List<UnitModel> targs = targets.GetAllDistUnit1 (_unit, _data.AbilityRange);
			foreach (UnitModel targ in targs) {

				var giveShield = _statChangeFactory.Create (StatChanges.AddedShield, new StatChangeData { value = (Fix64) _data.ShieldValue, receiver = targ, sender = _unit});
			_command.AddCommand (giveShield);
				//this is when I have to start putting more logic in the commands, because it ain't goin here.
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

		public class BuffAbilityParams
		{
			public int TargetPriority
			{
				get { return _data.targetPriority; }
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
			public int ShieldValue
			{
				get { return _data.int1; }
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
		public override string ToString()
		{
			return string.Format("[ShieldSplashAbility, unit={0}]", _unit.Id);
		}
	}
}