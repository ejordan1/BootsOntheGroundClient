using System.Reflection;
using Model.Abilities;
using Model.Units;
using UnityEngine;
using Utils;
using Model.Projectiles;
using Model.AI;
using Zenject;
using Data;

namespace Model.Projectiles
{
	public class ProjectileRepository
	{
		private readonly ProjectileRegistry _projectileRegistry;
		private readonly ProjectileFactory _projectileFactory;

		public ProjectileRepository(ProjectileRegistry projectileRegistry, ProjectileFactory projectileFactory)
		{
			_projectileRegistry = projectileRegistry;
			_projectileFactory = projectileFactory;
		}


		//CREATES PROJECTILE FROM THE STRING ID 
		public IProjectile CreateProjectileFromId(string projectileId, UnitModel sender, ITarget receiver, WorldPosition position)
		{
			var projectileIndexData = _projectileRegistry.Data.Find(item => item.Id.CompareOrdinal(projectileId));
			if (projectileIndexData == null)
			{
				Debug.LogWarning(string.Format("AbilityRepository > can't find a projectile with id {0}", projectileId));
				return null;
			}

			var projectileType = projectileIndexData.projectileType; //this is the class arrow
			//there is three things needed: the class, the indexData, and the data

//			Debug.Log(string.Format("ProjectileRepository > trying to create ability of type {0}", projectileType));

			return _projectileFactory.Create(projectileType, new ProjectileFactory.Param {sender = sender, receiver = receiver, position = position, projectileData =  projectileIndexData});
		}

		public class ProjectileFactory : IFactory<string, ProjectileFactory.Param, IProjectile>
		{
			private readonly DiContainer _container; //reference to main zenject class

			public ProjectileFactory(DiContainer container)
			{
				_container = container;
			}

			public IProjectile Create(string typeName, Param param)
			{
				var defaultAssembly = Assembly.GetExecutingAssembly();
				var type = defaultAssembly.GetType(typeName); //because the type is a varaible: makes it a variable object
				return _container.Instantiate(type, new object [] { param.projectileData, param.sender, param.receiver, param.position }) as IProjectile;
			}
			public struct Param {
				public UnitModel sender;
				public ITarget receiver;
				public WorldPosition position;
				public ProjectileData projectileData;
			}

		} 
	}
}