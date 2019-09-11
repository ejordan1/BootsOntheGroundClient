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
	public class FormationTargetAbility2: GameCommand, IAbility, ITargetAbility //not an AbilityCommand
	{
		private UnitModel _sender;
		private readonly UnitModel _unit;
		private FormationAbilityParams2 _data;

		public FormationTargetAbility2 (UnitModel unit, AbilityData abilityData)
		{
			_unit = unit;
			_data = new FormationAbilityParams2(abilityData);
			Target = new WorldPositionTarget(_data.worldPos);

		}


		public override GameCommandStatus FixedStep ()
		{
			Debug.Log ("I exist");
			WorldPosition wp = _data.commanderAbility2.GetFormationTargetsArray [_data.targetElement];
			if (wp != new WorldPosition (-5000, -5000)) {
				Target = new WorldPositionTarget (wp);
				Debug.Log ( "the target I received from commander is: " + (wp.X) + " " + (wp.Y));
				//Debug.Log (TargetPriority + " is my target priority");
				return GameCommandStatus.InProgress;
			} else {
				_unit.Abilities.Remove (this);
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

	    public override string ToString()
	    {
	        return string.Format("[FormationTargetAbility2, target=]", Target);
	    }
    }

	public class FormationAbilityParams2
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
		public int targetElement
		{
			get { return _data.int2; }
		}
		public CommanderAbility2 commanderAbility2 // this is so bad
		{
			get { return _data.commanderAbility2; }
		}

		private readonly AbilityData _data;

		public FormationAbilityParams2 (AbilityData data)
		{
			_data = data;
		}

	   
	}

}

//this ability just has a target and lets one frame pass and then removes itself. I feel like this is a crappy way of doing it; oh well.