using System;
using UnityEngine;
using MapSetup.Model;
using UnityEngine.SceneManagement;
using Data;
using System.Linq;
using System.Collections.Generic;
using Model.Units;

namespace Scripts.MapSetup.Services
{
	public class PrePlacement : ClientPage
	{
		public GameObject mapPrefab;
		int unitCount;
		public UnitData currentUnit;
		public bool pickedUpUnit;
		public AllianceType currentAlliance;
		public GameObject mapView;

		void Start(){
			currentAlliance = AllianceType.Team1;
			mapView = Instantiate (mapPrefab, Vector3.zero, Quaternion.identity);
			mapView.GetComponent<MapView> ()._mapModel = StaticMapCreateData.currentMap;
			currentUnit = BridgeData.unitRepo._unitsById.ElementAt(0).Value; 
		}

		void Update(){
			if (Input.GetMouseButtonDown (0) && pickedUpUnit == true) {
				pickedUpUnit = false;
				WorldPosition mousePos = new WorldPosition (Input.mousePosition.x, Input.mousePosition.y);
				StaticMapCreateData.currentMap._preFormationUnits.Add(new Model.Units.UnitPrep(currentUnit.Id, currentAlliance, mousePos));
				Debug.Log ("unit placed : " + currentUnit.Name + " " + mousePos.ToString ());
			}
		}

		public void Forward(){
			SceneManager.LoadScene (6);
			//foreach(UnitPrep unit in StaticData.currentMap._preFormationUnits){
			//	Debug.Log (unit._data.Name);
			//}
		}

		public void Back(){
			SceneManager.LoadScene (4);

		}

		public void ChangeUnit(){
			unitCount++;
			int thisCount = unitCount % BridgeData.unitRepo._unitsById.Count;
			currentUnit = BridgeData.unitRepo._unitsById.ElementAt(thisCount).Value; //does elementat work for this?
			Debug.Log(currentUnit);

		}

		public void PickUpUnit(){
			Debug.Log ("pickingupunit" + currentUnit);
			pickedUpUnit = true;
		}

		public void ChangeAlliance(){
			if (currentAlliance == AllianceType.Team1) {
				currentAlliance = AllianceType.Team2;
			} else {
				currentAlliance = AllianceType.Team1;
			}
			Debug.Log (currentAlliance);
		}
			


	}


}

