using System;
using Data;
using Services.Commands;
using UnityEngine;
using Utils;
using Zenject;
using Random = System.Random;

namespace Services
{
    public class LevelLoaderService : IInitializable
    {
        private readonly CommandProcessor _commandProcessor;
        private readonly IFactory<UnitData, AllianceType, WorldPosition, SpawnUnitCommand> _spawnUnitCommandFactory;
        private readonly UnitRegistry _unitRegistry;

        public LevelLoaderService(UnitRegistry unitRegistry,
            IFactory<UnitData, AllianceType, WorldPosition, SpawnUnitCommand> spawnUnitCommandFactory, CommandProcessor commandProcessor)
        {
            _spawnUnitCommandFactory = spawnUnitCommandFactory;
            _commandProcessor = commandProcessor;
            _unitRegistry = unitRegistry;
        }

        public void Initialize()
        {
            LoadUnits();
        }

        private void LoadUnits()
        {
            CreateArmy(AllianceType.Player, new WorldPosition(0, 5), 0f);
            CreateArmy(AllianceType.Foe, new WorldPosition(0, -5), 180f);
        }

        private void CreateArmy(AllianceType alliance, WorldPosition point, float heading)
        {
            var direction = MathExtensions.FromHeading(heading + 90f);

            // first line gets the unit registry data
            var unitData = _unitRegistry[0];
            SpawnAPoint(alliance, point, unitData, direction, 1);

            // second line
            unitData = _unitRegistry[1];
            point += MathExtensions.FromHeading(heading)*3f;
            SpawnAPoint(alliance, point, unitData, direction, 1);

			//Does not work because I couldn't figure out how to configure the unit view

			//unitData = _unitRegistry[2];
			//point += MathExtensions.FromHeading(heading)*3f;
			//SpawnAPoint(alliance, point, unitData, direction, 1);

			//unitData = _unitRegistry[3];
			//point += MathExtensions.FromHeading(heading)*3f;
			//SpawnAPoint(alliance, point, unitData, direction, 1);
        }

        private WorldPosition GetRandomSpot(Random random)
        {
            return new WorldPosition((float)random.NextDouble() * 20 - 10, (float)random.NextDouble() * 10 - 5);
        }

        private void SpawnAPoint(AllianceType alliance, WorldPosition point, UnitData unitData, WorldPosition direction, int count)
        {
            for (var i = 0; i < count; i++)
            {
                var command = _spawnUnitCommandFactory.Create(unitData, alliance, point);
                _commandProcessor.AddCommand(command);

                point += direction * 2f;
            }
        }
    }
}