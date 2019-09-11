using System;
using Data;
using FixMath.NET;
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
        private readonly UnitRepository _unitRepository;
        private readonly IFactory<UnitData, AllianceType, WorldPosition, SpawnUnitCommand> _spawnUnitCommandFactory;

        public LevelLoaderService(UnitRepository unitRepository, 
            IFactory<UnitData, AllianceType, WorldPosition, SpawnUnitCommand> spawnUnitCommandFactory, CommandProcessor commandProcessor)
        {
            _unitRepository = unitRepository;
            _spawnUnitCommandFactory = spawnUnitCommandFactory;
            _commandProcessor = commandProcessor;
        }

        public void Initialize()
        {
            LoadUnits();
        }

        private void LoadUnits()
        {
            //CreateArmy(AllianceType.Player, new WorldPosition(0, 5), 0f);
            //CreateArmy(AllianceType.Foe, new WorldPosition(0, -5), 0f);

            //SpawnAPoint(AllianceType.Foe, new WorldPosition(0, -5), _unitRepository.GetById("Mage"));

			CreateArmy1(AllianceType.Team1, 180f);
			CreateArmy2(AllianceType.Team2, 180f);
            //CreateDude(AllianceType.Ally, 180f);
        }

        

        //REMEMBER ALL ARRAY ELEMENTS ARE ONE LOWER THAN THEY ARE
        private void CreateArmy(AllianceType alliance, WorldPosition point, float heading)
        {
            var direction = MathExtensions.FromHeading(heading + 90f);

            // first line gets the unit registry data
            var unitData = _unitRepository.GetById("Spartan");
			point += MathExtensions.FromHeading(heading)*(Fix64) 3f;
            SpawnAPoint(alliance, point, unitData, direction, 1);

            // second line
            // unitData = _unitRegistry[1];
            //point += MathExtensions.FromHeading(heading)* (Fix64)15f;
            //SpawnAPoint(alliance, point, unitData, direction, 1);

			//Does not work because I couldn't figure out how to configure the unit view

			//unitData = _unitRegistry[2];
			//point += MathExtensions.FromHeading(heading)* (Fix64)10f;
			//SpawnAPoint(alliance, point, unitData, direction, 1);

			//unitData = _unitRegistry[3];
			//point += MathExtensions.FromHeading(heading)*3f;
			//SpawnAPoint(alliance, point, unitData, direction, 1);
        }


		private void CreateArmy1(AllianceType alliance, float heading)
		{
			var direction = MathExtensions.FromHeading(heading + 90f);

			// first line gets the unit registry data
			var unitData = _unitRepository.GetById("Barbarian");
			SpawnAPoint(alliance, new WorldPosition (-20, 10), unitData, direction, 1);

			unitData = _unitRepository.GetById("Giant");
			SpawnAPoint(alliance,  new WorldPosition (-20, 5), unitData, direction, 1);

			unitData = _unitRepository.GetById("Giant");
			SpawnAPoint(alliance, new WorldPosition (-25, 0), unitData, direction, 1);
            
			unitData = _unitRepository.GetById("Barbarian");
			SpawnAPoint(alliance, new WorldPosition (-20, -5), unitData, direction, 1);

			unitData = _unitRepository.GetById("SplashShielder");
			SpawnAPoint(alliance,  new WorldPosition (-20, -10), unitData, direction, 1);

			unitData = _unitRepository.GetById("Giant");
			SpawnAPoint(alliance, new WorldPosition (-20, -15), unitData, direction, 1);

			unitData = _unitRepository.GetById("Barbarian");
			SpawnAPoint(alliance, new WorldPosition (-20, -7), unitData, direction, 1);

			unitData = _unitRepository.GetById("Giant");
			SpawnAPoint(alliance,  new WorldPosition (-20, 8), unitData, direction, 1);






			//unitData = _unitRegistry[3];
			//point += MathExtensions.FromHeading(heading)*3f;
			//SpawnAPoint(alliance, point, unitData, direction, 1);
		}

		private void CreateArmy2(AllianceType alliance, float heading)
		{
			var direction = MathExtensions.FromHeading(heading + 90f);

			// first line gets the unit registry data
			var unitData = _unitRepository.GetById("Barbarian");
			SpawnAPoint(alliance, new WorldPosition (20, 10), unitData, direction, 1);

            unitData = _unitRepository.GetById("Giant");
			SpawnAPoint(alliance,  new WorldPosition (15, 5), unitData, direction, 1);

			unitData = _unitRepository.GetById("Barbarian");
			SpawnAPoint(alliance, new WorldPosition (20, 0), unitData, direction, 1);

			unitData = _unitRepository.GetById("Giant");
			SpawnAPoint(alliance, new WorldPosition (15, -5), unitData, direction, 1);

			unitData = _unitRepository.GetById("Giant");
			SpawnAPoint(alliance,  new WorldPosition (15, -10), unitData, direction, 1);

			unitData = _unitRepository.GetById("Barbarian");
			SpawnAPoint(alliance, new WorldPosition (20, -15), unitData, direction, 1);  //the target thing probably is still borken?


			unitData = _unitRepository.GetById("Archer");
			SpawnAPoint(alliance, new WorldPosition (20, -7), unitData, direction, 1);

			unitData = _unitRepository.GetById("Giant");
			SpawnAPoint(alliance,  new WorldPosition (25, 8), unitData, direction, 1);

			unitData = _unitRepository.GetById("SplashShielder");
			SpawnAPoint(alliance,  new WorldPosition (25, -2), unitData, direction, 1);

			unitData = _unitRepository.GetById("SplashShielder");
			SpawnAPoint(alliance,  new WorldPosition (25, 8), unitData, direction, 1);

		}

		private void CreateDude(AllianceType alliance, float heading)
		{
			var direction = MathExtensions.FromHeading(heading + 90f);

			// first line gets the unit registry data
			var unitData = _unitRepository.GetById("Barbarian");
			SpawnAPoint(alliance, new WorldPosition (-20, 10), unitData, direction, 1);

		}



        private WorldPosition GetRandomSpot(Random random)
        {
            return new WorldPosition((float)random.NextDouble() * 20 - 10, (float)random.NextDouble() * 10 - 5);
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
 