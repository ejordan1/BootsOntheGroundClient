using System.Collections.Generic;
using Model;
using Model.Units;
using ModestTree;
using UniRx;
using UnityEngine;
using Zenject;

namespace View
{
    public class WorldView : MonoBehaviour
    {
        private IFactory<UnitModel, UnitView> _unitFactory;

        private readonly Dictionary<UnitModel, UnitView> _unitsViews = new Dictionary<UnitModel, UnitView>();

        [Inject]
        public void Construct(WorldModel worldModel, IFactory<UnitModel, UnitView> unitFactory)
        {
            _unitFactory = unitFactory;

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
            
        }

        private void DespawnUnitView(UnitModel unitModel)
        {
            var view = _unitsViews[unitModel];
            Destroy(view.gameObject);
        }

        private void SpawnUnitView(UnitModel unitModel)
        {
            var view = _unitFactory.Create(unitModel);
            _unitsViews[unitModel] = view;
        }
    }
}