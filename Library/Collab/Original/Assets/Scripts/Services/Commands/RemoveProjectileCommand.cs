using System.Linq;
using Data;
using Model;
using Model.Units;
using UnityEngine;
using Model.Projectiles;
using System;

namespace Services.Commands
{
	public class RemoveProjectileCommand : GameCommand, IDisposable {

		private readonly WorldModel _worldModel;
		private readonly ProjectileModel _projectile;

		public RemoveProjectileCommand(ProjectileModel projectile, WorldModel worldModel)
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
