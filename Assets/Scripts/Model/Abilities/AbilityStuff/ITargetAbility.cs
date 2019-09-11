using System;
using Model.Abilities;
using Model.Units;
using Model.Abilities.AbilityStuff;
using Model.AI;

namespace Model.Abilities.AbilityStuff
{
	public interface ITargetAbility : IAbility
	{
        ITarget Target { get;}
		int TargetPriority { get;}
	}
}

