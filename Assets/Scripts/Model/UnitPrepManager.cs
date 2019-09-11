using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Data;
using Model.Units;

namespace Model {
public class UnitPrepManager {

	private readonly IFactory<string, AllianceType, WorldPosition, UnitPrep> _unitPrepFactory;


		public UnitPrepManager(IFactory<string, AllianceType, WorldPosition, UnitPrep> unitPrepFactory)
		{
			
			_unitPrepFactory = unitPrepFactory;

		}

	public UnitPrep SpawnUnitPrep(string unitID, AllianceType alliance, WorldPosition position){
		UnitPrep unit = _unitPrepFactory.Create (unitID, alliance, position);
		return unit;
	}
}
}