using Data;
using Model;
using Model.AI;
using Model.Units;
using UnityEngine;
using Zenject;

namespace Services.Commands
{
    public class SpawnUnitCommand : GameCommand
    {
        private readonly AllianceType _alliance;

        private readonly CommandProcessor _commandProcessor;
        
        private readonly UnitData _unitData;
        private readonly UnitManager _unitManager;
        private readonly WorldPosition _unitPosition;

        public SpawnUnitCommand(UnitData unitData, AllianceType alliance, WorldPosition unitPosition,
            UnitManager unitManager, CommandProcessor commandProcessor)
        {
            _unitData = unitData;
            _alliance = alliance;
            _unitPosition = unitPosition;
            _unitManager = unitManager;
            _commandProcessor = commandProcessor;


           
        }

        public override GameCommandStatus FixedStep()
        {
            var unit = _unitManager.SpawnUnit(_unitData, _alliance, _unitPosition);
//			Debug.Log ("spawning unit");
          //  _commandProcessor.AddCommand(_simpleAiFactory.Create(unit));

            return GameCommandStatus.Complete;
        }
    }
}