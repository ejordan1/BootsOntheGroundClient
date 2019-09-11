using UnityEngine;
using Services.CC;
using Model;
using Zenject;
using Data;
using System.Linq;

using Model.Units;
using Services.Commands;
using UnityEngine.SceneManagement;
using System.Collections;
using System;


namespace Services

{

	public class BattleEndCheckCommand : GameCommand
	{
		WorldModel _worldModel;
		CommandProcessor _commandProcessor;


		public BattleEndCheckCommand(WorldModel worldModel, HistoryManager historyManager, UnitManager unitManager, ProjectileManager projectileManager,
			CommandProcessor commandProcessor
			
		)
		{ 
			_worldModel = worldModel;
			_commandProcessor = commandProcessor;


			_commandProcessor.AddLateCommand(this); 
						
		}


		public override GameCommandStatus FixedStep()
		{
			int teamsDead = 0;
			foreach (AllianceType alliance in Enum.GetValues(typeof(AllianceType))) {
				if (allTeamUnitsDead (alliance)) {
					teamsDead++;
				}
			}
			Debug.Log (teamsDead);
			if (teamsDead > 6) {
				PresentResult ();
			}
		
			return GameCommandStatus.InProgress; //the real logic for this is the in the movement script
		}



		public void PresentResult(){
			Debug.Log ("battle is over !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			//_resetService.reset ();
			SceneManager.LoadScene(1);
		}



		public bool allTeamUnitsDead(AllianceType alliance){
			int numberofTeamUnits = 0;
			foreach (UnitModel unit in _worldModel.GetAllUnits().Where ((UnitModel unit) => unit.Alliance == alliance)){
				numberofTeamUnits ++;
				if (unit.AliveState.Value == UnitModel.AliveStateFlag.Alive)
					return false;
			}
			if (numberofTeamUnits > -1) {
				return true;
			} else {
				//Debug.Log (numberofTeamUnits + alliance.ToString());
				return false;
			}
		}


	}
}

//the reset game button is going to be independent from the result thing: you can reset it at any point; the result thing goes independently