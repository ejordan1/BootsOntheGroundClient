using Data;
using System.Collections.Generic;
using Model.Units;
using UnityEngine;
using View;
using Zenject;
using Model.AI;
using Services;
using Model.Projectiles;
using Services.Commands;

namespace Model
{
	public class ProjectileManager : GameCommand
	{
		private readonly WorldModel _worldModel;
		private readonly AbilityRepository _abilityRepository;
		private readonly CommandProcessor _commandProcessor;
		private readonly ProjectileRepository _projectileRepository;
	    private readonly TickService _tickService;
	    private readonly IFactory<IProjectile, RemoveProjectileCommand> _removeProjectileFactory;

        private readonly List<IProjectile> _hitProjectiles = new List<IProjectile>();

		public ProjectileManager(WorldModel worldModel, CommandProcessor commandProcessor, 
			ProjectileRepository projectileRepository, TickService tickService,
			IFactory<IProjectile, RemoveProjectileCommand> removeProjectileFactory)
		{
			_worldModel = worldModel;
			_projectileRepository = projectileRepository;
		    _tickService = tickService;
		    _commandProcessor = commandProcessor;
			_removeProjectileFactory = removeProjectileFactory;

			_commandProcessor.AddCommand (this); 

		}

		public override GameCommandStatus FixedStep ()
		{
			//save this outside the for loop so it doesn't remake it every frame

		    var tickTime = _tickService.tickTime;

            _hitProjectiles.Clear();

            for (int i = 0; i < _worldModel.Projectiles.Count; i++)
            {
                var projectile = _worldModel.Projectiles[i];
                if (projectile.MoveLogic(tickTime))			//moves projectiles, checks if they hit target, if true then adds 
                {
                    _hitProjectiles.Add(projectile);
                }
			}

			//for (int i = 0; i < _worldModel.Projectiles.Count; i++) {
			//	if (_worldModel.Projectiles[i].Position.IsInRange(_worldModel.Projectiles[i])
			//}

		    foreach (var hitProjectile in _hitProjectiles)
		    {
		        if (hitProjectile.ExecuteLogic())
		        {
		            DespawnProjectile(hitProjectile);
		        }
		    }

		    //execute if they are getting close: I need to save more data to make the execution calls. man, I should move projectiles in the projectile manager!
			return GameCommandStatus.InProgress;
		}

		public void SpawnProjectile(string projectileId, UnitModel sender, ITarget receiver, WorldPosition position)
		{
            // TODO: add command
			_worldModel.Projectiles.Add(_projectileRepository.CreateProjectileFromId (projectileId, sender, receiver, position));
		}

		public void DespawnProjectile(IProjectile projectileModel)
		{
			_commandProcessor.AddCommand(_removeProjectileFactory.Create (projectileModel));
		}

		public void DespawnAllProjectiles(){
			foreach (var projectile in _worldModel.Projectiles) {
				DespawnProjectile (projectile);
			}
		}

	}
}