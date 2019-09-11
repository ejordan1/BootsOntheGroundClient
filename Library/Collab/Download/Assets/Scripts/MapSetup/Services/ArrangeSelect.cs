using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapSetup.Model;
using UnityEngine.SceneManagement;
using Scripts.MapSetup.Services;

public class ArrangeSelect : ClientPage {

	public int count;
	public MapArrangement currentMapArrange;
	public GameObject mapView;
	public GameObject mapPrefab;
	// Use this for initialization
	void Start () {
		MapLoaderService.populateMaps ();
		currentMapArrange = StaticMapCreateData.mapList [0];
		mapView = Instantiate (mapPrefab, Vector3.zero, Quaternion.identity);
		mapView.GetComponent<MapView> ()._mapModel = currentMapArrange;
	}

	public void mapIncrease(){

		count++;
		currentMapArrange = StaticMapCreateData.mapList [count % StaticMapCreateData.mapList.Count];
		Debug.Log (StaticMapCreateData.currentMap + " " + count % StaticMapCreateData.mapList.Count);

		if (mapView != null) {
			GameObject.Destroy (mapView);
		}
		mapView = Instantiate (mapPrefab, Vector3.zero, Quaternion.identity);
		mapView.GetComponent<MapView> ()._mapModel = currentMapArrange;


		//show the map
	}

	public void forward(){
		StaticMapCreateData.currentMap = new MapModel (currentMapArrange);
		SceneManager.LoadScene (3);
	}
}
