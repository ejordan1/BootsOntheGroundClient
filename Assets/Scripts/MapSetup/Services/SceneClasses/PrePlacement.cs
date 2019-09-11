using System;
using UnityEngine;
using MapSetup.Model;
using UnityEngine.SceneManagement;
using Data;
using System.Linq;
using System.Collections.Generic;
using Model.Units;
using Zenject;
using Model;

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
		public Camera cam;


		public UnitPrepManager _unitPrepManager;


		public MapLoaderService _mapLoaderService;

		// Use this for initialization

		[Inject]
		public void Construct(MapLoaderService mapLoaderService, UnitPrepManager unitPrepManager)
		{
			_unitPrepManager = unitPrepManager;
			_mapLoaderService = mapLoaderService;

		}

		void Start(){
			currentAlliance = AllianceType.Team1;
			mapView = Instantiate (mapPrefab, Vector3.zero, Quaternion.identity);
			mapView.GetComponent<MapView> ()._mapModel = StaticMapCreateData._currentMap;
			currentUnit = BridgeData.unitRepo._unitsById.ElementAt(0).Value; 
			cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		}

		void Update(){
			if (Input.GetMouseButtonDown (0) && pickedUpUnit == true) {
				pickedUpUnit = false;
				Vector3 mouseVect = cam.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10));
				WorldPosition mouseWP = new WorldPosition (mouseVect.x * 3, mouseVect.z * 3); //the *3 is to callibrate for the computer screen
				UnitPrep unitPrep = _unitPrepManager.SpawnUnitPrep(currentUnit.Id, currentAlliance, mouseWP);
				StaticMapCreateData._currentMap._preFormationUnits.Add(unitPrep);
				Debug.Log ("unit placed : " + currentUnit.Name + " " + mouseWP.ToString ());
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
			
			switch (currentAlliance) {
			case AllianceType.Team1:
				currentAlliance = AllianceType.Team2;
				break;
			case AllianceType.Team2:
				currentAlliance = AllianceType.Team3;
				break;
			case AllianceType.Team3:
				currentAlliance = AllianceType.Team4;
				break;
			case AllianceType.Team4:
				currentAlliance = AllianceType.Team5;
				break;
			case AllianceType.Team5:
				currentAlliance = AllianceType.Team6;
				break;
			case AllianceType.Team6:
				currentAlliance = AllianceType.Team7;
				break;
			case AllianceType.Team7:
				currentAlliance = AllianceType.Team8;
				break;
			case AllianceType.Team8:
				currentAlliance = AllianceType.Team1;
				break;
			}
			Debug.Log (currentAlliance);
		}
	}


}

