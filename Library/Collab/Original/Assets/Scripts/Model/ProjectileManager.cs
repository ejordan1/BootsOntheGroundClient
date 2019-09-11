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
using System;

namespace Model
{
	public class ProjectileManager : GameCommand, IDisposable
	{
		private readonly WorldModel _worldModel;
		private readonly AbilityRepository _abilityRepository;
		private readonly CommandProcessor _commandProcessor;
		private readonly ProjectileRepository _projectileRepository;
		private readonly IFactory<ProjectileModel, RemoveProjectileCommand> _removeProjectileFactory;


		public ProjectileManager(WorldModel worldModel, CommandProcessor commandProcessor, 
			ProjectileRepository projectileRepository, 
			IFactory<ProjectileModel, RemoveProjectileCommand> removeProjectileFactory)
		{
			_worldModel = worldModel;
			_projectileRepository = projectileRepository;
			_commandProcessor = commandProcessor;
			_removeProjectileFactory = removeProjectileFactory;

			_commandProcessor.AddCommand (this); 

		}



		public override GameCommandStatus FixedStep ()
		{
			//save this outside the for loop so it doesn't remake it every frame

			for (int i = 0; i < _worldModel.Projectiles.Count; i++) {
				
				_worldModel.Projectiles [i].moveLogic ();
			}

			//for (int i = 0; i < _worldModel.Projectiles.Count; i++) {
			//	if (_worldModel.Projectiles[i].Position.IsInRange(_worldModel.Projectiles[i])
			//}

			foreach (ProjectileModel projectile in _worldModel.Projectiles) {
				if (projectile.Position.IsInRange (projectile._receiver.GetPosition(), projectile.range.value)) {
					projectile.executeLogic ();
					//DespawnProjectile (projectile);
					//Debug.Log ("dist between me and " + projectile._receiver.GetType() + "  = " + WorldPosition.Distance(projectile.GetPosition(), projectile._receiver.GetPosition()));


				}
			}

			//execute if they are getting close: I need to save more data to make the execution calls. man, I should move projectiles in the projectile manager!
			return GameCommandStatus.InProgress;
		}



		public void SpawnProjectile(string projectileId, UnitModel sender, ITarget receiver, WorldPosition position)
		{
			_worldModel.Projectiles.Add(_projectileRepository.CreateProjectileFromId (projectileId, sender, receiver, position));
		}

		public void DespawnProjectile(ProjectileModel projectileModel)
		{
			//_worldModel.Projectiles.Remove (projectileModel); this way actually works.
			_removeProjectileFactory.Create (projectileModel);  //this does not work but is an attempt
		}
		public WorldPosition Position { get; private set; }

		public void DespawnAllProjectiles(){
			foreach (ProjectileModel projectile in _worldModel.Projectiles) {
				DespawnProjectile (projectile);
			}
		}
		public void addThisToProcessor(){
			_commandProcessor.AddCommand (this); 
		}

		void IDisposable.Dispose ()
		{
			//throw new NotImplementedException ();
		}
	}
}