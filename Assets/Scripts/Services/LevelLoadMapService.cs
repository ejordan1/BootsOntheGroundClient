using System;
using Data;
using FixMath.NET;
using Services.Commands;
using UnityEngine;
using Utils;
using Zenject;
using Random = System.Random;
using MapSetup.Model;
using Scripts.MapSetup.Services;
using Model.Units;

namespace Services
{
	public class LevelLoadMapService : IInitializable
	{
		private readonly CommandProcessor _commandProcessor;
		private readonly UnitRepository _unitRepository;
		private readonly IFactory<UnitData, AllianceType, WorldPosition, SpawnUnitCommand> _spawnUnitCommandFactory;
		private MapModel _map;

		public LevelLoadMapService(UnitRepository unitRepository, 
			IFactory<UnitData, AllianceType, WorldPosition, SpawnUnitCommand> spawnUnitCommandFactory, CommandProcessor commandProcessor)
		{
			_unitRepository = unitRepository;
			_spawnUnitCommandFactory = spawnUnitCommandFactory;
			_commandProcessor = commandProcessor;
			_map = StaticAllData.currentMap;
		}

		public void Initialize()
		{
			LoadUnits();
		}

		private void LoadUnits()
		{
			foreach (UnitPrep unitPrep in _map._preFormationUnits) {
				Debug.Log (unitPrep._unitID + " " + unitPrep._alliance +  " "  + unitPrep._position);
				SpawnAPoint(unitPrep._alliance, unitPrep._position, _unitRepository.GetById(unitPrep._unitID));
			}
			foreach (UnitPrep unitPrep in _map._formationUnits) {
				SpawnAPoint(unitPrep._alliance, unitPrep._position, _unitRepository.GetById(unitPrep._unitID));
				Debug.Log ("form : " + unitPrep._unitID + " " + unitPrep._alliance +  " "  + unitPrep._position);
			}

		}

		private void SpawnAPoint(AllianceType alliance, WorldPosition point, UnitData unitData)
		{
			var command = _spawnUnitCommandFactory.Create(unitData, alliance, point);
			_commandProcessor.AddCommand(command);
		}

		private void SpawnAPoint(AllianceType alliance, WorldPosition point, UnitData unitData, WorldPosition direction, int count)
		{
			for (var i = 0; i < count; i++)
			{
				SpawnAPoint(alliance, point, unitData);

				point += direction * (Fix64) 2f;
			}
		}
	}
}
