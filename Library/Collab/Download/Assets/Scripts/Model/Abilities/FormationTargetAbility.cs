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
	public class FormationTargetAbility: GameCommand, IAbility, ITargetAbility //not an AbilityCommand
	{
		private UnitModel _sender;
		private readonly UnitModel _unit;
		private FormationAbilityParams _data;


		private bool oneFramePassed = false;

		public FormationTargetAbility (UnitModel unit, AbilityData abilityData)
		{
			_unit = unit;
			_data = new FormationAbilityParams(abilityData);
			Target = new WorldPositionTarget(_data.worldPos);

		}


		public override GameCommandStatus FixedStep ()
		{
			Debug.Log ("I exist");

			if (!oneFramePassed) {
				oneFramePassed = true;

				Target = new WorldPositionTarget(_data.worldPos);

				return GameCommandStatus.InProgress;
			} else {
				return GameCommandStatus.Complete;
			}

		}




		void IAbility.Init ()
		{
			
		}
		bool IAbility.Check ()
		{
			return true;
		}

		public ITarget Target { get; private set; }

		public int TargetPriority
		{
			get { return _data.TargetPriority; }
		}
	}

	public class FormationAbilityParams
	{
		public int TargetPriority
		{
			get { return _data.int1; }
		}
		public float AbilityRange
		{
			get { return _data.float1; }
		}
		public WorldPosition worldPos
		{
			get { return _data.worldPos1; }
		}
		public UnitModel Sender
		{
			get { return _data.unit1; }
		}

		private readonly AbilityData _data;

			public FormationAbilityParams (AbilityData data)
		{
			_data = data;
		}
	}

}

//this ability just has a target and lets one frame pass and then removes itself. I feel like this is a crappy way of doing it; oh well.