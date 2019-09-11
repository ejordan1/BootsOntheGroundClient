using Data;
using System.Collections.Generic;
using Model.Units;
using UnityEngine;
using View;
using Zenject;
using Model.AI;
using Services;
namespace Model
{
    public class UnitManager
    {
        private readonly WorldModel _worldModel;
        private readonly IFactory<UnitData, AllianceType, UnitModel> _unitFactory;
        private readonly AbilityRepository _abilityRepository;
        private readonly CommandProcessor _commandProcessor;


		public UnitManager(WorldModel worldModel, CommandProcessor commandProcessor,  IFactory<UnitData, AllianceType, UnitModel> unitFactory, AbilityRepository abilityRepository)
        {
            _worldModel = worldModel;
            _unitFactory = unitFactory;
            _abilityRepository = abilityRepository;
            _commandProcessor = commandProcessor;

        }

        public UnitModel SpawnUnit(UnitData unitData, AllianceType alliance, WorldPosition position
			//IFactory<UnitModel, Ability2> ability2Factory,
			//IFactory<UnitModel, Ability3> ability3Factory,
			//IFactory<UnitModel, Ability4> ability4Factory,
			//IFactory<UnitModel, Ability5> ability5Factory
		
		)
        {
            var unit = _unitFactory.Create(unitData, alliance);
            unit.SetPosition(position);

            foreach (var unitAbility in unitData.abilityIDs)
            {
                var ability = _abilityRepository.CreateAbilityFromId(unitAbility.AbilityId, unit, unitAbility.AbilityOverride ? unitAbility.AbilityOverrideData : null);
                unit.Abilities.Add(ability);
                _commandProcessor.AddCommand(ability);
            }

			/* I tried and couldn't get this to work
			ITargetAbility abil1 = ability2Factory.Create (unit);   //this is not at all how it will really work
			_commandProcessor.AddCommand (abil1);
			unit.abilities [1] = abil1;

			ITargetAbility abil2 = ability5Factory.Create (unit);
			_commandProcessor.AddCommand (abil2);
			unit.abilities [2] = abil2;

			ITargetAbility abil3 = ability4Factory.Create (unit);
			_commandProcessor.AddCommand (abil3);
			unit.abilities [3] = abil3;

			ITargetAbility abil4 = ability3Factory.Create (unit);
			_commandProcessor.AddCommand (abil4);
			unit.abilities [4] = abil4;

*/
            _worldModel.Units.Add(unit);
			//load up abilities; add them to unit: stored inside configuration of unit as array of string IDs.
			//helper class: takes ability registry: unit manager and spawn unit command: create ability by ID: pass the string,
			//and it returns the real IAbility state. That class will take the configuration ability data, define 
			//what kind of class we will create for this particular ability.

            return unit;
        }

        public void DespawnUnit(UnitModel unitModel)
        {
            _worldModel.Units.Remove(unitModel);
        }
    }
}