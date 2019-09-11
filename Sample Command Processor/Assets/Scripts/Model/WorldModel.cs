using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using Model.Units;
using UniRx;

namespace Model
{
    public class WorldModel
    {
        public readonly ReactiveCollection<UnitModel> Units = new ReactiveCollection<UnitModel>();

        
		public IEnumerable<UnitModel> GetAllyUnitsTo(AllianceType alliance)
		{
			return Units.Where(unit => alliance.IsFriendly(unit.Alliance));
		}

		public IEnumerable<UnitModel> GetEnemyUnitsTo(AllianceType alliance)
        {
            return Units.Where(unit => !alliance.IsFriendly(unit.Alliance));
        }

    }
}