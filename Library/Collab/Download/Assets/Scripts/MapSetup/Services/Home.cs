using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapSetup.Model;
using UnityEngine.SceneManagement;
using Scripts.MapSetup.Services;
using Requests;
using MyMvcProject.Data;

public class Home : MonoBehaviour {

	MapRequest mapRequest;

	public MapModel selectedAllMap;
	public MapModel selectedMyMap;
	public int mapIntCount;
	public int myMapIntCount;
	public List<MapModel> myMaps;
	public string tempVariableThisDesigner = "hi";


	//creates new map list in staticdata 
	void Start () {
		if (StaticAllData.clientPracticeMaps == null) {
			StaticAllData.clientPracticeMaps = new List<MapModel> ();
			MapLoaderService.populateMaps ();
		} //gets maps from server
		//mapRequest = new MapRequest ();  //change to singleton in zenject
		//mapRequest.GetAllMaps();

		//resets current map, gets it


		StaticAllData.currentMap = null;
		if (StaticAllData.clientPracticeMaps.Count != 0) {
			selectedAllMap = StaticAllData.clientPracticeMaps [0];
		}

		//creates "myMaps" as opposed to all maps
		myMaps = new List<MapModel> ();

		if (myMaps.Count > 0) {
			selectedMyMap = myMaps [0];
		}



		foreach (MapModel map in StaticAllData.clientPracticeMaps) {
			myMaps.Add (map);
		}
	}

	public void CreateNewMap(){
		SceneManager.LoadScene (2);
	}

	public void ChooseNextAllMap(){
		if (StaticAllData.clientPracticeMaps.Count > 0){
			mapIntCount++;
		selectedAllMap = StaticAllData.clientPracticeMaps
			[mapIntCount % StaticAllData.clientPracticeMaps.Count];
			Debug.Log (selectedAllMap._mapName);
		} else {
			Debug.Log("No maps in all maps");
		}
	}

	public void ChooseNextMyMap(){
		if (myMaps.Count > 0) {
			myMapIntCount++;
			selectedMyMap = myMaps [myMapIntCount % myMaps.Count];
			Debug.Log (selectedMyMap._mapName);
		} else {
			Debug.Log("No maps in my maps");
		}
	}

	public void PlayRandomMap(){
		if (StaticAllData.clientPracticeMaps.Count != 0) {
			MapModel randomMap = StaticAllData.clientPracticeMaps [0];
			PlayMap (randomMap);
		} else {
			Debug.Log ("no maps you fool!");
		}

	}

	public void PlayAllMap(){
		if (selectedAllMap != null) {
			PlayMap (selectedAllMap);
		} else {
			Debug.Log ("no all map selected (probably because there are no all maps");
		}
	}

	public void PlayMyMap(){
		if (selectedMyMap != null) {
			PlayMap (selectedMyMap);
		} else {
			Debug.Log ("no my map selected (probably because there are no my maps");
		}

	}
		

	public void PlayMap(MapModel map){
		Debug.Log ("playing " + map._mapName);
		StaticAllData.currentMap = map;
		SceneManager.LoadScene (7);
	}

	public void RefreshMaps(){

		Debug.Log ("Refreshing all maps");
	}

	public void MapPlusPlus(){
		Debug.Log ("not doing anything atm");
	}

	public void Logout(){
		RestClient.Headers["Username"] = "";

	//	RestClient.Headers["DeviceID"] = SystemInfo.deviceUniqueIdentifier;
		RestClient.Headers ["Password"] = "";
		RestClient.Headers ["Token"] = "";
		Debug.Log ("logged out");
		SceneManager.LoadScene (0);
	}
}
