using Data;
using Model;
using Model.Abilities;
using Model.AI;
using Model.Units;
using Services;
using Services.Commands;
using UnityEngine;
using View;
using Services.StatChange;
using Zenject;
using Model.Projectiles;
using Signals;
using Services.Buffs;
using Services.CC;
using MapSetup.Data;

using MapSetup.Model;
using Scripts.MapSetup.Services;




namespace Bootstrap
{
	public class MapCreateInstaller : MonoInstaller
	{
		[SerializeField] protected UnitRegistry UnitRegistry;
		[SerializeField] protected AbilityVariablesRegistry AbilityVariablesRegistry;
		[SerializeField] protected ProjectileRegistry ProjectileRegistry;
		[SerializeField] protected MapSetupRegistry MapSetupRegistry;


		public override void InstallBindings()
		{
			InstallRegistries ();
			InstalModel();
			//InstallCommands();
			InstallServices();
			InstallFactories();
			InstallSignals ();
		}

		public void InstalModel(){

			Container.Bind<WorldModel>().AsSingle();
//			Container.Bind<StaticMapCreateData>().AsSingle().NonLazy();
//			Container.Bind <StaticAllData>().AsSingle().NonLazy();
		}

		public void InstallRegistries()
		{
			BindRegistry<UnitRegistry, UnitData>(UnitRegistry);
			BindRegistry<AbilityVariablesRegistry, AbilityData> (AbilityVariablesRegistry);
			BindRegistry<ProjectileRegistry, ProjectileData> (ProjectileRegistry);
			BindRegistry<MapSetupRegistry, MapData> (MapSetupRegistry);
			Container.Bind<AbilityRepository>().AsSingle();
			Container.Bind<AbilityRepository.AbilityFactory>().AsSingle();
			Container.Bind<UnitRepository>().AsSingle();
			//	Container.Bind<MapSetupRepository>().AsSingle();  currently mono

		}

		private void InstallServices()
		{
			
			BindService<MapLoaderService> ();
			BindService<UnitPrepManager>();

			BindService<MapManager> ();  

		}





		private void InstallSignals(){

			Container.DeclareSignal<GameSignals.DamageReceiveSignal>();

		}

		private void InstallFactories()
		{
			Container
				.BindIFactory<MapArrangement>();
			Container
				.BindIFactory<MapArrangement, MapModel> ();

			Container
				.BindIFactory<UnitData, AllianceType, UnitModel>();

			Container
				.BindIFactory<string, AllianceType, WorldPosition, UnitPrep> ();

			Container
				.BindIFactory<UnitModel, UnitView>()
				.FromFactory<UnitView.Factory>();

			Container
				.BindIFactory<IProjectile, ProjectileView>()
				.FromFactory<ProjectileView.Factory>();
		}

		private void BindRegistry<TRegistry, TData>(TRegistry regsitry)
			where TData : class, IData
			where TRegistry : IAssetRegistry<TData>
		{
			Container
				.BindInterfacesAndSelfTo<TRegistry>()
				.FromInstance(regsitry)
				.AsSingle();

		}

		private void BindService<TService>()
		{
			Container
				.BindInterfacesAndSelfTo<TService>()
				.AsSingle()
				.NonLazy(); 
		}


	}
}




/*
 * 
 * game configuration: main data: spells, units, etc.
 * all the data is on the server. that way all the data will get the same file when you change it.
 * scriptable objects on the player side. Json file: sits on the web server: when the client starts it downloads the file,
 * deserializes, downloads into repository
 * 
 * skip this for now? in the end we need this. 
 * 
 * map data: the same for two or more players. They need the same map data. making sure the json is stored on the server not on the client
 * matchmaking: find two or more players, get them together. 
 * 
 * choose person: lets battle! agree to the battle,
 * other player needs to get the notification: another player opens up a socket connection, receives all the messages from the server
 * if a new battle request coming in, gets a call back, process on the client side. periodic request: ask the server: is anyone 
 * asking me for the battle?
 * 
 * sockets! dedicated server!
 * 
 * 
 * create new container, new scene with zenject, etc. pass the variables: the map setup, the battle setup, new scene
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * */