using System;
using Data;
using Model.Abilities;
using Model.Units;
using Services.Commands;
using Zenject;
using Services;
using Services.StatChange;
using UnityEngine;
using Model.Abilities.AbilityStuff;
using Model.AI;


namespace Model.Abilities
{
	public class MovementAbility : AbilityCommand, IAbility
	{
		private readonly UnitModel _unit;
	    private readonly TickService _tick;

	    public MovementAbility(UnitModel unit, AbilityData data, CommandProcessor command, TickService tick)
	    {
	        _unit = unit;
	        _tick = tick;
	    }

	    protected override void DoTheLogic()
		{ 
			//fix this later, put into hpcommand


            ITarget target = _unit.GetAbilityTarget();

			//this checks if that target object is made, and then if the value is not null
			if (_unit.IsAlive && target != null && target.notNull())
            {
                var targPos = target.GetPosition();
				if (targPos.IsInRange(_unit.Position, _unit.moveRange.value)) return;
                var forwardVect = _unit.Position.GetFowardVect(targPos);
                _unit.SetPosition(_unit.Position + (forwardVect * _unit.moveSpeed.value * _tick.tickTime));
            }
        }

	    public override void Init()
	    {
	        
	    }
			
	    public override string ToString()
	    {
	        return string.Format("[MovementAbility, target={0}]", _unit.GetAbilityTarget());
	    }
	}
}