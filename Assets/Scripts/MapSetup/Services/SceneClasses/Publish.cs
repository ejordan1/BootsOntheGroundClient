using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.MapSetup.Services;
using UnityEngine.SceneManagement;
using Zenject;

namespace Scripts.MapSetup.Services
{
public class Publish : ClientPage {


	
	public MapLoaderService _mapLoaderService;

	// Use this for initialization

	[Inject]
	public void Construct(MapLoaderService mapLoaderService)
		{

			_mapLoaderService = mapLoaderService;
		}

	public void Forward(){

			Debug.Log ("Publish: not implented to server yet");

			StaticAllData.allMaps.Add (StaticMapCreateData._currentMap);

			returnToHome ();
			serializedMap ();
		}

	public void Back(){
			SceneManager.LoadScene (6);
		}

	public void serializedMap(){

			StaticMapCreateData._mapSerialized = JsonUtility.ToJson (StaticMapCreateData._currentMap);
			Debug.Log (StaticMapCreateData._mapSerialized);
	

		}
	}
}
