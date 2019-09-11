using Data;
using System.Collections.Generic;
using Model.Units;
using UnityEngine;
using View;
using Zenject;
using Model.AI;
using Services;
using System;


namespace Model
{
	public class UnitManager : IDisposable
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

        public UnitModel SpawnUnit(UnitData unitData, AllianceType alliance, WorldPosition position)
        {
            var unit = _unitFactory.Create(unitData, alliance);
            unit.SetPosition(position);

            foreach (var unitAbility in unitData.abilityIDs)
            {
                var ability = _abilityRepository.CreateAbilityFromId(unitAbility.AbilityId, unit, unitAbility.AbilityOverride ? unitAbility.AbilityOverrideData : null);
                unit.Abilities.Add(ability); 
                _commandProcessor.AddCommand(ability);
            }

		
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
		public void DespawnAllUnits(){
			foreach (UnitModel unit in _worldModel.Units) {
				DespawnUnit (unit);
			}
		}

		void IDisposable.Dispose ()
		{
			//throw new NotImplementedException ();
		}
    }
}

//okay so the manager just gets the info from the registry; then it puts it into the createbilityfromid thing which then goes from there. 
//that is all the abilites array in the unit registry does. it does not play a role in the actual ability creation,
//therefore, making abilities in the game requires only giving the specific parameters to the createabilityfromid thing.