using System;
using UnityEngine;
using MapSetup.Model;
using UnityEngine.SceneManagement;
using Scripts.MapSetup.Services;

namespace AssemblyCSharp
{
	public class Arrange : ClientPage
	{
		public GameObject mapPrefab;
		public GameObject mapView;

		void Start(){

			StaticMapCreateData.selectedZone = StaticMapCreateData.currentMap.zoneModels [0];
			mapView = Instantiate (mapPrefab, Vector3.zero, Quaternion.identity);
			mapView.GetComponent<MapView> ()._mapModel = StaticMapCreateData.currentMap;
		}

		public int count;

		public void ZoneIncrease(){
			int thisCount = count % StaticMapCreateData.currentMap.zoneModels.Count;
			StaticMapCreateData.selectedZone = StaticMapCreateData.currentMap.zoneModels [thisCount];
			Debug.Log (thisCount + " " + StaticMapCreateData.selectedZone);
			count++;
		}
		
		public void ChangeTeam(){
		
			if (StaticMapCreateData.selectedZone != null) {
				if (StaticMapCreateData.selectedZone._party == MapModel.Party.P1) {
					StaticMapCreateData.selectedZone._party = MapModel.Party.P2;
				} else {
					StaticMapCreateData.selectedZone._party = MapModel.Party.P1;
				}
			}
			Debug.Log ("party changed to : " + StaticMapCreateData.selectedZone._party);
		}
	


		public void ChangeAlliance(){
			Debug.Log ("changing alliance (not implented))");
			if (StaticMapCreateData.selectedZone != null) {
				if (StaticMapCreateData.selectedZone._alliance == Data.AllianceType.Team1) {
					StaticMapCreateData.selectedZone._alliance = Data.AllianceType.Team2;
				} else {
					StaticMapCreateData.selectedZone._alliance = Data.AllianceType.Team1;
				}
			}
			Debug.Log ("alliance changed to : " + StaticMapCreateData.selectedZone._alliance);
		}


		public void forward(){
			SceneManager.LoadScene (4);

		}
		public void back(){
			
			SceneManager.LoadScene (2);

		}
	}
}

