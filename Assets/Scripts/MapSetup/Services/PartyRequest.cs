using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using MyMvcProject.Map;
using MyMvcProject.Data;
using Scripts.MapSetup.Services;
using MapSetup.Model;
using MapSetup.Model.ModelClasses;
using MapSetup.Model.Responses;
using MapSetup.Services;
using Model.Units;
using UnityEditor;

namespace Requests
{
	
	public class PartyRequest : MonoBehaviour
	{
		public string ServerPath = "http://localhost:5000/api/Party";
		

		
		public string userID = "user1";
	
		[ContextMenu("GetUnits")]
		public void GetUnits()
		{
			StartCoroutine(GetAllUnitsAsync());
		}

		[ContextMenu("PostUnits")]
		public void PostUnits()
		{
			StartCoroutine(PostUnitsAsync(unitPrepMaker()));
		}

		IEnumerator GetAllUnitsAsync()
		{
			Debug.Log("GetAllAsync...");
			yield return 1;

			var task = RestClient.Get(ServerPath); //rest client get

			yield return task.SendWebRequest(); 

			if (task.isNetworkError)
			{
				Debug.LogError(task.error);
			}
			else
			{
				
				var unitLists = task.Deserialize<LoadUnitsResponse>();
				if (unitLists == null)
				{
					Debug.Log("list was null");

				}
				else if (unitLists.unitPrepJsons == null)
				{
					Debug.Log("prep jsons were null");
				} else if (unitLists.unitPrepJsons.Count == 0)
				{
					Debug.Log("0 in list");
				}


				if (unitLists != null && unitLists.unitPrepJsons != null && unitLists.unitPrepJsons.Count > 0)
				{
					
					Debug.Log("received units !");
					foreach (UnitListContainer unitCont in unitLists.unitPrepJsons)
					{

						UnitPrepsModel unitPrepsM = JsonUtility.FromJson<UnitPrepsModel>(unitCont.UnitPrepJson);
						foreach (UnitPrep unit in unitPrepsM.unitPrepList)
						{
							StaticAllData.currentMap._formationUnits.Add(unit);
						}
					}
					HomeUtil.startHomeGame();
				}
				else
				{
					Debug.Log("either of lists is null or no units");	
				}
			}

			task.Dispose();
		}


		IEnumerator PostUnitsAsync(List<UnitPrep> unitPreps)
		{
			Debug.Log("PostScoreAsync...");
			yield return 1;
			UnitPrepsModel units = new UnitPrepsModel(StaticAllData.currentMap._formationUnits);
			
			
			var model = new UnitPrepUploadModel()
			{   
				
				unitPreps = JsonUtility.ToJson(units),
				userId = userID

			};

			var json = JsonUtility.ToJson(model);
			Debug.Log(json);
			var task = RestClient.Post(ServerPath, json);

			yield return task.SendWebRequest();

			if (task.isNetworkError)
			{
				Debug.LogError(task.error);
			}
			else
			{
				Debug.Log("Success"); //this will need to be communicated to home
			}

			task.Dispose();
		}

		//Creates units, unot currently in use
		List<UnitPrep> unitPrepMaker()
		{ 
			
			//change to current unit prep json

			
			List<UnitPrep> unitList = new List<UnitPrep>();
			unitList.Add(new Model.Units.UnitPrep("Spartan", AllianceType.Team1, new WorldPosition(2, 5)));
			unitList.Add(new Model.Units.UnitPrep("Spartan", AllianceType.Team1, new WorldPosition(4, 5)));
			unitList.Add(new Model.Units.UnitPrep("Barbarian", AllianceType.Team1, new WorldPosition(-5, 1)));
			unitList.Add(new Model.Units.UnitPrep("Barbarian", AllianceType.Team1, new WorldPosition(-3, 2)));
			unitList.Add(new Model.Units.UnitPrep("Barbarian", AllianceType.Team1, new WorldPosition(-1, 3)));
			unitList.Add(new Model.Units.UnitPrep("Barbarian", AllianceType.Team1, new WorldPosition(-4, 4)));
			return unitList;
		}
	}
}
