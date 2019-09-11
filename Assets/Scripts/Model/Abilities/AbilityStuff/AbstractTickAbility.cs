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
using Zenject;
using Model.Abilities.AbilityStuff;

namespace Model.Abilities.AbilityStuff
{
	public abstract class AbstractTickAbility : AbilityCommand
	{

		//private readonly UnitModel _unit;
		private readonly WorldModel _world;
		private readonly CommandProcessor _command;
	
		public virtual bool CanCast(UnitModel unit){ //made this a bool so it is usable
			if (!unit.AnyAbilitiesCasting() && !Casting && !Cooldown){
				return true;
			}
			return false;
		}

		public virtual bool CastFinalTick(){ 
			if (Casting  && CurrentTick > FinalCastTick){ 
				return true;
			}
			return false;
		}

				
		public virtual bool CooldownTickCheck(){ //made this a bool so it is usable
			if (Cooldown  && CurrentTick > FinalCooldownTick){ 
				return true;
			} 
			return false;
		}

		public virtual void SetCastTick(){
			Casting = true;
			FinalCastTick = CurrentTick + (int)(CastTime/TickTime);
		}
		public virtual void SetCooldownTick(){
			Cooldown = true;
			FinalCooldownTick= CurrentTick + (int)(CooldownTime/TickTime);   //all of the bools for checking cast and cooldown could live here, be taken out of the ability class
		}

		public virtual void CastCancel(){
			FinalCastTick = -1;
			Casting = false;
		}

		public virtual void CoolDownCancel(){
			FinalCooldownTick = -1;
			Cooldown = false; 
		}

		//public abstract bool Casting();
		//public abstract bool Cooldown();

		public abstract void Cast();
		public abstract void Execute ();


		public abstract bool Casting { get;  set; }
		public abstract bool Cooldown { get; set; }

		public abstract Fix64 CastTime { get; }
		public abstract Fix64 CooldownTime { get; }

		public abstract int CurrentTick  { get;}

		public abstract int FinalCastTick {get; set;}
		public abstract int FinalCooldownTick {get; set;}

		public abstract Fix64 TickTime { get;}




	}
}
//a minimal ability: has the life cycle: create the abstract methods 
//that should be called at each stage of the life cycle
//creation, initialization, execute, etc.
//create 

//the logic behind the tick will be hidden:
//
//make them virtual methods: 