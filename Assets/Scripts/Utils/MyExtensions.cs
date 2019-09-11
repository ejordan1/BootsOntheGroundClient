using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using Model.Projectiles;
using System;
using System.Text;
using FixMath.NET;
using Model.Units;

namespace Utils
{
    public static class MyExtensions
    {
		public static bool CompareOrdinal(this string first, string second)
		{
			return string.CompareOrdinal(first, second) == 0;
		}

		public static UnitModel GetClosestUnit(this IEnumerable<UnitModel> targets, UnitModel me)
		{
			return targets
 				.Where(unit => unit != me)
				.OrderBy(target => WorldPosition.DistanceSq(me.Position, target.Position))
				.FirstOrDefault();
		}

		public static UnitModel GetClosestUnit1(this IEnumerable<UnitModel> targets, UnitModel me)
		{
			return targets
				.Where (unit => {return unit.IsAlive;}) // the logic for if check 1 is null goes in the actual func part (if this is possible)
				.OrderBy(target => WorldPosition.DistanceSq(me.Position, target.Position))
				.FirstOrDefault();
		}

		public static UnitModel GetClosestUnit2(this IEnumerable<UnitModel> targets, UnitModel me)
		{
			return targets
				.Where (unit => {return unit.IsAlive;}) // the logic for if check 1 is null goes in the actual func part (if this is possible)
				.Where (unit => {return !unit.IsPetrified();})
				.OrderBy(target => WorldPosition.DistanceSq(me.Position, target.Position))
				.FirstOrDefault();
		}

		public static UnitModel GetHighestHealthUnit(this IEnumerable<UnitModel> targets, UnitModel me)
		{
			return targets
				.Where (unit => unit.IsAlive) // the logic for if check 1 is null goes in the actual func part (if this is possible)
				.Where (unit => !unit.IsPetrified())
				.Where (unit => me.Position.IsInRange(unit.Position, me.range.value))
				.OrderBy(target => target.hP.value)
				.FirstOrDefault();
		}


		public static UnitModel GetLowestHealthUnit(this IEnumerable<UnitModel> targets, UnitModel me)
		{
			return targets
				.Where (unit => unit.IsAlive) // the logic for if check 1 is null goes in the actual func part (if this is possible)
				.Where (unit => !unit.IsPetrified())
				.Where (unit => me.Position.IsInRange(unit.Position, me.range.value))
				.OrderBy(target => -target.hP.value)  //IS THIS HOW YOU DO ORDER BY LOWEST?
				.FirstOrDefault();
		}

		//public static UnitModel GetClosestUnitTargetingMe(this IEnumerable<UnitModel> targets, UnitModel me)
		//{
		//	return targets
		//		.Where (unit => {return unit.isAlive;}) // the logic for if check 1 is null goes in the actual func part (if this is possible
		//		.Where (unit => {return unit.petrified < 1;})
		//		.Where (unit => WorldPosition.DistanceSq(me.Position, unit.Position) < me.range.value)
		//		.OrderBy(target => -target.hP.value)  //IS THIS HOW YOU DO ORDER BY LOWEST?
		//		.FirstOrDefault();
		//}

		public static List<UnitModel> GetAllDistUnit1(this IEnumerable<UnitModel> targets, UnitModel me, Fix64 dist) //would prefer to not do it like this if possible
		{
			List<UnitModel> unitList = new List<UnitModel> ();
			foreach (UnitModel unit in targets){
				if ((WorldPosition.DistanceSq (me.Position, unit.Position) < dist) &&
				    unit.IsAlive &&
					!unit.IsPetrified()) {
					unitList.Add (unit);
				}
			}
			return unitList;
		}

		public static List<UnitModel> GetAllDistProjectile1(this IEnumerable<UnitModel> targets, ProjectileModel me, Fix64 dist) //would prefer to not do it like this if possible
		{
			List<UnitModel> unitList = new List<UnitModel> ();
			foreach (UnitModel unit in targets){
				if ((WorldPosition.DistanceSq (me.Position, unit.Position) < dist) &&
					unit.IsAlive &&
					!unit.IsPetrified()) {
					unitList.Add (unit);
				}
			}
			return unitList;
		}

        public static string PrettyPrint<T>(this T[] array, string separator = "; ")
        {
            if (array == null) return null;
            if (array.Length == 0) return "(empty)";
            var builder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                var item = array[i];
                builder.Append(item != null ? item.ToString() : "null");
                if (i < array.Length - 1) builder.Append(separator);
            }
            return builder.ToString();
        }
		/*

		private Func<UnitModel, bool> isAlive = unit => {return unit.isAlive;};
		private Func<UnitModel, bool> notDying = unit => {return !unit.dying;};
		private Func<UnitModel, bool> less50Health = unit => {return unit.hP.value < 50;};
		private Func<UnitModel, bool> more50Health = unit => {return unit.hP.value > 50;};
		private Func<UnitModel, bool> notPetrified = unit => {return !unit.petrified;};
		private Func<UnitModel, bool> notFrozen = unit => {return !unit.frozen;};
		private Func<UnitModel, bool> notImmune = unit => {return !unit.immune;};
		private Func<UnitModel, bool> notKnockedUp = unit => {return !unit.knockedUp;};
		private Func<UnitModel, bool> notHooked = unit => {return !unit.hooked;};
		private Func<UnitModel, bool> notSilenced = unit => {return !unit.silenced;};
		private Func<UnitModel, bool> notLaunched = unit => {return !unit.launched;};
		private Func<UnitModel, bool> notRooted = unit => {return !unit.rooted;};

		//private Func<UnitModel, UnitModel, int> closest = target => WorldPosition.DistanceSq(me.Position, target.Position);

        public static bool CompareOrdinal(this string first, string second)
        {
            return string.CompareOrdinal(first, second) == 0;
        }

		public static UnitModel GetClosestUnit(this IEnumerable<UnitModel> targets, UnitModel me, 
			Func<UnitModel, bool> check1 = null, 
			Func<UnitModel, bool> check2 = null, 
			Func<UnitModel, bool> check3 = null,
			Func<UnitModel, bool> check4 = null,
			Func<UnitModel, bool> check5 = null)
		{
			return targets
				.Where (check1 == true) // the logic for if check 1 is null goes in the actual func part (if this is possible)
				.Where (check2 == true)
				.Where (check3 == true)
				.Where (check4 == true)
				.Where (check5 == true)
				.OrderBy(target => WorldPosition.DistanceSq(me.Position, target.Position))
				.FirstOrDefault();
		}

		public static UnitModel[] GetUnits(this IEnumerable<UnitModel> targets, UnitModel me, 
			Func<UnitModel, bool> check1 = null, 
			Func<UnitModel, bool> check2 = null, 
			Func<UnitModel, bool> check3 = null,
			Func<UnitModel, bool> check4 = null,
			Func<UnitModel, bool> check5 = null)
		{
			return targets
				.Where (check1 == true) // the logic for if check 1 is null goes in the actual func part (if this is possible)
				.Where (check2 == true)
				.Where (check3 == true)
				.Where (check4 == true)
				.Where (check5 == true);
		}

		*/







    }

}

//data => {return data.strength > 0;}
//okay so I abstracted the checks: now to abstrac the criteria.