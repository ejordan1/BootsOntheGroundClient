using System;
using UnityEngine;
using MapSetup.Model;
using UnityEngine.SceneManagement;
using Data;
using System.Linq;
using System.Collections.Generic;
using Model.Units;
using FixMath.NET;

namespace Scripts.MapSetup.Services
{

	//have to see if it creates a new map, or just a list of the units, or what...
	public class Formation : ClientPage
	{
		int unitCount;
		public UnitData currentUnit;
		public bool pickedUpUnit;
		public MapModel.Party currentParty;
		public GameObject mapPrefab;
		public MapView mapView;
		public Camera cam;
		public int partyCount;
	

		void Start(){
			cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
			currentParty = MapModel.Party.P1;
			GameObject mapViewObj = Instantiate (mapPrefab, Vector3.zero, Quaternion.identity);
			mapView = mapViewObj.GetComponent<MapView> ();
			mapView.GetComponent<MapView> ()._mapModel = StaticMapCreateData.currentMap;
			currentUnit = BridgeData.unitRepo._unitsById.ElementAt(0).Value; 

		}

		void Update(){
			if (Input.GetMouseButtonDown (0) && pickedUpUnit == true) {
				Vector3 mouseVect = cam.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10));
				WorldPosition mouseWP = new WorldPosition (mouseVect.x, mouseVect.z);
				placeUnit (mouseWP, mouseVect);
				pickedUpUnit = false;
			}



		}


		void placeUnit (WorldPosition mouseWP, Vector3 mouseVect)  //VIEW BASED APPROACH

		 { 
			
			foreach (GameObject zoneObj in mapView.zonesDict.Keys) {  //currently non in here
				Debug.Log (zoneObj);
				ZoneView zoneView = zoneObj.GetComponent<ZoneView> ();
				if (zoneView._zoneModel._party == currentParty) {
					foreach (GameObject zoneFragObj in mapView.zonesDict[zoneObj]) {
						ZzfragView zzFrag = zoneFragObj.GetComponent<ZzfragView> ();
						if (overLapszZ (zzFrag, mouseVect)) {
							StaticMapCreateData.currentMap._preFormationUnits.Add (new Model.Units.UnitPrep (currentUnit.Id, zoneView._zoneModel._alliance, mouseWP));
							Debug.Log ("unit placed attempt : " + currentUnit.Name + " " + mouseWP.ToString ());
								pickedUpUnit = false;
								break;
							} else {
								Debug.Log ("didn't overlap with this zzfrag");
							}
					}
				}
			}
		}
	


		public bool overLapszZ(ZzfragView zZfrag, Vector3 mousePos){   //based on view
			if (zZfrag.GetComponent<ZzfragView> ()._ZzFragModel.pieceType == ZzFragModel.PieceType.rectangle) {
				if (
					mousePos.x > zZfrag.transform.position.x - (zZfrag.transform.lossyScale.x / 2) &&
					mousePos.x < zZfrag.transform.position.x + (zZfrag.transform.lossyScale.x / 2) &&
					mousePos.z > zZfrag.transform.position.z - (zZfrag.transform.lossyScale.y / 2) &&  //this is fucked, don't mess with it until total redo
					mousePos.z < zZfrag.transform.position.z + (zZfrag.transform.lossyScale.y / 2)) { //of the view stuff
					return true;
				}
			} else { //circle
				if (Vector3.Distance (mousePos, zZfrag.transform.position) < zZfrag.transform.localScale.x / 2) {
					Debug.Log(Vector3.Distance (mousePos, zZfrag.transform.position) < zZfrag.transform.localScale.x );
					return true;
				}
			}
			Debug.Log ("mousePos : " + mousePos);
			Debug.Log ("y " + zZfrag.transform.position.y);
			Debug.Log ("y sub" + zZfrag.transform.lossyScale.y / 2);
			return false;
		}


		public void ChangeUnit(){
			unitCount++;
			int thisCount = unitCount % BridgeData.unitRepo._unitsById.Count;
			currentUnit = BridgeData.unitRepo._unitsById.ElementAt(thisCount).Value; //does elementat work for this?
			Debug.Log(currentUnit);

		}

		public void ChangeParty(){
			partyCount++;
			if (currentParty == MapModel.Party.P1) {
				currentParty = MapModel.Party.AIOnly;
			} else {
				currentParty = MapModel.Party.P1;
			}
			Debug.Log ("party = " + currentParty);
		}


		public void PickUpUnit(){
//			Debug.Log ("pickingupunit" + currentUnit);
			pickedUpUnit = true;
		}
	
		public void Play(){
			Debug.Log ("playing");
		}
		public void Refresh(){
			Debug.Log ("refeshing");
		}
	}


}
