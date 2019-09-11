using System;
using Model.Abilities;
using Model.Units;
using Model.Abilities.AbilityStuff;

namespace Model.Abilities.AbilityStuff
{
	public interface ICastingAbility : IAbility
	{
		bool Casting { get;}
		bool Cooldown { get; }
	}
}

