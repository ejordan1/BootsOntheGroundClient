using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapSetup.Model;
using UnityEngine.SceneManagement;
using Scripts.MapSetup.Services;
using Zenject;
using Model;

public class ArrangeSelect : ClientPage {

	public int count;
	public MapArrangement currentMapArrange;
	public GameObject mapView;
	public GameObject mapPrefab;


	public MapLoaderService _mapLoaderService;
	public MapManager _mapManager;

	// Use this for initialization

	[Inject]
	public void Construct(MapLoaderService mapLoaderService, MapManager mapManager)
	{
		_mapLoaderService = mapLoaderService;
		_mapManager = mapManager;

	}

	void Start () {
		_mapLoaderService.populateMaps ();
		currentMapArrange = StaticMapCreateData._mapList [0];
		mapView = Instantiate (mapPrefab, Vector3.zero, Quaternion.identity);
		mapView.GetComponent<MapView> ()._mapModel = currentMapArrange;
	}



	public void mapIncrease(){

		count++;
		currentMapArrange = StaticMapCreateData._mapList [count % StaticMapCreateData._mapList.Count];
		Debug.Log (StaticMapCreateData._currentMap + " " + count % StaticMapCreateData._mapList.Count);

		if (mapView != null) {
			GameObject.Destroy (mapView);
		}
		mapView = Instantiate (mapPrefab, Vector3.zero, Quaternion.identity);
		mapView.GetComponent<MapView> ()._mapModel = currentMapArrange;


		//show the map
	}

	public void forward(){
		StaticMapCreateData._currentMap = _mapManager.CreateMapModel (currentMapArrange);
		Debug.Log (StaticMapCreateData._currentMap._zoneModels [0]);
		SceneManager.LoadScene (3);
	}
}
