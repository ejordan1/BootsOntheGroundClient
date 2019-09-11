using System.Collections;
using UnityEngine;
using MyMvcProject.Map;
using MyMvcProject.Data;
using Scripts.MapSetup.Services;
using MapSetup.Model;

namespace Requests
{
	
	public class MapRequest : MonoBehaviour
	{
		public string ServerPath = "http://localhost:5000/api/Map";

		public int mapID = 1;
		public string userID = "user1";
		//public string mapJSON = "asdf";
		public string mapTitle = "unitymap";

		[ContextMenu("Get All Maps")]
		public void GetAllMaps()
		{
			StartCoroutine(GetAllAsync());
		}

		[ContextMenu("Get Map")]
		public void GetSpecificMap()
		{
			StartCoroutine(GetMapAsync());
		}

		[ContextMenu("Post My Map")]
		public void PostMap()
		{
			StartCoroutine(PostMapAsync());
		}
		/*
		[ContextMenu("Delete My Map")]
		public void DeleteScore()
		{
			StartCoroutine(DeleteScoreAsync());
		} */

		
		//part of monobehaviour: substitute with unirx: 
		//dont use this. it is not necessary now. or look into unirx.
		IEnumerator GetAllAsync()
		{
			Debug.Log("GetAllAsync...");
			yield return 1;

			var task = RestClient.Get(ServerPath); //rest client get

			yield return task.SendWebRequest(); 

			if (task.isNetworkError)
			{
				Debug.LogError(task.error);
			}
			else
			{
				
				var maps = task.Deserialize<MapModelContainer>();

				if (maps == null) {
					Debug.Log ("Not Found or no maps");
				} else if (maps.mapList == null) {
					Debug.Log ("no maps");
				}
				else
				{
					Debug.Log("Success !");
					foreach (var map in maps.mapList)
					{	
						Debug.Log(map.MapTitle + " " + map.MapID + " " + map.MapJSON +  " " + map.UserID);
						MapModel mapDeserialized = JsonUtility.FromJson<MapModel> (map.MapJSON);
						StaticAllData.allMaps.Add (mapDeserialized);

						Debug.Log(mapDeserialized._zoneModels.Count);
						Debug.Log ("zone piece" + mapDeserialized._zoneModels [0].zonePieceModels [0]);
//						StaticAllData.allMaps.Add (map);
						//Debug.Log(map.MapJSON);
					}
				}
			}

			task.Dispose();
		}

		IEnumerator GetMapAsync()
		{

			Debug.Log("GetMapAsync...");
			yield return 1;
		
			ServerPath = "http://localhost:5000/api/Map/17";
			var task = RestClient.Get(ServerPath);


			//start the task and wait for it to complete
			yield return task.SendWebRequest();

			if (task.isNetworkError)
			{
				Debug.LogError(task.error);
			}
			else
			{	
				var map = task.Deserialize<MapResponse>();
			
				if(map == null)
					Debug.Log("NotFound");
				else  //warning, will return the first map with this name. couldn't pass an int
					Debug.Log(map.map.MapTitle + " " + map.map.MapID + " " + map.map.UserID +  " " + map.map.MapJSON);
				
			}
			//reset server path
			ServerPath = "http://localhost:5000/api/Map";
			task.Dispose();
		}


		IEnumerator PostMapAsync()
		{
			Debug.Log("PostScoreAsync...");
			yield return 1;

			var model = new MapModelJson
			{
				MapID = 0,
				UserID = userID,
				MapJSON = JsonUtility.ToJson(StaticMapCreateData._currentMap),
				MapTitle = mapTitle
				
			};

			var json = JsonUtility.ToJson(model);
			Debug.Log (json);
			var task = RestClient.Post(ServerPath, json);

			yield return task.SendWebRequest();

			if (task.isNetworkError)
			{
				Debug.LogError(task.error);
			}
			else
			{
				Debug.Log("Success");
			}

			task.Dispose();
		}
		/*
		IEnumerator DeleteScoreAsync()
		{
			Debug.Log("DeleteScoreAsync...");
			yield return 1;

			var task = RestClient.Delete(ServerPath, UserName);

			yield return task.SendWebRequest();

			if (task.isNetworkError)
			{
				Debug.LogError(task.error);
			}
			else
			{
				Debug.Log("Success");
			}

			task.Dispose();
		}
		*/
	}
}
