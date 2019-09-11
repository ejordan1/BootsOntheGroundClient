using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapSetup.Model;
using Data;
using FixMath.NET;
using UnityEngine.UI;



//YOU HAVE TO MANUALLY SET THE MAPMODEL HERE
public class MapView : MonoBehaviour {
	bool populated = false;
	public MapArrangement _mapModel;
	public Dictionary<GameObject, List<GameObject>> zonesDict;
	public GameObject zonePrefab;
	public GameObject zZFragPrefab;
	public GameObject buttonPrefab;
	int buttonCount;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (!populated && _mapModel != null) {   //would have preffered to do this in start with a factory
			Debug.Log("SFNISDNFISDNFDSFISDF");
			zonesDict = new Dictionary<GameObject, List<GameObject>>();
			foreach (ZoneModel zone in _mapModel._zoneModels) {
				GameObject zoneView = Instantiate (zonePrefab, zone.position.ToVector3 (), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
				zoneView.transform.parent = gameObject.transform;
				zonesDict.Add(zoneView, new List<GameObject>());
				zoneView.GetComponent<ZoneView>()._zoneModel = zone;
				foreach (ZzFragModel ZzFrag in zone.zonePieceModels) {
					GameObject zZView = Instantiate (zZFragPrefab, ZzFrag.position.ToVector3(), Quaternion.identity) as GameObject;
					zZView.transform.parent = zoneView.transform;
					zonesDict [zoneView].Add (zZView);
					zZView.GetComponent<ZzfragView> ()._ZzFragModel = ZzFrag;
				}
			}
			transform.rotation = Quaternion.Euler(new Vector3 (90, 0, 0));
			transform.localScale = new Vector3 (1, 1, 1); //if you change this it messes up the view-based unit placement for the circle
			populated = true;


		}
		if (_mapModel != null) {
			transform.localPosition = new Vector3 ((float)_mapModel.position.X, 0, (float)_mapModel.position.Y);

		}
	}

}
