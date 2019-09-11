using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
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
                .OrderBy(target => WorldPosition.DistanceSq(me.Position, target.Position))
                .FirstOrDefault();
        }
    }
}