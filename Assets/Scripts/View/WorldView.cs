using System.Collections.Generic;
using Model;
using Model.Units;
using ModestTree;
using UniRx;
using UnityEngine;
using Zenject;
using Model.Projectiles;

namespace View
{
    public class WorldView : MonoBehaviour
    {
        private IFactory<UnitModel, UnitView> _unitFactory;
		private IFactory<IProjectile, ProjectileView> _projectileFactory;

        private readonly Dictionary<UnitModel, UnitView> _unitsViews = new Dictionary<UnitModel, UnitView>();
		private readonly Dictionary<IProjectile, ProjectileView> _projectileViews = new Dictionary<IProjectile, ProjectileView>();

        [Inject]
        public void Construct(WorldModel worldModel, IFactory<UnitModel, UnitView> unitFactory, 
			IFactory<IProjectile, ProjectileView> projectileFactory)
        {
            _unitFactory = unitFactory;
			_projectileFactory = projectileFactory;

            worldModel
                .Units
                .ObserveAdd()
                .Subscribe(eventData => SpawnUnitView(eventData.Value))
                .AddTo(this);

            worldModel
                .Units
                .ObserveRemove()
                .Subscribe(eventData => DespawnUnitView(eventData.Value))
                .AddTo(this);

			worldModel
				.Projectiles
				.ObserveAdd()
				.Subscribe(eventData => SpawnProjectileView(eventData.Value))
				.AddTo(this);

			worldModel
				.Projectiles
				.ObserveRemove()
				.Subscribe(eventData => DespawnProjectileView(eventData.Value))
				.AddTo(this);
            
        }

        private void DespawnUnitView(UnitModel unitModel) //should be Iunit instead
        {
            var view = _unitsViews[unitModel];
            Destroy(view.gameObject);
        }

        private void SpawnUnitView(UnitModel unitModel)
        {
            var view = _unitFactory.Create(unitModel);
            _unitsViews[unitModel] = view;
        }

		private void DespawnProjectileView(IProjectile projectileModel)
		{
			var view = _projectileViews[projectileModel];
			Destroy(view.gameObject);
		}

		private void SpawnProjectileView(IProjectile projectileModel)
		{
			var view = _projectileFactory.Create(projectileModel);
			_projectileViews[projectileModel] = view;
		}
    }
}