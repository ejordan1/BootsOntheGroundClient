using System;
using UnityEngine;
using MapSetup.Model;
using UnityEngine.SceneManagement;
using Data;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Scripts.MapSetup.Services
{
	public class Design : ClientPage
	{
		int partyCount;
		int unitCount;

		public List<UnitToggle> currentParty;
		public string currentUnit;
		public UnitToggle currentUnitToggle;
		public int thisUnit;
		public GameObject mapPrefab;
		public GameObject mapView;

		void Start(){
				GameObject.Find ("MapName").GetComponent<Text> ().text = StaticMapCreateData.currentMap._mapName;
				GameObject.Find ("MapInfo").GetComponent<Text> ().text = StaticMapCreateData.currentMap._mapInfo;
				GameObject.Find ("MapDescription").GetComponent<Text> ().text = StaticMapCreateData.currentMap._mapDescription;


			mapView = Instantiate (mapPrefab, Vector3.zero, Quaternion.identity);
			mapView.GetComponent<MapView> ()._mapModel = StaticMapCreateData.currentMap;

			if (StaticMapCreateData.currentMap.UnitToggles._togglesAll == null) { //this is if you go back to this page
				MapLoaderService.populateUnitToggles (StaticMapCreateData.currentMap);
				Debug.Log ("adedunits");
			}
			currentParty = StaticMapCreateData.currentMap.UnitToggles._togglesAll[0]._togglesParty;
			currentUnit = currentParty[0].unit;
		}

		public void Forward(){
			StaticMapCreateData.currentMap._mapName = GameObject.Find ("MapName").GetComponent<Text> ().text;
			StaticMapCreateData.currentMap._mapInfo = GameObject.Find ("MapInfo").GetComponent<Text> ().text;
			StaticMapCreateData.currentMap._mapDescription = GameObject.Find ("MapDescription").GetComponent<Text> ().text;
			SceneManager.LoadScene (5);

		}

		public void Back(){
			SceneManager.LoadScene (3);

		}

		public void ChangeParty(){
			partyCount++;
			int thisCount = partyCount % StaticMapCreateData.currentMap.UnitToggles._togglesAll.Count; //p1, p2, p3, p4
			Debug.Log(StaticMapCreateData.currentMap.UnitToggles._togglesAll.Count);
			currentParty = StaticMapCreateData.currentMap.UnitToggles._togglesAll[thisCount]._togglesParty;
			Debug.Log("partycount = " + thisCount);
		}

		public void ChangeUnit(){
			
			unitCount++;
			thisUnit = unitCount % currentParty.Count;
			currentUnitToggle = currentParty[thisUnit]; //does elementat work for this?
			Debug.Log(currentUnitToggle.unit);

		}

		public void ToggleUnit(){   //currentUnit currently not being used, string doesn't work with list
			
			currentUnitToggle.toggle = !currentUnitToggle.toggle;
			Debug.Log(currentUnitToggle.toggle);
		}

	}


}

