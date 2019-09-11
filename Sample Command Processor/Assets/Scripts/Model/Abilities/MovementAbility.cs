using System;
using Data;
using Model.Abilities;
using Model.Units;
using Services.Commands;
using Zenject;
using Services;
using Services.Buffs;
using UnityEngine;

namespace Model.Abilities
{
	public class MovementAbility : AbilityCommand, IAbility
	{
		private readonly UnitModel _unit;
	    private readonly AbilityData _data;

	    public MovementAbility(UnitModel unit, AbilityData data)
	    {
	        _unit = unit;
	        _data = data;
	    }

	    protected override void DoTheLogic()
	    {
            var target = _unit.GetAbilityTarget();

            if (target != null)
            {
                var targPos = target.GetPosition();
                if (targPos.IsInRange(_unit.Position, 0.001f)) return;
                var forwardVect = _unit.Position.GetFowardVect(targPos);
                _unit.SetPosition(_unit.Position + (forwardVect * _unit.moveSpeed.value * Time.fixedDeltaTime));
            }
        }

	    public override void Init()
	    {
	        
	    }


	    public override bool Check()
	    {
	        return _unit.isAlive;
	    }
	}
}