using System.Reflection;
using Model.Abilities;
using Model.Units;
using UnityEngine;
using Utils;
using Zenject;
using Model.Abilities.AbilityStuff;

namespace Data
{
    public class AbilityRepository
    {
        private readonly AbilityVariablesRegistry _abilityRegistry;
        private readonly AbilityFactory _abilityFactory;

        public AbilityRepository(AbilityVariablesRegistry abilityRegistry, AbilityFactory abilityFactory) //gets abilityfactory from installer
        {
            _abilityRegistry = abilityRegistry;
            _abilityFactory = abilityFactory;
        }

        public IAbility CreateAbilityFromId(string abilityId, UnitModel unit, AbilityData overrideAbilityData = null) //sets the default value of overrideAbilityData to null
        {
            var abilityData = _abilityRegistry.Data.Find(item => item.Id.CompareOrdinal(abilityId));
            if (abilityData == null)
            {
                Debug.LogWarning(string.Format("AbilityRepository > can't find an ability with id {0}", abilityId));
                return null;
            }

            if (overrideAbilityData != null) abilityData = abilityData.Override(overrideAbilityData);

            var abilityType = abilityData.AbilityType;
            
//            Debug.Log(string.Format("AbilityRepository > trying to create ability of type {0}", abilityType));

            return _abilityFactory.Create(abilityType, abilityData, unit);
        }

        public class AbilityFactory : IFactory<string, AbilityData, UnitModel, IAbility>
        {
            private readonly DiContainer _container;

            public AbilityFactory(DiContainer container)
            {
                _container = container;
            }

            public IAbility Create(string typeName, AbilityData abilityData, UnitModel unit)
            {
                var defaultAssembly = Assembly.GetExecutingAssembly();
                var type = defaultAssembly.GetType(typeName);
                return _container.Instantiate(type, new object [] { abilityData, unit }) as IAbility;
            }
        } 
    }
}