using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using Model.Units;
using UniRx;
using Model.Projectiles;

namespace Model
{
    public class WorldModel
    {
        public readonly ReactiveCollection<UnitModel> Units = new ReactiveCollection<UnitModel>();
		public readonly ReactiveCollection<IProjectile> Projectiles = new ReactiveCollection<IProjectile>();

        
		public IEnumerable<UnitModel> GetAllyUnitsTo(AllianceType alliance)
		{
			return Units.Where(unit => alliance.IsFriendly(unit.Alliance));
		}

		public IEnumerable<UnitModel> GetEnemyUnitsTo(AllianceType alliance)
        {
            return Units.Where(unit => !alliance.IsFriendly(unit.Alliance));
        }

		public IEnumerable<UnitModel> GetAllUnits()
		{
			return Units;
		}

    }
}