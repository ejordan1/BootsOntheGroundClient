using System.Linq;
using Data;
using Model;
using Model.Units;
using UnityEngine;
using Model.Projectiles;

namespace Services.Commands
{
	public class RemoveProjectileCommand : GameCommand {

		private readonly WorldModel _worldModel;
		private readonly IProjectile _projectile;

		public RemoveProjectileCommand(IProjectile projectile, WorldModel worldModel)
		{
			_worldModel = worldModel;
			_projectile = projectile;
		}

		public override GameCommandStatus FixedStep() 
		{
			_worldModel.Projectiles.Remove(_projectile);
			Debug.Log ("despawned projectile");

			return GameCommandStatus.Complete;
		}
	}
}
