using Data;
using Model;
using Model.Abilities;
using Model.AI;
using Model.Units;
using Services;
using Services.Commands;
using UnityEngine;
using View;
using Zenject;
using Services.Buffs;

namespace Bootstrap
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] protected UnitRegistry UnitRegistry;
		[SerializeField] protected AbilityVariablesRegistry AbilityVariablesRegistry;


        public override void InstallBindings()
        {
            InstallRegistries();
            InstalModel();
            InstallCommands();
            InstallServices();
            InstallFactories();
        }

        private void InstallCommands()
        {
			Container.BindIFactory<UnitData, AllianceType, WorldPosition, SpawnUnitCommand>();

           
			Container.BindIFactory<IBuff, BuffData, BuffTargetCommand> ();

			Container.BindIFactory<UnitModel, UnitModel, int, SelectTargetCommand> ();
			Container.BindIFactory<UnitModel, Ability2> ();
			Container.BindIFactory<UnitModel, Ability3> ();
			Container.BindIFactory<UnitModel, Ability4> ();
			Container.BindIFactory<UnitModel, Ability5> ();

			Container.BindIFactory<UnitModel, MovementAbility> ();
        }

        private void InstalModel()
        {
            Container.Bind<WorldModel>().AsSingle();
        }

        private void InstallRegistries()
        {
            BindRegistry<UnitRegistry, UnitData>(UnitRegistry);
			BindRegistry<AbilityVariablesRegistry, AbilityData> (AbilityVariablesRegistry);
            Container.Bind<AbilityRepository>().AsSingle();
            Container.Bind<AbilityRepository.AbilityFactory>().AsSingle();

        }

        private void InstallServices()
        {
            BindService<CommandProcessor>();
            BindService<UnitManager>();
            BindService<LevelLoaderService>();
			BindService<AIManager> ();
			BindService<TickService> ();


        }

        private void InstallFactories()
        {
            Container
                .BindIFactory<UnitData, AllianceType, UnitModel>();



            Container
                .BindIFactory<UnitModel, UnitView>()
                .FromFactory<UnitView.Factory>();
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