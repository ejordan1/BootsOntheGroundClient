using System;
using Model.Abilities;
using Model.Units;

namespace Model.AI
{
	public interface ITargetAbility : IAbility
	{
        ITarget Target { get;}
		int TargetPriority { get;}
	}
}

