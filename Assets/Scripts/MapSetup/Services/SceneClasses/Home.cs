using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapSetup.Model;
using MapSetup.Services;
using UnityEngine.SceneManagement;
using Scripts.MapSetup.Services;
using Requests;
using MyMvcProject.Data;
using Zenject;

public class Home : MonoBehaviour {

	MapRequest mapRequest;

	public MapModel selectedAllMap;
	public MapModel selectedMyMap;
	public int mapIntCount;
	public int myMapIntCount;
	public List<MapModel> myMaps;
	public string tempVariableThisDesigner = "hi";

	public MapLoaderService _mapLoaderService;
	public PartyDataRequest _partyDataRequest;
	public PartyRequest _partyRequest;

	public enum ClientState {home, formation, battle}

	public ClientState clientState = ClientState.home;

	public GameObject homeCanvas;
	public GameObject formationCanvas;
	public GameObject battleCanvas;
	// Use this for initialization

	[Inject]
	public void Construct(MapLoaderService mapLoaderService)
	{
		_mapLoaderService = mapLoaderService;
	//	testInject.hello = 5;
	}
	//creates new map list in staticdata 
	public void HomeStart ()
	{
		SocketHandler.Open();
		
		/*homeCanvas = GameObject.Find("HomeCanvas");
		formationCanvas = GameObject.Find("FormationCanvas");
		battleCanvas = GameObject.Find("BattleCanvas");*/

		HomeUtil.home = this; //bad
		
		/*homeCanvas.SetActive(true);
		formationCanvas.SetActive(false);
		battleCanvas.SetActive(false);*/
		
		clientState = ClientState.home;
		if (StaticAllData.allMaps == null)
		{
			StaticAllData.allMaps = new List<MapModel>();
		}
		//USERJOIN
		//SocketHandler.userJoin();
		_partyRequest = GetComponent<PartyRequest>();
		_partyDataRequest = GetComponent<PartyDataRequest>();
		_partyDataRequest.GetPartyData(); //gets party data

		mapRequest = GetComponent<MapRequest>();
		mapRequest.GetAllMaps();
		
		//mapLoaderService is for testing 

		StaticAllData.currentMap = null;
		if (StaticAllData.allMaps.Count != 0) {
			selectedAllMap = StaticAllData.allMaps [0];
		}

		//creates "myMaps" as opposed to all maps
		myMaps = new List<MapModel> ();

		if (myMaps.Count > 0) {
			selectedMyMap = myMaps [0];
		}



		foreach (MapModel map in StaticAllData.allMaps)
		{
			if (map._creator == ClientStaticData.UserID)
			myMaps.Add (map);
		}
	}

	public void CreateNewMap(){
		SceneManager.LoadScene (2);
	}

	public void ChooseNextAllMap(){
		if (StaticAllData.allMaps.Count > 0){
			mapIntCount++;
			selectedAllMap = StaticAllData.allMaps
				[mapIntCount % StaticAllData.allMaps.Count];
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

	/*public void PlayRandomMap(){
		if (StaticAllData.clientPracticeMaps.Count != 0) {
			MapModel randomMap = StaticAllData.clientPracticeMaps [0];
			PlayMap (randomMap);
			Debug.Log ("Not actually random");
		} else {
			Debug.Log ("no maps you fool!");
		}

	}*/

		

	

	
	public void RefreshMaps(){

		Debug.Log ("adding actual maps");
		mapRequest.GetAllMaps(); //how to make this work? all the clients will need to refresh 
	}

	public void Logout(){
		RestClient.Headers["Username"] = "";

	//	RestClient.Headers["DeviceID"] = SystemInfo.deviceUniqueIdentifier;
		RestClient.Headers ["Password"] = "";
		RestClient.Headers ["Token"] = "";
		Debug.Log ("logged out");
		//SocketHandler.userLeft();
		SceneManager.LoadScene (0);
	}

	//the commit button will only be pressable if a map is selected
	
	[ContextMenu("Commit")]
	public void Commit()
	{
	//	SocketHandler.commit();
	}
	
	[ContextMenu("SuggestMap")]
	public void SuggestMap()
	{
		//get the string for the chosen map
	//	SocketHandler.mapSuggest(selectedAllMap._mapName);
	}

	[ContextMenu("ChooseMap")]
	public void ChooseMap()
	{
		//get the string for the chosen map
		//SocketHandler.mapChoose(selectedAllMap._mapName);
	}

	[ContextMenu("sendUnits")]
	public void sendUnits()
	{
		_partyRequest.PostUnits();
	}

	
	public void PlayMap(MapModel map)
	{
		clientState = ClientState.formation;
		homeCanvas.SetActive(false);
		battleCanvas.SetActive(false); 
		formationCanvas.SetActive(true);
		Debug.Log ("playing " + map._mapName);
		StaticAllData.currentMap = map;
		//SceneManager.LoadScene (7);
	}



	//you can send troops using this, if it gets all the troops for each player then it begins the battle,
	//otherwise it waits until five seconds after the timer to send start the battle
	//triggered from a button
	
	[ContextMenu("formationSet")]
	public void formationSet()
	{
		_partyRequest.PostUnits();
	}

	[ContextMenu("unitsGetRequest")]
	public void unitsGetRequest()
	{
		_partyRequest.GetUnits();
	}

	
	public void startGame()
	{
		SceneManager.LoadScene (8);
		clientState = ClientState.battle;
	}

	[ContextMenu("postMap")]
	public void postStaticAllMap()
	{
		mapRequest.PostMap();
	}








	//Legacy
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

}


//now I have to combine this with the other scenes