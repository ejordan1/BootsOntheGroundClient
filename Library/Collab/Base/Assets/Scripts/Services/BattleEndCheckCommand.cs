using UnityEngine;
using Services.CC;
using Model;
using Zenject;
using Data;
using System.Linq;

using Model.Units;
using Services.Commands;


namespace Services

{

	public class BattleEndCheckCommand : GameCommand
	{
		WorldModel _worldModel;
		HistoryManager _historyManager;
		UnitManager _unitManager;
		ProjectileManager _projectileManager;
		CommandProcessor _commandProcessor;
		ResetService _resetService;

		public BattleEndCheckCommand(WorldModel worldModel, HistoryManager historyManager, UnitManager unitManager, ProjectileManager projectileManager,
			CommandProcessor commandProcessor, ResetService resetService
			
		)
		{ 
			_worldModel = worldModel;
			_historyManager = historyManager;
			_unitManager = unitManager;
			_projectileManager = projectileManager;
			_commandProcessor = commandProcessor;
			_resetService = resetService;

			_commandProcessor.AddCommand (this); 
						
		}


		public override GameCommandStatus FixedStep()
		{

			if (allTeamUnitsDead (AllianceType.Player) == true) {
				Debug.Log ("ally units dead");
			}
			if (allTeamUnitsDead (AllianceType.Foe) == true) {
				Debug.Log ("foe units dead");
			}

			if (allTeamUnitsDead (AllianceType.Player) || allTeamUnitsDead (AllianceType.Foe)) {
				PresentResult();
			}
	
			return GameCommandStatus.InProgress; //the real logic for this is the in the movement script
		}



		public void PresentResult(){
			Debug.Log ("battle is over !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			//_resetService.reset ();
		}



		public bool allTeamUnitsDead(AllianceType alliance){
			int numberofTeamUnits = 0;
			foreach (UnitModel unit in _worldModel.GetAllUnits().Where ((UnitModel unit) => unit.Alliance == alliance)){
				numberofTeamUnits ++;
				if (unit.AliveState.Value == UnitModel.AliveStateFlag.Alive)
					return false;
			}
			if (numberofTeamUnits > 0) {
				return true;
			} else {
				Debug.Log (numberofTeamUnits + alliance.ToString());
				return false;
			}
		}

		public void addThisToProcessor(){
			_commandProcessor.AddCommand (this); 
		}
	}
}

//the reset game button is going to be independent from the result thing: you can reset it at any point; the result thing goes independently